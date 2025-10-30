using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MM_Services
{
    internal class TcpServer
    {
        private TcpListener listener;
        private List<TcpClient> clients = new List<TcpClient>(); // Keeps track of connected clients

        public void Start(int port)
        {
            listener = new TcpListener(IPAddress.Any, port);
            listener.Start();
            FileHelper.WriteLog("Služba spuštěna, čeká na připojení");

            Thread acceptThread = new Thread(AcceptClients);
            acceptThread.Start();
        }

        private void AcceptClients()
        {
            while (true)
            {
                try
                {
                    TcpClient client = listener.AcceptTcpClient();
                    clients.Add(client);
                    FileHelper.WriteLog("CLient připojen");

                    Thread clientThread = new Thread(() => HandleClient(client));
                    clientThread.Start();
                }
                catch (Exception ex)
                {
                    FileHelper.WriteLog("Chyba při přijímání klienta: " + ex.Message);
                }
            }
        }

        private void HandleClient(TcpClient client)
        {
            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[1024];
            int bytesRead;

            try
            {
                while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    FileHelper.WriteLog("Odpoveď od klienta: " + message);

                    // Echo message back to the client
                    byte[] response = Encoding.UTF8.GetBytes("Echo: " + message);
                    stream.Write(response, 0, response.Length);
                }
            }
            catch (Exception ex)
            {
                FileHelper.WriteLog("Chyba komunikace s klientem: " + ex.Message);
            }
            finally
            {
                stream.Close();
                client.Close();
                clients.Remove(client);
                FileHelper.WriteLog("Klient odpojený...");
            }
        }
    }
}
