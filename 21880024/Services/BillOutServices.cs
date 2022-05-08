using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _21880024.DAL;
using _21880024.Entities;

namespace _21880024.Services
{
    public class BillOutServices
    {
        public static int add(BillOut billOut)
        {
            try
            {
                if (billOut.Equals(null))
                {
                    return Error.NULL_VALUE;
                }
                BillOutRepository billOutRepository = BillOutRepository.getInstance();
                int result = billOutRepository.add(billOut);
                if (result != Error.ERROR && result != Error.NULL_VALUE)
                {
                    WareHouseRepository wareHouseRepository = WareHouseRepository.getInstance();
                    wareHouseRepository.exportItem(billOut);
                }
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return Error.ERROR;
            }
        }
        public static int getMaxId()
        {
            try
            {
                BillOutRepository billOutRepository = BillOutRepository.getInstance();
                return billOutRepository.getMaxId();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return Error.ERROR;
            }
        }
        public static int findIdByName(string name)
        {
            try
            {
                ProductRepository productRepository = ProductRepository.getInstance();
                return productRepository.findIdByName(name);
            }
            catch (Exception e)
            {
                return Error.ERROR;
            }
        }
        public static Product findProductById(int id)
        {
            Product product = new Product();
            try
            {
                ProductRepository productRepository = ProductRepository.getInstance();
                product = productRepository.findById(id);
                return product;
            }catch(Exception e)
            {
                return product;
            }
        }
        public static int addProductInBill(ProductInBill productInBill)
        {
            if (productInBill.Equals(null))
            {
                return Error.NULL_VALUE;
            }
            if (productInBill.number.Equals(0))
            {
                return Error.ZERO;
            }
            BillOutRepository billOutRepository = BillOutRepository.getInstance();
            int index = Error.NOT_FOUND;
            bool result = false;

            index = billOutRepository.checkExistProductInBill(productInBill);
            if (index.Equals(Error.NOT_FOUND))
            {
                result = billOutRepository.addProductInBill(productInBill);
            } else if (!index.Equals(Error.ERROR))
            {
                result = billOutRepository.updateProductInBill(productInBill, index);
            }
            return Error.SUCCESS;

        }
        public static int updateProductInBill(ProductInBill productInBill)
        {
            if (productInBill.Equals(null))
            {
                return Error.NULL_VALUE;
            }
            if (productInBill.number.Equals(0))
            {
                return Error.ZERO;
            }
            BillOutRepository billOutRepository = BillOutRepository.getInstance();
            int index = Error.NOT_FOUND;
            index = billOutRepository.checkExistProductInBill(productInBill);
            if (index.Equals(Error.NOT_FOUND))
            {
                billOutRepository.addProductInBill(productInBill);
            }
            else if (!index.Equals(Error.ERROR))
            {
                billOutRepository.updateProductInBill(productInBill, index);
            }
            return Error.SUCCESS;
        }

        public static List<ProductInBill> findAllProductInBill()
        {
            BillOutRepository billOutRepository = BillOutRepository.getInstance();
            return billOutRepository.findAllProductInBill();
        }
        public static int checkExistProductInBill(ProductInBill product)
        {
            BillOutRepository billOutRepository = BillOutRepository.getInstance();
            return billOutRepository.checkExistProductInBill(product); 
        }
        public static bool deleteProductInBill(int idDelete)
        {
            BillOutRepository billOutRepository = BillOutRepository.getInstance();
            return billOutRepository.deleteProductInBill(idDelete);
        }
        public static bool checkNumberProduct(ProductInBill productInBill)
        {
            WareHouseRepository wareHouseRepository = WareHouseRepository.getInstance();
            return wareHouseRepository.checkNumberProduct(productInBill);
        }
    }

}
