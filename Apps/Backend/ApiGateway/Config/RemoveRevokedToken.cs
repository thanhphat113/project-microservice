using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Quartz;

namespace ApiGateway.Config
{
    public class RemoveRevokedOrExpiredToken : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            using var client = new HttpClient();
            var response = await client.GetAsync("https://localhost:7000/Account/cleanup-token");
            Console.WriteLine($"API gọi lúc {DateTime.Now}: {response.StatusCode}");
        }
    }
}