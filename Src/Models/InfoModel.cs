using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Web;

namespace Ex3.Models
{
    public class InfoModel
    {
        private static InfoModel s_instace = null;

        public static InfoModel Instance
        {
            get
            {
                if (s_instace == null)
                {
                    s_instace = new InfoModel();
                }
                return s_instace;
            }
        }

        private IPEndPoint iPEndPoint;
        private TcpClient tcpClient;

        private bool isConnected;

        public Coordinate Coordinate { get; set; }

        public InfoModel()
        {
            isConnected = false;

            Coordinate = new Coordinate();
        }

        public void Connect(string ip, int port)
        {
            
            if (isConnected)
            {
                Disconnect();
            }
            tcpClient = new TcpClient();

            iPEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
            tcpClient.Connect(iPEndPoint);

            isConnected = true;
            
        }

        public void Disconnect()
        {
            if (isConnected)
            {
                tcpClient.Close();
                isConnected = false;
            }
        }

        private double parseDoubleFromMessage(string s)
        {
            int i1 = s.IndexOf("'"),
                i2 = s.LastIndexOf("'");
            s = s.Substring(i1 + 1, i2 - i1 - 1);
            return double.Parse(s);
        }

        public Coordinate GetCoordinate()
        {
            if (isConnected)
            {
                using (NetworkStream stream = tcpClient.GetStream())
                using (System.IO.StreamReader reader = new System.IO.StreamReader(stream))
                using (System.IO.StreamWriter writer = new System.IO.StreamWriter(stream))
                {
                    
                    writer.Write("get /position/longitude-deg\r\n");
                    writer.Flush();
                    string lon = reader.ReadLine();

                    writer.Write("get /position/latitude-deg\r\n");
                    writer.Flush();
                    string lat = reader.ReadLine();

                    this.Coordinate.Longitude = parseDoubleFromMessage(lon);
                    this.Coordinate.Latitude = parseDoubleFromMessage(lat);
                    
                }
            }
            else
            {
                this.Coordinate.Longitude = 0.0;
                this.Coordinate.Latitude = 0.0;
            }

            return this.Coordinate;
        }

        public Coordinate GetRudderThrottle()
        {
            if (isConnected)
            {
                using (NetworkStream stream = tcpClient.GetStream())
                using (System.IO.StreamReader reader = new System.IO.StreamReader(stream))
                using (System.IO.StreamWriter writer = new System.IO.StreamWriter(stream))
                {

                    writer.Write("get /controls/flight/rudder\r\n");
                    writer.Flush();
                    string lon = reader.ReadLine();

                    writer.Write("get /controls/engines/current-engine/throttle\r\n");
                    writer.Flush();
                    string lat = reader.ReadLine();

                    this.Coordinate.Longitude = parseDoubleFromMessage(lon);
                    this.Coordinate.Latitude = parseDoubleFromMessage(lat);

                }
            }
            else
            {
                this.Coordinate.Longitude = 0.0;
                this.Coordinate.Latitude = 0.0;
            }

            return this.Coordinate;
        }
    }
}