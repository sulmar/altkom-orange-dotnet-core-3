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
    public class EditModel : PageModel
    {
        [BindProperty]
        public Customer Customer { get; set; }

        private readonly ICustomerService customerService;

        public EditModel(ICustomerService customerService)
        {
            this.customerService = customerService;
        }

        // [BindProperty(SupportsGet = true)]
        [TempData]
        public int Id { get; set; }

        //public void OnGet(int id)
        //{
        //    Customer = customerService.Get(id);
        //}

        public void OnGet()
        {
            // var customerId = TempData.Peek("CustomerId");

            Customer = customerService.Get(Id);
        }

        //public IActionResult OnPost(Customer customer)
        //{
        //    customerService.Update(customer);

        //    return RedirectToPage("./Index");
        //}

        public IActionResult OnPost()
        {
            customerService.Update(Customer);

            TempData.Remove("Id");

            return RedirectToPage("./Index");
        }
    }
}
