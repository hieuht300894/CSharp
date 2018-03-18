using HostKVM.UserControls;
using System;
using System.Collections.Generic;
using System.Net;
using System.Windows.Forms;
using System.Linq;
using System.Text.RegularExpressions;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Drawing;
using System.Net.NetworkInformation;

namespace HostKVM.GUI
{
    public partial class frmMain : frmBase
    {
        #region Variables
        List<ClientInfo> lstClients = new List<ClientInfo>();
        List<ClientInfo> lstProcesses = new List<ClientInfo>();
        #endregion

        #region Constructors
        public frmMain()
        {
            InitializeComponent();
        }
        #endregion

        #region Methods

        #region Init client
        void LoadConfig()
        {
            Config config = clsIO.LoadFile<Config>(clsGeneral.DirConfig, clsGeneral.FileConfig, clsGeneral.ExtConfig);
            clsGeneral.Config = config;
            clsGeneral.AddressServer = config.AddressServer.ParseAddress();
            clsGeneral.frmMain = this;

            CheckConfig();
        }
        void CheckConfig()
        {
            clsGeneral.MainServer?.CloseListening();

            if (clsGeneral.AddressServer == null || !clsGeneral.Config.IPServer.Equals(clsGeneral.DefaultIP))
            {
                clsGeneral.Config.IPServer = string.Empty;
                clsGeneral.Config.PortServer = 0;
                clsGeneral.AddressServer = clsGeneral.Config.AddressServer.ParseAddress();
                clsGeneral.Config.Clients.Clear();

                frmAddressServerConfig frmAddress = new frmAddressServerConfig();
                frmAddress.ShowDialog(this);
            }
            if (clsGeneral.Config.TotalNumber == 0)
            {
                clsGeneral.Config.ColumnNumber = 0;
                clsGeneral.Config.RowNumber = 0;
                clsGeneral.Config.Clients.Clear();

                frmNumberClientConfig frmNumber = new frmNumberClientConfig();
                frmNumber.ShowDialog(this);
            }

            if (clsGeneral.AddressServer == null || clsGeneral.Config.TotalNumber == 0)
            {
                clsGeneral.PortStatus = clsGeneral.fKey.NO_CONFIGURATION;
                clsGeneral._SetTitle?.Invoke();
            }
            else
            {
                clsGeneral.PortStatus = clsGeneral.fKey.OFF;
                clsGeneral._SetTitle?.Invoke();

                InitClient();
                ResizeClient();
                StartServer();
            }
        }
        void ResizeClient()
        {
            if (clsGeneral.Config.TotalNumber == 0)
                return;

            int maxWitdh = tpClient.PreferredSize.Width;
            int avrWidth = maxWitdh / clsGeneral.Config.ColumnNumber;

            tpClient.AutoScroll = false;
            tpClient.HorizontalScroll.Maximum = maxWitdh;
            tpClient.AutoScroll = true;

            lstClients.ForEach(x =>
            {
                x.Control.Width = avrWidth - x.Control.Margin.Left - x.Control.Margin.Right;
                x.Control.txtFSN.Width = x.Control.lbClientName.Width - x.Control.lbFSN.Width - x.Control.btnConfig.Width;
                x.Control.txtIMEI.Width = x.Control.lbClientName.Width - x.Control.lbIMEI.Width - x.Control.btnConfig.Width;
            });
        }
        void InitClient()
        {
            int i = 0;  /* Index of row */
            int j = 0;  /* Index of column */
            int k = 0;  /* No. */
            int l = 1; /* 1: Run left to right, -1: Run right to left */

            lstClients.Clear();
            tpClient.Controls.Clear();
            tpClient.ColumnStyles.Clear();
            tpClient.RowStyles.Clear();

            if (clsGeneral.Config.TotalNumber == 0 || clsGeneral.AddressServer == null)
                return;

            tpClient.RowCount = clsGeneral.Config.RowNumber;
            tpClient.ColumnCount = clsGeneral.Config.ColumnNumber;

            for (i = 0; i < clsGeneral.Config.RowNumber; i++)
            {
                tpClient.RowStyles.Add(new RowStyle() { SizeType = SizeType.AutoSize });
            }
            for (j = 0; j < clsGeneral.Config.ColumnNumber; j++)
            {
                tpClient.ColumnStyles.Add(new ColumnStyle() { SizeType = SizeType.AutoSize });
            }

            i = 0;
            j = 0;
            for (k = 0; k < clsGeneral.Config.TotalNumber; k++)
            {
                DeviceControl dc = new DeviceControl();
                dc.Name = string.Format("{0}{1}", clsGeneral.fKey.CLIENT.ToString(), (k + 1));

                Client client = clsGeneral.Config.Clients.FirstOrDefault(x => x.ControlName.Equals(dc.Name));
                ClientInfo cInfo = new ClientInfo()
                {
                    Control = dc,
                    ClientName = (k + 1).ToString(),
                    ControlName = string.Format("{0}{1}", clsGeneral.fKey.CLIENT.ToString(), (k + 1)),
                    RowID = i + 1,
                    ColumnID = j + 1,
                    Position = k + 1
                };
                lstClients.Add(cInfo);

                if (client == null)
                    clsGeneral.Config.Clients.Add(client = new Client() { ClientName = cInfo.ClientName, ControlName = cInfo.ControlName, RowID = cInfo.RowID, ColumnID = cInfo.ColumnID });

                cInfo.Client = client;
                cInfo.Client.IPClient = client.IPClient;
                cInfo.Client.PortClient = client.PortClient;

                LoadClientInfo(cInfo);

                dc.txtFSN.DataBindings.Clear();
                dc.txtFSN.DataBindings.Add("Text", cInfo, "FSN", true, DataSourceUpdateMode.OnPropertyChanged);

                dc.txtIMEI.DataBindings.Clear();
                dc.txtIMEI.DataBindings.Add("Text", cInfo, "IMEI", true, DataSourceUpdateMode.OnPropertyChanged);

                dc.btnConfig.Click -= BtnConfig_Click;
                dc.btnConfig.Click += BtnConfig_Click;

                dc.txtFSN.TextChanged -= TxtFSN_TextChanged;
                dc.txtFSN.TextChanged += TxtFSN_TextChanged;

                dc.txtIMEI.TextChanged -= TxtIMEI_TextChanged;
                dc.txtIMEI.TextChanged += TxtIMEI_TextChanged;

                dc.txtFSN.PreviewKeyDown -= TxtFSN_PreviewKeyDown;
                dc.txtFSN.PreviewKeyDown += TxtFSN_PreviewKeyDown;

                dc.txtIMEI.PreviewKeyDown -= TxtIMEI_PreviewKeyDown;
                dc.txtIMEI.PreviewKeyDown += TxtIMEI_PreviewKeyDown;

                tpClient.Controls.Add(dc, j, i);

                j += l;

                if (j < 0)
                {
                    l = 1;
                    i++;
                    j += l;
                }
                else if (j >= clsGeneral.Config.ColumnNumber)
                {
                    l = -1;
                    i++;
                    j += l;
                }
            }
            //for (i = 0; i < clsGeneral.Config.RowNumber; i++)
            //{
            //    for (j = 0; j < clsGeneral.Config.ColumnNumber; j++)
            //    {
            //        k++;

            //        DeviceControl dc = new DeviceControl();
            //        dc.Name = string.Format("{0}{1}{2}", clsGeneral.fKey.CLIENT.ToString(), i, j);

            //        Client client = clsGeneral.Config.Clients.FirstOrDefault(x => x.ControlName.Equals(dc.Name));
            //        ClientInfo cInfo = new ClientInfo()
            //        {
            //            Control = dc,
            //            ClientName = k.ToString(),
            //            ControlName = string.Format("{0}{1}{2}", clsGeneral.fKey.CLIENT.ToString(), i, j),
            //            RowID = i + 1,
            //            ColumnID = j + 1,
            //            Position = k
            //        };
            //        lstClients.Add(cInfo);
            //        if (client == null)
            //            clsGeneral.Config.Clients.Add(client = new Client() { ClientName = cInfo.ClientName, ControlName = cInfo.ControlName, RowID = cInfo.RowID, ColumnID = cInfo.ColumnID });

            //        cInfo.Client = client;
            //        cInfo.Client.IPClient = client.IPClient;
            //        cInfo.Client.PortClient = client.PortClient;

            //        LoadClientInfo(cInfo);

            //        dc.txtFSN.DataBindings.Clear();
            //        dc.txtFSN.DataBindings.Add("Text", cInfo, "FSN", true, DataSourceUpdateMode.OnPropertyChanged);

            //        dc.txtIMEI.DataBindings.Clear();
            //        dc.txtIMEI.DataBindings.Add("Text", cInfo, "IMEI", true, DataSourceUpdateMode.OnPropertyChanged);

            //        dc.btnConfig.Click -= BtnConfig_Click;
            //        dc.btnConfig.Click += BtnConfig_Click;

            //        dc.txtFSN.TextChanged -= TxtFSN_TextChanged;
            //        dc.txtFSN.TextChanged += TxtFSN_TextChanged;

            //        dc.txtIMEI.TextChanged -= TxtIMEI_TextChanged;
            //        dc.txtIMEI.TextChanged += TxtIMEI_TextChanged;

            //        dc.txtFSN.PreviewKeyDown -= TxtFSN_PreviewKeyDown;
            //        dc.txtFSN.PreviewKeyDown += TxtFSN_PreviewKeyDown;

            //        dc.txtIMEI.PreviewKeyDown -= TxtIMEI_PreviewKeyDown;
            //        dc.txtIMEI.PreviewKeyDown += TxtIMEI_PreviewKeyDown;

            //        tpClient.Controls.Add(dc, j, i);
            //    }
            //}
        }
        void LoadClientInfo(ClientInfo cInfo)
        {
            cInfo.ClientName = cInfo.Client.ClientName.IsEmpty() ? cInfo.Client.ControlName : cInfo.Client.ClientName;
            cInfo.IPClient = cInfo.Client.IPClient;
            cInfo.PortClient = cInfo.Client.PortClient;
            cInfo.ClientHost = cInfo.AddressClient.ParseAddress();
            cInfo.Control.SetHeaderTitle(ClientName: cInfo.ClientName);
            cInfo.Control.SetHeaderStatus(IPAddress: cInfo.IPClient, Port: cInfo.PortClient, Status: cInfo.ConnectionStatus);
            cInfo.Control.SetFooterStatus(Status: cInfo.ClientStatus);
            cInfo.Control.SetFSN(FSN: string.Empty);
            cInfo.Control.SetIMEI(IMEI: string.Empty, IMEIRequired: clsGeneral.Config.IMEIRequired == 1);
            cInfo.Control.SetFooterSKUNumber(string.Empty);
            cInfo.Control.SetFooterReturnCode(string.Empty);

            cInfo.Control.SetErrorFSN(string.Empty);
            cInfo.Control.SetErrorIMEI(string.Empty);
            cInfo.Control.SetErrorStatus(string.Empty);

            cInfo.Control.LockTextBox();

            ReloadConnection(cInfo);
        }
        bool ValidAddress()
        {
            bool chk = true;

            if (clsGeneral.Config.AddressServer.IsEmpty())
                return false;
            else if (!clsGeneral.Config.AddressServer.CheckFormat(clsGeneral.regexAddress))
                return false;

            return chk;
        }
        #endregion

