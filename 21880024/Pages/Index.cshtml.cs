using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _21880024.Services;
using _21880024.Entities;

namespace _21880024.Pages
{
    [BindProperties]
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public List<Product> products { get; set; }

        [BindProperty(SupportsGet =true)]
        public int id { get; set; }

        public int productNumber { get; set; }
        public string productName { get; set; }

        public DateTime expireDate { get; set; }

        public string company { get; set; }

        public DateTime dateOfManufacture { get; set; }

        public string productType { get; set; }

        public int price { get; set; }

        public string typeSearch { get; set; }

        public string key { get; set; }

        [BindProperty(SupportsGet = true)]
        public int error { get; set; }
        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            
            if (error.Equals(Error.DUPLICATE))
            {
                SetAlert(ErrorMessage.DUPLICATE, Error.ERROR);
            }
            else if (error.Equals(Error.ZERO))
            {
                SetAlert(ErrorMessage.ZERO, Error.ERROR);
            } else if (error.Equals(Error.SUCCESS))
            {
                SetAlert(ErrorMessage.SUCCESS, Error.SUCCESS);
            } else if (error.Equals(Error.EMPTY_PRODUCTTYPE))
            {
                SetAlert(ErrorMessage.EMPTY_PRODUCTTYPE, Error.ERROR);
            }
            products = new List<Product>();
            products = ProductServices.findAll();
            if (id > 0)
            {
                ProductServices.delete(id);
            }

        }
        public void OnPost()
        {
            products = ProductServices.search(typeSearch,key);
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
