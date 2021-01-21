using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Altkom.Orange.IServices;
using Altkom.Orange.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Altkom.Orange.RazorPages.Pages.Customers
{
    public class IndexModel : PageModel
    {
        private readonly ICustomerService customerService;

        public IEnumerable<Customer> Customers { get; set; }

        [ActivatorUtilitiesConstructor]
        public IndexModel(ICustomerService customerService, IConfiguration configuration)
        {
            int quantity = int.Parse(configuration["CustomerOptions:Quantity"]);

            this.customerService = customerService;
        }

        public IndexModel()
        {

        }

        public void OnGet()
        {
            Customers = customerService.Get();
        }


        public IActionResult OnPostEdit(int id)
        {
            TempData["Id"] = id;
            TempData.Keep("Id");

            return RedirectToPage("/Customers/Edit");
        }

        public IActionResult OnGetEdit(int id)
        {
            TempData["Id"] = id;
            TempData.Keep("Id");

            return RedirectToPage("/Customers/Edit");
        }
    }
}
