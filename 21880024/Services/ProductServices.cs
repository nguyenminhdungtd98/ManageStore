using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _21880024.DAL;
using _21880024.Entities;

namespace _21880024.Services
{
    public class ProductServices
    {

        public static List<Product> findAll()
        {

            List<Product> products = new List<Product>();
            try
            {
                ProductRepository repository = ProductRepository.getInstance();
                products = repository.findAll();
                return products;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        public static int delete(int id)
        {
            ProductRepository repository = ProductRepository.getInstance();
            if (id >= 0)
            {
                return repository.delete(id);
            }
            else
            {
                return -2;
            }
        }
        public static int add(Product product)
        {
            ProductRepository repository = ProductRepository.getInstance();
            try
            {
                int indexId = repository.checkExist(product.productNumber);
                int indexName = repository.checkExist(product.productName.Trim());
                if (indexId >= 0 || indexName >= 0)
                {
                    return Error.DUPLICATE;
                }
                if (product.productNumber == 0)
                {
                    return Error.ZERO;
                }
                else
                {
                    repository.add(product);
                    return Error.SUCCESS;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            
        }
        public static int checkExist(int id)
        {
            ProductRepository repository = ProductRepository.getInstance();
            if (id > 0)
            {
                return repository.checkExist(id);
            }
            else 
            { 
                return -1;
            }
        }
        public static Product findById(int id)
        {
            ProductRepository repository = ProductRepository.getInstance();
            if (id > 0)
            {
                return repository.findById(id);
            }
            else
            {
                Product product = new Product();
                return product;
            }

        }
        public static int checkExistToUpdate(int id, int idCurrent)
        {
            ProductRepository repository = ProductRepository.getInstance();
            if (id > 0)
            {
                return repository.checkExistToUpdate(id,idCurrent);
            }
            else
            {
                return -1;
            }
        }
        public static int update(Product product, int idCurrent)
        {
            ProductRepository repository = ProductRepository.getInstance();
            return repository.update(product, idCurrent);
        }
        public static List<Product> search(string typeSearch, string key)
        {
            ProductRepository repository = ProductRepository.getInstance();
            return repository.search(typeSearch,key);
        }
    }
}
