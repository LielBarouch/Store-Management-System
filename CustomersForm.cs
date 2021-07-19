using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace FInalP
{
    public partial class CustomersForm : Form
    {
        public CustomersForm()
        {
            InitializeComponent();
        }
        //פונקציה למילוי שדה הלקוחות וטבלת הלקוחות
        public void Fill()
        {
            Customer[] customers = DBSQL.Instance.GetCustomers();
            dgvCustomers.DataSource = customers;
            cbCustomer.Items.Clear();

            if (customers != null)
            {
                for(int i = 0; i < customers.Length; i++)
                {
                    cbCustomer.Items.Add(customers[i].Id);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CustomersForm_Load(object sender, EventArgs e)
        {
            Fill();
        }
        //פונקציה לכפתור יצירת הלקוח
        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            AddCustomerForm addCusF = new AddCustomerForm();
            addCusF.ShowDialog();
            Fill();
        }
        //פונקציה למחיקת לקוח לפי המספר שלו
        private void btnDelCustomer_Click(object sender, EventArgs e)
        {
            Hashtable getId = new Hashtable();
            int id = Convert.ToInt32(cbCustomer.SelectedItem);
            getId.Add("Id", id);
            if (getId.Keys.Count > 0)
            {
                DBSQL.Instance.DeleteCustomer(getId);
                cbCustomer.Text = string.Empty;
                Fill();
            }
        }
    }
}