        #region Check client's connection
        void ReloadConnection()
        {
            foreach (ClientInfo info in lstClients)
            {
                SendCommand(info, clsExtension.ConvertCommand(clsGeneral.fKey.CONNECTING.ToString(), clsGeneral.fKey.QUESTION.ToString()));
            }
        }
        void ReloadConnection(ClientInfo client)
        {
            SendCommand(client, clsExtension.ConvertCommand(clsGeneral.fKey.CONNECTING.ToString(), clsGeneral.fKey.QUESTION.ToString()));
        }
        void LoadClient(DeviceControl control)
        {
            ClientInfo client = lstClients.FirstOrDefault(x => x.Control.Name.Equals(control.Name));
            if (client != null)
            {
                frmAddressClientConfig frm = new frmAddressClientConfig(client);
                frm.ShowDialog(this);
            }
        }
        #endregion

        #region Start server
        void StartServer()
        {
            clsGeneral.updateStatus = new clsGeneral.UpdateStatusCallback(UpdateStatus);

            // Create a new instance of the object
            clsGeneral.MainServer = new clsServer(clsGeneral.AddressServer);

            // Hook the StatusChanged event handler to mainServer_StatusChanged
            clsServer.StatusChanged -= clsGeneral.mainServer_StatusChanged;
            clsServer.StatusChanged += clsGeneral.mainServer_StatusChanged;

            // Start listening for connections
            clsGeneral.MainServer?.StartListening();
        }
        #endregion

