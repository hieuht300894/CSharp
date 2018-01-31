using GameBlackJack.BUS;
using GameBlackJack.MODEL;
using HostKVM.UserControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameBlackJack.GUI
{
    public partial class frmMain : frmBase
    {
        #region Variables
        delegate void UpdateStatusCallback(string address, string msg);
        UpdateStatusCallback updateStatus;
        List<ClientInfo> lstClients = new List<ClientInfo>();
        List<ClientInfo> lstPlayings = new List<ClientInfo>();
        clsServer mainServer;
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
            clsGeneral.AddressServer = config.Address.ParseAddress();

            LoadNumberClient();
            LoadAddressServer();
        }
        void LoadAddressServer()
        {
            if (clsGeneral.AddressServer == null)
            {
                frmAddressServerConfig frmAddress = new frmAddressServerConfig();
                if (frmAddress.ShowDialog(this) == DialogResult.OK)
                {
                    clsGeneral.Config.ColumnNumber = clsGeneral.DefaultNumOfColumns;
                    clsGeneral.Config.RowNumber = clsGeneral.DefaultNumOfRows;
                    clsGeneral.Config.Clients = new List<Client>();

                    frmNumberClientConfig frmNumber = new frmNumberClientConfig();
                    if (frmNumber.ShowDialog(this) == DialogResult.OK)
                    {
                        SaveConfig();
                        LoadConfig();
                    }
                    else
                    {
                        LoadConfig();
                    }
                }
            }
            else
            {
                //lbIPNetwork.Text = clsGeneral.Config.Address;

                foreach (ClientInfo info in lstClients)
                {
                    info.IPServer = clsGeneral.Config.IP;
                    info.PortServer = clsGeneral.Config.Port;
                    info.AddressServer = clsGeneral.Config.Address;
                    info.ServerHost = clsGeneral.AddressServer;

                    if (info.Client != null)
                    {
                        info.Client.IPServer = clsGeneral.Config.IP;
                        info.Client.PortServer = clsGeneral.Config.Port;
                        info.Client.AddressServer = clsGeneral.Config.Address;
                    }
                }
            }
        }
        void LoadNumberClient()
        {
            if (clsGeneral.Config.TotalNumber > 0)
            {
                InitClient();
                ResizeClient();
            }
            else
            {
                frmNumberClientConfig frmNumber = new frmNumberClientConfig();
                if (frmNumber.ShowDialog(this) == DialogResult.OK)
                {
                    clsGeneral.AddressServer = null;
                    clsGeneral.Config.IP = string.Empty;
                    clsGeneral.Config.Port = clsGeneral.DefaultPort;
                    clsGeneral.Config.Address = string.Empty;
                    clsGeneral.Config.Clients = new List<Client>();

                    frmAddressServerConfig frmAddress = new frmAddressServerConfig();
                    if (frmAddress.ShowDialog(this) == DialogResult.OK)
                    {
                        SaveConfig();
                        LoadConfig();
                    }
                    else
                    {
                        LoadConfig();
                    }
                }
            }
        }
        void ResizeClient()
        {
            if (clsGeneral.Config.TotalNumber == 0) return;

            int avrWidth = tpClient.Size.Width / clsGeneral.Config.ColumnNumber;

            lstClients.ForEach(x =>
            {
                x.Control.Width = avrWidth - x.Control.Margin.Left - x.Control.Margin.Right;
            });
        }
        void InitClient()
        {
            int i = 0;
            int j = 0;

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
            for (i = 0; i < clsGeneral.Config.RowNumber; i++)
            {
                for (j = 0; j < clsGeneral.Config.ColumnNumber; j++)
                {
                    DeviceControl dc = new DeviceControl();
                    dc.Name = string.Format("{0}{1}{2}", "Client", i, j);

                    Client client = clsGeneral.Config.Clients.FirstOrDefault(x => x.ControlName.Equals(dc.Name));
                    ClientInfo cInfo = new ClientInfo() { Control = dc, ClientName = string.Format("{0} {1}{2}", "Client", i + 1, j + 1), ControlName = string.Format("{0}{1}{2}", "Client", i, j), RowID = i + 1, ColumnID = j + 1 };
                    lstClients.Add(cInfo);
                    if (client == null)
                        clsGeneral.Config.Clients.Add(client = new Client() { ClientName = cInfo.ClientName, ControlName = cInfo.ControlName, RowID = cInfo.RowID, ColumnID = cInfo.ColumnID });

                    cInfo.Client = client;
                    cInfo.Client.IPClient = client.IPClient;
                    cInfo.Client.PortClient = client.PortClient;
                    cInfo.Client.AddressClient = client.AddressClient;

                    LoadClientInfo(cInfo);

                    dc.txtFSN.DataBindings.Clear();
                    dc.txtFSN.DataBindings.Add("Text", cInfo, "FSN", true, DataSourceUpdateMode.OnPropertyChanged);

                    dc.txtIMEI.DataBindings.Clear();
                    dc.txtIMEI.DataBindings.Add("Text", cInfo, "IMEI", true, DataSourceUpdateMode.OnPropertyChanged);

                    dc.btnConfig.Click -= BtnConfig_Click;
                    dc.btnConfig.Click += BtnConfig_Click;

                    dc.txtFSN.TextChanged -= TxtMain_TextChanged;
                    dc.txtFSN.TextChanged += TxtMain_TextChanged;

                    dc.txtIMEI.TextChanged -= TxtMain_TextChanged;
                    dc.txtIMEI.TextChanged += TxtMain_TextChanged;

                    tpClient.Controls.Add(dc, j, i);
                }
            }
        }
        void LoadClientInfo(ClientInfo cInfo)
        {
            cInfo.ClientName = cInfo.Client.ClientName.IsEmpty() ? cInfo.Client.ControlName : cInfo.Client.ClientName;
            cInfo.IPClient = cInfo.Client.IPClient;
            cInfo.PortClient = cInfo.Client.PortClient;
            cInfo.AddressClient = cInfo.Client.AddressClient;
            cInfo.ClientHost = cInfo.AddressClient.ParseAddress();
            cInfo.Control.SetInfo(ClientName: cInfo.ClientName, Address: cInfo.AddressClient, Status: cInfo.ConnectionStatus);
            cInfo.Control.SetStatus(Status: cInfo.ClientStatus);
            cInfo.Control.SetFSN(FSN: cInfo.FSN);
            cInfo.Control.SetIMEI(IMEI: cInfo.IMEI);
            ReloadConnection(cInfo);
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
            //SendCommand(client, clsExtension.ConvertCommand(clsGeneral.fKey.CONNECTING.ToString(), clsGeneral.fKey.QUESTION.ToString()));
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
            updateStatus = UpdateStatus;

            // Create a new instance of the object
            mainServer = new clsServer(clsGeneral.AddressServer);

            // Hook the StatusChanged event handler to mainServer_StatusChanged
            clsServer.StatusChanged -= mainServer_StatusChanged;
            clsServer.StatusChanged += mainServer_StatusChanged;

            // Start listening for connections
            mainServer.StartListening();
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
                if (key.Equals(clsGeneral.fKey.REGISTER.ToString()))
                {
                    if (value.Equals(clsGeneral.fKey.ACCEPT.ToString()))
                    {
                        client.ConnectionStatus = clsGeneral.fKey.ON;
                        client.ConnectionMessage = clsGeneral.fKey.EMPTY;
                    }
                    if (value.Equals(clsGeneral.fKey.DENY.ToString()))
                    {
                        if (client.ClientStatus == clsGeneral.fKey.PROCESSING)
                        {
                            client.ClientStatus = clsGeneral.fKey.FINISHED;
                            client.ClientMessage = clsGeneral.fKey.NO_CLIENT_RESULT;
                            this.InvokeExt(new Action(() => { client.Control.SetStatus(FSN: client.FSN, Status: client.ClientMessage); }));
                            ResetStatus();
                        }

                        client.ConnectionStatus = clsGeneral.fKey.OFF;
                        client.ConnectionMessage = clsGeneral.fKey.EMPTY;
                    }

                    this.InvokeExt(new Action(() => { client.Control.SetInfo(ClientName: client.ClientName, Address: client.AddressClient, Status: client.ConnectionStatus); }));
                }
                else if (key.Equals(clsGeneral.fKey.USERNAME.ToString()))
                {
                    client.ClientName = value;
                    client.Client.ClientName = value;
                    client.ConnectionMessage = clsGeneral.fKey.EMPTY;
                    this.InvokeExt(new Action(() => { client.Control.SetInfo(ClientName: value, Address: client.AddressClient, Status: client.ConnectionStatus); }));
                }
                else if (key.Equals(clsGeneral.fKey.BEGIN.ToString()))
                {
                    client.ClientStatus = clsGeneral.fKey.BEGIN;
                }
                else if (key.Equals(clsGeneral.fKey.TESTING.ToString()))
                {
                    client.ClientStatus = clsGeneral.fKey.TESTING;
                }
                else if (key.Equals(clsGeneral.fKey.RESULT.ToString()))
                {
                    client.ClientStatus = clsGeneral.fKey.FINISHED;
                    if (value.Equals(clsGeneral.fKey.PASS.ToString()))
                        client.ClientMessage = clsGeneral.fKey.PASS;
                    if (value.Equals(clsGeneral.fKey.FAILED.ToString()))
                        client.ClientMessage = clsGeneral.fKey.FAILED;
                    this.InvokeExt(new Action(() => { client.Control.SetStatus(FSN: client.FSN, Status: client.ClientMessage); }));

                    ResetStatus();
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
            bool chk = true;

            if (info.ConnectionStatus != clsGeneral.fKey.ON)
                chk = false;

            if (info.FSN.IsEmpty())
            {
                info.Control.txtFSN.SetError(info.errorFSN, "FSN isn't valid");
                chk = false;
            }
            else
            {
                info.Control.txtFSN.SetError(info.errorFSN, "");
            }

            if (info.IMEI.IsEmpty())
            {
                info.Control.txtIMEI.SetError(info.errorIMEI, "IMEI isn't valid");
                chk = false;
            }
            else
            {
                info.Control.txtIMEI.SetError(info.errorIMEI, "");
            }

            return chk;
        }
        void SaveConfig()
        {
            clsGeneral.Config.SaveFile(clsGeneral.DirConfig, clsGeneral.FileConfig, clsGeneral.ExtConfig);
        }
        void ResetStatus()
        {
            if (lstPlayings.All(x => x.ClientStatus == clsGeneral.fKey.FINISHED))
            {
                this.InvokeExt(new Action(() =>
                {
                    //btnStart.Enabled = true;
                    lstPlayings.ForEach(x =>
                    {
                        x.Control.txtFSN.Enabled = true;
                        x.Control.txtIMEI.Enabled = true;
                        x.Control.btnConfig.Enabled = true;
                    });
                }));
            }
        }
        void StartClient()
        {
            lstPlayings.Clear();

            lstClients.ForEach(x =>
            {
                if (ValidData(x))
                {
                    lstPlayings.Add(x);

                    this.InvokeExt(new Action(() =>
                    {
                        //btnStart.Enabled = false;
                        x.ClientStatus = clsGeneral.fKey.PROCESSING;
                        x.ClientMessage = clsGeneral.fKey.PROCESSING;
                        x.Control.txtFSN.Enabled = false;
                        x.Control.txtIMEI.Enabled = false;
                        x.Control.btnConfig.Enabled = false;
                        x.Control.SetStatus(Status: clsGeneral.fKey.PROCESSING);
                    }));

                    SendCommand(x, clsExtension.ConvertCommand(clsGeneral.fKey.FSN.ToString(), x.FSN));
                    SendCommand(x, clsExtension.ConvertCommand(clsGeneral.fKey.IMEI.ToString(), x.IMEI));
                    SendCommand(x, clsExtension.ConvertCommand(clsGeneral.fKey.BEGIN.ToString(), clsGeneral.fKey.EMPTY.ToString()));
                }
            });
        }
        void CustomForm()
        {
            //btnStart.Click -= BtnStart_Click;
            //btnStart.Click += BtnStart_Click;
            mnNumberOfClients.Click -= MnNumberClient_Click;
            mnNumberOfClients.Click += MnNumberClient_Click;
            mnServerConfiguation.Click -= MnAddressServer_Click;
            mnServerConfiguation.Click += MnAddressServer_Click;
            SizeChanged -= FrmMain_SizeChanged;
            SizeChanged += FrmMain_SizeChanged;
        }
        #endregion

        #endregion

        #region Events
        protected override void FrmBase_Load(object sender, EventArgs e)
        {
            base.FrmBase_Load(sender, e);

            LoadConfig();
            StartServer();
            CustomForm();
        }
        protected override void FrmBase_FormClosing(object sender, FormClosingEventArgs e)
        {
            base.FrmBase_FormClosing(sender, e);

            SaveConfig();
            clsServer.StatusChanged -= mainServer_StatusChanged;
            mainServer.CloseListening();
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
        void TxtMain_TextChanged(object sender, EventArgs e)
        {
            TextBox txtMain = (TextBox)sender;
            TableLayoutPanel tpMain = (TableLayoutPanel)txtMain.Parent;
            DeviceControl control = (DeviceControl)tpMain.Parent;

            ClientInfo client = lstClients.FirstOrDefault(x => x.Control.Name.Equals(control.Name));
            if (client != null && client.ClientStatus == clsGeneral.fKey.FINISHED)
            {
                client.ClientStatus = clsGeneral.fKey.WAITING;
                client.ClientMessage = clsGeneral.fKey.EMPTY;
                client.Control.SetStatus(Status: client.ClientStatus);
            }
        }
        void mainServer_StatusChanged(object sender, StatusChangedEventArgs e)
        {
            // Call the method that updates the form
            updateStatus?.Invoke(e.EventAddress, e.EventMessage);
        }
        void MnNumberClient_Click(object sender, EventArgs e)
        {
            frmNumberClientConfig frmNumber = new frmNumberClientConfig();
            if (frmNumber.ShowDialog(this) == DialogResult.OK)
            {
                clsGeneral.AddressServer = null;
                clsGeneral.Config.IP = string.Empty;
                clsGeneral.Config.Port = clsGeneral.DefaultPort;
                clsGeneral.Config.Address = string.Empty;
                clsGeneral.Config.Clients = new List<Client>();

                frmAddressServerConfig frmAddress = new frmAddressServerConfig();
                if (frmAddress.ShowDialog(this) == DialogResult.OK)
                {
                    SaveConfig();
                    LoadConfig();
                }
                else
                {
                    LoadConfig();
                }
            }
        }
        void MnAddressServer_Click(object sender, EventArgs e)
        {
            frmAddressServerConfig frmAddress = new frmAddressServerConfig();
            if (frmAddress.ShowDialog(this) == DialogResult.OK)
            {
                clsGeneral.Config.ColumnNumber = clsGeneral.DefaultNumOfColumns;
                clsGeneral.Config.RowNumber = clsGeneral.DefaultNumOfRows;
                clsGeneral.Config.Clients = new List<Client>();

                frmNumberClientConfig frmNumber = new frmNumberClientConfig();
                if (frmNumber.ShowDialog(this) == DialogResult.OK)
                {
                    SaveConfig();
                    LoadConfig();
                }
                else
                {
                    LoadConfig();
                }
            }
        }
        void FrmMain_SizeChanged(object sender, EventArgs e)
        {
            ResizeClient();
        }
        #endregion
    }
}
