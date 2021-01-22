using Altkom.Orange.GrpcService.Protos;
using Altkom.Orange.IServices;
using Altkom.Orange.Models;
using Bogus;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Altkom.Orange.GrpcService.Services
{
    public class OrangeCustomerService : CustomerService.CustomerServiceBase
    {
        private readonly ILogger<OrangeCustomerService> logger;
        private readonly ICustomerService customerService;

        public OrangeCustomerService(ILogger<OrangeCustomerService> logger, ICustomerService customerService)
        {
            this.logger = logger;
            this.customerService = customerService;
        }

        public override async Task<AddCustomerResponse> AddCustomer(AddCustomerRequest request, ServerCallContext context)
        {
            logger.LogInformation($"{request.FirstName} {request.LastName} {request.Email}");

            Customer customer = new Models.Customer { FirstName = request.FirstName, LastName = request.LastName };


            customerService.Add(customer);

            var response = new AddCustomerResponse { IsConfirmed = true, Id = customer.Id };

            await Task.Delay(TimeSpan.FromSeconds(5));

            return response;
        }

        public override async Task YouHaveGotNewCustomer(YouHaveGotNewCustomerRequest request, IServerStreamWriter<YouHaveGotNewCustomerResponse> responseStream, ServerCallContext context)
        {
            var responses = new Faker<YouHaveGotNewCustomerResponse>()
                 .RuleFor(p => p.FirstName, f => f.Person.FirstName)
                 .RuleFor(p => p.LastName, f => f.Person.LastName)
                 .RuleFor(p => p.Email, f => f.Person.Email)
                 .RuleFor(p => p.GroupName, f => f.PickRandom(new string[] { "A", "B", "C" }))
                .GenerateForever();

            foreach (var response in responses.Where(r=>r.GroupName == request.GroupName))
            {
                await responseStream.WriteAsync(response);

                await Task.Delay(TimeSpan.FromSeconds(0.01));
            }
        }

    }
}
