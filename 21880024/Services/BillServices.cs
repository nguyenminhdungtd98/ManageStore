using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _21880024.Entities;
using _21880024.DAL;

namespace _21880024.Services
{
    public class BillServices
    {
        public static List<Bill> findAll()
        {
            BillRepository billRepository = BillRepository.getInstance();
            return billRepository.findAll();
        }
        public static int add(Bill bill)
        {
            try
            {
                if (bill.number <= 0)
                {
                    return Error.ZERO;
                }
                BillRepository billRepository = BillRepository.getInstance();
                if (bill.productNumber == 0)
                {
                    return Error.ZERO;
                }
                return billRepository.add(bill);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        public static int getMaxId()
        {
            try
            {
                BillRepository billRepository = BillRepository.getInstance();
                return billRepository.getMaxId();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public static int findIdByName(string name)
        {
            try
            {
                if (name == null)
                {
                    return Error.NULL_VALUE;
                }
                ProductRepository repository = ProductRepository.getInstance();
                int id = repository.findIdByName(name);
                if (id == Error.NOT_FOUND)
                {
                    return Error.NOT_FOUND;
                }
                return id;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public static DateTime findExpireDateById(int id)
        {
            try
            {
                ProductRepository repository = ProductRepository.getInstance();
                DateTime expireDate = repository.findExpireDateById(id);
                return expireDate;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static int delete(int id)
        {
            if (id <= 0)
            {
                return Error.ZERO;
            }
            BillRepository repository = BillRepository.getInstance();
            if(repository.delete(id)!= Error.ERROR)
            {
                return Error.SUCCESS;
            }
            return Error.ERROR;

        }
        public static Bill findById(int id)
        {
            BillRepository repository = BillRepository.getInstance();
            return repository.findById(id);
        }
        public static int update(Bill bill, int idCurrent)
        {
            try{
                BillRepository repository = BillRepository.getInstance();
                WareHouseRepository wRepository = WareHouseRepository.getInstance();
                ProductRepository pRepository = ProductRepository.getInstance();
                Bill billOld = repository.findById(idCurrent);
                Warehouse warehouse = wRepository.findById(billOld.productNumber);
                int numberFinal = 0;
                if (billOld.productName.Equals(bill.productName))
                {
                    warehouse.number = bill.number;
                    wRepository.updateItem(warehouse);
                }
                else
                {
                    numberFinal = warehouse.number - billOld.number;
                    warehouse.number = numberFinal;
                    wRepository.updateItem(warehouse);
                    int productId = pRepository.findIdByName(bill.productName);
                    Product product = pRepository.findById(productId);
                    Warehouse warehouseNew = 
                        new Warehouse(product.productNumber, bill.productName, product.expireDate, bill.number, product.productType);
                    wRepository.add(warehouseNew);
                }
                return repository.update(bill, idCurrent);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        public static Product findProductById(int id)
        {
            ProductRepository repository = ProductRepository.getInstance();
            return repository.findById(id);
        }
        public static List<Bill> search(string typeSearch, string key)
        {
            BillRepository repository = BillRepository.getInstance();
            return repository.search(typeSearch, key);
        }
    }
}
