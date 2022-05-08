using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _21880024.Entities
{
    public struct ProductInBill
    {
        public int productNumber { get; set; }
        public string productName { get; set; }

        public int price { get; set; }
        public int number { get; set; }

        public int total { get; set; }
        public ProductInBill(int productNumber, string productName, int price, int number, int total)
        {
            this.productNumber = productNumber;
            this.productName = productName;
            this.price = price;
            this.number = number;
            this.total = total;
        }
    }
}
