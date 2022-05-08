using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _21880024.Entities
{
    public struct Bill
    {
        public int numberBill { get; set; }
        public DateTime createDate { get; set; }
        public int productNumber { get; set; }
        public string productName { get; set; }

        public int number { get; set; }

        public string productType { get; set; }

        public Bill(int numberBill, DateTime createDate, int productNumber, string productName, int number, string productType)
        {
            this.numberBill = numberBill;
            this.createDate = createDate;
            this.productNumber = productNumber;
            this.productName = productName;
            this.number = number;
            this.productType = productType;
        }
    }
}
