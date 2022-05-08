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
    public class AddProductModel : PageModel
    {
        public int productNumber { get; set; }
        public string productName { get; set; }

        public DateTime expireDate { get; set; }

        public string company { get; set; }

        public DateTime dateOfManufacture { get; set; }

        public string productType { get; set; }

        public int price { get; set; }

        public List<ProductType> productTypes { get; set; }
        [BindProperty(SupportsGet =true)]
        public int error { get; set; }
        public void OnGet()
        {
            productTypes = ProductTypeServices.findAll();
            if (error.Equals(Error.DUPLICATE))
            {
                SetAlert(ErrorMessage.DUPLICATE, 3);
            } else if (error.Equals(Error.ZERO))
            {
                SetAlert(ErrorMessage.ZERO, 3);
            }
        }

        public void OnPost()
        {
            try
            {
                Product product = new Product(productNumber, productName, expireDate, company, dateOfManufacture, productType, price);
                int result = ProductServices.add(product);
                if (result == Error.DUPLICATE)
                {
                    Response.Redirect("/AddProduct?error=" + Error.DUPLICATE);
                } else if (result == Error.ZERO)
                {
                    Response.Redirect("/AddProduct?error=" + Error.ZERO);
                }
                else
                {
                    Response.Redirect("/index");
                }

            }
            catch (Exception e)
            {
                SetAlert(e.Message, Error.ERROR);
            }
        }
        protected void SetAlert(string message, int type)
        {
            TempData["AlertMessage"] = message;
            if (type == 1)
            {
                TempData["AlertType"] = "alert-success";

            }
            else if (type == 2)
            {
                TempData["AlertType"] = "alert-warning";
            }
            else if (type == 3)
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
