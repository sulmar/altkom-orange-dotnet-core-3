using Altkom.Orange.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Altkom.Orange.SignalRHub.Hubs
{
    public class CustomersHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        public async Task SendNewCustomer(Customer customer)
        {
            await Clients.All.SendAsync("YouHaveGotNewCustomer", customer);
        }
    }
}
