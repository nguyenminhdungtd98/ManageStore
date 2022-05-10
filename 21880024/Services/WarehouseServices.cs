using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _21880024.Entities;
using _21880024.DAL;

namespace _21880024.Services
{
    public class WarehouseServices
    {
        public static List<Warehouse> findAll()
        {
            WareHouseRepository wareHouseRepository = WareHouseRepository.getInstance();
            return wareHouseRepository.findAll();
        }
        public static int add(Warehouse warehouse)
        {
            try
            {
                WareHouseRepository wareHouseRepository = WareHouseRepository.getInstance();
                if (warehouse.productNumber == 0)
                {
                    return Error.ZERO;
                }
                int index = wareHouseRepository.checkExist(warehouse.productNumber);
                if (index < 0)
                {
                    index = wareHouseRepository.add(warehouse);
                    return index;
                }
                else
                {
                    wareHouseRepository.updateNumberItem(warehouse);
                    return Error.SUCCESS;
                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        public static List<Warehouse> findByProductType(string productType)
        {
            try
            {
                WareHouseRepository wareHouseRepository = WareHouseRepository.getInstance();
                return wareHouseRepository.findByProductType(productType);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        public static List<Warehouse> findItemsExpire()
        {
            WareHouseRepository wareHouseRepository = WareHouseRepository.getInstance();
            return wareHouseRepository.findItemsExpire();
        }
    }
}
