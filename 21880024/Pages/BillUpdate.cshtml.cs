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
    public class BillUpdateModel : PageModel
    {
        public int numberBill { get; set; }
        public DateTime createDate { get; set; }
        public int productNumber { get; set; }
        public string productName { get; set; }

        public DateTime expireDate { get; set; }

        public int number { get; set; }
        public string productType { get; set; }
        public List<ProductType> productTypes { get; set; }
        public List<Product> products { get; set; }
        [BindProperty(SupportsGet = true)]
        public int error { get; set; }

        [BindProperty(SupportsGet = true)]
        public int idUpdate { get; set; }
        public void OnGet()
        {
            try
            {
                if (error == Error.ZERO)
                {
                    SetAlert(ErrorMessage.ZERO, Error.ERROR);
                }
                else if (error == Error.NULL_VALUE)
                {
                    SetAlert(ErrorMessage.NULL_VALUE, Error.ERROR);
                }
                else if (error == Error.NOT_FOUND)
                {
                    SetAlert(ErrorMessage.NOT_FOUND, Error.ERROR);
                }
                else if (error == Error.ERROR)
                {
                    SetAlert(ErrorMessage.ERROR, Error.ERROR);
                }
                productTypes = ProductTypeServices.findAll();
                products = ProductServices.findAll();

                Bill bill = BillServices.findById(idUpdate);
                numberBill = bill.numberBill;
                
                productName = bill.productName;
                productType = bill.productType;
                createDate = bill.createDate;
                number = bill.number;
            }
            catch (Exception e)
            {
                SetAlert(e.Message, Error.ERROR);
            }
        }
        public void OnPost()
        {
            try
            {
                productNumber = BillServices.findIdByName(productName);
                Bill bill = new Bill(numberBill,createDate,productNumber,productName,number,productType);
                int result  = BillServices.update(bill,numberBill);
                if(result == Error.ERROR)
                {
                    Response.Redirect("/Bill?error=" + Error.ERROR);
                }
                else
                {
                    Response.Redirect("/Bill?error=" + Error.SUCCESS);
                }

            }
            catch (Exception e)
            {
                Response.Redirect("/BillUpdate?error=" + Error.ERROR);
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
