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
    public class BillOutModel : PageModel
    {
        public int numberBillOut { get; set; }
        public DateTime createDate { get; set; }

        public int productNumber { get; set; }
        public string productName { get; set; }

        public int price { get; set; }
        public int number { get; set; }

        public int total { get; set; }
        public List<ProductType> productTypes { get; set; }
        public List<Product> products { get; set; }

        public List<ProductInBill> productInBills { get; set; }
        public List<BillOut> billOuts { get; set; }
        [BindProperty(SupportsGet = true)]
        public int error { get; set; }
        [BindProperty(SupportsGet = true)]
        public int idDeleteP { get; set; }

        public string save { get; set; }
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
            else if (error == Error.OUTSTOCK)
            {
                SetAlert(ErrorMessage.OUTSTOCK, Error.ERROR);
            }
                productTypes = ProductTypeServices.findAll();
            products = ProductServices.findAll();
            numberBillOut = BillOutServices.getMaxId() + 1;
            createDate = DateTime.Now;
            productInBills = BillOutServices.findAllProductInBill();
            if (idDeleteP > 0)
            {
                if (BillOutServices.deleteProductInBill(idDeleteP))
                {
                    Response.Redirect("/BillOut");
                }

            }
        }
        public void OnPost()
        {
            try
            {
                if (save == null)
                {
                    int id = BillOutServices.findIdByName(productName);
                    productNumber = id;
                    Product product = BillOutServices.findProductById(id);
                    numberBillOut = BillOutServices.getMaxId();

                    //BillOut billOut = new BillOut();
                    //billOut.numberBillOut = numberBillOut + 1;
                    //billOut.createDate = DateTime.Now;
                    //billOuts = new List<BillOut>();
                    //billOuts.Add(billOut);

                    ProductInBill productInBill = new ProductInBill();
                    productInBill.productNumber = productNumber;
                    productInBill.productName = productName;
                    productInBill.number = number;
                    productInBill.price = product.price;
                    productInBill.total = product.price * number;
                    if (!BillOutServices.checkNumberProduct(productInBill))
                    {
                        Response.Redirect("/BillOut?error=" + Error.OUTSTOCK);
                    }
                    else
                    {
                        int result = BillOutServices.addProductInBill(productInBill);
                        if (result == Error.NULL_VALUE)
                        {
                            Response.Redirect("/BillOut?error=" + Error.NULL_VALUE);
                        }
                        else if (result == Error.ZERO)
                        {
                            Response.Redirect("/BillOut?error=" + Error.ZERO);
                        }
                        else if (result == Error.ERROR)
                        {
                            Response.Redirect("/BillOut?error=" + Error.ERROR);
                        }
                        else if (result.Equals(Error.SUCCESS))
                        {
                            Response.Redirect("/BillOut");
                        }
                    }
                }
                else
                {
                    productInBills = BillOutServices.findAllProductInBill();
                    BillOut billOut = new BillOut();
                    billOut.numberBillOut = numberBillOut;
                    billOut.productInBill = productInBills;
                    billOut.createDate = createDate;
                    BillOutServices.add(billOut);
                    Response.Redirect("/BillOut?error="+ Error.SUCCESS);

                }
                


            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                Response.Redirect("/BillOut?error=" + Error.ERROR);
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
