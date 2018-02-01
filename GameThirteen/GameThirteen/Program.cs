using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace GameThirteen
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Initialize server");
            LoadConfig();
            StartServer();
            CustomForm();
            Console.WriteLine("Initialize completed");
            Console.WriteLine("Press a key to exit...");
            Console.ReadLine();
            StopServer();
            Console.WriteLine("Shutting down...");
        }

        delegate void UpdateStatusCallback(string address, string msg);
        static UpdateStatusCallback updateStatus;
        static clsServer mainServer;

        static void LoadConfig()
        {
            Config config = clsIO.LoadFile<Config>(clsGeneral.DirConfig, clsGeneral.FileConfig, clsGeneral.ExtConfig);
            clsGeneral.Config = config;
            clsGeneral.AddressServer = config.Address.ParseAddress();

            LoadNumberClient();
            LoadAddressServer();
        }
        static void LoadAddressServer()
        {
            if (clsGeneral.AddressServer == null || clsGeneral.Config.TotalNumber == 0)
            {
                Console.WriteLine("Init address server fail");
                return;
            }

            Console.WriteLine("Init address server success");
            foreach (ClientInfo info in clsGeneral.Clients)
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
        static void LoadNumberClient()
        {
            InitClient();
        }
        static void InitClient()
        {
            int i = 0;
            int j = 0;

            clsGeneral.Clients.Clear();

            if (clsGeneral.Config.TotalNumber == 0 || clsGeneral.AddressServer == null)
            {
                Console.WriteLine("Init client fail");
                return;
            }

            Console.WriteLine("Init client success");
            for (i = 0; i < clsGeneral.Config.RowNumber; i++)
            {
                for (j = 0; j < clsGeneral.Config.ColumnNumber; j++)
                {
                    string Name = string.Format("{0}{1}{2}", clsGeneral.fKey.CLIENT.ToString(), i, j);

                    Client client = clsGeneral.Config.Clients.FirstOrDefault(x => x.ControlName.Equals(Name));
                    ClientInfo cInfo = new ClientInfo() { ClientName = string.Format("{0} {1}{2}", clsGeneral.fKey.CLIENT.ToString(), i + 1, j + 1), ControlName = Name, RowID = i + 1, ColumnID = j + 1 };
                    clsGeneral.Clients.Add(cInfo);
                    if (client == null)
                        clsGeneral.Config.Clients.Add(client = new Client() { ClientName = cInfo.ClientName, ControlName = cInfo.ControlName, RowID = cInfo.RowID, ColumnID = cInfo.ColumnID });

                    cInfo.Client = client;
                    cInfo.Client.IPClient = client.IPClient;
                    cInfo.Client.PortClient = client.PortClient;
                    cInfo.Client.AddressClient = client.AddressClient;

                    LoadClientInfo(cInfo);
                }
            }
        }
        static void LoadClientInfo(ClientInfo cInfo)
        {
            cInfo.ClientName = cInfo.Client.ClientName.IsEmpty() ? cInfo.Client.ControlName : cInfo.Client.ClientName;
            cInfo.IPClient = cInfo.Client.IPClient;
            cInfo.PortClient = cInfo.Client.PortClient;
            cInfo.AddressClient = cInfo.Client.AddressClient;
            cInfo.ClientHost = cInfo.AddressClient.ParseAddress();
            ReloadConnection(cInfo);
        }
        static void ReloadConnection()
        {
            foreach (ClientInfo info in clsGeneral.Clients)
            {
                SendCommand(info, clsExtension.ConvertCommand(clsGeneral.fKey.CONNECTING.ToString(), clsGeneral.fKey.QUESTION.ToString()));
            }
        }
        static void ReloadConnection(ClientInfo client)
        {
            //SendCommand(client, clsExtension.ConvertCommand(clsGeneral.fKey.CONNECTING.ToString(), clsGeneral.fKey.QUESTION.ToString()));
        }
        static void StartServer()
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
        static void StopServer()
        {
            SaveConfig();
            clsServer.StatusChanged -= mainServer_StatusChanged;
            mainServer.CloseListening();
        }
        static void UpdateStatus(string address, string msg)
        {
            ClientInfo client = clsGeneral.Clients.FirstOrDefault(x => x.AddressClient.Equals(address));

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
                        client.ConnectionStatus = clsGeneral.fKey.OFF;
                        client.ConnectionMessage = clsGeneral.fKey.EMPTY;
                    }
                }
                else if (key.Equals(clsGeneral.fKey.USERNAME.ToString()))
                {
                    client.ClientName = value;
                    client.Client.ClientName = value;
                    client.ConnectionMessage = clsGeneral.fKey.EMPTY;
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
                }

                client.ClientStatus = (clsGeneral.fKey)Enum.Parse(typeof(clsGeneral.fKey), key);
                Console.WriteLine("{0}: [{1}]=[{2}]", client.ClientName, key, value);
            }
        }
        static void SendCommand(ClientInfo client, string msg)
        {
            clsServer.Request(client.AddressClient, msg);
        }
        static bool ValidData(ClientInfo info)
        {
            bool chk = true;

            if (info.ConnectionStatus != clsGeneral.fKey.ON)
                chk = false;

            return chk;
        }
        static void SaveConfig()
        {
            clsGeneral.Config.Clients.Clear();
            clsGeneral.Config.SaveFile(clsGeneral.DirConfig, clsGeneral.FileConfig, clsGeneral.ExtConfig);
        }
        static void ResetStatus()
        {
            if (clsGeneral.Playings.All(x => x.ClientStatus == clsGeneral.fKey.FINISHED))
            {
            }
        }
        static void StartClient()
        {
            clsGeneral.Playings.Clear();

            clsGeneral.Clients.ForEach(x =>
            {
                if (ValidData(x))
                {
                    clsGeneral.Playings.Add(x);
                    SendCommand(x, clsExtension.ConvertCommand(clsGeneral.fKey.BEGIN.ToString(), clsGeneral.fKey.EMPTY.ToString()));
                }
            });
        }
        static void CustomForm()
        {
        }
        static void mainServer_StatusChanged(object sender, StatusChangedEventArgs e)
        {
            // Call the method that updates the form
            updateStatus?.Invoke(e.EventAddress, e.EventMessage);
        }
    }
}
