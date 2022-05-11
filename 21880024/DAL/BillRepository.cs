using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _21880024.Entities;
using Newtonsoft.Json;
using System.IO;

namespace _21880024.DAL
{
    public class BillRepository
    {
        static BillRepository instance = null;
        private static List<Bill> bills;
        public BillRepository()
        {
            bills = new List<Bill>();
        }
        public static BillRepository getInstance()
        {
            if (instance == null)
            {
                instance = new BillRepository();
            }
            return instance;
        }

        private static int numberBillCurrent = 0;
        public Bill findById(int id)
        {
            Bill bill = new Bill();
            foreach (Bill item in bills)
            {
                if (item.numberBill.Equals(id))
                {
                    return item;
                }
            }

            return bill;

        }
        public List<Bill> findAll()
        {
            try
            {
                bills = loadData();
                return bills;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        public int checkExist(int id)
        {
            try
            {
                int index = Error.NOT_FOUND;
                bills = loadData();
                for (int i = 0; i < bills.Count; i++)
                {
                    if (bills[i].numberBill.Equals(id))
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
        public int add(Bill bill)
        {
            try
            {
                if (numberBillCurrent.Equals(0))
                {
                    numberBillCurrent = 1;
                    bill.numberBill = numberBillCurrent;
                }

                bills = loadData();

                if (!bill.Equals(null))
                {
                    if (bills.Count > 0)
                    {
                        bill.numberBill = getMaxId()+1;
                    }
                    bills.Add(bill);
                    if (SaveFileData(bills))
                    {
                        numberBillCurrent++;
                        return bill.productNumber;
                    }
                    else
                    {
                        return Error.ERROR;
                    }
                }
                else
                {
                    return Error.NULL_VALUE;
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public bool SaveFileData(List<Bill> bills)
        {
            try
            {
                string productsUpdate = JsonConvert.SerializeObject(bills);
                using (var output = new StreamWriter(@".\Data\Bill.json"))
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
        public List<Bill> loadData()
        {
            try
            {
                string billsJson = "";
                billsJson = System.IO.File.ReadAllText(@".\Data\bill.json");
                List<Bill> billsTemp = JsonConvert.DeserializeObject<List<Bill>>(billsJson);
                if (billsTemp!= null)
                {
                    bills = billsTemp;
                }
                
                return bills;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }


        }
        public int getMaxId()
        {
            try
            {
                int maxId = 0;
                foreach (Bill bill in bills)
                {
                    if (maxId < bill.numberBill)
                    {
                        maxId = bill.numberBill;
                    }
                }
                return maxId;
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
                bills = loadData();
                int index = Error.NOT_FOUND;
                int size = bills.Count;
                for (int i = 0; i < size; i++)
                {
                    if (bills[i].numberBill.Equals(id))
                    {
                        index = i;
                        break;
                    }
                }
                bills.RemoveAt(index);
                bool result = SaveFileData(bills);
                if (bills.Count.Equals(size - 1) && result)
                {
                    return id;
                }
                else
                {
                    return Error.ERROR;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return Error.ERROR;
            }
        }
        public int update(Bill bill, int idCurrent)
        {
            try
            {
                int index = checkExist(idCurrent);
                if (index >= 0)
                {
                    bills.RemoveAt(index);
                    SaveFileData(bills);
                }
                else
                {
                    return Error.NOT_FOUND;
                }
                int id = add(bill);
                if (id > 0)
                {
                    SaveFileData(bills);
                    return id;
                }
                else
                {
                    return Error.ERROR;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            


        }
        public List<Bill> search(string typeSearch, string key)
        {
            loadData();
            List<Bill> billSearch = new List<Bill>();
            try
            {
                switch (typeSearch)
                {
                    case "numberBill":
                        foreach (Bill bi in bills)
                        {
                            bool convertInt = int.TryParse(key, out int result);
                            if (convertInt && bi.numberBill.Equals(result))
                            {
                                billSearch.Add(bi);
                                break;
                            }
                        }
                        break;
                    case "createDate":
                        foreach (Bill bi in bills)
                        {
                            //DateTime dateTime = DateTime.Parse(key.Trim());
                            if (bi.createDate.ToString().Contains(key.Trim()))
                            {
                                billSearch.Add(bi);
                            }
                        }
                        break;
                    default: break;
                }
                return billSearch;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }

        }
    }
}
