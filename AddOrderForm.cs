using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FInalP
{
    public partial class AddOrderForm : Form
    {

        private Item item = null;
        private Order order = null;
        public AddOrderForm()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddOrderForm_Load(object sender, EventArgs e)
        {
            Fill();
        }
        //מילוי השדות בחירה
        private void Fill()
        {
            Item[] items = DBSQL.Instance.GetAllItems();
            Customer[] customers = DBSQL.Instance.GetCustomers();

            if (items != null)
            {
                for(int i = 0; i < items.Length; i++)
                {
                    if (items[i].UnitsInStock != 0)
                    {
                        cbItem.Items.Add(items[i].Id);
                    }
                }
            }

            if (customers != null)
            {
                for(int i = 0; i < customers.Length; i++)
                {
                    cbCustomer.Items.Add(customers[i].Id);
                }
            }
        }
        //יצירת אובייקט הזמנה
        private void createOrder()
        {
            DateTime date = DateTime.Today;
            order = new Order();
            order.CustomerId = Convert.ToInt64(cbCustomer.SelectedItem);
            order.Count = Convert.ToInt32(nudAmount.Value);
            order.Price = item.Price * order.Count;
            order.OrderDate = Convert.ToString(date.ToString("dd/MM/yyyy"));
            order.ItemId = Convert.ToInt64(cbItem.SelectedItem);
            tbTotal.Text = Convert.ToString(order.Price);
        }

        
        //פונקציה לקבלת מספרי מוצרים והצגתם
        private void cbItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            order = new Order();
            order.ItemId = cbItem.SelectedItem != null ? Convert.ToInt64(cbItem.SelectedItem) : 0;
            if (order.ItemId > 0)
            {
                item = DBSQL.Instance.GetItemById(order.ItemId);
                nudAmount.Maximum = item.UnitsInStock;
                nudAmount.Minimum = 1;
            }
            
        }
        //בדיקת שדות הקלט
        private void checkIfEmpty(object sender, EventArgs e)
        {
            item = DBSQL.Instance.GetItemById(Convert.ToInt64(cbItem.SelectedItem));
            long customer = Convert.ToInt64(cbCustomer.SelectedItem);
            if (item != null && customer > 0)
            {
                createOrder();
                btnPlaceOrder.Enabled = true;
            }
            else
            {
                btnPlaceOrder.Enabled = false;
                order = null;
                tbTotal.Text = string.Empty;
            }
        }
        //פונקציה ליצירת הזמנה
        private void btnPlaceOrder_Click(object sender, EventArgs e)
        {
            DBSQL.Instance.InsertOrder(order);
            item.QtySold += order.Count;
            item.UnitsInStock -= order.Count;
            DBSQL.Instance.UpdateItem(item);
        }
    }
}
