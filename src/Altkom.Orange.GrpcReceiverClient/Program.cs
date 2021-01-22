using Altkom.Orange.GrpcService.Protos;
using Grpc.Core;
using Grpc.Net.Client;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Altkom.Orange.GrpcReceiverClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello gRPC Receiver!");

            const string url = "https://localhost:5001";

            GrpcChannel channel = GrpcChannel.ForAddress(url);
            var client = new CustomerService.CustomerServiceClient(channel);

            YouHaveGotNewCustomerRequest request = new YouHaveGotNewCustomerRequest { GroupName = "B" };

            var call = client.YouHaveGotNewCustomer(request);

            // C# 7.xx

            CancellationTokenSource cts = new CancellationTokenSource();

            //while(await call.ResponseStream.MoveNext(cts.Token))
            //{
            //    var response = call.ResponseStream.Current;

            //    Console.WriteLine($"{response.FirstName} {response.LastName} {response.Email}");
            //}

            // C# 8.0
            await foreach(var response in call.ResponseStream.ReadAllAsync())
            {
                Console.WriteLine($"{response.FirstName} {response.LastName} {response.Email} {response.GroupName}");
            }

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
