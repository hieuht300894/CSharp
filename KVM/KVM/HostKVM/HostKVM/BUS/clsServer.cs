using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace HostKVM
{
    /// <summary>
    /// Create a StatusChanged event base EventArgs
    /// </summary>
    public class StatusChangedEventArgs : EventArgs
    {
        string eventMessage;
        string eventAddress;

        public string EventMessage { get { return eventMessage; } }
        public string EventAddress { get { return eventAddress; } }

        public StatusChangedEventArgs(string eventAddress, string eventMessage)
        {
            this.eventAddress = eventAddress;
            this.eventMessage = eventMessage;
        }
    }

    public delegate void StatusChangedEventHandler(object sender, StatusChangedEventArgs e);

    public class clsServer
    {
        /// <summary>
        /// List client connected
        /// </summary>
        static Dictionary<string, Connection> Connections = new Dictionary<string, Connection>();
        IPEndPoint ipServer;
        TcpClient tcpClient;
        TcpListener tlsClient;
        CancellationTokenSource cancellation;
        Task taskClient;
        public static event StatusChangedEventHandler StatusChanged;

        public clsServer(IPAddress _ipServer, int _port)
        {
            ipServer = new IPEndPoint(_ipServer, _port);
            Connections = new Dictionary<string, Connection>();
        }
        public clsServer(IPEndPoint ip)
        {
            ipServer = ip;
            Connections = new Dictionary<string, Connection>();
        }

        /// <summary>
        /// Check client is existed
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public static bool CheckExist(string address)
        {
            return Connections.Any(x => x.Key.Equals(address));
        }

        /// <summary>
        /// Add new client
        /// </summary>
        /// <param name="address"></param>
        /// <param name="connection"></param>
        public static void AddClient(string address, Connection connection)
        {
            Connections.Add(address, connection);
        }

        /// <summary>
        /// Remove client
        /// </summary>
        /// <param name="address"></param>
        public static void RemoveClient(string address)
        {
            string _addressClient = Connections.Select(x => x.Key).FirstOrDefault(x => x.Equals(address));
            if (!_addressClient.IsEmpty())
            {
                Response(address, clsExtension.ConvertCommand(clsGeneral.fKey.REGISTER.ToString(), clsGeneral.fKey.DENY.ToString()));

                Connection connection = Connections[address];
                connection.CloseConnection();

                Connections.Remove(address);
            }
        }

        /// <summary>
        /// Receive a message from client and send to form 
        /// </summary>
        /// <param name="address"></param>
        /// <param name="message"></param>
        public static void Response(string address, string message)
        {
            StatusChanged?.Invoke(null, new StatusChangedEventArgs(address, message));

            string _addressClient = Connections.Select(x => x.Key).FirstOrDefault(x => x.Equals(address));
            if (!_addressClient.IsEmpty())
            {
                if (message.IsEmpty())
                    RemoveClient(address);
                else
                    Connections[address].Request(message);
            }
        }

        /// <summary>
        /// Send a message to client
        /// </summary>
        /// <param name="address"></param>
        /// <param name="message"></param>
        public static void Request(string address, string message)
        {
            string _addressClient = Connections.Select(x => x.Key).FirstOrDefault(x => x.Equals(address));
            if (!_addressClient.IsEmpty())
                Connections[address].Request(message);
        }

        /// <summary>
        /// Host start listening client
        /// </summary>
        public void StartListening()
        {
            try
            {
                tlsClient = new TcpListener(ipServer);
                tlsClient.Start();

                clsGeneral.PortStatus = clsGeneral.fKey.ON;
                clsGeneral._SetTitle?.Invoke();

                cancellation = new CancellationTokenSource();
                taskClient = new Task(() => KeepListening(), cancellation.Token);
                taskClient.Start();
            }
            catch
            {
                CloseListening();
            }
        }

        /// <summary>
        /// Host's waiting client connect
        /// </summary>
        void KeepListening()
        {
            while (true)
            {
                try
                {
                    if (cancellation.IsCancellationRequested)
                        break;

                    tcpClient = tlsClient.AcceptTcpClient();
                    Connection connection = new Connection(tcpClient, ipServer, (IPEndPoint)tcpClient.Client.RemoteEndPoint);
                    connection.TaskClient.Start();
                }
                catch { break; }
            }
        }

        /// <summary>
        /// Host close listening
        /// </summary>
        public void CloseListening()
        {
            clsGeneral.PortStatus = clsGeneral.fKey.OFF;
            clsGeneral._SetTitle?.Invoke();

            try
            {
                if (cancellation != null)
                    cancellation.Cancel();
                if (tcpClient != null)
                    tcpClient.Close();
                if (tlsClient != null)
                    tlsClient.Stop();
            }
            catch { }
        }
    }

    public class Connection
    {
        //TcpClient tcpClient;
        //IPEndPoint ipClient;
        //IPEndPoint ipServer;
        //CancellationTokenSource cancellation;
        //Task taskClient;
        //string address;
        string key = string.Empty;
        string value = string.Empty;

        public TcpClient TcpClient { get; private set; }
        public IPEndPoint IpClient { get; private set; }
        public IPEndPoint IpServer { get; private set; }
        public CancellationTokenSource Cancellation { get; private set; }
        public Task TaskClient { get; private set; }
        public string Address { get; private set; }

        /// <summary>
        /// Init a object
        /// </summary>
        /// <param name="_tcpClient"></param>
        /// <param name="_ipServer"></param>
        /// <param name="_ipClient"></param>
        public Connection(TcpClient _tcpClient, IPEndPoint _ipServer, IPEndPoint _ipClient)
        {
            TcpClient = _tcpClient;
            IpServer = _ipServer;
            IpClient = _ipClient;
            Address = _ipClient.ToString();
            Cancellation = new CancellationTokenSource();
            TaskClient = new Task(() => AcceptClient(), Cancellation.Token);
        }

        /// <summary>
        /// Client close connection
        /// </summary>
        public void CloseConnection()
        {
            try
            {
                Cancellation?.Cancel();
                TcpClient?.Close();
            }
            catch { }
        }

        /// <summary>
        /// Client is accepted connect to host
        /// </summary>
        public void AcceptClient()
        {
            if (!Address.IsEmpty())
                Response();
        }

        /// <summary>
        /// Client send message
        /// </summary>
        /// <param name="Message"></param>
        public void Request(string Message)
        {
            try
            {
                // Send the message to the current user 
                StreamWriter writer = new StreamWriter(TcpClient.GetStream());
                writer.WriteLine(Message);
                writer.Flush();
            }
            catch { }
        }

        /// <summary>
        /// Client receive a message
        /// </summary>
        public void Response()
        {
            try
            {
                string strReader = string.Empty;
                StreamReader reader = new StreamReader(TcpClient.GetStream());

                strReader = reader.ReadLine() ?? string.Empty;
                strReader.ParseCommand(out key, out value);

                if (!key.IsEmpty() && !value.IsEmpty())
                {
                    if (!clsServer.CheckExist(Address))
                    {
                        clsServer.AddClient(Address, this);

                        if (clsGeneral.Config.Clients.Any(x => x.AddressClient.Equals(Address)))
                        {
                            clsServer.Response(Address, clsExtension.ConvertCommand(clsGeneral.fKey.REGISTER.ToString(), clsGeneral.fKey.ACCEPT.ToString()));
                        }
                        else
                        {
                            clsServer.Response(Address, clsExtension.ConvertCommand(clsGeneral.fKey.REGISTER.ToString(), clsGeneral.fKey.DENY.ToString()));
                            clsServer.RemoveClient(Address);
                        }
                    }
                }
                else
                {
                    clsServer.RemoveClient(Address);
                }

                while (true)
                {
                    if (Cancellation.IsCancellationRequested)
                    {
                        clsServer.RemoveClient(Address);
                        break;
                    }
                    else
                    {
                        strReader = reader.ReadLine() ?? string.Empty;
                        strReader.ParseCommand(out key, out value);

                        if (!key.IsEmpty() && !value.IsEmpty())
                        {
                            clsServer.Response(Address, strReader);
                        }
                        else
                        {
                            clsServer.RemoveClient(Address);
                            break;
                        }
                    }
                };
            }
            catch
            {
                clsServer.RemoveClient(Address);
            }
        }
    }
}