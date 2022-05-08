using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _21880024.Entities
{
    public struct Product
    {
        public int productNumber { get; set; }
        public string productName  { get; set; }

        public DateTime expireDate { get; set; }

        public string company { get; set; }

        public DateTime dateOfManufacture { get; set; }

        public string productType { get; set; }

        public int price { get; set; }
        
        public Product(int productNumber, string productName, DateTime expireDate, string company, DateTime dateOfManufacture, string productType, int price)
        {
            this.productNumber = productNumber;
            this.productName = productName;
            this.expireDate = expireDate;
            this.company = company;
            this.dateOfManufacture = dateOfManufacture;
            this.productType = productType;
            this.price = price;

        }
    }
}
