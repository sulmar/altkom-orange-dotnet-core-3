using Altkom.Orange.IServices;
using Altkom.Orange.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Altkom.Orange.SignalRHub.Hubs
{
   // [Authorize]
    public class StrongTypedCustomersHub : Hub<ICustomerClient>
    {
        public override async Task OnConnectedAsync()
        {
            HttpContext httpContext = Context.GetHttpContext();

            string groupName = httpContext.Request.Headers["Group"].ToString();

            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

            // Context.User.Identity.Name

            //var groups = Context.User.FindAll(c => c.Type == ClaimTypes.GroupSid);

            //foreach (var group in groups)
            //{
            //    await Groups.AddToGroupAsync(Context.ConnectionId, group.Value);
            //}
        }

        public async Task SendNewCustomer(Customer customer)
        {
            // await Clients.Others.YouHaveGotNewCustomer(customer);

            await Clients.Group("A").YouHaveGotNewCustomer(customer);
        }

        public async Task Ping()
        {
            await Clients.Caller.Pong();
        }
    }
}
