using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _21880024.Entities
{
    public struct ProductType
    {
        public int productTypeNumber { get; set; }
        public string productTypeName { get; set; }

        public ProductType(int productTypeNumber, string productTypeName)
        {
            this.productTypeName = productTypeName;
            this.productTypeNumber = productTypeNumber;
        }
    }
}
