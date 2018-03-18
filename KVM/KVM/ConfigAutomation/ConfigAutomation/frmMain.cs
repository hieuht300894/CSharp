using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace ConfigAutomation
{
    /// <summary>
    /// 20160203: Change _ConfigOutFormat from DateTime.Now to DateTime.UtcNow, SWI use UTC DateTime to generate .7xxout file name
    /// </summary>
    public partial class frmMain : Form
    {
        #region Fields
        #region FTP Out
        private string _ServerAddress = "";
        private int _ServerPort = 25;
        private string _UserName = "";
        private string _Password = "";
        private string _ServerDirectory = "";
        #endregion

        #region Current Config Info
        //private string _TesterName = Environment.MachineName;
        private string _TesterName = "VNJBL-OD-CUS025";
        private string _ProcessStep = "CONFIG";
        private string _FSN = "";
        private string _IMEI = "";
        private string _Number = "";///SKU Number
        private string _Revision = "";///SKU Revision
        private string _ConfigStatus = "";
        private DateTime? _ConfigStartUTC = null;
        private int _ConfigExitCode = -1;///Default
        private string _ConfigReturnCode = "";
        private bool _UploadOutFile = false;
        private bool _ConfigDone = false;
        private DateTime _ConfigStartTime;

        private string _ConfigExe = "";
        private string _SIEWSUrl = "";
        private string _FSN2 = "";

        #endregion

        #region Process Out
        private DateTime _LastProcessOut;
        #endregion 

        #region Config Folder
        private string _7xxOutFormat = "ddMMyyyy";
        private string _ConfigLog = @"C:\AC710\OUT\Logs";
        private string _ConfigErrors = @"C:\AC710\OUT\Logs\errors";
        private string _ConfigOut = @"C:\AC710\OUT";
        private string _ConfigOutFormat = "";
        #endregion
        //private string _ApplicationName = AssemblyTitle();
        private string _ApplicationTitle = string.Format("{0} {1}", AssemblyTitle(), Application.ProductVersion);

        #endregion

        #region Form Events
        private void frmMain_Load(object sender, EventArgs e)
        {
            int clientMode = 0;
            int.TryParse(GetappSettings("ClientMode"), out clientMode);

            clsGeneral.ClientMode = clientMode;
            clsGeneral.frmMain = this;
            clsGeneral._ApplicationTitle = _ApplicationTitle;
            clsGeneral._TesterName = _TesterName;
            clsGeneral._SetTitle();
            clsGeneral.Device.FSN = string.Empty;
            clsGeneral.Device.IMEI = string.Empty;
            clsGeneral.Device.txtFSN = txtFSN;
            clsGeneral.Device.txtIMEI = txtIMEI;
            clsGeneral.Device.txtConfigExe = txtConfigExe;
            clsGeneral.Device.txtConfigFile = txtConfigFile;
            clsGeneral.Device.txtConfigFolder = txtConfigFolder;
            clsGeneral.Device.txtConfigLog = txtConfigLog;
            clsGeneral.Device.IsScanIMEI = true;

            if (clsGeneral.ClientMode == 1)
            {
                // KVM
                LoadConfig();
                CheckConfig();
            }

            CustomForm();

            int _Timeout1stWS = 3;//for 1st web service, 3 seconds
            int _Timeout2ndWS = 10;//for 2nd web service, 10 seconds
            try
            {
                _Timeout1stWS = int.Parse(GetappSettings("Timeout1stWS", @"3"));
            }
            catch { }
            try
            {
                _Timeout2ndWS = int.Parse(GetappSettings("Timeout2ndWS", @"10"));
            }
            catch { }

            #region SIEWS Url
            string strsiewsconnect = string.Empty;
            _SIEWSUrl = global::ConfigAutomation.Properties.Settings.Default.ConfigAutomation_SIEWS_INT;
            if (!WebServiceIsRunning(_SIEWSUrl, _Timeout1stWS, out strsiewsconnect))
            {
                FileErrorLogger.LogError(MethodInfo.GetCurrentMethod().Name, string.Format("{0} {1}", strsiewsconnect, _SIEWSUrl));
                _SIEWSUrl = GetApplicationSettings("ConfigAutomation.Properties.Settings", "ConfigAutomation_SIEWS_INT2nd");
                if (string.IsNullOrEmpty(_SIEWSUrl))
                {
                    SaveApplicationSettings("ConfigAutomation.Properties.Settings", "ConfigAutomation_SIEWS_INT2nd", "http://10.124.16.194/SIE-INT/INT.asmx");///vnhcmm0app02
                    _SIEWSUrl = GetApplicationSettings("ConfigAutomation.Properties.Settings", "ConfigAutomation_SIEWS_INT2nd");
                }
                if (!WebServiceIsRunning(_SIEWSUrl, _Timeout2ndWS, out strsiewsconnect))
                {
                    FileErrorLogger.LogError(MethodInfo.GetCurrentMethod().Name, string.Format("{0} {1}", strsiewsconnect, _SIEWSUrl));
                    if (clsGeneral.ClientMode == 0)
                    {
                        MessageBox.Show(string.Format("{0} {1}", strsiewsconnect, _SIEWSUrl), this._ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        //SendMessage(clsExtension.ConvertCommand(clsGeneral.fKey.ERROR.ToString(), string.Format("{0} {1}", strsiewsconnect, _SIEWSUrl)));
                        //SendMessage(clsExtension.ConvertCommand(clsGeneral.fKey.RESULT.ToString(), clsGeneral.fKey.FAILED.ToString()));

                        SendError(string.Format("{0} {1}", strsiewsconnect, _SIEWSUrl));
                    }
                    Application.Exit();
                }
            }
            //var ws = new SIEWS.INT();
            //ws.Url = _SIEWSUrl;
            #endregion SIEWS Url

            #region FTP
            _ServerAddress = GetappSettings("ServerAddress", @"10.27.91.10");
            try
            {
                _ServerPort = int.Parse(GetappSettings("ServerPort", @"25"));
            }
            catch
            {
                _ServerPort = 21;
            }
            _UserName = GetappSettings("UserName", @"JabilSFS");
            _Password = GetappSettings("Password", @"jabilsfs");
            _ServerDirectory = GetappSettings("ServerDirectory", @"");
            #endregion

            #region Config Folder
            _ConfigLog = GetappSettings("ConfigLog", @"C:\AC710\OUT\Logs");
            _ConfigErrors = GetappSettings("ConfigErrors", @"C:\AC710\OUT\Logs\errors");
            _ConfigOut = GetappSettings("ConfigOut", @"C:\AC710\OUT");
            #endregion

            #region Init Data
            txtConfigLog.Text = this._ConfigLog;
            #endregion

            ///Call process out for 1st time
            #region Monitor OUT folder

            _LastProcessOut = DateTime.Now.AddDays(-1);
            _ConfigOutFormat = string.Format("{0}.{1}", DateTime.Now.AddDays(-1).ToString(this._7xxOutFormat), "7xxout");
            this._UploadOutFile = true;
            ProcessOut();

            _LastProcessOut = DateTime.Now;
            _ConfigOutFormat = string.Format("{0}.{1}", DateTime.Now.ToString(this._7xxOutFormat), "7xxout");
            this._UploadOutFile = true;
            ProcessOut();

            trmMonitorOut.Enabled = true;
            #endregion

            #region MemoryManagement
            MemoryManagement.FlushMemory();
            #endregion
        }
        public frmMain()
        {
            InitializeComponent();
            //this.Text = string.Format("{0} - {1} - {2}", this._ApplicationTitle, this._TesterName, clsGeneral.fKey.NO_CONFIGURATION.ToString().Replace("_", " "));
        }
        #endregion

        #region Control Events
        private void btnOk_Click(object sender, EventArgs e)
        {
            ResetData();
            var strreturn = string.Empty;
            trmMonitorOut.Enabled = false;
            try
            {
                this._FSN = txtFSN.Text.Trim();
                this._IMEI = txtIMEI.Text.Trim();
                if (string.IsNullOrEmpty(this._FSN))
                {
                    txtFSN.Focus();
                    txtFSN.SelectAll();
                    return;
                }
                else if (string.IsNullOrEmpty(this._IMEI))
                {
                    FSNKeyPress();
                    //txtIMEI.Focus();
                    //txtIMEI.SelectAll();
                    return;
                }

                Cursor.Current = Cursors.WaitCursor;
                var ws = new SIEWS.INT();
                ws.Url = _SIEWSUrl;
                var result = ws.CallIntConfig(this._FSN, this._IMEI, _TesterName, Application.ProductName, Application.ProductVersion);
                txtConfigFolder.Text = "";
                txtConfigExe.Text = "";
                txtConfigFile.Text = "";

                if (result.OkToConfig)
                {
                    try
                    {
                        var filename = Path.Combine(result.ConfigFolder, result.ConfigExe);
                        if (!File.Exists(filename))
                        {
                            strreturn = string.Format("Config exe file {0} does not exist.", filename);
                            goto btnOk_Click_Exit;
                        }
                        if (!File.Exists(result.ConfigFile))
                        {
                            strreturn = string.Format("Config cfg file {0} does not exist.", result.ConfigFile);
                            goto btnOk_Click_Exit;
                        }
                        string strconfigexewithoutextenstion = Path.GetFileNameWithoutExtension(result.ConfigExe);
                        Process[] processes = Process.GetProcessesByName(strconfigexewithoutextenstion);
                        if (processes.Length > 0)
                        {
                            strreturn = string.Format("Config exe file {0} is running. Please complete and close it before calling another one.", result.ConfigExe);
                            goto btnOk_Click_Exit;
                        }

                        txtConfigFolder.Text = result.ConfigFolder;
                        txtConfigExe.Text = result.ConfigExe;
                        this._ConfigExe = result.ConfigExe;
                        txtConfigFile.Text = result.ConfigFile;
                        this._Number = result.Number;
                        this._Revision = result.Revision;

                        var strcommandline = string.Format("{0}\\{1} {2}", result.ConfigFolder, result.ConfigExe, result.ConfigCommandLine);
                        if (clsGeneral.ClientMode == 0)
                            MessageBox.Show(strcommandline, this._ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else
                            SendMessage(clsExtension.ConvertCommand(clsGeneral.fKey.SKU_NUMBER.ToString(), string.Format("{0}/ {1}", result.Number, result.Revision)));
                        FileErrorLogger.LogInformation(this._ApplicationTitle, strcommandline);

                        ProcessStartInfo startInfo = new ProcessStartInfo(filename);
                        startInfo.Arguments = result.ConfigCommandLine; // your arguments
                        startInfo.CreateNoWindow = false;
                        Process.Start(startInfo);

                        StartConfiguration();

                    }
                    catch (Exception ex)
                    {
                        strreturn = ex.Message;
                        goto btnOk_Click_Exit;
                    }
                }
                else
                {
                    strreturn = result.OkToConfigMessage;
                    goto btnOk_Click_Exit;
                }
                txtFSN.Focus();
                txtFSN.SelectAll();
            }
            catch (Exception ex)
            {
                strreturn = ex.Message;
                goto btnOk_Click_Exit;
            }
            btnOk_Click_Exit:
            Cursor.Current = Cursors.Default;
            if (!string.IsNullOrEmpty(strreturn))
            {
                trmMonitorOut.Enabled = false;
                if (clsGeneral.ClientMode == 0)
                {
                    MessageBox.Show(strreturn, this._ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    //SendMessage(clsExtension.ConvertCommand(clsGeneral.fKey.ERROR.ToString(), strreturn));
                    //SendMessage(clsExtension.ConvertCommand(clsGeneral.fKey.RESULT.ToString(), clsGeneral.fKey.FAILED.ToString()));
                    SendError(strreturn);
                }
                FileErrorLogger.LogError(MethodInfo.GetCurrentMethod().Name, strreturn);
            }

        }
        private void ResetData()
        {
            this._FSN = "";
            this._IMEI = "";
            this._ConfigStatus = "";
            this._Number = "";
            this._Revision = "";
            this._ConfigStartUTC = DateTime.UtcNow;
            this._ConfigExitCode = -1;
            this._ConfigReturnCode = "";
            this._ConfigDone = false;
            this._ConfigStartTime = DateTime.Now;
            this._ConfigExe = "";
            this._FSN2 = "";

            lblStatus.Text = "Input FSN/ IMEI";
            lblStatus.BackColor = System.Drawing.Color.Yellow;
            lblStatus.ForeColor = System.Drawing.Color.DarkGreen;
        }
        private void StartConfiguration()
        {
            lblStatus.Text = string.Format("{0} - PROCESSING", this._FSN);
            lblStatus.BackColor = System.Drawing.Color.Yellow;
            lblStatus.ForeColor = System.Drawing.Color.DarkGreen;
            txtFSN.Enabled = false;
            txtIMEI.Enabled = false;
            btnOk.Enabled = false;

            this._ConfigStatus = "";
            this._ConfigStartUTC = DateTime.UtcNow;
            this._ConfigExitCode = -1;
            this._ConfigReturnCode = "";
            this._ConfigDone = false;

            trmMonitorLog.Enabled = true;
        }
        private void EndConfiguration()
        {
            trmMonitorLog.Enabled = false;
            lblStatus.Text = string.Format("{0} - {1} - {2}", this._FSN, this._ConfigStatus, this._ConfigReturnCode);
            ///2018-03-06: Call test done
            var ws = new SIEWS.INT();
            ws.Url = _SIEWSUrl;
            var result = ws.CallIntTestDone(_FSN
                , _TesterName
                , _ProcessStep
                , _ConfigStartUTC
                , DateTime.UtcNow
                , _ConfigStatus == "FAILED" ? "F" : "P"
                , Application.ProductName
                , Application.ProductVersion);

            if (this._ConfigStatus == "FAILED")
            {
                //SendMessage(clsExtension.ConvertCommand(clsGeneral.fKey.ERROR.ToString(), _ConfigReturnCode));
                //SendMessage(clsExtension.ConvertCommand(clsGeneral.fKey.RESULT.ToString(), clsGeneral.fKey.FAILED.ToString()));

                SendCode(_ConfigReturnCode);
                SendError(_ConfigReturnCode);

                lblStatus.BackColor = System.Drawing.Color.Red;
                lblStatus.ForeColor = System.Drawing.Color.White;
                ///Send failed message to MES
                //var ws = new SIEWS.INT();
                //ws.Url = _SIEWSUrl;
                var resultF = ws.CallIntF(this._FSN
                    , this._TesterName
                    , this._ProcessStep
                    , this._ConfigReturnCode
                    , this._ConfigReturnCode
                    , this._IMEI
                    , this._FSN2
                    , Application.ProductName
                    , Application.ProductVersion);
            }
            else
            {
                SendCode(_ConfigReturnCode);
                SendMessage(clsExtension.ConvertCommand(clsGeneral.fKey.RESULT.ToString(), clsGeneral.fKey.PASS.ToString()));

                lblStatus.BackColor = System.Drawing.Color.Green;
                lblStatus.ForeColor = System.Drawing.Color.White;
                
            }
            txtFSN.Enabled = true;
            txtIMEI.Enabled = true;
            txtFSN.Text = "";
            txtIMEI.Text = "";
            txtIMEI.Enabled = false;
            btnOk.Enabled = true;
            txtFSN.Focus();

        }
        #endregion

        #region Timer
        private void trmMonitorOut_Tick(object sender, EventArgs e)
        {
            ProcessOut();
            MemoryManagement.FlushMemory();
        }
        private void trmMonitorLog_Tick(object sender, EventArgs e)
        {
            ProcessLogAndErrors();
            if (this._ConfigDone)
            {
                EndConfiguration();
            }
            MemoryManagement.FlushMemory();
        }
        #endregion

        #region Out
        private void ProcessOut()
        {
            #region Process Files
            try
            {
                string[] filePaths = Directory.GetFiles(@"" + this._ConfigOut, this._ConfigOutFormat);
                ///Process files
                for (int i = 0; i < filePaths.Length; i++)
                {
                    bool bUpload = false;
                    string strfilepath = filePaths[i];
                    //string strfilename = Path.GetFileName(strfilepath);
                    //string strfiledate = StringLeft(strfilename, 6); ///File Name format for 7xxout file is ddMMyyyy
                    //                                                 ///
                    string strftpfilename = string.Format("{0}_{1}", Environment.MachineName, Path.GetFileName(strfilepath));
                    ///Check file date must greater than _LastProcessOut
                    DateTime dtLastWriteTime = File.GetLastWriteTime(strfilepath);
                    //DateTime daCreationTime = File.GetCreationTime(strfilepath);
                    FileErrorLogger.LogError(MethodInfo.GetCurrentMethod().Name, string.Format("{0} - {1}", dtLastWriteTime.ToString(), this._LastProcessOut.ToString()));
                    if (dtLastWriteTime >= this._LastProcessOut)
                    {
                        if (IsFileLocked(strfilepath))
                        {
                            ///wait wait wait
                        }
                        else
                        {
                            bUpload = true;///Ok to upload to server
                        }
                    }
                    #region Upload FTP
                    ///bUpload: 7xxout file is changed
                    ///this._UploadOutFile: force to update 7xxout file of current date
                    if (bUpload
                        || (this._UploadOutFile))
                    {

                        #region FTP
                        FileErrorLogger.LogInformation(MethodInfo.GetCurrentMethod().Name, strftpfilename);
                        using (EnterpriseDT.Net.Ftp.FTPConnection ftpClient = new EnterpriseDT.Net.Ftp.FTPConnection())
                        {
                            ftpClient.ServerAddress = this._ServerAddress;
                            ftpClient.ServerPort = this._ServerPort;
                            ftpClient.UserName = this._UserName;
                            ftpClient.Password = this._Password;
                            ftpClient.ServerDirectory = this._ServerDirectory;
                            ftpClient.Connect();
                            if (ftpClient.IsConnected)
                            {
                                try
                                {
                                    ftpClient.UploadFile(strfilepath, strftpfilename, false);
                                }
                                catch (Exception ex)
                                {
                                    FileErrorLogger.LogError(MethodInfo.GetCurrentMethod().Name, ex.Message);
                                }
                                ftpClient.Close();
                                ftpClient.Dispose();
                            }
                        }
                        #endregion

                        _LastProcessOut = DateTime.Now;
                        this._UploadOutFile = false;
                    }
                    #endregion
                }
                //this._ConfigOutFormat = string.Format("{0}.{1}", DateTime.Now.ToString(this._7xxOutFormat), "7xxout");
                this._ConfigOutFormat = string.Format("{0}.{1}", DateTime.UtcNow.ToString(this._7xxOutFormat), "7xxout");
            }
            catch (Exception ex)
            {
                FileErrorLogger.LogError(MethodInfo.GetCurrentMethod().Name, ex.Message);
            }

            #endregion
        }
        #endregion

        #region Log
        private void ProcessLogAndErrors()
        {
            try
            {
                if (ProcessIsRunning(this._ConfigExe))///Running, wait wait wait
                { }
                else
                {
                    this._ConfigDone = true;///Done => find Return Code from log file
                    #region Config Logs Folder
                    string strLogFileFormat = string.Format("*{0}*.log", this._FSN);///get log file of processing FSN
                    string[] filePaths = Directory.GetFiles(@"" + this._ConfigLog, strLogFileFormat);
                    ///Process files
                    for (int i = 0; i < filePaths.Length; i++)
                    {
                        bool bFinished = false;
                        string strfilepath = filePaths[i];
                        ///1. File is not locked
                        ///2. LastWriteTime must greater than this._ConfigStartTime
                        ///3. Exist FSN in file (search asc)
                        ///4. Exist IMEI in file (search asc)
                        ///5. Lookup Return Code (search desc, from the end line back)
                        if (!IsFileLocked(strfilepath))
                        {
                            DateTime dtLastWriteTime = File.GetLastWriteTime(strfilepath);
                            if (dtLastWriteTime >= this._ConfigStartTime)
                            {
                                string[] lines = File.ReadAllLines(strfilepath, Encoding.UTF7);
                                bool bFound = false;
                                bFound = FindString(lines, this._FSN, false);
                                if (bFound)///If found FSN, continue search IMEI
                                {
                                    bFound = FindString(lines, this._IMEI, false);
                                    if (bFound)
                                    {
                                        bFound = FindString(lines, "Return Code", true);
                                        if (bFound)///Found Return Code
                                        {
                                            bFinished = true;
                                            bFound = FindReturnCode(lines, "Return Code", out this._ConfigReturnCode);
                                            if (bFound)
                                            {
                                                if (this._ConfigReturnCode.Equals("Return Code = 0", StringComparison.CurrentCultureIgnoreCase))
                                                {
                                                    this._ConfigStatus = "PASS";
                                                }
                                                else
                                                {
                                                    this._ConfigStatus = "FAILED";
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        if (bFinished)
                        {
                            break;
                        }
                    }
                    #endregion Config Logs Folder
                    #region Extract Exit Code from Return Code
                    try
                    {
                        if (!string.IsNullOrEmpty(this._ConfigReturnCode))
                        {
                            this._ConfigExitCode = Convert.ToInt32(Regex.Match(this._ConfigReturnCode, "\\d+").Value);
                        }
                    }
                    catch { this._ConfigExitCode = -1; }
                    #endregion Extract Exit Code from Return Code
                    #region Config Errors Folder
                    if (string.IsNullOrEmpty(this._ConfigReturnCode)
                        || this._ConfigExitCode == 10
                        || this._ConfigExitCode == 300)
                    {
                        strLogFileFormat = "*.log";
                        filePaths = Directory.GetFiles(@"" + this._ConfigErrors, strLogFileFormat);
                        ///Process files
                        for (int i = 0; i < filePaths.Length; i++)
                        {
                            bool bFinished = false;
                            string strfilepath = filePaths[i];
                            ///1. File is not locked
                            ///2. LastWriteTime must greater than this._ConfigStartTime
                            ///3. Exist FSN in file (search asc)
                            ///4. Exist IMEI in file (search asc)
                            ///5. Lookup Return Code (search desc, from the end line back)
                            if (!IsFileLocked(strfilepath))
                            {
                                DateTime dtLastWriteTime = File.GetLastWriteTime(strfilepath);
                                if (dtLastWriteTime >= this._ConfigStartTime)
                                {
                                    string[] lines = File.ReadAllLines(strfilepath, Encoding.UTF7);
                                    bool bFound = false;
                                    bFound = FindString(lines, this._FSN, false);
                                    if (bFound)///If found FSN, continue search IMEI
                                    {
                                        bFound = FindString(lines, this._IMEI, false);
                                        if (bFound)
                                        {
                                            bFound = FindString(lines, "Return Code", true);
                                            if (bFound)///Found Return Code
                                            {
                                                bFinished = true;
                                                if (string.IsNullOrEmpty(this._ConfigReturnCode))///find new Return Code in errors if only not found in log folders
                                                {
                                                    bFound = FindReturnCode(lines, "Return Code", out this._ConfigReturnCode);
                                                }
                                                if (bFound)
                                                {
                                                    if (this._ConfigReturnCode.Equals("Return Code = 0", StringComparison.CurrentCultureIgnoreCase))
                                                    {
                                                        this._ConfigStatus = "PASS";///??????
                                                    }
                                                    else
                                                    {
                                                        this._ConfigStatus = "FAILED";
                                                        ///find 2nd FSN in .log file in case Return Code = 10 or 300
                                                        Find2ndFSN(lines, "FSN mismatch (mdm=", out this._FSN2);
                                                    }
                                                }
                                            }
                                            else///Not found Return Code
                                            {
                                                ///By default, if found .log with format FSN*.log, then change the status & code
                                                this._ConfigStatus = "FAILED";
                                                this._ConfigReturnCode = "NOT FOUND RETURN CODE";
                                            }
                                        }
                                    }
                                }
                            }
                            ///If finished, read log file
                            ///Last line (row) should be 20150723 03:37:12 UTC DLG: Return Code = 0
                            ///if Return Code = 0 => PASS
                            ///else FAILED
                            if (bFinished)
                            {
                                break;
                            }
                        }
                    }
                    #endregion Config Errors Folder
                    if (string.IsNullOrEmpty(this._ConfigReturnCode))
                    {
                        ///By default, if found .log with format FSN*.log, then change the status & code
                        this._ConfigStatus = "FAILED";
                        this._ConfigReturnCode = "NOT FOUND RETURN CODE";
                    }
                    #region Extract Exit Code from Return Code
                    try
                    {
                        if (!string.IsNullOrEmpty(this._ConfigReturnCode))
                        {
                            this._ConfigExitCode = Convert.ToInt32(Regex.Match(this._ConfigReturnCode, "\\d+").Value);
                        }
                    }
                    catch { this._ConfigExitCode = -1; }
                    #endregion Extract Exit Code from Return Code
                    ///If finished, read log file
                    ///Last line (row) should be 20150723 03:37:12 UTC DLG: Return Code = 0
                    ///if Return Code = 0 => PASS
                    ///else FAILED
                    if (this._ConfigExitCode == 0)
                    {
                        this._ConfigOutFormat = string.Format("{0}.{1}", DateTime.UtcNow.ToString(this._7xxOutFormat), "7xxout");
                        this._UploadOutFile = true;
                        ProcessOut();
                    }
                }
            }
            catch (Exception ex)
            {
                FileErrorLogger.LogError(MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
        //private void ProcessErrors()
        //{
        //    try
        //    {
        //        if (ProcessIsRunning(this._ConfigExe)) ///Running, wait wait wait
        //        {
        //        }
        //        else
        //        {
        //            this._ConfigDone = true;///Done => find Return Code from log file
        //            #region Config Errors Folder
        //            string strLogFileFormat = "*.log";
        //            string[] filePaths = Directory.GetFiles(@"" + this._ConfigErrors, strLogFileFormat);
        //            ///Process files
        //            for (int i = 0; i < filePaths.Length; i++)
        //            {
        //                bool bFinished = false;
        //                string strfilepath = filePaths[i];
        //                ///1. File is not locked
        //                ///2. LastWriteTime must greater than this._ConfigStartTime
        //                ///3. Exist FSN in file (search asc)
        //                ///4. Exist IMEI in file (search asc)
        //                ///5. Lookup Return Code (search desc, from the end line back)
        //                if (!IsFileLocked(strfilepath))
        //                {
        //                    DateTime dtLastWriteTime = File.GetLastWriteTime(strfilepath);
        //                    if (dtLastWriteTime >= this._ConfigStartTime)
        //                    {
        //                        string[] lines = File.ReadAllLines(strfilepath, Encoding.UTF7);
        //                        bool bFound = false;
        //                        bFound = FindString(lines, this._FSN, false);
        //                        if (bFound)///If found FSN, continue search IMEI
        //                        {
        //                            bFound = FindString(lines, this._IMEI, false);
        //                            if (bFound)
        //                            {
        //                                bFound = FindString(lines, "Return Code", true);
        //                                if (bFound)///Found Return Code
        //                                {
        //                                    bFinished = true;
        //                                    bFound = FindReturnCode(lines, "Return Code", out this._ConfigReturnCode);
        //                                    if (bFound)
        //                                    {
        //                                        if (this._ConfigReturnCode.Equals("Return Code = 0", StringComparison.CurrentCultureIgnoreCase))
        //                                        {
        //                                            this._ConfigStatus = "PASS";
        //                                        }
        //                                        else
        //                                        {
        //                                            this._ConfigStatus = "FAILED";
        //                                            ///find 2nd FSN in .log file in case Return Code = 10
        //                                            Find2ndFSN(lines, "FSN mismatch (mdm=", out this._FSN2);
        //                                        }
        //                                    }
        //                                }
        //                                else///Not found Return Code
        //                                {
        //                                    ///By default, if found .log with format FSN*.log, then change the status & code
        //                                    this._ConfigStatus = "FAILED";
        //                                    this._ConfigReturnCode = "NOT FOUND RETURN CODE";
        //                                }
        //                            }
        //                        }
        //                    }
        //                }
        //                else
        //                {
        //                    continue;
        //                }
        //                ///If finished, read log file
        //                ///Last line (row) should be 20150723 03:37:12 UTC DLG: Return Code = 0
        //                ///if Return Code = 0 => PASS
        //                ///else FAILED
        //                if (bFinished)
        //                {
        //                    break;
        //                }
        //            }
        //            #endregion Config Errors Folder
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        FileErrorLogger.LogError(MethodInfo.GetCurrentMethod().Name, ex.Message);
        //    }


        //}
        private bool FindReturnCode(string[] lines
          , string strFindString
          , out string strReturnCode)
        {
            bool bFound = false;
            strReturnCode = string.Empty;
            int iLength = lines.Length;
            for (int j = iLength - 1; j >= 0; j--)
            {
                var line = lines[j].Trim();
                //int iPosition = line.IndexOf("Return Code", 0);
                int iPosition = line.IndexOf(strFindString, 0);
                if (iPosition >= 0)///found
                {
                    strReturnCode = line.Substring(iPosition);
                    bFound = true;
                    break;
                }
            }
            return bFound;
        }
        private bool Find2ndFSN(string[] lines
         , string strFindString
         , out string str2ndFSN)
        {
            bool bFound = false;
            str2ndFSN = string.Empty;
            int iLength = lines.Length;
            for (int j = iLength - 1; j >= 0; j--)
            {
                var line = lines[j].Trim();
                int iPosition = line.IndexOf(strFindString, 0);
                if (iPosition >= 0)///found
                {
                    int iPosition2 = line.IndexOf(", cmd=", 0);
                    if (iPosition2 > iPosition)
                    {
                        int start = iPosition + strFindString.Length;
                        if (iPosition2 > start)
                        {
                            str2ndFSN = line.Substring(start, iPosition2 - start).Trim();
                            bFound = true;
                            break;
                        }
                    }
                }
            }
            return bFound;
        }
        #endregion

        #region Control Events (Others)
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void txtIMEI_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                var imei = txtIMEI.Text.Trim();
                if (!string.IsNullOrEmpty(imei))
                {
                    btnOk.PerformClick();
                }
            }
        }
        private void txtFSN_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                FSNKeyPress();
                //var fsn = txtFSN.Text.Trim();
                //if (!string.IsNullOrEmpty(fsn))
                //{
                //    ///2017-09-22: Add function to verify input FSN belongs to SKU which required IMEI or not
                //    Cursor.Current = Cursors.WaitCursor;
                //    var ws = new SIEWS.INT();
                //    ws.Url = _SIEWSUrl;
                //    var result = ws.CallIntConfigR(fsn, _TesterName
                //        , Application.ProductName, Application.ProductVersion);
                //    //txtConfigFolder.Text = "";
                //    //txtConfigExe.Text = "";
                //    //txtConfigFile.Text = "";
                //    Cursor.Current = Cursors.Default;
                //    if (result.OkToConfig) {
                //        if (result.IsScanIMEI)
                //        {
                //            txtIMEI.Enabled = true;
                //            txtIMEI.Focus();
                //            txtIMEI.SelectAll();
                //        }
                //        else
                //        {
                //            txtIMEI.Enabled = false;
                //            txtIMEI.Text = result.IMEI;
                //            btnOk.PerformClick();
                //        }
                //    }
                //    else
                //    {
                //        MessageBox.Show(result.OkToConfigMessage, this._ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                //        FileErrorLogger.LogError(MethodInfo.GetCurrentMethod().Name, result.OkToConfigMessage);
                //        txtFSN.Focus();
                //        txtFSN.SelectAll();
                //    }
                //}

            }
        }
        private void FSNKeyPress()
        {
            var fsn = txtFSN.Text.Trim();
            if (!string.IsNullOrEmpty(fsn))
            {
                ///2017-09-22: Add function to verify input FSN belongs to SKU which required IMEI or not
                Cursor.Current = Cursors.WaitCursor;
                var ws = new SIEWS.INT();
                ws.Url = _SIEWSUrl;
                var result = ws.CallIntConfigR(fsn, _TesterName, Application.ProductName, Application.ProductVersion);

                //txtConfigFolder.Text = "";
                //txtConfigExe.Text = "";
                //txtConfigFile.Text = "";
                Cursor.Current = Cursors.Default;
                if (result.OkToConfig)
                {
                    if (result.IsScanIMEI)
                    {
                        if (clsGeneral.ClientMode == 0)///User Mode
                        {
                            txtIMEI.Enabled = true;
                            txtIMEI.Focus();
                            txtIMEI.SelectAll();
                        }
                        else///KVM Mode
                        {
                            if (clsGeneral.Device.IsScanIMEI)
                            {
                                txtIMEI.Enabled = true;
                                txtIMEI.Focus();
                                txtIMEI.SelectAll();
                            }
                            else
                            {
                                //SendMessage(clsExtension.ConvertCommand(clsGeneral.fKey.ERROR.ToString(), "Scanning IMEI is required."));
                                //SendMessage(clsExtension.ConvertCommand(clsGeneral.fKey.RESULT.ToString(), clsGeneral.fKey.FAILED.ToString()));

                                SendError("Scanning IMEI is required.");
                            }
                        }
                    }
                    else
                    {
                        if (clsGeneral.ClientMode == 0)///User Mode
                        {
                            txtIMEI.Enabled = false;
                            txtIMEI.Text = result.IMEI;
                            btnOk.PerformClick();
                        }
                        else///KVM Mode
                        {
                            if (clsGeneral.Device.IsScanIMEI)
                            {
                                //SendMessage(clsExtension.ConvertCommand(clsGeneral.fKey.ERROR.ToString(), "Scanning IMEI is not required."));
                                //SendMessage(clsExtension.ConvertCommand(clsGeneral.fKey.RESULT.ToString(), clsGeneral.fKey.FAILED.ToString()));

                                SendError("Scanning IMEI is not required.");
                            }
                            else
                            {
                                txtIMEI.Enabled = false;
                                txtIMEI.Text = result.IMEI;
                                btnOk.PerformClick();
                            }
                        }

                    }
                }
                else
                {
                    if (clsGeneral.ClientMode == 0)/// User Mode
                    {
                        MessageBox.Show(result.OkToConfigMessage, this._ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else/// KVM Mode
                    {
                        //SendMessage(clsExtension.ConvertCommand(clsGeneral.fKey.ERROR.ToString(), result.OkToConfigMessage));
                        //SendMessage(clsExtension.ConvertCommand(clsGeneral.fKey.RESULT.ToString(), clsGeneral.fKey.FAILED.ToString()));

                        SendError(result.OkToConfigMessage);
                    }
                    FileErrorLogger.LogError(MethodInfo.GetCurrentMethod().Name, result.OkToConfigMessage);
                    txtFSN.Focus();
                    txtFSN.SelectAll();
                }
            }
        }
        private void txtIMEI_Enter(object sender, EventArgs e)
        {
            txtIMEI.SelectAll();
        }
        private void txtFSN_Enter(object sender, EventArgs e)
        {
            txtFSN.SelectAll();
        }
        void txtFSN_EnabledChanged(object sender, EventArgs e)
        {
            txtFSN.EnabledChanged -= txtFSN_EnabledChanged;
            if (clsGeneral.ClientMode == 1)
                txtFSN.Enabled = false;
            txtFSN.EnabledChanged += txtFSN_EnabledChanged;
        }
        void txtIMEI_EnabledChanged(object sender, EventArgs e)
        {
            txtIMEI.EnabledChanged -= txtIMEI_EnabledChanged;
            if (clsGeneral.ClientMode == 1)
                txtIMEI.Enabled = false;
            txtIMEI.EnabledChanged += txtIMEI_EnabledChanged;
        }
        void btnOk_EnabledChanged(object sender, EventArgs e)
        {
            btnOk.EnabledChanged -= btnOk_EnabledChanged;
            if (clsGeneral.ClientMode == 1)
                btnOk.Enabled = false;
            btnOk.EnabledChanged += btnOk_EnabledChanged;
        }
        #endregion

        #region Private Methods
        public static void SaveApplicationSettings(string section, string key, string value)
        {
            try
            {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                ClientSettingsSection applicationSettingsSection = (ClientSettingsSection)configFile.SectionGroups["applicationSettings"].Sections[section];
                SettingElement element = applicationSettingsSection.Settings.Get(key);
                if (null != element)
                {
                    applicationSettingsSection.Settings.Remove(element);
                    element.Value.ValueXml.InnerXml = value;
                    applicationSettingsSection.Settings.Add(element);
                }
                else
                {
                    element = new SettingElement(key, SettingsSerializeAs.String);
                    element.Value = new SettingValueElement();
                    System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                    element.Value.ValueXml = doc.CreateElement("value");

                    element.Value.ValueXml.InnerXml = value;
                    applicationSettingsSection.Settings.Add(element);
                }
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("applicationSettings");
                //var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                //var settings = configFile.SectionGroups.Settings;
                //if (settings[key] == null)
                //{
                //    settings.Add(key, value);
                //}
                //else
                //{
                //    settings[key].Value = value;
                //}
                //configFile.Save(ConfigurationSaveMode.Modified);
                ////ConfigurationManager.RefreshSection("appSettings");
                ////ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }
            catch ///(ConfigurationErrorsException)
            {
                //Console.WriteLine("Error writing app settings");
            }
        }
        public static string GetApplicationSettings(string section, string key)
        {
            string value = string.Empty;
            try
            {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                ClientSettingsSection applicationSettingsSection = (ClientSettingsSection)configFile.SectionGroups["applicationSettings"].Sections[section];
                SettingElement element = applicationSettingsSection.Settings.Get(key);
                if (element != null)
                {
                    value = element.Value.ValueXml.InnerXml;
                }
            }
            catch ///(ConfigurationErrorsException)
            {
                //Console.WriteLine("Error retrieving app settings");
            }
            return value;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strurl"></param>
        /// <param name="itimeoutinseconds">in seconds</param>
        /// <param name="strresult"></param>
        /// <returns></returns>
        public bool WebServiceIsRunning(string strurl
            , int itimeoutinseconds
            , out string strresult)
        {
            strresult = string.Empty;
            try
            {
                WebClient client = new WebClient();
                var request = (HttpWebRequest)WebRequest.Create(strurl);
                request.Timeout = itimeoutinseconds * 1000;///2 seconds
                var response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    // Web service up and running  
                    return true;
                }
                else
                {
                    // Web service is down  
                    return false;
                }
            }
            catch (Exception ex)
            {
                strresult = ex.Message;
                return false;
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strprocessname">Ex: 9904308_1.2.1.2.exe</param>
        /// <returns></returns>
        private bool ProcessIsRunning(string strprocessname)
        {
            bool bIsRunning = false;
            string strconfigexewithoutextenstion = Path.GetFileNameWithoutExtension(strprocessname);
            Process[] processes = Process.GetProcessesByName(strconfigexewithoutextenstion);
            if (processes.Length > 0)
            {
                bIsRunning = true;
            }
            return bIsRunning;
        }
        private bool FindString(string[] lines
            , string strFindString
            , bool bDescSort = false)
        {
            bool bFound = false;
            int iLength = lines.Length;
            if (!bDescSort)
            {
                for (int i = 0; i < iLength; i++)
                {
                    var line = lines[i].Trim();
                    int iPosition = line.IndexOf(strFindString, 0);
                    if (iPosition >= 0)///found
                    {
                        bFound = true;
                        break;
                    }
                }
            }
            else
            {
                for (int i = iLength - 1; i >= 0; i--)
                {
                    var line = lines[i].Trim();
                    int iPosition = line.IndexOf(strFindString, 0);
                    if (iPosition >= 0)///found
                    {
                        bFound = true;
                        break;
                    }
                }
            }
            return bFound;
        }
        private string GetappSettings(string strkey, string strdefaultvalue = "")
        {
            string strreturn = strdefaultvalue;
            try
            {
                strreturn = ConfigurationManager.AppSettings[strkey];
            }
            catch
            {
            }
            if (string.IsNullOrEmpty(strreturn))
            {
                strreturn = strdefaultvalue;
            }
            return strreturn;
        }
        private void SaveappSettings(params KeyValuePair<string, string>[] keyValues)
        {
            try
            {
                bool IsChanged = false;

                keyValues = keyValues ?? new KeyValuePair<string, string>[] { };

                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                XDocument document = XDocument.Load(config.AppSettings.ElementInformation.Source);
                if (document.Root == null)
                    return;

                XElement eleAppSettings = document.Root.Elements("appSettings").FirstOrDefault();
                if (eleAppSettings == null) return;

                List<XElement> eleAdds = eleAppSettings.Elements("add").ToList();

                foreach (var keyValue in keyValues)
                {
                    XElement element = eleAdds.FirstOrDefault(x => x.Attribute("key") != null && x.Attribute("key").Value.Equals(keyValue.Key));
                    if (element != null)
                    {
                        XAttribute attribute = element.Attribute("value");
                        if (attribute != null)
                        {
                            if (!attribute.Value.Equals(keyValue.Value))
                            {
                                attribute.Value = keyValue.Value;
                                IsChanged = true;
                            }
                        }
                        else
                        {
                            attribute = new XAttribute("value", keyValue.Value);
                            element.Add(attribute);
                            IsChanged = true;
                        }
                    }
                    else
                    {
                        element = new XElement("add");
                        element.Add(new XAttribute("key", keyValue.Key));
                        element.Add(new XAttribute("value", keyValue.Value));
                        eleAppSettings.Add(element);
                        IsChanged = true;
                    }
                }

                if (IsChanged)
                    document.Save(config.AppSettings.ElementInformation.Source);
            }
            catch { }
        }
        public static string AssemblyTitle()
        {
            AssemblyTitleAttribute title = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false)[0] as AssemblyTitleAttribute;
            return title.Title;
        }
        protected virtual bool IsFileLocked(string filePath)
        {
            FileStream stream = null;
            FileInfo file = new FileInfo(filePath);
            try
            {
                stream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            }
            catch (IOException)
            {
                //the file is unavailable because it is:
                //still being written to
                //or being processed by another thread
                //or does not exist (has already been processed)
                return true;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }

            //file is not locked
            return false;
        }
        public static string StringLeft(string param, int length)
        {
            //we start at 0 since we want to get the characters starting from the
            //left and with the specified lenght and assign it to a variable
            string result = param.Substring(0, length);
            //return the result of the operation
            return result;
        }
        public static string StringRight(string param, int length)
        {
            //start at the index based on the lenght of the sting minus
            //the specified lenght and assign it a variable
            string result = param.Substring(param.Length - length, length);
            //return the result of the operation
            return result;
        }
        #endregion

        #region KVM
        delegate void UpdateStatusCallback(string strMessage);
        delegate void CloseConnectionCallback();
        delegate void UpdateConnectionStatus(bool IsVailable);
        UpdateStatusCallback updateStatus;
        CloseConnectionCallback closeConnection;
        UpdateConnectionStatus updateConnection;

        Task taskClient;
        TcpClient tcpClient;
        CancellationTokenSource cancellation;
        System.Timers.Timer tAvailable = new System.Timers.Timer() { AutoReset = true, Interval = 1000 };

        void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveConfig();
            StopClient();
        }

        void LoadConfig()
        {
            txtFSN.Text = string.Empty;
            txtIMEI.Text = string.Empty;

            clsGeneral.Config.IPClient = GetappSettings("IPClient");
            clsGeneral.Config.IPServer = GetappSettings("IPServer");

            int portClient = clsGeneral.DefaultClientPort;
            int portServer = clsGeneral.DefaultServerPort;
            int.TryParse(GetappSettings("PortClient"), out portClient);
            int.TryParse(GetappSettings("PortServer"), out portServer);

            clsGeneral.Config.PortClient = portClient;
            clsGeneral.Config.PortServer = portServer;

            clsGeneral.ClientHost = clsGeneral.Config.AddressClient.ParseAddress();
            clsGeneral.ServerHost = clsGeneral.Config.AddressServer.ParseAddress();
        }
        void CheckConfig()
        {
            tAvailable.Stop();
            closeConnection?.Invoke();

            if (clsGeneral.ClientHost == null || clsGeneral.ServerHost == null || !clsGeneral.Config.IPClient.Equals(clsGeneral.DefaultIP))
            {
                clsGeneral.Config.IPClient = string.Empty;
                clsGeneral.Config.PortClient = 0;
                clsGeneral.ClientHost = null;

                frmAddressConfig frmClient = new frmAddressConfig();
                frmClient.ShowDialog(this);
            }

            CheckConnection();
        }
        void CheckConnection()
        {
            clsGeneral._SetTitle();

            if (ValidAddress())
            {
                updateConnection = new UpdateConnectionStatus((_IsVailable) =>
                {
                    if (_IsVailable && clsGeneral.PortStatus == clsGeneral.fKey.OFF)
                    {
                        // Fire when port client is on
                        clsGeneral.PortStatus = clsGeneral.fKey.ON;
                        clsGeneral._SetTitle();
                    }
                    else if (!_IsVailable && clsGeneral.PortStatus == clsGeneral.fKey.ON)
                    {
                        // Fire when port client is off
                        clsGeneral.PortStatus = clsGeneral.fKey.OFF;
                        clsGeneral._SetTitle();
                    }

                    if (clsGeneral.PortStatus == clsGeneral.fKey.ON && clsGeneral.ConnectionStatus == clsGeneral.fKey.DISCONNECTED)
                    {
                        // if port client is on and disconnected then try connecting client to host
                        tAvailable.Stop();
                        StartClient();
                    }
                });
                CheckAvailable();
            }
        }
        void CheckAvailable(double delay = 1000)
        {
            tAvailable = tAvailable ?? new System.Timers.Timer() { AutoReset = true, Interval = delay };
            tAvailable.Elapsed += (_sender, _event) =>
            {
                tAvailable.Stop();

                if (updateConnection != null)
                {
                    TcpState state = clsGeneral.ClientHost.CheckAvailable();
                    if (state == TcpState.Unknown) { updateConnection?.Invoke(true); }
                    else if (state == TcpState.Established) { }
                    else { updateConnection?.Invoke(false); }

                    tAvailable.Start();
                }
            };
            tAvailable.Start();
        }
        bool ValidAddress()
        {
            bool chk = true;

            if (clsGeneral.Config.AddressClient.IsEmpty())
                return false;
            else if (!clsGeneral.Config.AddressClient.CheckFormat(clsGeneral.regexAddress))
                return false;

            if (clsGeneral.Config.AddressServer.IsEmpty())
                return false;
            else if (!clsGeneral.Config.AddressServer.CheckFormat(clsGeneral.regexAddress))
                return false;

            return chk;
        }
        void StopClient()
        {
            try
            {
                clsGeneral.PortStatus = clsGeneral.fKey.OFF;
                clsGeneral.ConnectionStatus = clsGeneral.fKey.DISCONNECTED;
                clsGeneral._SetTitle();

                cancellation?.Cancel();
                tcpClient?.Close();
            }
            catch (Exception ex)
            {
                clsIO.SaveLog(clsGeneral.DirLog, clsGeneral.FileLog, clsGeneral.ExtLog, ex.Message);
            }
        }
        void StartClient()
        {
            try
            {
                updateStatus = new UpdateStatusCallback(UpdateStatus);
                closeConnection = new CloseConnectionCallback(CloseConnection);

                // Start a new TCP connections to the chat server
                tcpClient = new TcpClient(clsGeneral.ClientHost);
                tcpClient.Connect(clsGeneral.ServerHost);

                // Start the thread for receiving messages and further communication
                cancellation = new CancellationTokenSource();
                taskClient = new Task(() => ReceiveMessages(), cancellation.Token);
                taskClient.Start();

                // Send the desired username to the server
                SendMessage(clsExtension.ConvertCommand(clsGeneral.fKey.REGISTER.ToString(), clsGeneral.fKey.QUESTION.ToString()));
            }
            catch (Exception ex)
            {
                clsIO.SaveLog(clsGeneral.DirLog, clsGeneral.FileLog, clsGeneral.ExtLog, ex.Message);
                closeConnection?.Invoke();
            }
        }
        void ReceiveMessages()
        {
            try
            {
                // Receive the response from the server
                StreamReader reader = new StreamReader(tcpClient.GetStream());

                // If the first character of the response is 1, connection was successful
                string strReader = reader.ReadLine() ?? string.Empty;

                string key = string.Empty;
                string value = string.Empty;
                strReader.ParseCommand(out key, out value);

                if (key.Equals(clsGeneral.fKey.REGISTER.ToString()) && value.Equals(clsGeneral.fKey.ACCEPT.ToString()))
                {
                    clsGeneral.ConnectionStatus = clsGeneral.fKey.CONNECTED;
                    clsGeneral._SetTitle();
                }
                else { throw new Exception(); }

                // While we are successfully connected, read incoming lines from the server
                while (true)
                {
                    strReader = reader.ReadLine() ?? string.Empty;
                    if (strReader.IsEmpty())
                        throw new Exception();
                    updateStatus?.Invoke(strReader);
                }
            }
            catch
            {
                closeConnection?.Invoke();
            }
        }
        void UpdateStatus(string msg)
        {
            try
            {
                string key = string.Empty;
                string value = string.Empty;

                clsExtension.ParseCommand(msg, out key, out value);

                if (!key.IsEmpty() && !value.IsEmpty())
                {
                    if (key.Equals(clsGeneral.fKey.IS_SCAN_IMEI.ToString()))
                    {
                        if (value.Equals("1"))
                            clsGeneral.Device.IsScanIMEI = true;
                        else
                            clsGeneral.Device.IsScanIMEI = false;
                    }
                    else if (key.Equals(clsGeneral.fKey.FSN.ToString()))
                    {
                        SetFSN(value);
                    }
                    else if (key.Equals(clsGeneral.fKey.IMEI.ToString()))
                    {
                        if (value.Equals(clsGeneral.fKey.EMPTY.ToString()))
                            SetIMEI(string.Empty);
                        else
                            SetIMEI(value);
                    }
                    else if (key.Equals(clsGeneral.fKey.BEGIN.ToString()))
                    {
                        StartTesting();
                    }
                    else if (key.Equals(clsGeneral.fKey.CONNECTING.ToString()) && value.Equals(clsGeneral.fKey.QUESTION.ToString()))
                    {
                        SendMessage(clsExtension.ConvertCommand(clsGeneral.fKey.CONNECTING.ToString(), clsGeneral.fKey.OK.ToString()));
                    }
                    else if (key.Equals(clsGeneral.fKey.STOP.ToString()) && value.Equals(clsGeneral.fKey.OK.ToString()))
                    {
                        closeConnection?.Invoke();
                    }
                }
            }
            catch (Exception ex)
            {
                clsIO.SaveLog(clsGeneral.DirLog, clsGeneral.FileLog, clsGeneral.ExtLog, ex.Message);
                closeConnection?.Invoke();
            }
        }
        void SendError(string msg)
        {
            SendMessage(clsExtension.ConvertCommand(clsGeneral.fKey.ERROR.ToString(), msg));
            SendMessage(clsExtension.ConvertCommand(clsGeneral.fKey.RESULT.ToString(), clsGeneral.fKey.FAILED.ToString()));

            clsGeneral.Device.txtFSN.ResetText();
            clsGeneral.Device.txtIMEI.ResetText();
            clsGeneral.Device.txtConfigFolder.ResetText();
            clsGeneral.Device.txtConfigExe.ResetText();
            clsGeneral.Device.txtConfigFile.ResetText();
        }
        void SendCode(string msg)
        {
            string regexCode = @"(return code = )(?<Code>\d+)";
            string code = clsGeneral.fKey.EMPTY.ToString();
            if (Regex.IsMatch(msg.ToLower(), regexCode))
                SendMessage(clsExtension.ConvertCommand(clsGeneral.fKey.RETURN_CODE.ToString(), msg));
            else
                SendMessage(clsExtension.ConvertCommand(clsGeneral.fKey.RETURN_CODE.ToString(), clsGeneral.fKey.EMPTY.ToString()));

            //string regexCode = @"(return code = )(?<Code>\d+)";
            //string code = clsGeneral.fKey.EMPTY.ToString();
            //try
            //{
            //    Match match = Regex.Match(msg.ToLower(), regexCode);
            //    code = match.Groups["Code"].Value;
            //}
            //catch { }

            //SendMessage(clsExtension.ConvertCommand(clsGeneral.fKey.RETURN_CODE.ToString(), code));
        }
        void SendMessage(string msg, double delay = 100)
        {
            delay = delay > 0 ? delay : 1;

            System.Timers.Timer timer = new System.Timers.Timer() { AutoReset = true, Interval = delay };
            timer.Elapsed += (_sender, _event) =>
            {
                System.Timers.Timer _timer = (System.Timers.Timer)_sender;
                _timer.Stop();
                try
                {
                    StreamWriter writer = new StreamWriter(tcpClient.GetStream());
                    writer.WriteLine(msg);
                    writer.Flush();
                }
                catch (Exception ex)
                {
                    clsIO.SaveLog(clsGeneral.DirLog, clsGeneral.FileLog, clsGeneral.ExtLog, ex.Message);
                    closeConnection?.Invoke();
                }
            };
            timer.Start();
        }
        void CloseConnection()
        {
            clsGeneral.ConnectionStatus = clsGeneral.fKey.DISCONNECTED;
            clsGeneral._SetTitle();

            try
            {
                cancellation?.Cancel();
                tcpClient?.Close();
            }
            catch (Exception ex)
            {
                clsIO.SaveLog(clsGeneral.DirLog, clsGeneral.FileLog, clsGeneral.ExtLog, ex.Message);
            }
            finally { CheckConnection(); }
        }
        void StartTesting()
        {
            this.InvokeExt(() => { btnOk_Click(btnOk, new EventArgs()); });
        }
        void SaveConfig()
        {
            if (clsGeneral.ClientMode == 1)
            {
                SaveappSettings(
                    new KeyValuePair<string, string>("IPClient", clsGeneral.Config.IPClient),
                    new KeyValuePair<string, string>("IPServer", clsGeneral.Config.IPServer),
                    new KeyValuePair<string, string>("PortClient", clsGeneral.Config.PortClient.ToString()),
                    new KeyValuePair<string, string>("PortServer", clsGeneral.Config.PortServer.ToString()));
            }
        }
        void CustomForm()
        {
            if (clsGeneral.ClientMode == 0)
            {
                btnOk.Visible = true;
                btnKVMConfig.Visible = false;
                txtFSN.Enabled = true;
                txtIMEI.Enabled = false;
                btnOk.Enabled = true;

                txtFSN.EnabledChanged -= txtFSN_EnabledChanged;
                txtIMEI.EnabledChanged -= txtIMEI_EnabledChanged;
                btnOk.EnabledChanged -= btnOk_EnabledChanged;
            }
            else
            {
                btnOk.Visible = false;
                btnKVMConfig.Visible = true;
                txtFSN.Enabled = false;
                txtIMEI.Enabled = false;
                btnOk.Enabled = false;

                txtFSN.EnabledChanged -= txtFSN_EnabledChanged;
                txtFSN.EnabledChanged += txtFSN_EnabledChanged;
                txtIMEI.EnabledChanged -= txtIMEI_EnabledChanged;
                txtIMEI.EnabledChanged += txtIMEI_EnabledChanged;
                btnOk.EnabledChanged -= btnOk_EnabledChanged;
                btnOk.EnabledChanged += btnOk_EnabledChanged;
                FormClosing -= frmMain_FormClosing;
                FormClosing += frmMain_FormClosing;
                btnKVMConfig.Click -= BtnKVMConfig_Click;
                btnKVMConfig.Click += BtnKVMConfig_Click;
            }
        }
        void SetFSN(string fsn)
        {
            this.BeginInvokeExt(() => { txtFSN.Text = fsn.Trim(); });
        }
        void SetIMEI(string imei)
        {
            this.BeginInvokeExt(() => { txtIMEI.Text = imei.Trim(); });
        }

        void BtnKVMConfig_Click(object sender, EventArgs e)
        {
            bool IsChanged = false;

            Action<bool> Reload = (bRe) => { IsChanged = bRe; };

            frmAddressConfig frmClient = new frmAddressConfig();
            frmClient.ReloadData = new frmAddressConfig.LoadData(Reload);
            frmClient.ShowDialog(this);

            if (IsChanged)
                CloseConnection();
        }
        #endregion
    }
}
