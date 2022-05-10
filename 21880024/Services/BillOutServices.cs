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
        public static List<BillOut> findAll()
        {
            BillOutRepository billOutRepository = BillOutRepository.getInstance();
            return billOutRepository.findAll();
        }

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
                Console.WriteLine(e.Message);
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
                Console.WriteLine(e.Message);
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
        public static BillOut findById(int id)
        {
            BillOut billOut = new BillOut();
            BillOutRepository repository = BillOutRepository.getInstance();
            billOut = repository.findById(id);
            return billOut;
        }
        public static bool checkExistProductToEdit(string name, List<ProductInBill> productInBills)
        {
            try
            {
                foreach (ProductInBill item in productInBills )
                {
                    if (item.productName.Equals(name))
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        public static List<ProductInBill> updateProductToEdit(string name, int number, List<ProductInBill> productInBills)
        {
            try
            {
                for (int i = 0; i < productInBills.Count; i++)
                {
                    if (productInBills[i].productName.Equals(name))
                    {
                        ProductInBill productInBillTemp = productInBills[i];
                        productInBillTemp.number = number;
                        productInBills[i] = productInBillTemp;
                    }
                }
                return productInBills;
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return productInBills;
            }
        }
        public static int updateBillOut(BillOut billOut)
        {
            BillOutRepository billOutRepository = BillOutRepository.getInstance();
            int index = billOutRepository.checkExist(billOut.numberBillOut);
            
            return billOutRepository.update(billOut, billOut.numberBillOut);
        }
        public static bool addProductInBills(List<ProductInBill> productInBills)
        {
            try
            {
                foreach (ProductInBill item in productInBills)
                {
                    BillOutRepository repository = BillOutRepository.getInstance();
                    repository.addProductInBill(item);
                }
                return true;
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        public static bool deleteAllProductInBill()
        {
            BillOutRepository repository = BillOutRepository.getInstance();
            return repository.deleteAllProductInBill();
        }
        public static bool updateWareHouseOut(BillOut billOut)
        {
            WareHouseRepository wareHouseRepository = WareHouseRepository.getInstance();
            return wareHouseRepository.updateWareHouseOut(billOut);
        }
        public static bool updateWareHouseOutToEdit(BillOut billOutnew, BillOut billOutOld)
        {
            WareHouseRepository wareHouseRepository = WareHouseRepository.getInstance();
            return wareHouseRepository.updateWareHouseOutToEdit(billOutnew, billOutOld);
        }
        public static bool updateWareHouseOutToEdit(BillOut billOut)
        {
            WareHouseRepository wareHouseRepository = WareHouseRepository.getInstance();
            return wareHouseRepository.updateWareHouseOutToEdit(billOut);
        }
        public static bool saveBillOutTemp(List<BillOut> billOutTemp)
        {
            BillOutRepository billOutRepository = BillOutRepository.getInstance();
            return billOutRepository.SaveBillOutTemp(billOutTemp);
        }
        public static List<BillOut> loadBillOutTemp()
        {
            BillOutRepository billOutRepository = BillOutRepository.getInstance();
            return billOutRepository.loadBillOutTemp();
        }
        public static List<BillOut> search(string typeSearch, string key)
        {
            BillOutRepository repository = BillOutRepository.getInstance();
            return repository.search(typeSearch, key);
        }

        public static int delete(int id)
        {
            if (id <= 0)
            {
                return Error.ZERO;
            }
            BillOutRepository repository = BillOutRepository.getInstance();
            if (repository.delete(id))
            {
                return Error.SUCCESS;
            }
            return Error.ERROR;

        }
        public static bool deleteAllBillOutTemps()
        {
            BillOutRepository billOutRepository = BillOutRepository.getInstance();
            return billOutRepository.deleteAllBillOutTemp();
        }
    }

}