        #region Receive message
        void UpdateStatus(string address, string msg)
        {
            ClientInfo client = lstClients.FirstOrDefault(x => x.AddressClient.Equals(address));

            if (client == null) return;

            string key = string.Empty;
            string value = string.Empty;
            msg.ParseCommand(out key, out value);

            if (!key.IsEmpty() && !value.IsEmpty())
            {
                if (key.Equals(clsGeneral.fKey.CONNECTING.ToString()))
                {
                    client.ClientStatus = clsGeneral.fKey.WAITING;
                    client.ClientMessage = clsGeneral.fKey.EMPTY;

                    if (value.Equals(clsGeneral.fKey.OK.ToString()))
                    {
                        client.ConnectionStatus = clsGeneral.fKey.ON;
                        client.ConnectionMessage = clsGeneral.fKey.EMPTY;

                        this.InvokeExt(new Action(() =>
                        {
                            client.Control.UnLockTextBox();
                        }));
                    }
                    else
                    {
                        client.ConnectionStatus = clsGeneral.fKey.OFF;
                        client.ConnectionMessage = clsGeneral.fKey.EMPTY;

                        this.InvokeExt(new Action(() =>
                        {
                            client.Control.LockTextBox();
                        }));
                    }

                    this.InvokeExt(new Action(() =>
                    {
                        client.Control.SetHeaderStatus(IPAddress: client.IPClient, Port: client.PortClient, Status: client.ConnectionStatus);
                        client.Control.SetFooterStatus(FSN: client.FSN, Status: client.ClientStatus);
                    }));
                }
                else if (key.Equals(clsGeneral.fKey.REGISTER.ToString()))
                {
                    if (value.Equals(clsGeneral.fKey.ACCEPT.ToString()))
                    {
                        client.ConnectionStatus = clsGeneral.fKey.ON;
                        client.ConnectionMessage = clsGeneral.fKey.EMPTY;

                        this.InvokeExt(new Action(() =>
                        {
                            client.Control.UnLockTextBox();
                            client.Control.SetFooterStatus();
                        }));
                    }
                    if (value.Equals(clsGeneral.fKey.DENY.ToString()))
                    {
                        if (client.ClientStatus == clsGeneral.fKey.PROCESSING)
                        {
                            client.ClientStatus = clsGeneral.fKey.FINISHED;
                            client.ClientMessage = clsGeneral.fKey.NO_CLIENT_RESULT;
                            this.InvokeExt(new Action(() => { client.Control.SetFooterStatus(FSN: client.FSN, Status: client.ClientMessage); }));
                            ResetStatus();
                        }

                        client.ConnectionStatus = clsGeneral.fKey.OFF;
                        client.ConnectionMessage = clsGeneral.fKey.EMPTY;

                        this.InvokeExt(new Action(() =>
                        {
                            client.Control.LockTextBox();
                        }));
                    }

                    this.InvokeExt(new Action(() =>
                    {
                        client.Control.SetHeaderStatus(IPAddress: client.IPClient, Port: client.PortClient, Status: client.ConnectionStatus);
                    }));
                }
                else if (key.Equals(clsGeneral.fKey.USERNAME.ToString()))
                {
                    client.ClientName = value;
                    client.Client.ClientName = value;
                    client.ConnectionMessage = clsGeneral.fKey.EMPTY;
                    this.InvokeExt(new Action(() => { client.Control.SetHeaderStatus(IPAddress: client.IPClient, Port: client.PortClient, Status: client.ConnectionStatus); }));
                }
                else if (key.Equals(clsGeneral.fKey.ERROR.ToString()))
                {
                    this.InvokeExt(new Action(() => { client.Control.SetErrorStatus(value); }));
                }
                else if (key.Equals(clsGeneral.fKey.RESULT.ToString()))
                {
                    client.ClientStatus = clsGeneral.fKey.FINISHED;
                    if (value.Equals(clsGeneral.fKey.PASS.ToString()))
                        client.ClientMessage = clsGeneral.fKey.PASS;
                    if (value.Equals(clsGeneral.fKey.FAILED.ToString()))
                        client.ClientMessage = clsGeneral.fKey.FAILED;
                    this.InvokeExt(new Action(() => { client.Control.SetFooterStatus(FSN: client.FSN, Status: client.ClientMessage); }));

                    ResetStatus();
                }
                else if (key.Equals(clsGeneral.fKey.SKU_NUMBER.ToString()))
                {
                    this.InvokeExt(() =>
                    {
                        if (value.Equals(clsGeneral.fKey.EMPTY.ToString()))
                            client.Control.SetFooterSKUNumber(string.Empty);
                        else
                            client.Control.SetFooterSKUNumber(value);
                    });
                }
                else if (key.Equals(clsGeneral.fKey.RETURN_CODE.ToString()))
                {
                    this.InvokeExt(() =>
                    {
                        if (value.Equals(clsGeneral.fKey.EMPTY.ToString()))
                            client.Control.SetFooterReturnCode(string.Empty);
                        else
                            client.Control.SetFooterReturnCode(value);
                    });
                }
            }
        }
        #endregion

