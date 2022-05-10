using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _21880024.Entities;
using _21880024.DAL;

namespace _21880024.Services
{
    public class ProductTypeServices
    {
        public static List<ProductType> findAll()
        {
            List<ProductType> productTypes = new List<ProductType>();
            try
            {
                ProductTypeRepository repository = ProductTypeRepository.getInstance();
                productTypes = repository.findAll();
                return productTypes;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        public static int add(ProductType product)
        {
            ProductTypeRepository repository = ProductTypeRepository.getInstance();
            if (!product.Equals(null))
            {
                return repository.add(product);
            }
            else
            {
                return -1;
            }
        }
        public static int checkExist(int id, string productTypeName)
        {
            ProductTypeRepository repository = ProductTypeRepository.getInstance();
            if (id > 0)
            {
                return repository.checkExist(id, productTypeName);
            }
            else
            {
                return -1;
            }
        }
        public static int delete(int id)
        {
            ProductTypeRepository repository = ProductTypeRepository.getInstance();
            BillRepository billRepository = BillRepository.getInstance();
            BillOutRepository billOutRepository = BillOutRepository.getInstance();

            bool permisionDelete = true;
            List<Bill> bills = billRepository.findAll();
            ProductRepository repositoryProduct = ProductRepository.getInstance();
            List<Product> products = new List<Product>();

            if (id > 0)
            {
                ProductType productType = repository.findById(id);
                products = repositoryProduct.findByProductType(productType.productTypeName);
                    if (products != null)
                    {
                        foreach (Product item in products)
                        {

                            foreach (Bill item1 in bills)
                            {
                                if (item1.productNumber == item.productNumber)
                                {
                                    permisionDelete = false;
                                    break;
                                }
                            }
                            List<BillOut> billOuts = billOutRepository.findAll();
                            foreach (BillOut item2 in billOuts)
                            {
                                foreach (ProductInBill item3 in item2.productInBill)
                                {
                                    if (item3.productNumber == item.productNumber)
                                    {
                                        permisionDelete = false;
                                        break;
                                    }
                                }
                                if (!permisionDelete)
                                {
                                    break;
                                }
                            }

                        }
                    
                    }
                if (permisionDelete)
                {
                    if (repositoryProduct.deleteAllProductByProductType(productType.productTypeName))
                    {
                        return repository.delete(id);
                    }
                    else
                    {
                        return Error.ERROR;
                    }
                }
                else
                {
                    return Error.PERMISION;
                }

            }
            else
            {
                return -2;
            }
        }
        public static int checkExistToUpdate(int id, int idCurrent, string productTypeName)
        {
            ProductTypeRepository repository = ProductTypeRepository.getInstance();
            if (id > 0)
            {
                return repository.checkExistToUpdate(id, idCurrent, productTypeName);
            }
            else
            {
                return -1;
            }
        }
        public static int update(ProductType productType, int idCurrent)
        {
            ProductTypeRepository repository = ProductTypeRepository.getInstance();
            return repository.update(productType, idCurrent);
        }
        public static ProductType findById(int id)
        {
            ProductTypeRepository repository = ProductTypeRepository.getInstance();
            if (id > 0)
            {
                return repository.findById(id);
            }
            else
            {
                ProductType productType = new ProductType();
                return productType;
            }

        }
        public static List<ProductType> search(string typeSearch, string key)
        {
            ProductTypeRepository repository = ProductTypeRepository.getInstance();
            return repository.search(typeSearch, key);
        }
    }

}
