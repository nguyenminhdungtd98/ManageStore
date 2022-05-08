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
    public class UpdateModel : PageModel
    {
        [BindProperty(SupportsGet =true)]
        public int id { get; set; }
        public int productNumber { get; set; }
        public string productName { get; set; }

        public DateTime expireDate { get; set; }

        public string company { get; set; }

        public DateTime dateOfManufacture { get; set; }

        public string productType { get; set; }

        public int price { get; set; }
        public void OnGet()
        {
            if (ProductServices.checkExist(id) < 0)
            {
                SetAlert("Mã sản phẩm không tồn tại",3);
                Response.Redirect("/index");
            }
            else
            {
                Product product = ProductServices.findById(id);
                if (product.productNumber > 0)
                {
                    productNumber = product.productNumber;
                    productName = product.productName;
                    expireDate = product.expireDate;
                    company = product.company;
                    dateOfManufacture = product.dateOfManufacture;
                    productType = product.productType;
                    price = product.price;
                }
            }
        }
        public void OnPost()
        {

            Product product = new Product(productNumber, productName, expireDate, company, dateOfManufacture, productType, price);
                if (ProductServices.checkExistToUpdate(productNumber,id) > 0)
                {
                    SetAlert("Mã sản phẩm đã tồn tại", 3);
                }
                else
                {
                    ProductServices.update(product,id);
                    SetAlert("Đã chỉnh sửa thành công", 1);
                    System.Threading.Thread.Sleep(2000);
                    Response.Redirect("/index");
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
