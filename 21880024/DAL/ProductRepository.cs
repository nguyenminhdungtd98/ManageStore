using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _21880024.Entities;
using Newtonsoft.Json;
using System.IO;

namespace _21880024.DAL
{
    public class ProductRepository
    {
        static ProductRepository instance = null;
        private static List<Product> products;
        public ProductRepository()
        {
            products = new List<Product>();
        }
        public static ProductRepository getInstance()
        {
            if (instance == null)
            {
                instance = new ProductRepository();
            }
            return instance;
        }

        public List<Product> findAll()
        {

            try
            {
                products = loadData();
                return products;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        public int delete(int id)
        {
            try
            {
                products = loadData();
                int index = 0;
                int size = products.Count;
                for (int i = 0; i < size; i++)
                {
                    if (products[i].productNumber.Equals(id))
                    {
                        index = i;
                        break;
                    }
                }
                products.RemoveAt(index);
                bool result = SaveFileData(products);
                if (products.Count.Equals(size - 1) && result)
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
        public int add(Product product)
        {
            try
            {
                loadData();
                if (!product.Equals(null))
                {
                    products.Add(product);
                    if (SaveFileData(products))
                    {
                        return product.productNumber;
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

        public int checkExist(int id)
        {
            try
            {
                int index = Error.NOT_FOUND;
                products = loadData();
                if (products != null)
                {
                    for (int i = 0; i < products.Count; i++)
                    {
                        if (products[i].productNumber.Equals(id))
                        {
                            index = i;
                            break;
                        }
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
        public int checkExist(string name)
        {
            try
            {
                int index = Error.NOT_FOUND;
                products = loadData();
                if (products != null)
                {
                    for (int i = 0; i < products.Count; i++)
                    {
                        if (products[i].productName.Equals(name))
                        {
                            index = i;
                            break;
                        }
                    }
                }
                return index;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return Error.ERROR;
            }
        }
        public bool SaveFileData(List<Product> products)
        {
            try
            {
                string productsUpdate = JsonConvert.SerializeObject(products);
                using (var output = new StreamWriter(@"D:\StudyDoc\IT\Học kì 2\Kĩ thuật lập trình\21880024\21880024\Data\product.json"))
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
        public Product findById(int id)
        {
            Product product = new Product();
            foreach (Product pro in products)
            {
                if (pro.productNumber.Equals(id))
                {
                    return pro;
                }
            }

            return product;

        }
        public List<Product> loadData()
        {
            try
            {
                string productJson = "";
                productJson = System.IO.File.ReadAllText(@"D:\StudyDoc\IT\Học kì 2\Kĩ thuật lập trình\21880024\21880024\Data\product.json");
                List<Product> productsTemp = JsonConvert.DeserializeObject<List<Product>>(productJson);
                if (productsTemp != null)
                {
                    products = productsTemp;
                }
                return products;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }


        }
        public int checkExistToUpdate(int id, int idCurrent)
        {
            try
            {
                int index = -2;
                products = loadData();
                for (int i = 0; i < products.Count; i++)
                {
                    if (products[i].productNumber.Equals(id) && !products[i].productNumber.Equals(idCurrent))
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
        public int update(Product product, int idCurrent)
        {
            int index = checkExist(idCurrent);
            if (index >= 0)
            {
                products.RemoveAt(index);
                SaveFileData(products);
            }
            else
            {
                return -1;
            }
            int id = add(product);
            if (id > 0)
            {
                SaveFileData(products);
                return id;
            }
            else
            {
                return -2;
            }


        }
        public List<Product> search(string typeSearch, string key)
        {
            loadData();
            List<Product> productSearch = new List<Product>();
            try
            {
                switch (typeSearch)
                {
                    case "productNumber":
                        foreach (Product po in products)
                        {
                            bool convertInt = int.TryParse(key, out int result);
                            if (convertInt && po.productNumber.Equals(result))
                            {
                                productSearch.Add(po);
                                break;
                            }
                        }
                        break;
                    case "productName":
                        foreach (Product po in products)
                        {
                            if (po.productName.Contains(key))
                            {
                                productSearch.Add(po);
                            }
                        }
                        break;
                    case "expireDate":
                        foreach (Product po in products)
                        {
                            DateTime datetime1 = new DateTime();
                            datetime1 = DateTime.Parse(key.Trim());
                            if (po.expireDate.Equals(datetime1))
                            {
                                productSearch.Add(po);
                            }
                        }
                        break;
                    case "company":
                        foreach (Product po in products)
                        {
                            if (po.company.Contains(key.Trim()))
                            {
                                productSearch.Add(po);
                            }
                        }
                        break;
                    case "dateOfManufacture":
                        DateTime datetime = new DateTime();
                        datetime = DateTime.Parse(key.Trim());
                        foreach (Product po in products)
                        {

                            if (po.dateOfManufacture.Equals(datetime))
                            {
                                productSearch.Add(po);
                            }
                        }
                        break;
                    case "productType":
                        foreach (Product po in products)
                        {
                            if (po.productType.Contains(key))
                            {
                                productSearch.Add(po);
                            }
                        }
                        break;
                    case "price":
                        bool convertInt1 = int.TryParse(key, out int result1);
                        foreach (Product po in products)
                        {
                            if (convertInt1 && po.price.Equals(result1))
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
        public int findIdByName(string name)
        {
            try
            {
                int id = Error.NOT_FOUND;
                foreach (Product item in products)
                {
                    if (item.productName.Equals(name.Trim()))
                    {
                        id = item.productNumber;
                    }
                }
                return id;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public DateTime findExpireDateById(int id)
        {
            try
            {
                DateTime date = new DateTime();
                foreach (Product item in products)
                {
                    if (item.productNumber.Equals(id))
                    {
                        date = item.expireDate;
                    }
                }
                return date;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<Product> findByProductType(string name)
        {
            try
            {
                loadData();
                List<Product> productTemps = new List<Product>();
                foreach (Product item in products)
                {
                    if (item.productType == name)
                    {
                        productTemps.Add(item);
                    }
                }
                return productTemps;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        public bool deleteAllProductByProductType(string name)
        {
            try
            {
                loadData();
                List<int> ids = new List<int>();
                foreach (Product product in products)
                {
                    if (product.productType ==  name)
                    {
                        ids.Add(product.productNumber);
                    }
                }
                for(int i = 0; i < ids.Count; i++)
                {
                    delete(ids[i]);
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
    }

}
