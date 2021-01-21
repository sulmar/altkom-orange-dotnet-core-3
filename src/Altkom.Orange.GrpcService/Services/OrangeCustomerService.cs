using Altkom.Orange.GrpcService.Protos;
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

        public OrangeCustomerService(ILogger<OrangeCustomerService> logger)
        {
            this.logger = logger;
        }

        public override Task<AddCustomerResponse> AddCustomer(AddCustomerRequest request, ServerCallContext context)
        {
            logger.LogInformation($"{request.FirstName} {request.LastName} {request.Email}");

            var response = new AddCustomerResponse { IsConfirmed = true };

            return Task.FromResult(response);
        }

    }
}
