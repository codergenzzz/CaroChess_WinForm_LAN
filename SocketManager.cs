using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;

namespace _241018_CaroChess_WinForm
{
    public class SocketManager
    {
        #region CLIENT
        Socket client;

        /// <summary>
        /// Thiết lập kết nối TCP đến server
        /// </summary>
        /// <returns>thành công/ thất bại</returns>
        public bool ConnectServer()
        {
            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(IP), PORT);
            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                client.Connect(ipep);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region SERVER
        Socket server;


        /// <summary>
        /// Tạo 1 server TCP, lắng nghe và chấp nhận kết nối từ client
        /// </summary>
        public void CreateServer()
        {
            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(IP), PORT);
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            server.Bind(ipep);
            server.Listen(10);  // 10: độ dài hàng đợi tối đa của các yêu cầu kết nối đang chờ xử lí

            Thread acceptClient = new Thread(() =>
            {
                client = server.Accept();
            });

            acceptClient.IsBackground = true;   // thread tự động kết thúc khi ứng dụng kết thúc mà ko cần phải dừng thủ công
            acceptClient.Start();
        }
        #endregion

        #region BOTH
        public string IP = "127.0.0.1";
        public int PORT = 8888;
        public const int BUFFER = 1024;
        public bool IsServer = true;

        public bool Send(object obj)
        {
            byte[] sendData = SerializeData(obj);

            return SendData(client, sendData);
        }

        public object Receive()
        {
            byte[] receiveData = new byte[BUFFER];
            bool isOk = ReceiveData(client, receiveData);

            if (isOk)
            {
                return DeserializeData(receiveData);
            }
            else
            {
                return "ERROR: Cann't not receidata";
            }
        }

        private bool SendData(Socket target, byte[] data)
        {
            return target.Send(data) > 0 ? true : false;
        }

        private bool ReceiveData(Socket target, byte[] data)
        {
            return target.Receive(data) > 0 ? true : false;
        }


        /// <summary>
        /// Tuần tự hóa (serialize) một đối tượng bất kỳ thành một mảng byte (byte[])
        /// </summary>
        /// <param name="o">Đối tượng ban đầu</param>
        /// <returns>mảng byte[] đại diện cho đối tượng</returns>
        public byte[] SerializeData(Object o)
        {
            MemoryStream ms = new MemoryStream();
#pragma warning disable SYSLIB0011

            BinaryFormatter bf1 = new BinaryFormatter();
            bf1.Serialize(ms, o);
            return ms.ToArray();
        }

        /* Thay thế:
         * 
         public byte[] SerializeData(object o)
         {
             return JsonSerializer.SerializeToUtf8Bytes(o);
         }
         */


        /// <summary>
        ///  Chuyển đổi một mảng byte (byte[]) đã được tuần tự hóa (serialized) 
        ///  thành đối tượng ban đầu của nó (deserialize)
        /// </summary>
        /// <param name="bytes">mảng dữ liệu</param>
        /// <returns>Đối tượng dữ liệu ban đầu</returns>
        public object DeserializeData(byte[] bytes)
        {
            // Đọc dữ liệu từ mảng bytes

            MemoryStream ms = new MemoryStream(bytes);

            // Giải tuần tự hóa dòng dữ liệu nhị phân thì MemoryStream
            // BinaryFormatter: lớp này dùng để tuần tự hóa và giải tuần tự hóa
            // đối tượng dưới dạng binary

#pragma warning disable SYSLIB0011
            BinaryFormatter bf = new BinaryFormatter();
            ms.Position = 0;
            return bf.Deserialize(ms);
        }

        /* Thay thế:
         * 
         public object DeserializeData(byte[] bytes)
         {
             return JsonSerializer.Deserialize<object>(bytes);
         }
         */


        /// <summary>
        /// Tìm và trả về địa chỉ IPv4 cục bộ của máy tính
        /// </summary>
        /// <param name="type">Loại giao diện mạng: Ethernet, Wireless..</param>
        /// <returns>IPv4</returns>
        public string GetLocalIPv4(NetworkInterfaceType type)
        {
            string output = "";

            foreach (NetworkInterface item in NetworkInterface.GetAllNetworkInterfaces())
            {
                // Kiểm tra loại và trạng thái của giao diện mạng
                if (item.NetworkInterfaceType == type && item.OperationalStatus == OperationalStatus.Up)
                {
                    // Duyệt qua ALL địa chỉ IP của giao diện mạng hiện tại
                    foreach (UnicastIPAddressInformation ip in item.GetIPProperties().UnicastAddresses)
                    {
                        // Kiểm tra địa chỉ có thuộc họ AddressFamily.InterNetwork, tức IPv4
                        if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        {
                            output = ip.Address.ToString();
                        }
                    }
                }
            }
            return output;
        }
        #endregion
    }
}
