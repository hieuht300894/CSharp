using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace GameBlackJack
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

    class clsServer
    {
        /// <summary>
        /// List client connected
        /// </summary>
        public static Dictionary<string, Connection> Connections = new Dictionary<string, Connection>();
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

    class Connection
    {
        TcpClient tcpClient;
        IPEndPoint ipClient;
        IPEndPoint ipServer;
        CancellationTokenSource cancellation;
        Task taskClient;
        string address;
        string key = string.Empty;
        string value = string.Empty;

        public TcpClient TcpClient { get { return tcpClient; } }
        public IPEndPoint IpClient { get { return ipClient; } }
        public IPEndPoint IpServer { get { return ipServer; } }
        public CancellationTokenSource Cancellation { get { return cancellation; } }
        public Task TaskClient { get { return taskClient; } }
        public string Address { get { return address; } }

        /// <summary>
        /// Init a object
        /// </summary>
        /// <param name="_tcpClient"></param>
        /// <param name="_ipServer"></param>
        /// <param name="_ipClient"></param>
        public Connection(TcpClient _tcpClient, IPEndPoint _ipServer, IPEndPoint _ipClient)
        {
            tcpClient = _tcpClient;
            ipServer = _ipServer;
            ipClient = _ipClient;
            address = _ipClient.ToString();
            cancellation = new CancellationTokenSource();
            taskClient = new Task(() => AcceptClient(), cancellation.Token);
        }

        /// <summary>
        /// Client close connection
        /// </summary>
        public void CloseConnection()
        {
            try
            {
                if (cancellation != null)
                    cancellation.Cancel();
                if (tcpClient != null)
                    tcpClient.Close();
            }
            catch (Exception ex) { }
        }

        /// <summary>
        /// Client is accepted connect to host
        /// </summary>
        public void AcceptClient()
        {
            if (address.IsEmpty())
                return;

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
                StreamWriter writer = new StreamWriter(tcpClient.GetStream());
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
                StreamReader reader = new StreamReader(tcpClient.GetStream());

                strReader = reader.ReadLine() ?? string.Empty;
                strReader.ParseCommand(out key, out value);

                if (!key.IsEmpty() && !value.IsEmpty())
                {
                    if (!clsServer.Connections.Any(x => x.Key.Equals(address)))
                    {
                        clsServer.Connections.Add(address, this);
                        clsServer.Response(address, clsExtension.ConvertCommand(clsGeneral.fKey.REGISTER.ToString(), clsGeneral.fKey.ACCEPT.ToString()));

                        if (!clsGeneral.Config.Clients.Any(x => x.AddressClient.Equals(address)))
                            clsServer.RemoveClient(address);
                    }
                }
                else
                    clsServer.RemoveClient(address);

                while (true)
                {
                    if (cancellation.IsCancellationRequested)
                    {
                        clsServer.RemoveClient(address);
                        break;
                    }
                    else
                    {
                        strReader = reader.ReadLine() ?? string.Empty;
                        strReader.ParseCommand(out key, out value);

                        if (!key.IsEmpty() && !value.IsEmpty())
                            clsServer.Response(address, strReader);
                        else
                        {
                            clsServer.RemoveClient(address);
                            break;
                        }
                    }
                };
            }
            catch
            {
                clsServer.RemoveClient(address);
            }
        }
    }
}