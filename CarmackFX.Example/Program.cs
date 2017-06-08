using CarmackFX.Client.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarmackFX.Example
{
    class Program
    {
        static void test_v2(string host, UInt16 port)
        {
            var wait_response = true;

            UdpSocket client = null;

            // 创建一个实例
            client = new UdpSocket((byte[] buf) =>
            {
                wait_response = false;
                Console.WriteLine("recv message: {0}", System.Text.ASCIIEncoding.ASCII.GetString(buf));
            });

            // 绑定端口
            client.Connect(host, port);

            byte[] content = System.Text.UTF8Encoding.UTF8.GetBytes("what's up");

            List<byte> data = new List<byte>();
            data.AddRange(BitConverter.GetBytes(content.Length + 13).Reverse());
            data.AddRange(BitConverter.GetBytes(DateTime.Now.Ticks).Reverse());
            data.Add(0);
            data.AddRange(content);

            // 发送消息
            client.Send(data.ToArray());

            // update.
            while (wait_response)
            {
                client.Update();
                System.Threading.Thread.Sleep(10);
            }
        }

        static void Main(string[] args)
        {

            test_v2("127.0.0.1", 18000);
        }
    }
}