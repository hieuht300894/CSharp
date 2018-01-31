using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientApp
{
    public partial class frmConfig : frmBase
    {
        string UserName = Environment.MachineName;
        delegate void UpdateStatusCallback(string strMessage);
        delegate void CloseConnectionCallback();
        delegate void UpdateConnectionStatus(bool IsVailable, bool IsListening);
        UpdateStatusCallback updateStatus;
        CloseConnectionCallback closeConnection;
        UpdateConnectionStatus updateConnection;

        Task taskClient;
        TcpClient tcpClient;
        CancellationTokenSource cancellation;

        ErrorProvider eClientAddress = new ErrorProvider();
        ErrorProvider eServerAddress = new ErrorProvider();
        ErrorProvider eFSN = new ErrorProvider();
        ErrorProvider eIMEI = new ErrorProvider();
        ErrorProvider eConfigFolder = new ErrorProvider();
        ErrorProvider eConfigExe = new ErrorProvider();
        ErrorProvider eConfigFile = new ErrorProvider();
        ErrorProvider eConfigLog = new ErrorProvider();

        bool IsAvailable;
        bool IsListening;

        public frmConfig()
        {
            InitializeComponent();
        }
        protected override void frmBase_Load(object sender, EventArgs e)
        {
            base.frmBase_Load(sender, e);

            LoadConfig();
            LoadAddressClient();
            LoadAddressServer();
            CheckConnection();
            CustomForm();
        }
        protected override void frmBase_FormClosing(object sender, FormClosingEventArgs e)
        {
            base.frmBase_FormClosing(sender, e);

            SaveConfig();
            StopClient();
        }

        void LoadConfig()
        {
            Config config = clsIO.LoadFile<Config>(clsGeneral.DirConfig, clsGeneral.FileConfig, clsGeneral.ExtConfig);

            txtFSN.Text = string.Empty;
            txtIMEI.Text = string.Empty;

            clsGeneral.Config.IPClient = config.IPClient;
            clsGeneral.Config.IPServer = config.IPServer;
            clsGeneral.Config.PortClient = config.PortClient;
            clsGeneral.Config.PortServer = config.PortServer;
            clsGeneral.Config.AddressClient = config.AddressClient;
            clsGeneral.Config.AddressServer = config.AddressServer;

            clsGeneral.ClientHost = clsGeneral.Config.AddressClient.ParseAddress();
            clsGeneral.ServerHost = clsGeneral.Config.AddressServer.ParseAddress();
        }
        void LoadAddressClient()
        {
            if (clsGeneral.ClientHost == null)
            {
                frmAddressClientConfig frm = new frmAddressClientConfig();
                frm.ShowDialog(this);
            }
        }
        void LoadAddressServer()
        {
            if (clsGeneral.ServerHost == null)
            {
                frmAddressServerConfig frm = new frmAddressServerConfig();
                frm.ShowDialog(this);
            }
        }
        void CheckConnection()
        {
            Action action1 = new Action(() =>
            {
                updateConnection = new UpdateConnectionStatus((_IsVailable, _IsListening) =>
                {
                    this.BeginInvokeExt(new Action(() =>
                    {
                        bool IsSuccess = _IsVailable && _IsListening;
                        btnConnect.Enabled = IsSuccess;

                        if (IsSuccess)
                            StartClient();

                        //if (_IsVailable)
                        //    txtIPClient.SetError(eClientAddress, string.Empty);
                        //else
                        //    txtIPClient.SetError(eClientAddress, "Client is not available");

                        //if (_IsListening)
                        //    txtIPServer.SetError(eServerAddress, string.Empty);
                        //else
                        //    txtIPServer.SetError(eServerAddress, "Server is not available");
                    }));

                });
                updateConnection(false, false);

                CheckAvailable();
                CheckListening();
            });

            Action action2 = new Action(() =>
            {
                System.Timers.Timer timer = new System.Timers.Timer() { AutoReset = true, Interval = 1000 };
                timer.Start();

                timer.Elapsed += (_s, _e) =>
                {
                    timer.Interval = 5000;
                    timer.Stop();

                    if ((bool)Invoke(new Func<bool>(() => { return ValidAddress(); })))
                        action1();
                    else
                        timer.Start();
                };
            });

            Task t = new Task(() => action2());
            t.Start();
        }
        void CheckAvailable(double delay = 1000)
        {
            Task t = new Task(() =>
            {
                System.Timers.Timer timer = new System.Timers.Timer() { AutoReset = true, Interval = delay };
                timer.Elapsed += (_sender, _event) =>
                {
                    if (updateConnection == null)
                        timer.Stop();
                    else
                    {
                        timer.Stop();
                        TcpState state = clsGeneral.ClientHost.CheckAvailable();
                        if (state == TcpState.Unknown)
                        {
                            if ((state == TcpState.Unknown) != IsAvailable)
                            {
                                IsAvailable = state == TcpState.Unknown;
                                updateConnection(IsAvailable, IsListening);
                            }
                        }
                        else if (state == TcpState.Established)
                        {
                            if ((state == TcpState.Established) != IsAvailable)
                            {
                                IsAvailable = state == TcpState.Established;
                                updateConnection(IsAvailable, IsListening);
                            }
                        }
                        else
                        {
                            IsAvailable = false;
                            updateConnection(IsAvailable, IsListening);
                        }
                        timer.Start();
                    }
                };
                timer.Start();
            });
            t.Start();
        }
        void CheckListening(double delay = 1000)
        {
            Task t = new Task(() =>
            {
                System.Timers.Timer timer = new System.Timers.Timer() { AutoReset = true, Interval = delay };
                timer.Elapsed += (_sender, _event) =>
                {
                    if (updateConnection == null)
                        timer.Stop();
                    else
                    {
                        timer.Stop();
                        TcpState stateListen = clsGeneral.ServerHost.CheckListening();
                        if (stateListen == TcpState.Listen)
                        {
                            if ((stateListen == TcpState.Listen) != IsListening)
                            {
                                IsListening = stateListen == TcpState.Listen;
                                updateConnection(IsAvailable, IsListening);
                            }
                        }
                        else
                        {
                            IsListening = false;
                            updateConnection(IsAvailable, IsListening);
                        }
                        timer.Start();
                    }
                };
                timer.Start();
            });
            t.Start();
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
        bool ValidData()
        {
            bool chk = true;

            if (txtFSN.Text.IsEmpty())
            {
                chk = false;
                this.InvokeExt(() => { txtFSN.SetError(eFSN, "FSN is not blank"); });
            }
            else
            {
                this.InvokeExt(() => { txtFSN.SetError(eFSN, ""); });
            }

            if (txtIMEI.Text.IsEmpty())
            {
                chk = false;
                this.InvokeExt(() => { txtIMEI.SetError(eIMEI, "INEI is not blank"); });
            }
            else
            {
                this.InvokeExt(() => { txtIMEI.SetError(eIMEI, ""); });
            }

            if (txtConfigExe.Text.IsEmpty())
            {
                chk = false;
                this.InvokeExt(() => { txtConfigExe.SetError(eConfigExe, "ConfigExe is not blank"); });
            }
            else
            {
                this.InvokeExt(() => { txtConfigExe.SetError(eConfigExe, ""); });
            }

            if (txtConfigFile.Text.IsEmpty())
            {
                chk = false;
                this.InvokeExt(() => { txtConfigFile.SetError(eConfigFile, "ConfigFile is not blank"); });
            }
            else
            {
                this.InvokeExt(() => { txtConfigFile.SetError(eConfigFile, ""); });
            }

            if (txtConfigFolder.Text.IsEmpty())
            {
                chk = false;
                this.InvokeExt(() => { txtConfigFolder.SetError(eConfigFolder, "ConfigFolder is not blank"); });
            }
            else
            {
                this.InvokeExt(() => { txtConfigFolder.SetError(eConfigFolder, ""); });
            }

            if (txtConfigLog.Text.IsEmpty())
            {
                chk = false;
                this.InvokeExt(() => { txtConfigLog.SetError(eConfigLog, "ConfigLog is bot blank"); });
            }
            else
            {
                this.InvokeExt(() => { txtConfigLog.SetError(eConfigLog, ""); });
            }

            return chk;
        }
        void StopClient()
        {
            CloseConnection();
        }
        void StartClient()
        {
            try
            {
                if (tcpClient != null && tcpClient.Connected) { return; }

                updateStatus = new UpdateStatusCallback(UpdateStatus);
                closeConnection = new CloseConnectionCallback(StopClient);

                // Start a new TCP connections to the chat server
                tcpClient = new TcpClient(clsGeneral.ClientHost);
                tcpClient.Connect(clsGeneral.ServerHost);

                // Start the thread for receiving messages and further communication
                cancellation = new CancellationTokenSource();
                taskClient = new Task(() => ReceiveMessages(), cancellation.Token);
                taskClient.Start();

                // Send the desired username to the server
                StreamWriter writer = new StreamWriter(tcpClient.GetStream());

                writer.WriteLine(clsExtension.ConvertCommand(clsGeneral.fKey.USERNAME.ToString(), UserName));
                writer.Flush();
            }
            catch (Exception ex)
            {
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
                    this.BeginInvokeExt(new Action(() => { lbMessage.Text = clsGeneral.fKey.CONNECTED.ToString(); }));
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
                    if (key.Equals(clsGeneral.fKey.USERNAME.ToString()))
                    {
                        clsGeneral.Config.ClientName = value;
                    }
                    else if (key.Equals(clsGeneral.fKey.FSN.ToString()))
                    {
                        this.BeginInvokeExt(new Action(() => { txtFSN.Text = value; }));

                    }
                    else if (key.Equals(clsGeneral.fKey.IMEI.ToString()))
                    {
                        this.BeginInvokeExt(new Action(() => { txtIMEI.Text = value; }));

                    }
                    else if (key.Equals(clsGeneral.fKey.BEGIN.ToString()))
                    {
                        this.BeginInvokeExt(new Action(() => { lbMessage.Text = clsGeneral.fKey.PROCESSING.ToString(); }));
                        SendMessage(clsExtension.ConvertCommand(clsGeneral.fKey.PROCESSING.ToString(), clsGeneral.fKey.EMPTY.ToString()));
                        StartTesting();
                    }
                    else if (key.Equals(clsGeneral.fKey.CONNECTING.ToString()) && value.Equals(clsGeneral.fKey.QUESTION.ToString()))
                    {
                        this.BeginInvokeExt(new Action(() => { lbMessage.Text = clsGeneral.fKey.CONNECTED.ToString(); }));

                        SendMessage(clsExtension.ConvertCommand(clsGeneral.fKey.CONNECTING.ToString(), clsGeneral.fKey.OK.ToString()));
                    }
                    else if (key.Equals(clsGeneral.fKey.STOP.ToString()) && value.Equals(clsGeneral.fKey.OK.ToString()))
                    {
                        this.BeginInvokeExt(new Action(() =>
                        {
                            lbMessage.Text = clsGeneral.fKey.DISCONNECTED.ToString();
                            closeConnection?.Invoke();
                        }));

                    }
                }
            }
            catch { closeConnection?.Invoke(); }
        }
        void SendMessage(string msg, double delay = 500)
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
                catch
                {
                    closeConnection?.Invoke();
                }
            };
            timer.Start();
        }
        void CloseConnection()
        {
            try
            {
                this.BeginInvokeExt(new Action(() => { lbMessage.Text = clsGeneral.fKey.DISCONNECTED.ToString(); }));

                if (cancellation != null)
                    cancellation.Cancel();
                if (tcpClient != null)
                    tcpClient.Close();
            }
            catch { }

            IsAvailable = IsListening = false;
        }
        void StartTesting()
        {
            double total = 5;
            double count = 0;

            Task t = new Task(() =>
            {
                System.Timers.Timer timer = new System.Timers.Timer() { AutoReset = true, Interval = 1000 };
                timer.Elapsed += (_sender, _event) =>
                {
                    if (count > total)
                    {
                        timer.Stop();
                        this.BeginInvokeExt(new Action(() => { lbMessage.Text = clsGeneral.fKey.PASS.ToString(); }));
                        SendMessage(clsExtension.ConvertCommand(clsGeneral.fKey.RESULT.ToString(), clsGeneral.fKey.PASS.ToString()));
                    }

                    count++;
                };

                if (ValidData())
                {
                    timer.Start();
                }
                else
                {
                    this.BeginInvokeExt(new Action(() => { lbMessage.Text = clsGeneral.fKey.FAILED.ToString(); }));
                    SendMessage(clsExtension.ConvertCommand(clsGeneral.fKey.RESULT.ToString(), clsGeneral.fKey.FAILED.ToString()));
                }
            });
            t.Start();
        }
        void SaveConfig()
        {
            clsGeneral.Config.ClientName = UserName;

            clsGeneral.Config.SaveFile(clsGeneral.DirConfig, clsGeneral.FileConfig, clsGeneral.ExtConfig);
        }
        void CustomForm()
        {
            btnConnect.Click -= BtnConnect_Click;
            btnConnect.Click += BtnConnect_Click;
        }
        void SetControlValue(string FSN = "", string IMEI = "", string ConfigFolder = "", string ConfigExe = "", string ConfigFile = "", string ConfigLog = "")
        {
            if (!string.IsNullOrWhiteSpace(FSN))
                txtFSN.Text = FSN.Trim();
            if (!string.IsNullOrWhiteSpace(IMEI))
                txtIMEI.Text = IMEI.Trim();
            if (!string.IsNullOrWhiteSpace(ConfigFolder))
                txtConfigFolder.Text = ConfigFolder.Trim();
            if (!string.IsNullOrWhiteSpace(ConfigExe))
                txtConfigExe.Text = ConfigExe.Trim();
            if (!string.IsNullOrWhiteSpace(ConfigFile))
                txtConfigFile.Text = ConfigFile.Trim();
            if (!string.IsNullOrWhiteSpace(ConfigLog))
                txtConfigLog.Text = ConfigLog.Trim();
        }

        void BtnConnect_Click(object sender, EventArgs e)
        {
            SaveConfig();
            StartClient();
        }
    }
}
