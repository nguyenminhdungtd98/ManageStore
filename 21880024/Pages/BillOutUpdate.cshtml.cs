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
    public class BillOutUpdateModel : PageModel
    {
        [BindProperty]
        public int numberBillOut { get; set; }
        [BindProperty]
        public DateTime createDate { get; set; }
        [BindProperty]
        public int productNumber { get; set; }
        [BindProperty]
        public string productName { get; set; }
        [BindProperty]
        public int price { get; set; }
        [BindProperty]
        public int number { get; set; }
        [BindProperty]
        public int total { get; set; }
        [BindProperty]
        public List<ProductType> productTypes { get; set; }
        [BindProperty]
        public List<Product> products { get; set; }
        [BindProperty]
        public List<ProductInBill> productInBills { get; set; }
        [BindProperty(SupportsGet = true)]
        public int error { get; set; }
        [BindProperty(SupportsGet = true)]
        public int idDeleteP { get; set; }
        [BindProperty]
        public string edit { get; set; }
        [BindProperty(SupportsGet = true)]
        public int idUpdate { get; set; }
        [BindProperty]
        public List<Warehouse> productsInStock { get; set; }

        public BillOut billOut { get; set; }
        public BillOut billOutOld = new BillOut();
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
                else if (error == Error.OUTSTOCK)
                {
                    SetAlert(ErrorMessage.OUTSTOCK, Error.ERROR);
                }
                List<BillOut> billOutTemps = new List<BillOut>();
                billOutTemps = BillOutServices.loadBillOutTemp();
                productsInStock = WarehouseServices.findAll();
                // if bill out temp  = null then Get bill out and save to txt file
                if (billOut.numberBillOut == 0)
                {
                    billOut = BillOutServices.findById(idUpdate);
                    
                    if (billOutTemps == null || billOutTemps.Count == 0)
                    {
                        billOutTemps = new List<BillOut>();
                        billOutTemps.Add(billOut);
                        BillOutServices.saveBillOutTemp(billOutTemps);
                    }
                }

                 
                if (!billOut.Equals(null) && BillOutServices.findAllProductInBill() != null && BillOutServices.findAllProductInBill().Count == 0)
                {
                    BillOutServices.addProductInBills(billOut.productInBill);
                }

                if (productInBills == null)
                {
                    productInBills = BillOutServices.findAllProductInBill();
                }
                numberBillOut = billOutTemps[0].numberBillOut;
                createDate = billOutTemps[0].createDate;

                products = ProductServices.findAll();

                if (idDeleteP > 0)
                {
                    if (BillOutServices.deleteProductInBill(idDeleteP))
                    {
                        Response.Redirect("/BillOutUpdate");
                    }

                }
            }
            catch (Exception e)
            {
                SetAlert(e.Message, Error.ERROR);
            }

        }
        public void OnPost()
        {
            List<BillOut> billOutTemps = BillOutServices.loadBillOutTemp();
            //get bill out temp

            if (number == 0 && edit == null)
            {
                Response.Redirect("/BillOutUpdate?error=" + Error.ZERO + "&idUpdate=" + billOut.numberBillOut);
            }
            else
            {
                if (edit == null)
                {
                    if (BillOutServices.checkExistProductToEdit(productName, billOutTemps[0].productInBill))
                    {
                        List<ProductInBill> productInBillsTemp = BillOutServices.findAllProductInBill();
                        productInBills = BillOutServices.updateProductToEdit(productName, number, productInBillsTemp);
                        BillOutServices.deleteAllProductInBill();
                        BillOutServices.addProductInBills(productInBills);
                        //BillOutServices.updateWareHouseOutToEdit(billOut, billOutOld);
                        Response.Redirect("/BillOutUpdate?idUpdate=" + billOut.numberBillOut);
                    }
                    else
                    {
                        int id = BillOutServices.findIdByName(productName);
                        productNumber = id;
                        Product product = BillOutServices.findProductById(id);

                        ProductInBill productInBill = new ProductInBill();
                        productInBill.productNumber = product.productNumber;
                        productInBill.productName = productName;
                        productInBill.number = number;
                        productInBill.price = product.price;
                        productInBill.total = product.price * number;

                        productInBills = BillOutServices.findAllProductInBill();
                        productInBills.Add(productInBill);
                        BillOutServices.deleteAllProductInBill();
                        BillOutServices.addProductInBills(productInBills);
                        //BillOutServices.updateWareHouseOutToEdit(billOut);
                        Response.Redirect("/BillOutUpdate?idUpdate=" + billOut.numberBillOut);
                    }
                }
                else
                {
                    billOut = billOutTemps[0];
                    productInBills = BillOutServices.findAllProductInBill();
                    BillOut billOutNew = new BillOut();
                    billOutNew.productInBill = productInBills;
                    billOutNew.numberBillOut = billOut.numberBillOut;
                    billOutNew.createDate = billOut.createDate;
                    BillOutServices.updateBillOut(billOutNew);
                    BillOutServices.updateWareHouseOutToEdit(billOutNew,billOut);
                    List<BillOut> deleteBillOuts = new List<BillOut>();
                    BillOutServices.deleteAllProductInBill();
                    BillOutServices.saveBillOutTemp(deleteBillOuts);
                    Response.Redirect("/BillOutManage?error=" + Error.SUCCESS);
                }
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
