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
    public class ProductTypeModel : PageModel
    {
        public List<ProductType> productTypes { get; set; }

        [BindProperty(SupportsGet = true)]
        public int id { get; set; }

        [BindProperty(SupportsGet = true)]
        public int idDelete { get; set; }

        [BindProperty(SupportsGet = true)]
        public int idUpdate { get; set; }

        public int productTypeNumber { get; set; }
        public string productTypeName { get; set; }

        public string typeSearch { get; set; }

        public string key { get; set; }
        [BindProperty(SupportsGet = true)]
        public int error { get; set; }
        public void OnGet()
        {
            if (error.Equals(Error.DUPLICATE))
            {
                SetAlert(ErrorMessage.DUPLICATE, Error.ERROR);
            }
            else if (error.Equals(Error.ZERO))
            {
                SetAlert(ErrorMessage.ZERO, Error.ERROR);
            }
            else if (error.Equals(Error.SUCCESS))
            {
                SetAlert(ErrorMessage.SUCCESS, Error.SUCCESS);
            } else if (error.Equals(Error.PERMISION))
            {
                SetAlert(ErrorMessage.PERMISION, Error.ERROR);
            }
            productTypes = new List<ProductType>();
            int idTemp = 0;
            if (idDelete > 0)
            {
                idTemp = 1;
            }else if (idUpdate > 0)
            {
                idTemp = 2;
            }
            else
            {
                idTemp = -1;
            }
            switch (idTemp)
            {
                case 1:

                    int result = ProductTypeServices.delete(idDelete);
                    if (result == Error.ERROR)
                    {
                        Response.Redirect("/ProductType?error=" + Error.ERROR);
                    } else if (result == Error.PERMISION)
                    {
                        Response.Redirect("/ProductType?error=" + Error.PERMISION);
                    }
                    
                    
                    break;
                case 2:
                    ProductType productType = ProductTypeServices.findById(idUpdate);
                    if (productType.productTypeNumber > 0)
                    {
                        productTypeNumber = productType.productTypeNumber;
                        productTypeName = productType.productTypeName;
                    }
                    break;
            }
            productTypes = ProductTypeServices.findAll();
        }
        public void OnPost()
        {
            if (idUpdate <= 0 && key == null)
            {
                if (productTypeNumber != 0)
                {
                    ProductType productType = new ProductType(productTypeNumber, productTypeName);
                    if (ProductTypeServices.checkExist(productTypeNumber, productTypeName) < 0)
                    {
                        ProductTypeServices.add(productType);
                        Response.Redirect("/ProductType?error=" + Error.SUCCESS);
                    }
                    else
                    {
                        Response.Redirect("/ProductType?error=" + Error.DUPLICATE);
                    }
                }
                else
                {
                    Response.Redirect("/ProductType?error="+ Error.ERROR);
                }
            }
            else if(idUpdate > 0 )
            {
                ProductType productType = new ProductType(productTypeNumber, productTypeName);
                if (ProductTypeServices.checkExistToUpdate(productTypeNumber, idUpdate, productTypeName) > 0)
                {
                    Response.Redirect("/ProductType?error=" + Error.DUPLICATE);
                }
                else
                {
                    ProductTypeServices.update(productType, idUpdate);
                    Response.Redirect("/ProductType?error=" + Error.SUCCESS);
                }
            }
            else
            {
                productTypes = ProductTypeServices.search(typeSearch, key);
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
