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

namespace Client
{
    public partial class Client : Form
    {
        Socket _clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        byte[] recivedBuffer = new byte[1024];
        public Client()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        private void Client_Load(object sender, EventArgs e)
        {
        }
        private void btnConnect_Click_1(object sender, EventArgs e)
        {
            try
            {
                lblClientmsg.Text = "Please wait Connectiong to the server................";
                _clientSocket.Connect(IPAddress.Parse("127.0.0.1"), 9686);
                lblClientmsg.Text = ("Connected!");
                _clientSocket.BeginReceive(recivedBuffer, 0, recivedBuffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallBack), _clientSocket);
                byte[] buffer = Encoding.ASCII.GetBytes("@@" + textName.Text);
                _clientSocket.Send(buffer);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        private void ReceiveCallBack(IAsyncResult aResult)
        {
            try
            {
                Socket socket = (Socket)aResult.AsyncState;
                int received = socket.EndReceive(aResult);
                byte[] dataBuf = new byte[received];
                Array.Copy(recivedBuffer, dataBuf, received);
                string message = Encoding.ASCII.GetString(dataBuf);
                if (message.StartsWith("@@")) 
                {
                    int count = frdCheckList.Items.Count;
                    if (frdCheckList.Items.Count == 0)
                    {
                        frdCheckList.Items.Add(message);
                    }
                    for (int i = 0; i <count; i++)
                    {
                        if (!frdCheckList.Items[i].ToString().Equals(message))
                        {
                            frdCheckList.Items.Add(message);
                        } 
                    }
                }
                else if (message.StartsWith("-->")) 
                {
                    string[] msgName= message.Split('_');
                    Textstatus.Items.Add(msgName[0]+" : "+ msgName[1]);
                    Textstatus.Items.Add(Environment.NewLine);
                }
                else 
                {
                    Textstatus.Items.Add("Server : " + message);
                    Textstatus.Items.Add(Environment.NewLine);
                }
                _clientSocket.BeginReceive(recivedBuffer, 0, recivedBuffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallBack), _clientSocket);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString()); ;
            }
        }
        private void btnSend_Click_1(object sender, EventArgs e)
        {
            try
            {
                for(int i = 0; i < frdCheckList.CheckedItems.Count; i++) 
                {
                    string name = frdCheckList.CheckedItems[i].ToString();
                    byte[] sendMsg = Encoding.ASCII.GetBytes("-->"+name+"_"+textMsg.Text);
                    _clientSocket.Send(sendMsg);
                }
                Textstatus.Items.Add("Me : " + textMsg.Text);
                Textstatus.Items.Add(Environment.NewLine);
                textMsg.Text = string.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString()); ;
            }
        }
    }
}
