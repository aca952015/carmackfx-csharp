using CarmackFX.Client.Core;
using System;
using System.Collections.Generic;
using System.Linq;

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
        // 测试v1版本，有握手过程，服务器决定conv的分配
        //test_v1("192.168.1.2", 4444);

        //Console.WriteLine("**********************************************************");

       // 测试v2版本，没有握手过程，客户端自行决定conv的分配
       // 适合配合 kcp-go
        test_v2("127.0.0.1", 18000);
    }
}

