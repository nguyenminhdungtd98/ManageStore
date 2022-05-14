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
    public class BillOutManageModel : PageModel
    {
        public int numberBillOut { get; set; }
        public DateTime createDate { get; set; }

        public List<ProductInBill> productInBill;

        public List<BillOut> billOuts { get; set; }

        public string typeSearch { get; set; }

        public string key { get; set; }

        [BindProperty(SupportsGet =true)]
        public int id { get; set; }

        [BindProperty(SupportsGet = true)]
        public int error { get; set; }

        public void OnGet()
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
            }else if (error == Error.SUCCESS)
            {
                SetAlert(ErrorMessage.SUCCESS, Error.SUCCESS);
            }
            billOuts = BillOutServices.findAll();
            BillOutServices.deleteAllProductInBill();
            if (id > 0)
            {
                BillOutServices.delete(id);
                Response.Redirect("/BillOutManage?error=" + Error.SUCCESS);
            }
        }
        public void OnPost()
        {
            if (key != null)
            {
                billOuts = BillOutServices.search(typeSearch, key.Trim());
                
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
