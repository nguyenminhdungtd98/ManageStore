using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _21880024.Entities
{
    public struct Warehouse
    {
        public int productNumber { get; set; }
        public string productName { get; set; }

        public DateTime expireDate { get; set; }

        public int number; 
        public string productType { get; set; }

        public Warehouse(int productNumber, string productName, DateTime expireDate, int number, string productType)
        {
            this.productNumber = productNumber;
            this.productName = productName;
            this.expireDate = expireDate;
            this.number = number;
            this.productType = productType;
        }
    }
}
