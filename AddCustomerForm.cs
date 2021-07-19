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
    public partial class AddCustomerForm : Form
    {
        private Customer customer = null;
        public AddCustomerForm()
        {
            InitializeComponent();
        }
        //פונקציה ליצירת אובייקט לקוח חדש
        private void createNewCustomer()
        {
            customer = new Customer();
            customer.FirstName = tbFName.Text;
            customer.LastName = tbLName.Text;
            customer.Phone = tbPhone.Text;
            customer.Email = tbEmail.Text;
            customer.Address = tbAddress.Text;
        }

        //פונקציה שבודקת אם השדות מלאים
        private void notEmptyCheck(object sender, EventArgs e)
        {
            if (validationCheck())
            {
                createNewCustomer();
                btnAddNewC.Enabled = true;
            }
            else
            {
                btnAddNewC.Enabled = false;
                customer = null;
            }
        }
        //פונקציית עזר לבדיקת השדות
        private bool validationCheck()
        {
            string firstName = tbFName.Text;
            string lastName = tbLName.Text;
            string address = tbAddress.Text;
            string phone = tbPhone.Text;
            string email = tbEmail.Text;

            return firstName != string.Empty && lastName != string.Empty && address != string.Empty
            && phone != string.Empty && phone.All(char.IsDigit) && email != string.Empty;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //פונקציה ליצירת לקוח
        private void btnAddNewC_Click(object sender, EventArgs e)
        {
            DBSQL.Instance.InsertCustomer(customer);
        }
    }
}