        #region Send message
        void SendCommand(ClientInfo client, string msg)
        {
            clsServer.Request(client.AddressClient, msg);
        }
        #endregion

        #region Custom
        bool ValidData(ClientInfo info)
        {
            info.Control.LockControls();
            info.Control.SetErrorFSN(string.Empty);
            info.Control.SetErrorIMEI(string.Empty);
            info.Control.SetErrorStatus(string.Empty);

            bool chk = true;

            if (info.ConnectionStatus != clsGeneral.fKey.ON)
                chk = false;

            if (info.FSN.Trim().IsEmpty())
            {
                info.Control.SetErrorFSN("FSN is required.");
                chk = false;
            }
            else if (lstClients.Any(x => !x.ControlName.Equals(info.ControlName) && x.FSN.Equals(info.FSN.Trim())))
            {
                info.Control.SetErrorFSN("FSN is existed");
                chk = false;
            }
            else
            {
                info.Control.SetErrorFSN(string.Empty);
            }

            if (clsGeneral.Config.IMEIRequired == 1)
            {
                if (info.IMEI.Trim().IsEmpty())
                {
                    info.Control.SetErrorIMEI("IMEI is required.");
                    chk = false;
                }
                else
                {
                    info.Control.SetErrorIMEI(string.Empty);
                }
            }
            else
            {
                info.Control.SetErrorIMEI(string.Empty);
            }

            return chk;
        }
        void SaveConfig()
        {
            clsGeneral.Config.SaveFile(clsGeneral.DirConfig, clsGeneral.FileConfig, clsGeneral.ExtConfig);
        }
        void ResetStatus()
        {
            if (lstProcesses.All(x => x.ClientStatus == clsGeneral.fKey.FINISHED))
            {
                this.InvokeExt(new Action(() =>
                {
                    btnStart.Enabled = true;
                    mnFile.Enabled = true;
                    lstProcesses.ForEach(x =>
                    {
                        x.Control.txtFSN.TextChanged -= TxtFSN_TextChanged;
                        x.Control.txtIMEI.TextChanged -= TxtIMEI_TextChanged;

                        x.Control.txtFSN.ResetText();
                        x.Control.txtIMEI.ResetText();
                        x.Control.UnlockControls();

                        x.Control.txtFSN.TextChanged += TxtFSN_TextChanged;
                        x.Control.txtIMEI.TextChanged += TxtIMEI_TextChanged;
                    });
                }));
            }
        }
        void StartClient()
        {
            lstProcesses.Clear();

            lstClients.ForEach(x =>
            {
                if (x.ConnectionStatus == clsGeneral.fKey.ON && x.ClientStatus == clsGeneral.fKey.WAITING)
                {
                    if (ValidData(x))
                    {
                        lstProcesses.Add(x);

                        this.InvokeExt(new Action(() =>
                        {
                            btnStart.Enabled = false;
                            mnFile.Enabled = false;
                            x.ClientStatus = clsGeneral.fKey.PROCESSING;
                            x.ClientMessage = clsGeneral.fKey.PROCESSING;
                            x.Control.SetFooterStatus(FSN: x.FSN, Status: x.ClientStatus);
                        }));

                        SendCommand(x, clsExtension.ConvertCommand(clsGeneral.fKey.IS_SCAN_IMEI.ToString(), clsGeneral.Config.IMEIRequired.ToString()));
                        SendCommand(x, clsExtension.ConvertCommand(clsGeneral.fKey.FSN.ToString(), x.FSN));
                        if (clsGeneral.Config.IMEIRequired == 1)
                            SendCommand(x, clsExtension.ConvertCommand(clsGeneral.fKey.IMEI.ToString(), x.IMEI));
                        else
                            SendCommand(x, clsExtension.ConvertCommand(clsGeneral.fKey.IMEI.ToString(), clsGeneral.fKey.EMPTY.ToString()));
                        SendCommand(x, clsExtension.ConvertCommand(clsGeneral.fKey.BEGIN.ToString(), clsGeneral.fKey.EMPTY.ToString()));
                    }
                    else
                    {
                        x.Control.UnlockControls();
                    }
                }
            });

            if (lstProcesses.Count == 0)
                this.showWarming(Caption: "No Device Ready.");
        }
        void CustomForm()
        {
            btnStart.Click -= BtnStart_Click;
            btnStart.Click += BtnStart_Click;
            mnNumberClient.Click -= mnNumberClient_Click;
            mnNumberClient.Click += mnNumberClient_Click;
            mnAddressServer.Click -= MnAddressServer_Click;
            mnAddressServer.Click += MnAddressServer_Click;
            SizeChanged -= FrmMain_SizeChanged;
            SizeChanged += FrmMain_SizeChanged;
        }
        void SetTitle()
        {
            this.BeginInvokeExt(() => { Text = string.Format("{0} - {1} - {2} - {3} ({4})", clsGeneral.AppTitle, clsGeneral.AppVersion, clsGeneral.PCName, clsGeneral.Config.AddressServer, clsGeneral.PortStatus.ToString()); });
        }
        void FindNextControl(Control ctr)
        {
            Control nextCtr = GetNextControl(ctr, true);
            while (true)
            {
                if (nextCtr is TextBox && nextCtr.Enabled)
                    break;
                if (nextCtr is Button)
                    break;
                if (nextCtr is DeviceControl)
                {
                    if (((DeviceControl)nextCtr).Controls[0] is TableLayoutPanel)
                    {
                        nextCtr = ((DeviceControl)nextCtr).Controls[0].Controls[0];
                    }
                }
                else
                    nextCtr = GetNextControl(nextCtr, true);
            }
            nextCtr.Focus();
        }
        #endregion

