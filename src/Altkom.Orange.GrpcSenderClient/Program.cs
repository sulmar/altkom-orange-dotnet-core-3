using Altkom.Orange.Fakers;
using Altkom.Orange.GrpcService.Protos;
using Grpc.Net.Client;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Altkom.Orange.GrpcSenderClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello gRPC Sender!");

            var customerFaker = new CustomerFaker();

            var customers = customerFaker.GenerateForever();
            var requests = customers.Select(c => new AddCustomerRequest { FirstName = c.FirstName, LastName = c.LastName, Email = c.Email, IsRemoved = c.IsRemoved });

            const string url = "https://localhost:5001";

            GrpcChannel channel = GrpcChannel.ForAddress(url);
            var client = new CustomerService.CustomerServiceClient(channel);

            foreach (var customer in requests)
            {
                var response = await client.AddCustomerAsync(customer);

                Console.WriteLine($"Sent {customer.FirstName} {customer.LastName} {response.IsConfirmed} {response.Id}");

                await Task.Delay(TimeSpan.FromSeconds(0.01));
            }

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();

        }
    }
}
