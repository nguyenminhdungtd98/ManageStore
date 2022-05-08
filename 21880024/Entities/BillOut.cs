using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _21880024.Entities
{
    public struct BillOut
    {
        public int numberBillOut { get; set; }
        public DateTime createDate { get; set; }

        public List<ProductInBill> productInBill;
  
        public BillOut(int numberBillOut, DateTime createDate, List<ProductInBill> productInBill)
        {
            this.numberBillOut = numberBillOut;
            this.createDate = createDate;
            this.productInBill = productInBill;

        }


    }
}
