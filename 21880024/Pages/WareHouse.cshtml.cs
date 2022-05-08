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
    public class WareHouseModel : PageModel
    {
        public List<Warehouse> itemsInWarehouse { get; set; }
        public List<ProductType> productTypes { get; set; }

        public List<Warehouse> itemsExpire { get; set; }
        public void OnGet()
        {
            itemsInWarehouse = WarehouseServices.findAll();
            productTypes = ProductTypeServices.findAll();
            itemsExpire = WarehouseServices.findItemsExpire();

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
