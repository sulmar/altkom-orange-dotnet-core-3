using Altkom.Orange.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Altkom.Orange.IServices
{
    public interface IAuthorizationService
    {
        bool TryAuthorize(string username, string password, out Customer customer);
    }
}
