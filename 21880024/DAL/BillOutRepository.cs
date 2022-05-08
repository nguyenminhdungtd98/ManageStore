using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _21880024.Entities;
using _21880024.Services;
using Newtonsoft.Json;
using System.IO;
namespace _21880024.DAL

{
    public class BillOutRepository
    {
        static BillOutRepository instance = null;
        private static List<BillOut> billOuts;
        private static List<ProductInBill> productInBills;
        public BillOutRepository()
        {
            billOuts = new List<BillOut>();
            productInBills = new List<ProductInBill>();
        }
        public static BillOutRepository getInstance()
        {
            if (instance == null)
            {
                instance = new BillOutRepository();
            }
            return instance;
        }

        private static int numberBillOutCurrent = 0;

        public BillOut findById(int id)
        {
            BillOut BillOut = new BillOut();
            foreach (BillOut item in billOuts)
            {
                if (item.numberBillOut.Equals(id))
                {
                    return item;
                }
            }

            return BillOut;

        }
        public List<BillOut> findAll()
        {
            try
            {
                billOuts = loadData();
                return billOuts;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        public List<ProductInBill> findAllProductInBill()
        {
            try
            {
                productInBills = loadDataProductInBill();
                return productInBills;
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
                billOuts = loadData();
                for (int i = 0; i < billOuts.Count; i++)
                {
                    if (billOuts[i].numberBillOut.Equals(id))
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
        public int add(BillOut billOut)
        {
            try
            {
                loadData();
                if (numberBillOutCurrent.Equals(0))
                {
                    numberBillOutCurrent = 1;
                    billOut.numberBillOut = numberBillOutCurrent;
                }

                if (!billOut.Equals(null))
                {
                    if (billOuts.Count > 0)
                    {
                        billOut.numberBillOut = getMaxId() + 1;
                    }
                    billOuts.Add(billOut);
                    if (SaveFileData(billOuts))
                    {
                        numberBillOutCurrent++;
                        List<ProductInBill> deleteProducts = new List<ProductInBill>();
                        SaveFileData(deleteProducts);
                        return billOut.numberBillOut;
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
        public bool SaveFileData(List<BillOut> billOuts)
        {
            try
            {
                string productsUpdate = JsonConvert.SerializeObject(billOuts);
                using (var output = new StreamWriter(@"D:\StudyDoc\IT\Học kì 2\Kĩ thuật lập trình\21880024\21880024\Data\BillOut.json"))
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
        public List<BillOut> loadData()
        {
            try
            {
                string billOutsJson = "";
                billOutsJson = System.IO.File.ReadAllText(@"D:\StudyDoc\IT\Học kì 2\Kĩ thuật lập trình\21880024\21880024\Data\BillOut.json");
                List<BillOut> billOutsTemp = JsonConvert.DeserializeObject<List<BillOut>>(billOutsJson);
                if (billOutsTemp != null)
                {
                    billOuts = billOutsTemp;
                }

                return billOuts;
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
                foreach (BillOut BillOut in billOuts)
                {
                    if (maxId < BillOut.numberBillOut)
                    {
                        maxId = BillOut.numberBillOut;
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
        public bool delete(int id)
        {
            bool result = false;
            try
            {
                billOuts = loadData();
                int index = Error.NOT_FOUND;
                int size = billOuts.Count;
                for (int i = 0; i < size; i++)
                {
                    if (billOuts[i].numberBillOut.Equals(id))
                    {
                        index = i;
                        break;
                    }
                }
                billOuts.RemoveAt(index);
                result = SaveFileData(billOuts);
                if (billOuts.Count.Equals(size - 1) && result)
                {
                    return result;
                }
                else
                {
                    return result;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return result;
            }
        }
        public int update(BillOut BillOut, int idCurrent)
        {
            try
            {
                int index = checkExist(idCurrent);
                if (index >= 0)
                {
                    billOuts.RemoveAt(index);
                    SaveFileData(billOuts);
                }
                else
                {
                    return Error.NOT_FOUND;
                }
                int id = add(BillOut);
                if (id > 0)
                {
                    SaveFileData(billOuts);
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
        public List<BillOut> search(string typeSearch, string key)
        {
            loadData();
            List<BillOut> billOutsearch = new List<BillOut>();
            try
            {
                switch (typeSearch)
                {
                    case "numberBillOut":
                        foreach (BillOut bi in billOuts)
                        {
                            bool convertInt = int.TryParse(key, out int result);
                            if (convertInt && bi.numberBillOut.Equals(result))
                            {
                                billOutsearch.Add(bi);
                                break;
                            }
                        }
                        break;
                    case "createDate":
                        foreach (BillOut bi in billOuts)
                        {
                            //DateTime dateTime = DateTime.Parse(key.Trim());
                            if (bi.createDate.ToString().Contains(key.Trim()))
                            {
                                billOutsearch.Add(bi);
                            }
                        }
                        break;
                    default: break;
                }
                return billOutsearch;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }

        }
        public bool addProductInBill(ProductInBill productInBill)
        {
            try
            {
                loadDataProductInBill();
                productInBills.Add(productInBill);
                SaveFileData(productInBills);
                return true;
            }catch(Exception e)
            {
                return false;
            }
        }
        public List<ProductInBill> loadDataProductInBill()
        {
            try
            {
                string productInBillJson = "";
                productInBillJson = System.IO.File.ReadAllText(@"D:\StudyDoc\IT\Học kì 2\Kĩ thuật lập trình\21880024\21880024\Data\ProductInBill.json");
                List<ProductInBill> productInBillsTemp = JsonConvert.DeserializeObject<List<ProductInBill>>(productInBillJson);
                if (productInBillsTemp != null)
                {
                    productInBills = productInBillsTemp;
                }

                return productInBills;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        public bool SaveFileData(List<ProductInBill> productInBills)
        {
            try
            {
                string productInBillsJson = JsonConvert.SerializeObject(productInBills);
                using (var output = new StreamWriter(@"D:\StudyDoc\IT\Học kì 2\Kĩ thuật lập trình\21880024\21880024\Data\ProductInBill.json"))
                {
                    if (null != productInBillsJson)
                    {
                        // optionally modify line.
                        output.WriteLine(productInBillsJson);
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
        public int checkExistProductInBill(ProductInBill product)
        {
            try
            {
                loadDataProductInBill();
                int index = Error.NOT_FOUND;
                for (int i = 0; i < productInBills.Count; i++)
                {
                    if (product.productNumber.Equals(productInBills[i].productNumber))
                    {
                        return i;
                    }
                }
                return index;

            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return Error.ERROR;

            }
        }
        public bool updateNumberProductInBill(ProductInBill product, int indexCurrent)
        {
            try
            {
                loadDataProductInBill();
                int numberOld = productInBills[indexCurrent].number;
                int totalOld = productInBills[indexCurrent].total;
                product.number = product.number + numberOld;

                product.total = product.total + totalOld;
                productInBills[indexCurrent] = product;
                SaveFileData(productInBills);
                return true;
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        public bool updateProductInBill(ProductInBill product, int indexCurrent)
        {
            try
            {
                loadDataProductInBill();
                productInBills[indexCurrent] = product;
                SaveFileData(productInBills);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        public bool deleteProductInBill(int id)
        {
            bool result = false;
            try
            {
                loadDataProductInBill();
                int index = Error.NOT_FOUND;
                int size = productInBills.Count;
                for (int i = 0; i < size; i++)
                {
                    if (productInBills[i].productNumber.Equals(id))
                    {
                        index = i;
                        break;
                    }
                }
                productInBills.RemoveAt(index);
                result = SaveFileData(productInBills);
                if (productInBills.Count.Equals(size - 1) && result)
                {
                    return result;
                }
                else
                {
                    return result;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return result;
            }
        }
    }
}
