using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FInalP
{
    public class DBSQL:DBcon
    {
        private static DBSQL instance;

        private DBSQL()//Consructor for the class
        {
            baseDatabaseName = "store";
            baseUserName = "root";
            basePassword = string.Empty;
        }

        public static DBSQL Instance//Create a new instance with sql connection if the instance is empty
        {
            get
            {
                if (instance == null)
                {
                    DBcon connection = DBcon.Instance();
                    instance = new DBSQL();
                }
                return instance;
            }
        }

        public static string DatabaseName
        {
            set
            {
                if (value != string.Empty)
                {
                    baseDatabaseName = value;
                }
            }
            get
            {
                return baseDatabaseName;
            }
        }

        public static string UserName
        {
            set
            {
                if (value != string.Empty)
                {
                    baseUserName = value;
                }
            }
            get
            {
                return baseUserName;
            }
        }

        public static string Password
        {
            set
            {
                if (value != string.Empty)
                {
                    basePassword = value;
                }
            }
            get
            {
                return basePassword;
            }
        }
        //פונקציה ליצור ולהחזיר מחרוזת משנה לשאילתה עם כל הפרמטרים שנבחרו
        public string WhereBuilder(Hashtable queryParams, string cmdStr)
        {
            string substr = "";
            foreach (DictionaryEntry entry in queryParams)
            {
                substr += (substr.Length > 0 ? " AND " : "") + entry.Key + "=" + (entry.Value is string ? "'" + entry.Value + "'" : entry.Value);
            }
            if (substr.Length > 0) 
            {
                cmdStr += " WHERE " + substr;
            }
            return cmdStr;
        }
        //פוקצייה לקבלת המוצרים
        public Item[] GetAllItems()
        {
            string cmdStr = "SELECT * FROM Items";
            return ItemDetails(cmdStr);
        }
        //פונקצייה לקבלת המוצרים עם תנאי
        public Item[] GetAllItems(Hashtable queryPrm)
        {
            string cmdStr = "SELECT * FROM Items";
            cmdStr = WhereBuilder(queryPrm, cmdStr);
            return ItemDetails(cmdStr);
        }
        //פונקציה לקבלת מוצר לפי מספר מזהה
        public Item GetItemById(long id)
        {
            DataSet ds = new DataSet();
            Item item = null;
            string cmdStr = "SELECT * FROM Items WHERE Id=@id";

            using(MySqlCommand command=new MySqlCommand(cmdStr))
            {
                command.Parameters.AddWithValue("@id", id);
                ds = GetMultipleQuery(command);
            }
            return OneItemDetails(ds, item);
        }

        //פונקציה קבלת מוצר לפי שם שלו
        public Item GetItemByName(string name)
        {
            DataSet ds = new DataSet();
            Item item = null;
            string cmdStr = "SELECT * From Items WHERE ItemName=@name";
            using(MySqlCommand command=new MySqlCommand(cmdStr))
            {
                command.Parameters.AddWithValue("@name", name);
                ds = GetMultipleQuery(command);
            }
            return OneItemDetails(ds, item);
        }
        //עדכון כמות מוצר וכמות יחידות שנמכרו מהמוצר
        public void UpdateItem(Item item)
        {
            string cmdStr = "UPDATE Items SET UnitsInStock=@UnitsInStock, QtySold=@QtySold WHERE Id=@id";

            using (MySqlCommand command = new MySqlCommand(cmdStr))
            {
                command.Parameters.AddWithValue("@id", item.Id);
                command.Parameters.AddWithValue("@UnitsInStock", item.UnitsInStock);
                command.Parameters.AddWithValue("@QtySold", item.QtySold);

                base.ExecuteSimpleQuery(command);
            } 
        }

        //פונקציה לעדכון מוצר לפי השם שלו
        public void UpdateItemByName(Item item,string oldName, bool changeFlag)
        {
            string cmdStr = "UPDATE Items " +
                "SET ItemName=@ItemName, Price=@Price, UnitsInStock=@UnitsInStock, QtySold=@QtySold, SupplierId=@SupplierId " +
                "WHERE ItemName=@OldName";

            if (InsertOrUpdateItem(cmdStr, item, oldName, changeFlag) > 0)
            {
                MessageBox.Show(oldName + " updated!");
            }
            else
            {
                MessageBox.Show("Error! cant update " + oldName);
            }
        }

        //פןנקציה עזר לקבלת המידע על המוצר 
        private Item OneItemDetails(DataSet ds,Item item)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = ds.Tables[0];
            }
            catch { }

            if (dt.Rows.Count > 0)
            {
                item = new Item();
                item.Id = Convert.ToInt64(dt.Rows[0][0].ToString());
                item.Name = dt.Rows[0][1].ToString();
                item.Price = Convert.ToDouble(dt.Rows[0][2].ToString());
                item.UnitsInStock = Convert.ToInt32(dt.Rows[0][3].ToString());
                item.QtySold = Convert.ToInt32(dt.Rows[0][4].ToString());
                item.SupplierId = Convert.ToInt64(dt.Rows[0][5].ToString());
            }
            return item;
        }

        //פונקצית עזר לקבלת המוצרים
        private Item[] ItemDetails(string cmdStr)
        {
            Item[] items = null;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            using(MySqlCommand command =new MySqlCommand(cmdStr))
            {
                ds = GetMultipleQuery(command);
            }
            try
            {
                dt = ds.Tables[0];
            }
            catch { }

            if(dt.Rows.Count>0)
            {
                items = new Item[dt.Rows.Count];
                for (int i = 0; i < items.Length; i++)
                {
                    items[i] = new Item();
                    items[i].Id = Convert.ToInt64(dt.Rows[i][0].ToString());
                    items[i].Name = dt.Rows[i][1].ToString();
                    items[i].Price = Convert.ToDouble(dt.Rows[i][2].ToString());
                    items[i].UnitsInStock = Convert.ToInt32(dt.Rows[i][3].ToString());
                    items[i].QtySold = Convert.ToInt32(dt.Rows[i][4].ToString());
                    items[i].SupplierId = Convert.ToInt64(dt.Rows[i][5].ToString());
                }
            }
            return items;
        }
        //פונקציה להכנסת ספק
        public void InsertSupplier(Supplier supplier)
        {
            string cmdStr = "INSERT INTO Suppliers(SupplierName, Phone)VALUES(@Name,@Phone)";
            if (InsertSupplierToDB(cmdStr, supplier, true) > 0)
            {
                MessageBox.Show("You just added a new supplier!");
            }
            else
            {
                MessageBox.Show("Error! cant add a new supplier!");
            }
        }
        //פונקציה למחיקת ספק
        public void DeleteSupplier(Hashtable queryP)
        {
            string cmdStr = "DELETE FROM Suppliers";
            cmdStr = WhereBuilder(queryP, cmdStr);

            using(MySqlCommand command=new MySqlCommand(cmdStr))
            {
                if (base.ExecuteSimpleQuery(command) > 0)
                {
                    MessageBox.Show("Hey! You just deleted a supplier!");
                }
                else
                {
                    MessageBox.Show("Error! cant delete supplier!");
                }
            }
        }
        //פונקציה המכניסה או מעדכנת את הספק בבסיס הנתונים
        public int InsertSupplierToDB(string cmdStr, Supplier supplier, bool change)
        {
            using (MySqlCommand command = new MySqlCommand(cmdStr))
            {
                if (CheckIfItemExist(supplier.Name) > 0 && change)
                {
                    MessageBox.Show("Supplier already exists in stock!");
                }
                else
                {
                    command.Parameters.AddWithValue("@Name", supplier.Name);
                    command.Parameters.AddWithValue("@Phone", supplier.Phone);
                    
                    return base.ExecuteSimpleQuery(command);
                }
            }
            return 0;
        }

        //פונקציה לקבלת ספק
        public Supplier[] GetSuppliers()
        {
            string cmdStr = "SELECT * FROM Suppliers";
            return SupplierDetails(cmdStr);
        }
        //פונקציה לקבלת ספק לפי תנאי
        public Supplier[] GetSuppliers(Hashtable queryPrm)
        {
            string cmdStr = "SELECT * FROM Suppliers";
            cmdStr = WhereBuilder(queryPrm, cmdStr);
            return SupplierDetails(cmdStr);
        }
        //פונקציית עזר לקבלת מידע על הספקים
        public Supplier[] SupplierDetails(string cmdStr)
        {
            Supplier[] suppliers = null;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            using (MySqlCommand command = new MySqlCommand(cmdStr))
            {
                ds = GetMultipleQuery(command);
            }
            try
            {
                dt = ds.Tables[0];
            }
            catch { }

            if (dt.Rows.Count > 0)
            {
                suppliers = new Supplier[dt.Rows.Count];
                for (int i = 0; i < suppliers.Length; i++)
                {
                    suppliers[i] = new Supplier();
                    suppliers[i].Id = Convert.ToInt64(dt.Rows[i][0].ToString());
                    suppliers[i].Name = dt.Rows[i][1].ToString();
                    suppliers[i].Phone = dt.Rows[i][2].ToString();
                }
            }
            return suppliers;
        }
        //פונקציה להחדרת המוצר החדש לתוך הDB
        public void InsertItemToDB(Item item)
        {
            string cmdStr="INSERT INTO Items (ItemName,Price,UnitsInStock,QtySold,SupplierId)"+
                "VALUES(@ItemName,@Price,@UnitsInStock,@QtySold,@SupplierId)";
            if (InsertOrUpdateItem(cmdStr, item, "", true) > 0)
            {
                MessageBox.Show("You just added a new item to stock!");
            }
            else
            {
                MessageBox.Show("Error, cant add item to stock!");
            }
        }
        //פונקציית עזר לשליחת להכנסת מוצר חדש לתוך הבסיס מידע שלנו או עידכונו
        public int InsertOrUpdateItem(string cmdStr,Item item,string oldItemName,bool change)
        {
            using(MySqlCommand command=new MySqlCommand(cmdStr))
            {
                if (CheckIfItemExist(item.Name) > 0 && change)
                {
                    MessageBox.Show("Item already exists in stock!");
                }
                else
                {
                    command.Parameters.AddWithValue("@ItemName", item.Name);
                    command.Parameters.AddWithValue("@Price", item.Price);
                    command.Parameters.AddWithValue("@UnitsInStock", item.UnitsInStock);
                    command.Parameters.AddWithValue("@QtySold", item.QtySold);
                    command.Parameters.AddWithValue("@SupplierId", item.SupplierId);

                    if (cmdStr.Contains("OldName"))
                    {
                        command.Parameters.AddWithValue("OldName", oldItemName);
                    }

                    return base.ExecuteSimpleQuery(command);
                }
            }
            return 0;
        }
        //פונקצייה לבדוק אם המוצר קיים כבר
        public int CheckIfItemExist(string itemName)
        {
            int check;
            string cmdStr = "SELECT COUNT(*) FROM Items WHERE ItemName=@itemName";
            using(MySqlCommand command=new MySqlCommand(cmdStr))
            {
                command.Parameters.AddWithValue("@itemName", itemName);
                check = ExecuteScalarIntQuery(command);
            }
            return check;
        }
        //פונקציה למחיקת מוצר
        public void DeleteItem(Hashtable queryPrm)
        {
            string cmdStr = "DELETE FROM Items";
            cmdStr = WhereBuilder(queryPrm, cmdStr);
            using(MySqlCommand command=new MySqlCommand(cmdStr))
            {
                if (base.ExecuteSimpleQuery(command) > 0)
                {
                    MessageBox.Show("Item Deleted!");
                }
                else
                {
                    MessageBox.Show("Error, cant delete item!");
                }
            }
        }
        //פונקציה להכנסת לקוח למערכת, יוצרת שאילתה
        public void InsertCustomer(Customer customer)
        {
            string cmdStr = "INSERT INTO Customers(FirstName,LastName,Phone,Address,Email)" + "" +
                "Values(@FirstName, @LastName, @Phone, @Address, @Email)";
            if (InsertCustomerToDB(customer, cmdStr) > 0)
            {
                MessageBox.Show("Hey! You just added a new customer!");
            }
            else
            {
                MessageBox.Show("Error! cant add a new customer!");
            }
        }
        //פונקצית עזר להכנסת הלקוח לבסיס הנתונים
        private int InsertCustomerToDB(Customer customer,string cmdStr)
        {
            using(MySqlCommand command=new MySqlCommand(cmdStr))
            {
                command.Parameters.AddWithValue("@FirstName", customer.FirstName);
                command.Parameters.AddWithValue("@LastName", customer.LastName);
                command.Parameters.AddWithValue("@Phone", customer.Phone);
                command.Parameters.AddWithValue("@Address", customer.Address);
                command.Parameters.AddWithValue("@Email", customer.Email);

                return base.ExecuteSimpleQuery(command);
            }
        }

        //פונקצית עזר לקבלת לקוחות
        public Customer[] GetCustomers()
        {
            string cmdStr = "SELECT * FROM Customers";
            
            return CustomerDetails(cmdStr);
        }

        //פונקצית עזר לקבלת לקוחות על פי תנאי
        public Customer[] GetCustomers(Hashtable queryP)
        {
            string cmdStr = "SELECT * FROM Customers";
            cmdStr = WhereBuilder(queryP, cmdStr);
            return CustomerDetails(cmdStr);
        }
        //פונקציה למחיקת לקוח
        public void DeleteCustomer(Hashtable queryP)
        {
            string cmdStr = "DELETE FROM Customers";
            cmdStr = WhereBuilder(queryP, cmdStr);
            using(MySqlCommand command=new MySqlCommand(cmdStr))
            {
                if (base.ExecuteSimpleQuery(command) > 0)
                {
                    MessageBox.Show("You just deleted a customer!");
                }
                else
                {
                    MessageBox.Show("Error! cant delete the customer!");
                }
            }
        }

        //פונקציה המחזירה מערך של לקוחות מהבסיס נתונים
        private Customer[] CustomerDetails(string cmdStr)
        {
            Customer[] customers = null;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            using(MySqlCommand command=new MySqlCommand(cmdStr))
            {
                ds = GetMultipleQuery(command);
            }
            try
            {
                dt = ds.Tables[0];
            }
            catch { }

            if (dt.Rows.Count > 0)
            {
                customers = new Customer[dt.Rows.Count];

                for(int i = 0; i < customers.Length; i++)
                {
                    customers[i] = new Customer();
                    customers[i].Id = Convert.ToInt64(dt.Rows[i][0].ToString());
                    customers[i].FirstName = dt.Rows[i][1].ToString();
                    customers[i].LastName = dt.Rows[i][2].ToString();
                    customers[i].Phone = dt.Rows[i][3].ToString();
                    customers[i].Address = dt.Rows[i][4].ToString();
                    customers[i].Email = dt.Rows[i][5].ToString();
                }
            }
            return customers;
        }
        //פונקציה ליצירת הזמנה, יוצר שאילתה
        public void InsertOrder(Order order)
        {
            string cmdStr= "INSERT INTO Orders(Price, OrderDate, Count, CustomerId, ItemId)"+
                "VALUES(@Price, @OrderDate, @Count, @CustomerId, @ItemId)";
            if (OrderToDB(cmdStr, order) > 0)
            {
                MessageBox.Show("Hey! you created an order!");
            }
            else
            {
                MessageBox.Show("Error! cant create an order!");
            }
        }
        //פונקציית עזר להכנסת ההזמנה לבסיס הנתונים
        private int OrderToDB(string cmdStr,Order order)
        {
            using(MySqlCommand command=new MySqlCommand(cmdStr))
            {
                command.Parameters.AddWithValue("@Price", order.Price);
                command.Parameters.AddWithValue("@OrderDate", order.OrderDate);
                command.Parameters.AddWithValue("@Count", order.Count);
                command.Parameters.AddWithValue("@CustomerId", order.CustomerId);
                command.Parameters.AddWithValue("@ItemId", order.ItemId);

                return base.ExecuteSimpleQuery(command);
            }
        }

        //פונקציה לקבלת ההזמנות
        public Order[] GetOrders()
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            Order[] orders = null;
            string cmdStr = "SELECT * FROM Orders";

            using(MySqlCommand command=new MySqlCommand(cmdStr))
            {
                ds = GetMultipleQuery(command);
            }

            try
            {
                dt = ds.Tables[0];
            }
            catch { }

            if (dt.Rows.Count > 0)
            {
                orders = new Order[dt.Rows.Count];

                for(int i = 0; i < orders.Length; i++)
                {
                    orders[i] = new Order();
                    orders[i].Id = Convert.ToInt32(dt.Rows[i][0].ToString());
                    orders[i].Price = Convert.ToDouble(dt.Rows[i][1].ToString());
                    orders[i].OrderDate = dt.Rows[i][2].ToString();
                    orders[i].Count = Convert.ToInt32(dt.Rows[i][3].ToString());
                    orders[i].CustomerId = Convert.ToInt32(dt.Rows[i][4].ToString());
                    orders[i].ItemId = Convert.ToInt32(dt.Rows[i][5].ToString());
                }
            }
            return orders;
        }

        //פןנקציה המחזירה את סה"כ הכנסות
        public int GetTotalEarnings()
        {
            int total;
            string cmdStr = "SELECT SUM(Price) FROM Orders";

            using(MySqlCommand command=new MySqlCommand(cmdStr))
            {
                total = ExecuteScalarIntQuery(command);
            }
            return total;
        }


        //פונקציה המחזירה את כמות הלקוחות שלנו
        public int GetCustomersCount()
        {
            int CustomersCount;
            string cmdStr = "SELECT COUNT(*) FROM Customers";

            using (MySqlCommand command = new MySqlCommand(cmdStr))
            {
                CustomersCount = ExecuteScalarIntQuery(command);
            }
            return CustomersCount;
        }

    }
}
