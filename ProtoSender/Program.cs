using EasyNetQ;
using EasyNetQ.Topology;
using Proto;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtoSender
{
    class Program
    {
        static void Main(string[] args)
        {

            var requestBuilder = GetJobConfigRequest.CreateBuilder();
            requestBuilder.SetJobId("Job:123");
            requestBuilder.SetUserId("test@test.com");
            var request = requestBuilder.Build();

            byte[] messageBytes;
            using (var memory = new MemoryStream())
            {
                request.WriteTo(memory);
                messageBytes = memory.ToArray();
            }

            var server = "10.51.3.43";
            var username = "evault";
            var password = "xxxxxx";
            var advancedBus = RabbitHutch.CreateBus(String.Format("host={0};username={1};password={2}", server, username, password)).Advanced;
            var toAgentQueue = advancedBus.QueueDeclare("jamesqueue", false, true, false, true);
            var exchange = advancedBus.ExchangeDeclare("evault.to", ExchangeType.Topic);
            var toAgentBinding = advancedBus.Bind(exchange, toAgentQueue, "jamesqueue");
            var myMessage = new Message<byte[]> (messageBytes);
            var sm = new SerializedMessage(new MessageProperties(), messageBytes);
            //var backupMessage = new Message<TextMessage>(myMessage);
            advancedBus.Publish(exchange, "jamesqueue", false, false, new MessageProperties(), messageBytes);
            advancedBus.BindingDelete(toAgentBinding);
            advancedBus.Dispose();
           
        }
    }
}
