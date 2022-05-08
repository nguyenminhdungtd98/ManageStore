using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using _21880024.Entities;
using _21880024.Services;

namespace _21880024.Pages
{
    [BindProperties]
    public class BillModel : PageModel
    {
        public List<Bill> bills { get; set; }

        public string typeSearch { get; set; }

        public string key { get; set; }
        [BindProperty(SupportsGet =true)]
        public int id { get; set; }
        public void OnGet()
        {
            if (id != 0) {
                if (BillServices.delete(id) == Error.SUCCESS)
                {
                    SetAlert(ErrorMessage.SUCCESS, Error.SUCCESS);
                }
            }

            bills = BillServices.findAll();
        }
        public void OnPost()
        {
            if (key != null)
            {
                bills = BillServices.search(typeSearch, key.Trim());
            }
        }
        protected void SetAlert(string message, int type)
        {
            TempData["AlertMessage"] = message;
            if (type == -2)
            {
                TempData["AlertType"] = "alert-success";

            }
            else if (type == 2)
            {
                TempData["AlertType"] = "alert-warning";
            }
            else if (type == -3)
            {
                TempData["AlertType"] = "alert-danger";
            }
            else
            {
                TempData["AlertType"] = "alert-info";
            }
        }
    }
}
