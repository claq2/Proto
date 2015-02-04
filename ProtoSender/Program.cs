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

            using (var file = new FileStream("..\\..\\..\\request.bin", FileMode.Create))
            {
                request.WriteTo(file);
            }
        }
    }
}
