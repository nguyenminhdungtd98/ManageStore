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
    public class BillInModel : PageModel
    {
        public int numberBill { get; set; }
        public DateTime createDate { get; set; }
        public int productNumber { get; set; }
        public string productName { get; set; }

        public DateTime expireDate { get; set; }

        public int number { get; set; }
        public List<ProductType> productTypes { get; set; }
        public List<Product> products { get; set; }
        [BindProperty(SupportsGet =true)]
        public int error { get; set; }

        public void OnGet()
        {
            try
            {
                if(error == Error.ZERO)
                {
                    SetAlert(ErrorMessage.ZERO, Error.ERROR);
                }else if (error == Error.NULL_VALUE)
                {
                    SetAlert(ErrorMessage.NULL_VALUE, Error.ERROR);
                }
                else if (error == Error.NOT_FOUND)
                {
                    SetAlert(ErrorMessage.NOT_FOUND, Error.ERROR);
                }
                productTypes = ProductTypeServices.findAll();
                products = ProductServices.findAll();
                numberBill = BillServices.getMaxId() + 1;
                createDate = DateTime.Now;
            }
            catch (Exception e)
            {
                SetAlert(e.Message,Error.ERROR);
            }
        }
        public void OnPost()
        {
            try
            {

                int id = BillServices.findIdByName(productName);
                Product productTypeTemp = BillServices.findProductById(id);

                if (id == Error.NOT_FOUND)
                {
                    Response.Redirect("/BillIn?error=" + Error.NOT_FOUND);
                } else if (id == Error.NULL_VALUE)
                {
                    Response.Redirect("/BillIn?error=" + Error.NULL_VALUE);
                }
                numberBill = BillServices.getMaxId() + 1;
                //add bill in
                Bill bill = new Bill();
                bill.createDate = DateTime.Now;
                bill.productNumber = id;
                bill.productName = productName;
                bill.number = number;
                bill.productType = productTypeTemp.productType;
                bill.numberBill = numberBill;
                int result = BillServices.add(bill);
                if (result == Error.ZERO)
                {
                    Response.Redirect("/BillIn?error=" + Error.ZERO);
                }

                // add item to warehouse
                Warehouse warehouse = new Warehouse();
                warehouse.productNumber = id;
                warehouse.productName = productName;
                warehouse.number = number;
                warehouse.expireDate = BillServices.findExpireDateById(id);
                warehouse.productType = productTypeTemp.productType;
                int result1 = WarehouseServices.add(warehouse);
                if (result1 == Error.ZERO)
                {
                    Response.Redirect("/BillIn?error=" + Error.ZERO);
                }
                Response.Redirect("/Bill?error=" + Error.SUCCESS);
            }
            catch (Exception e)
            {
                SetAlert(e.Message, Error.ERROR);
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
