using Altkom.Orange.Fakers;
using Altkom.Orange.Models;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Altkom.Orange.SignalRSenderClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("Hello Sender Signal-R!");

            const string url = "http://localhost:5000/signalr/customers";

            // dotnet add package Microsoft.AspNetCore.SignalR.Client
            HubConnection connection = new HubConnectionBuilder()
                .WithUrl(url)
                .WithAutomaticReconnect(new[] { TimeSpan.Zero, TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(30) })
                .Build();


            // js
            // @microsoft/signalr

            connection.Closed += error =>
            {
                if (connection.State == HubConnectionState.Disconnected)
                {
                    Console.WriteLine("Utracono polaczenie");
                }

                return Task.CompletedTask;
            };

            connection.Reconnecting += error =>
            {
                if (connection.State == HubConnectionState.Reconnecting)
                {
                    Console.WriteLine("Reconnecting...");
                }

                return Task.CompletedTask;
            };

            Console.WriteLine("Connecting...");
            await connection.StartAsync();
            Console.WriteLine("Connected.");

            CustomerFaker customerFaker = new CustomerFaker();

            IEnumerable<Customer> customers = customerFaker.GenerateForever();  // yield

            foreach (Customer customer in customers)
            {
                Console.WriteLine($"Send {customer.FirstName} {customer.LastName}");
                await connection.SendAsync("SendNewCustomer", customer);
                Console.WriteLine($"Sent.");

                await Task.Delay(TimeSpan.FromSeconds(0.1));
            }

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();

            Console.ResetColor();
        }
    }
}
