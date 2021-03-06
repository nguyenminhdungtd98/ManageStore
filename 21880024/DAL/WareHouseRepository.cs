using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _21880024.Entities;
using Newtonsoft.Json;
using System.IO;


namespace _21880024.DAL
{
    public class WareHouseRepository
    {
        private static List<Warehouse> itemsInWarehouse;

        static WareHouseRepository instance = null;
        public WareHouseRepository()
        {
            itemsInWarehouse = new List<Warehouse>();
        }
        public static WareHouseRepository getInstance()
        {
            if (instance == null)
            {
                instance = new WareHouseRepository();
            }
            return instance;
        }

        public List<Warehouse> findAll()
        {
            try
            {
                itemsInWarehouse = loadData();
                return itemsInWarehouse;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        public List<Warehouse> findByProductType(string productType)
        {
            try
            {
                List<Warehouse> warehouses = new List<Warehouse>();
                loadData();
                foreach (Warehouse item in itemsInWarehouse)
                {
                    if (item.productType == productType)
                    {
                        warehouses.Add(item);
                    }
                }
                return warehouses;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        public Warehouse findById(int id)
        {
            Warehouse itemInWarehouse = new Warehouse();
            foreach (Warehouse item in itemsInWarehouse)
            {
                if (item.productNumber.Equals(id))
                {
                    return item;
                }
            }

            return itemInWarehouse;

        }
        public int checkExist(int id)
        {
            try
            {
                int index = Error.NOT_FOUND;
                itemsInWarehouse = loadData();
                for (int i = 0; i < itemsInWarehouse.Count; i++)
                {
                    if (itemsInWarehouse[i].productNumber.Equals(id))
                    {
                        index = i;
                        break;
                    }
                }
                return index;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public int add(Warehouse itemInWarehouse)
        {
            try
            {
                itemsInWarehouse = loadData();
                if (!itemInWarehouse.Equals(null))
                {
                    itemsInWarehouse.Add(itemInWarehouse);
                    if (SaveFileData(itemsInWarehouse))
                    {
                        return itemInWarehouse.productNumber;
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
        public bool SaveFileData(List<Warehouse> itemsInWarehouse)
        {
            try
            {
                string productsUpdate = JsonConvert.SerializeObject(itemsInWarehouse);
                using (var output = new StreamWriter(@".\Data\warehouse.json"))
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
        public List<Warehouse> loadData()
        {
            try
            {
                string itemsInWarehouseJson = "";
                itemsInWarehouseJson = System.IO.File.ReadAllText(@".\Data\warehouse.json");
                List<Warehouse> itemsInWarehouseTemp = JsonConvert.DeserializeObject<List<Warehouse>>(itemsInWarehouseJson);
                if (itemsInWarehouseTemp != null)
                {
                    itemsInWarehouse = itemsInWarehouseTemp;
                }
                return itemsInWarehouse;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }


        }
        public bool updateNumberItem(Warehouse warehouse)
        {
            try
            {
                loadData();
                for (int i = 0; i < itemsInWarehouse.Count; i++)
                {
                    if (warehouse.productNumber.Equals(itemsInWarehouse[i].productNumber))
                    {
                        warehouse.number += itemsInWarehouse[i].number;
                        itemsInWarehouse[i] = warehouse;
                        break;
                    }
                }
                SaveFileData(itemsInWarehouse);
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public bool updateItem(Warehouse warehouse)
        {
            try
            {
                loadData();
                int index = -1;
                for (int i = 0; i < itemsInWarehouse.Count; i++)
                {
                    if (warehouse.productNumber.Equals(itemsInWarehouse[i].productNumber))
                    {
                        if (warehouse.number <= 0)
                        {
                            index = i;
                        }
                        itemsInWarehouse[i] = warehouse;
                        break;
                    }
                }
                if (index != -1)
                {
                    itemsInWarehouse.RemoveAt(index);
                }
                SaveFileData(itemsInWarehouse);
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<Warehouse> findItemsExpire()
        {
            try
            {
                List<Warehouse> itemsExpire = new List<Warehouse>();
                loadData();
                foreach (Warehouse item in itemsInWarehouse)
                {
                    if (item.expireDate < DateTime.Now)
                    {
                        itemsExpire.Add(item);
                    }
                }
                return itemsExpire;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }

        }
        public bool exportItem(BillOut billOut)
        {
            try
            {
                for (int i = 0; i < itemsInWarehouse.Count; i++)
                {
                    for (int j = 0; j < billOut.productInBill.Count; j++)
                    {
                        if (itemsInWarehouse[i].productNumber.Equals(billOut.productInBill[j].productNumber))
                        {
                            int numberOld = itemsInWarehouse[i].number;
                            Warehouse warehouseUpdate = new Warehouse();
                            warehouseUpdate = itemsInWarehouse[i];
                            warehouseUpdate.number = numberOld - billOut.productInBill[j].number;
                            itemsInWarehouse[i] = warehouseUpdate;
                        }
                    }

                }
                SaveFileData(itemsInWarehouse);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        public bool checkNumberProduct(ProductInBill productInBill)
        {
            try
            {
                foreach (Warehouse item in itemsInWarehouse)
                {
                    if (item.productNumber.Equals(productInBill.productNumber) && item.number < productInBill.number)
                    {
                        return false;
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
        public bool updateWareHouseOut(BillOut billOut)
        {
            try
            {
                for (int i = 0; i < billOut.productInBill.Count; i++)
                {
                    for (int j = 0; j < itemsInWarehouse.Count; j++)
                    {
                        if (billOut.productInBill[i].productNumber == itemsInWarehouse[j].productNumber)
                        {
                            Warehouse itemEdit = itemsInWarehouse[j];
                            int numberOld = itemEdit.number;
                            itemEdit.number = numberOld - billOut.productInBill[i].number;
                            itemsInWarehouse[j] = itemEdit;
                        }
                    }
                }
                SaveFileData(itemsInWarehouse);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        public bool updateWareHouseOutToEdit(BillOut billOutnew, BillOut billOutOld)
        {
            try
            {
                loadData();
                if (billOutnew.productInBill.Count == billOutOld.productInBill.Count)
                {
                    for (int i = 0; i < billOutnew.productInBill.Count; i++)
                    {
                        for (int j = 0; j < billOutOld.productInBill.Count; j++)
                        {
                            Warehouse itemEdit = new Warehouse();
                            int numberFinal = 0;
                            int index = 0;
                            if ((billOutnew.productInBill[i].productNumber == billOutOld.productInBill[j].productNumber) && (billOutnew.productInBill[i].number != billOutOld.productInBill[j].number))
                            {
                                numberFinal = billOutnew.productInBill[i].number - billOutOld.productInBill[j].number;
                                index = checkExist(billOutnew.productInBill[i].productNumber);
                                itemEdit = itemsInWarehouse[index];

                                itemEdit.number = itemEdit.number - numberFinal;
                                itemsInWarehouse[index] = itemEdit;

                                SaveFileData(itemsInWarehouse);
                                loadData();


                            }

                        }
                    }
                }
                else if(billOutnew.productInBill.Count > billOutOld.productInBill.Count) 
                {
                    List<int> productTemps = new List<int>();
                    for (int i = 0; i < billOutnew.productInBill.Count; i++)
                    {
                        for (int j = 0; j < billOutOld.productInBill.Count; j++)
                        {
                            if (billOutnew.productInBill[i].productNumber == billOutOld.productInBill[j].productNumber)
                            {
                                productTemps.Add(billOutnew.productInBill[i].productNumber);
                            }
                        }
                    }
                    for (int i = 0; i < billOutnew.productInBill.Count; i++)
                    {
                        for (int j = 0; j < billOutOld.productInBill.Count; j++)
                        {
                            Warehouse itemEdit = new Warehouse();
                            int numberFinal = 0;
                            int index = 0;
                            if ((billOutnew.productInBill[i].productNumber == billOutOld.productInBill[j].productNumber) && (billOutnew.productInBill[i].number != billOutOld.productInBill[j].number))
                            {
                                numberFinal = billOutnew.productInBill[i].number - billOutOld.productInBill[j].number;
                                index = checkExist(billOutnew.productInBill[i].productNumber);
                                itemEdit = itemsInWarehouse[index];

                                itemEdit.number = itemEdit.number - numberFinal;
                                itemsInWarehouse[index] = itemEdit;

                                SaveFileData(itemsInWarehouse);
                                loadData();

                            }

                        }
                    }
                    ProductInBill product = new ProductInBill();
                    for (int i = 0; i < billOutnew.productInBill.Count; i++)
                    {
                        bool skip = false;
                        for (int j  = 0; j < productTemps.Count; j++)
                        {
                            if (billOutnew.productInBill[i].productNumber == productTemps[j])
                            {
                                skip = true;
                                break;
                            }
                        }
                        if (!skip)
                        {
                            Warehouse warehouse = new Warehouse();
                            product = billOutnew.productInBill[i];
                            int index = checkExist(product.productNumber);
                            warehouse = itemsInWarehouse[index];
                            warehouse.number = warehouse.number - product.number;

                            itemsInWarehouse[index] = warehouse;

                            SaveFileData(itemsInWarehouse);
                            loadData();
                        }

                    }



                } else
                {
                    List<int> productTemps = new List<int>();
                    for (int i = 0; i < billOutnew.productInBill.Count; i++)
                    {
                        for (int j = 0; j < billOutOld.productInBill.Count; j++)
                        {
                            if (billOutnew.productInBill[i].productNumber == billOutOld.productInBill[j].productNumber)
                            {
                                productTemps.Add(billOutnew.productInBill[i].productNumber);
                            }
                        }
                    }
                    for (int i = 0; i < billOutnew.productInBill.Count; i++)
                    {
                        for (int j = 0; j < billOutOld.productInBill.Count; j++)
                        {
                            Warehouse itemEdit = new Warehouse();
                            int numberFinal = 0;
                            int index = 0;
                            if ((billOutnew.productInBill[i].productNumber == billOutOld.productInBill[j].productNumber) && (billOutnew.productInBill[i].number != billOutOld.productInBill[j].number))
                            {
                                numberFinal = billOutnew.productInBill[i].number - billOutOld.productInBill[j].number;
                                index = checkExist(billOutnew.productInBill[i].productNumber);
                                itemEdit = itemsInWarehouse[index];

                                itemEdit.number = itemEdit.number - numberFinal;
                                itemsInWarehouse[index] = itemEdit;

                                SaveFileData(itemsInWarehouse);
                                loadData();

                            }

                        }
                    }
                    
                    for (int i = 0; i < billOutOld.productInBill.Count; i++)
                    {
                        ProductInBill product = new ProductInBill();
                        bool skip = false;
                        for (int j = 0; j < productTemps.Count; j++)
                        {
                            if (billOutOld.productInBill[i].productNumber == productTemps[j])
                            {
                                skip = true;
                                break;
                            }
                        }
                        if (!skip)
                        {
                            Warehouse warehouse = new Warehouse();
                            product = billOutOld.productInBill[i];
                            int index = checkExist(product.productNumber);
                            warehouse = itemsInWarehouse[index];
                            warehouse.number = warehouse.number + product.number;

                            itemsInWarehouse[index] = warehouse;

                            SaveFileData(itemsInWarehouse);
                            loadData();
                        }

                    }
                }
                SaveFileData(itemsInWarehouse);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        public bool updateWareHouseOutToEdit(BillOut billOut)
        {
            try
            {
                for (int i = 0; i < billOut.productInBill.Count; i++)
                {
                    for (int j = 0; j < itemsInWarehouse.Count; j++)
                    {
                        if (billOut.productInBill[i].productNumber == itemsInWarehouse[j].productNumber && billOut.productInBill[i].number != itemsInWarehouse[j].number)
                        {
                            Warehouse itemEdit = itemsInWarehouse[j];
                            int numberOld = itemEdit.number;
                            itemEdit.number = numberOld - billOut.productInBill[i].number;
                            itemsInWarehouse[j] = itemEdit;
                        }
                    }
                }
                SaveFileData(itemsInWarehouse);
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
