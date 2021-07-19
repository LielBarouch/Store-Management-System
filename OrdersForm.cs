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
    public partial class OrdersForm : Form
    {
        private Order order = null;
        private Item Item = null;
        public OrdersForm()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void OrdersForm_Load(object sender, EventArgs e)
        {
            Fill();
        }
        //פונקציה למילוי טבלת ההזמנות
        public void Fill()
        {
            dgvOrders.DataSource = DBSQL.Instance.GetOrders();
        }
        //פונקציה מעבר לטופס יצירת הזמנה
        private void btnAddOrder_Click(object sender, EventArgs e)
        {
            AddOrderForm addOrder = new AddOrderForm();
            addOrder.ShowDialog();
            Fill();
        }
    }
}
