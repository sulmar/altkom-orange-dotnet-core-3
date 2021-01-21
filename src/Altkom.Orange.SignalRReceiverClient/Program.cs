using Altkom.Orange.IServices;
using Altkom.Orange.Models;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace Altkom.Orange.SignalRReceiverClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("Hello Receiver Signal-R!");

            Console.Write("Type group: ");

            string groupName = Console.ReadLine();

            const string url = "http://localhost:5000/signalr/customers";

            // dotnet add package Microsoft.AspNetCore.SignalR.Client
            HubConnection connection = new HubConnectionBuilder()
                 .WithUrl(url, options =>
                 {
                     options.Headers["Group"] = groupName;
                 })
                .Build();

            //HubConnection<T>
            //    connection.OnYouHaveGotNewCustomer(customer => Console.WriteLine($"Received {customer.FirstName} {customer.LastName}"));

            //connection.On<Customer>("YouHaveGotNewCustomer",
            //    customer => Console.WriteLine($"Received {customer.FirstName} {customer.LastName}"));

            connection.On<Customer>(nameof(ICustomerClient.YouHaveGotNewCustomer),
                customer => Console.WriteLine($"Received {customer.FirstName} {customer.LastName}"));

            //connection.On("Pong", 
            //    () => Console.WriteLine("Pong"));

            connection.On(nameof(ICustomerClient.Pong),
              () => Console.WriteLine("Pong"));

            Console.WriteLine("Connecting...");
            await connection.StartAsync();
            Console.WriteLine("Connected.");

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();

            Console.ResetColor();
        }
    }
}
