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
    public partial class AddSupplierForm : Form
    {
        private Supplier supplier = null;
        public AddSupplierForm()
        {
            InitializeComponent();
        }
        //פונקציה ליצירת ספק
        private void btnAddNew_Click(object sender, EventArgs e)
        {
            DBSQL.Instance.InsertSupplier(supplier);
        }
        //פונקציה ליצירת אובייקט ספק
        private void createSupplier()
        {
            supplier = new Supplier();
            supplier.Name = tbSupName.Text;
            supplier.Phone = tbPhone.Text;
        }
        //פונקציה לבדיקת תקינות הקלט
        private void notEmptyCheck(object sender, EventArgs e)
        {
            if (tbPhone.Text != string.Empty && tbPhone.Text != string.Empty && tbPhone.Text.All(char.IsDigit))
            {
                createSupplier();
                btnAddNew.Enabled = true;
            }
            else
            {
                supplier = null;
                btnAddNew.Enabled = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