        #endregion

        #region Events
        protected override void frmBase_Load(object sender, EventArgs e)
        {
            base.frmBase_Load(sender, e);

            LoadConfig();
            CustomForm();
        }
        protected override void frmBase_FormClosing(object sender, FormClosingEventArgs e)
        {
            base.frmBase_FormClosing(sender, e);

            SaveConfig();
            clsServer.StatusChanged -= clsGeneral.mainServer_StatusChanged;
            clsGeneral.MainServer?.CloseListening();
        }
        void BtnStart_Click(object sender, EventArgs e)
        {
            StartClient();
        }
        void Label_DoubleClick(object sender, EventArgs e)
        {
            Label lbMain = (Label)sender;
            TableLayoutPanel tpMain = (TableLayoutPanel)lbMain.Parent;
            DeviceControl control = (DeviceControl)tpMain.Parent;

            LoadClient(control);
        }
        void TpMain_DoubleClick(object sender, EventArgs e)
        {
            TableLayoutPanel tpMain = (TableLayoutPanel)sender;
            DeviceControl control = (DeviceControl)tpMain.Parent;

            LoadClient(control);
        }
        void BtnConfig_Click(object sender, EventArgs e)
        {
            Button btnMain = (Button)sender;
            TableLayoutPanel tpMain = (TableLayoutPanel)btnMain.Parent;
            DeviceControl control = (DeviceControl)tpMain.Parent;

            LoadClient(control);
        }
        void TxtFSN_TextChanged(object sender, EventArgs e)
        {
            TextBox txtMain = (TextBox)sender;
            TableLayoutPanel tpMain = (TableLayoutPanel)txtMain.Parent;
            DeviceControl control = (DeviceControl)tpMain.Parent;

            ClientInfo client = lstClients.FirstOrDefault(x => x.Control.Name.Equals(control.Name));
            if (client != null && client.ClientStatus == clsGeneral.fKey.FINISHED)
            {
                client.ClientStatus = clsGeneral.fKey.WAITING;
                client.ClientMessage = clsGeneral.fKey.EMPTY;
                client.Control.SetFooterStatus(Status: client.ClientStatus);
                client.Control.SetFooterSKUNumber(string.Empty);
                client.Control.SetFooterReturnCode(string.Empty);
                client.Control.SetErrorFSN(string.Empty);
                client.Control.SetErrorIMEI(string.Empty);
                client.Control.SetErrorStatus(string.Empty);
            }
        }
        void TxtIMEI_TextChanged(object sender, EventArgs e)
        {
            TextBox txtMain = (TextBox)sender;
            TableLayoutPanel tpMain = (TableLayoutPanel)txtMain.Parent;
            DeviceControl control = (DeviceControl)tpMain.Parent;

            ClientInfo client = lstClients.FirstOrDefault(x => x.Control.Name.Equals(control.Name));
            if (client != null && client.ClientStatus == clsGeneral.fKey.FINISHED)
            {
                client.ClientStatus = clsGeneral.fKey.WAITING;
                client.ClientMessage = clsGeneral.fKey.EMPTY;
                client.Control.SetFooterStatus(Status: client.ClientStatus);
                client.Control.SetIMEI(IMEIRequired: clsGeneral.Config.IMEIRequired == 1);
                client.Control.SetFooterSKUNumber(string.Empty);
                client.Control.SetFooterReturnCode(string.Empty);
                client.Control.SetErrorFSN(string.Empty);
                client.Control.SetErrorIMEI(string.Empty);
                client.Control.SetErrorStatus(string.Empty);
            }
        }
        void mnNumberClient_Click(object sender, EventArgs e)
        {
            bool IsChanged = false;

            Action<bool> reload = (bRe) => { IsChanged = bRe; };

            frmNumberClientConfig frmNumber = new frmNumberClientConfig();
            frmNumber.ReloadData = new frmNumberClientConfig.LoadData(reload);
            frmNumber.ShowDialog(this);

            if (IsChanged)
            {
                SaveConfig();
                InitClient();
                ResizeClient();
                StartServer();
            }

            //bool IsChanged = false;

            //Action<bool> reload = (bRe) => { IsChanged = bRe; };

            //frmNumberClientConfig frmNumber = new frmNumberClientConfig();
            //frmNumber.ReloadData = new frmNumberClientConfig.LoadData(reload);
            //if (frmNumber.ShowDialog(this) == DialogResult.OK && IsChanged)
            //{
            //    frmAddressServerConfig frmAddress = new frmAddressServerConfig();
            //    frmAddress.ShowDialog(this);
            //}

            //if (IsChanged)
            //{
            //    SaveConfig();
            //    InitClient();
            //    ResizeClient();
            //    StartServer();
            //}
        }
        void MnAddressServer_Click(object sender, EventArgs e)
        {
            bool IsChanged = false;

            Action<bool> reload = (bRe) => { IsChanged = bRe; };

            frmAddressServerConfig frmAddress = new frmAddressServerConfig();
            frmAddress.ReloadData = new frmAddressServerConfig.LoadData(reload);
            if (frmAddress.ShowDialog(this) == DialogResult.OK && IsChanged)
            {
                frmNumberClientConfig frmNumber = new frmNumberClientConfig();
                frmNumber.ShowDialog(this);
            }

            if (IsChanged)
            {
                SaveConfig();
                InitClient();
                ResizeClient();
                StartServer();
            }
            else
            {
                lstClients.ForEach(x => LoadClientInfo(x));
            }
        }
        void FrmMain_SizeChanged(object sender, EventArgs e)
        {
            ResizeClient();
        }
        void TxtFSN_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                TextBox txtMain = (TextBox)sender;
                TableLayoutPanel tpMain = (TableLayoutPanel)txtMain.Parent;
                DeviceControl control = (DeviceControl)tpMain.Parent;

