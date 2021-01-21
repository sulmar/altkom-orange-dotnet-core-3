using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Altkom.Orange.IServices;
using Altkom.Orange.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Altkom.Orange.RazorPages.Pages.Customers
{
    public class InfoModel : PageModel
    {
        [BindProperty]
        public Customer Customer { get; set; }

        private readonly ICustomerService customerService;

        public InfoModel(ICustomerService customerService)
        {
            this.customerService = customerService;
        }

        //[BindProperty(SupportsGet = true)]
        //public int Id { get; set; }

        //public void OnGet(int id)
        //{
        //    Customer = customerService.Get(id);
        //}

        public void OnGet(int id)
        {
            Customer = customerService.Get(id);
        }

        public IActionResult OnPost()
        {
            customerService.Update(Customer);

            return Page();
        }
    }
}
