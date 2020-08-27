using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server
{
    public partial class Server : Form
    {
        public class WebSockets
        {
            public Socket _socket { get; set; }
            public string _name { get; set; }
            public WebSockets(Socket socket)
            {
                _socket = socket;
            }
        }

        byte[] buffer = new byte[1024];

        //Create server socket End Point
        public Socket _serversocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        public List<WebSockets> _clientsList { get; set; }
        public Server()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            _clientsList = new List<WebSockets>();
        }

        private void Server_Load(object sender, EventArgs e)
        {
            //set up server
            setUpServer();
        }
        private void setUpServer()
        {
            try
            {
                serverMsg.Text = "setting up server..............";
                //Bind the server to the Specific IP and Port
                _serversocket.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9686));
                serverMsg.Text = "server running..............";

                //Listen to the Clients
                _serversocket.Listen(1);

                //Accept the clients connection
                _serversocket.BeginAccept(new AsyncCallback(AcceptCallBack), null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString()); ;
            }
        }
        private void AcceptCallBack(IAsyncResult aResult)
        {
            try
            {
                Socket socket = _serversocket.EndAccept(aResult);
                _clientsList.Add(new WebSockets(socket));
                Clients_List.Items.Add(socket.RemoteEndPoint.ToString());

                //Receive Data from clients
                socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallBack), socket);
                //again accept the clients connection
                _serversocket.BeginAccept(new AsyncCallback(AcceptCallBack), null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString()); ;
            }
        }
        private void ReceiveCallBack(IAsyncResult aResult)
        {
            Socket socket = (Socket)aResult.AsyncState;
            if (socket.Connected)
            {
                int Received;
                try
                {
                    //Received Data
                    Received = socket.EndReceive(aResult);
                }
                catch (Exception)
                {
                    for (int i = 0; i < _clientsList.Count; i++)
                    {
                        if (_clientsList[i]._socket.RemoteEndPoint.ToString().Equals(socket.RemoteEndPoint.ToString()))
                        {
                            _clientsList.RemoveAt(i);
                            serverMsg.Text = "No of Client Connected: " + _clientsList.Count.ToString();
                        }
                    }
                    return;
                }
                if (Received != 0)
                {
                    byte[] dataBuffer = new byte[Received];
                    Array.Copy(buffer, dataBuffer, Received);

                    //Convert byte data to string
                    string Text = Encoding.ASCII.GetString(dataBuffer);

                    if (Text.StartsWith("@@"))
                    {
                        for (int i = 0; i < Clients_List.Items.Count; i++)
                        {
                            if (socket.RemoteEndPoint.ToString().Equals(_clientsList[i]._socket.RemoteEndPoint.ToString()))
                            {
                                Clients_List.Items.RemoveAt(i);
                                Clients_List.Items.Insert(i, Text.Substring(1, Text.Length - 1));
                                _clientsList[i]._name = Text;
                                for(int j = 0; j < _clientsList.Count; j++) 
                                {
                                    for(int k = 0; k < _clientsList.Count; k++) 
                                    {
                                        //Convert string to byte
                                        byte[] clientBuffer = Encoding.ASCII.GetBytes(_clientsList[k]._name);

                                        //Sends The number of Registered Clients
                                        _clientsList[j]._socket.BeginSend(clientBuffer, 0, clientBuffer.Length, SocketFlags.None, new AsyncCallback(SendClientCallBack), _clientsList[j]._socket);
                                    }
                                }
                                //Receive Data from clients
                                socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallBack), socket);
                                return;
                            }
                        }
                    }
                    string clientName = null;
                    //Appending Clients Message To the ListBox
                    for (int i = 0; i < _clientsList.Count; i++)
                    {
                        if (socket.RemoteEndPoint.ToString().Equals(_clientsList[i]._socket.RemoteEndPoint.ToString()))
                        {
                            string [] txtMsg=Text.Split('_');
                            textStatus.Items.Add(_clientsList[i]._name + " : " + txtMsg[1]);
                            clientName = _clientsList[i]._name;
                        }
                    }
                    string[] clMsg=Text.Split('_');
                    char[] nm = clMsg[0].ToCharArray();
                    string name = null;
                    for(int j = 4; j < nm.Length; j++) 
                    {
                        name += nm[j];
                    }
                    for(int k = 0; k < Clients_List.Items.Count; k++) 
                    {
                        string dummyname =Clients_List.Items[k].ToString();
                        if (dummyname.Equals(name)) 
                        {
                            byte[] clientMsg = Encoding.ASCII.GetBytes("-->"+ clientName + "_"+clMsg[1]);
                            _clientsList[k]._socket.BeginSend(clientMsg, 0, clientMsg.Length, SocketFlags.None, new AsyncCallback(SendCallback), _clientsList[k]._socket);
                        }
                    }
                   
                    //Receive Data from clients
                    socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallBack), socket);
                }
            }
        }
        private void SendClientCallBack(IAsyncResult AR)
        {
            Socket socket = (Socket)AR.AsyncState;
            socket.EndSend(AR);
        }
        private void btnSend_Click_1(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < Clients_List.CheckedItems.Count; i++)
                {
                    string name = Clients_List.CheckedItems[i].ToString();
                    for (int j = 0; j < _clientsList.Count; j++)
                    {
                        if (_clientsList[j]._socket.Connected && _clientsList[j]._name.Equals("@" + name))
                        {
                            SendData(_clientsList[j]._socket, textMsg.Text);
                        }
                    }
                }
                textStatus.Items.Add("Server : " + textMsg.Text);
                textMsg.Text = string.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString()); ;
            }
        }
        private void SendData(Socket socket, string Messsage)
        {
            byte[] sendBuffer = Encoding.ASCII.GetBytes(Messsage);
            socket.BeginSend(sendBuffer, 0, sendBuffer.Length, SocketFlags.None, new AsyncCallback(SendCallback), socket);
        }
        private void SendCallback(IAsyncResult AR)
        {
            Socket socket = (Socket)AR.AsyncState;
            socket.EndSend(AR);
        }
        private void btnDisconnect_Click_1(object sender, EventArgs e)
        {
            try
            {
                //Take no of Disconnect clients 
                int noOfClients = Clients_List.CheckedItems.Count;
                string[] clientNames = new String[noOfClients];

                Clients_List.CheckedItems.CopyTo(clientNames, 0);
                for (int i = 0; i < noOfClients; i++)
                {
                    string name = clientNames[i];
                    for (int j = 0; j < _clientsList.Count; j++)
                    {
                        if (_clientsList[j]._socket.Connected && _clientsList[j]._name.Equals("@" + name))
                        {
                            _clientsList[j]._socket.Close();
                            if (!_clientsList[j]._socket.Connected)
                            {
                                Clients_List.Items.Remove(name);
                                connectionlabel.Text = _clientsList[j]._name + " Disconnected.........";
                                _clientsList.RemoveAt(j);
                            }
                        }
                    }
                    _serversocket.BeginAccept(new AsyncCallback(AcceptCallBack), null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString()); ;
            }
        }
    }
}