                control.txtFSN.PreviewKeyDown -= TxtFSN_PreviewKeyDown;

                if (control.txtFSN.Text.IsEmpty())
                {
                    control.SetErrorFSN("FSN is required");
                    e.IsInputKey = true;
                }
                else if (lstClients.Any(x => !x.ControlName.Equals(control.Name) && x.FSN.Equals(control.txtFSN.Text.Trim())))
                {
                    control.SetErrorFSN("FSN is existed");
                    e.IsInputKey = true;
                }
                else
                {
                    control.SetErrorFSN(string.Empty);
                }


                control.txtFSN.PreviewKeyDown += TxtFSN_PreviewKeyDown;
            }
            else if (e.KeyCode == Keys.Enter)
            {
                TextBox txtMain = (TextBox)sender;
                TableLayoutPanel tpMain = (TableLayoutPanel)txtMain.Parent;
                DeviceControl control = (DeviceControl)tpMain.Parent;

                if (control.txtFSN.Text.IsEmpty())
                {
                    control.SetErrorFSN("FSN is required");
                }
                else if (lstClients.Any(x => !x.ControlName.Equals(control.Name) && x.FSN.Equals(control.txtFSN.Text.Trim())))
                {
                    control.SetErrorFSN("FSN is existed");
                }
                else
                {
                    control.SetErrorFSN(string.Empty);
                    FindNextControl(control.txtFSN);
                    //control.txtIMEI.Select();
                }
            }
            else if (e.KeyCode == Keys.Escape)
            {
                TextBox txtMain = (TextBox)sender;
                TableLayoutPanel tpMain = (TableLayoutPanel)txtMain.Parent;
                DeviceControl control = (DeviceControl)tpMain.Parent;

                control.txtFSN.TextChanged -= TxtFSN_TextChanged;

                control.txtFSN.ResetText();
                control.SetErrorFSN(string.Empty);
                control.SetErrorStatus(string.Empty);

                control.txtFSN.TextChanged += TxtFSN_TextChanged;
            }
        }
        void TxtIMEI_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                TextBox txtMain = (TextBox)sender;
                TableLayoutPanel tpMain = (TableLayoutPanel)txtMain.Parent;
                DeviceControl control = (DeviceControl)tpMain.Parent;

                control.txtIMEI.PreviewKeyDown -= TxtIMEI_PreviewKeyDown;
                if (clsGeneral.Config.IMEIRequired == 1)
                {
                    if (control.txtIMEI.Text.IsEmpty())
                    {
                        control.SetErrorIMEI("IMEI is required");
                        e.IsInputKey = true;
                    }
                    else
                    {
                        control.SetErrorIMEI(string.Empty);
                    }
                }
                else
                {
                    control.SetErrorIMEI(string.Empty);
                }

                control.txtIMEI.PreviewKeyDown += TxtIMEI_PreviewKeyDown;
            }
            else if (e.KeyCode == Keys.Enter)
            {
                TextBox txtMain = (TextBox)sender;
                TableLayoutPanel tpMain = (TableLayoutPanel)txtMain.Parent;
                DeviceControl control = (DeviceControl)tpMain.Parent;

                if (clsGeneral.Config.IMEIRequired == 1)
                {
                    if (control.txtIMEI.Text.IsEmpty())
                    {
                        control.SetErrorIMEI("IMEI is required");
                    }
                    else
                    {
                        control.SetErrorIMEI(string.Empty);
                        FindNextControl(control.txtIMEI);
                        //SelectNextControl(GetNextControl(control.txtIMEI, true), true, true, true, true);
                    }
                }
                else
                {
                    control.SetErrorIMEI(string.Empty);
                }
            }
            else if (e.KeyCode == Keys.Escape)
            {
                TextBox txtMain = (TextBox)sender;
                TableLayoutPanel tpMain = (TableLayoutPanel)txtMain.Parent;
                DeviceControl control = (DeviceControl)tpMain.Parent;

                control.txtIMEI.TextChanged -= TxtIMEI_TextChanged;

                control.txtIMEI.ResetText();
                control.SetErrorIMEI(string.Empty);
                control.SetErrorStatus(string.Empty);

                control.txtIMEI.TextChanged += TxtIMEI_TextChanged;
            }
        }
        #endregion
    }
}
