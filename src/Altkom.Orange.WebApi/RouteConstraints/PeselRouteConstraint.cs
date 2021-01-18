using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Altkom.Orange.WebApi.RouteConstraints
{
    public interface IPeselValidator
    {
        bool Validate(string pesel);
    }

    public class MyPeselValidator : IPeselValidator
    {
        public bool Validate(string pesel)
        {
            return pesel.Length == 11;
        }
    }

    public class PeselRouteConstraint : IRouteConstraint
    {
        private readonly IPeselValidator peselValidator;

        public PeselRouteConstraint(IPeselValidator peselValidator)
        {
            this.peselValidator = peselValidator;
        }

        public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            if (values.TryGetValue(routeKey, out object routeValue))
            {
                string number = routeValue.ToString();

                return peselValidator.Validate(number);
            }

            return false;
        }
    }
}
