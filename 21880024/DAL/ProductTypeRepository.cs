using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _21880024.Entities;
using Newtonsoft.Json;
using System.IO;

namespace _21880024.DAL
{
    public class ProductTypeRepository
    {
        static ProductTypeRepository instance = null;
        private static List<ProductType> productTypes;
        public ProductTypeRepository()
        {
            productTypes = new List<ProductType>();
        }
        public static ProductTypeRepository getInstance()
        {
            if (instance == null)
            {
                instance = new ProductTypeRepository();
            }
            return instance;
        }

        public List<ProductType> findAll()
        {

            try
            {
                productTypes = loadData();
                return productTypes;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        public List<ProductType> loadData()
        {
            try
            {
                productTypes = new List<ProductType>();
                string productTypeJson = System.IO.File.ReadAllText(@".\Data\ProductType.json");
                List<ProductType> productTypesTemp = JsonConvert.DeserializeObject<List<ProductType>>(productTypeJson);
                if (productTypesTemp != null)
                {
                    productTypes = productTypesTemp;
                }
                return productTypes;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }


        }

        public int add(ProductType productType)
        {
            try
            {
                productTypes = loadData();
                if (!productType.Equals(null))
                {
                    productTypes.Add(productType);
                    if (SaveFileData(productTypes))
                    {
                        return productType.productTypeNumber;
                    }
                    else
                    {
                        return -1;
                    }
                }
                else
                {
                    return -1;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return -1;
            }
        }
        public bool SaveFileData(List<ProductType> productTypes)
        {
            try
            {
                string productsUpdate = JsonConvert.SerializeObject(productTypes);
                using (var output = new StreamWriter(@".\Data\ProductType.json"))
                {
                    if (null != productsUpdate)
                    {
                        // optionally modify line.
                        output.WriteLine(productsUpdate);
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        public int checkExist(int id, string productTypeName)
        {
            try
            {
                int index = -2;
                productTypes = loadData();
                for (int i = 0; i < productTypes.Count; i++)
                {
                    if (productTypes[i].productTypeNumber.Equals(id) || productTypes[i].productTypeName.Equals(productTypeName))
                    {
                        index = i;
                        break;
                    }
                }
                return index;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return -1;
            }
        }
        public int delete(int id)
        {
            try
            {
                productTypes = loadData();
                int index = 0;
                int size = productTypes.Count;
                for (int i = 0; i < size; i++)
                {
                    if (productTypes[i].productTypeNumber.Equals(id))
                    {
                        index = i;
                        break;
                    }
                }
                productTypes.RemoveAt(index);
                bool result = SaveFileData(productTypes);
                if (productTypes.Count.Equals(size - 1) && result)
                {
                    return id;
                }
                else
                {
                    return -1;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return -1;
            }
        }
        public int checkExistToUpdate(int id, int idCurrent, string productTypeName)
        {
            try
            {
                int index = -2;
                productTypes = loadData();
                for (int i = 0; i < productTypes.Count; i++)
                {
                    if ((productTypes[i].productTypeNumber.Equals(id) || productTypes[i].productTypeName.Equals(productTypeName)) && !productTypes[i].productTypeNumber.Equals(idCurrent))
                    {
                        index = i;
                        break;
                    }
                }
                return index;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return -1;
            }
        }
        public int update(ProductType productType, int idCurrent)
        {
            int index = checkExist(idCurrent, productType.productTypeName);
            if (index >= 0)
            {
                productTypes.RemoveAt(index);
                SaveFileData(productTypes);
            }
            else
            {
                return -1;
            }
            int id = add(productType);
            if (id > 0)
            {
                SaveFileData(productTypes);
                return id;
            }
            else
            {
                return -2;
            }

        }
        public ProductType findById(int id)
        {
            ProductType productType = new ProductType();
            foreach (ProductType pro in productTypes)
            {
                if (pro.productTypeNumber.Equals(id))
                {
                    return pro;
                }
            }

            return productType;

        }
        public List<ProductType> search(string typeSearch, string key)
        {
            loadData();
            List<ProductType> productSearch = new List<ProductType>();
            try
            {
                switch (typeSearch)
                {
                    case "productTypeNumber":
                        foreach (ProductType po in productTypes)
                        {
                            bool convertInt = int.TryParse(key, out int result);
                            if (convertInt && po.productTypeNumber.Equals(result))
                            {
                                productSearch.Add(po);
                                break;
                            }
                        }
                        break;
                    case "productTypeName":
                        foreach (ProductType po in productTypes)
                        {
                            if (po.productTypeName.Contains(key))
                            {
                                productSearch.Add(po);
                            }
                        }
                        break;
                    default: break;
                }
                return productSearch;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }

        }
    }
}
