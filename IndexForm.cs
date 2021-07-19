using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FInalP
{
    public partial class frmHome : Form
    {
        private DBSQL mySQL;
        //חיבור לבסיס הנתונים
        public frmHome()
        {
            InitializeComponent();
            DBSQL.DatabaseName = "store";
            DBSQL.UserName = "root";
            DBSQL.Password = string.Empty;

            mySQL = DBSQL.Instance;
        }

        private void btnItems_Click(object sender, EventArgs e)
        {
            frmItems formItem = new frmItems();
            formItem.ShowDialog();
            Fill();
        }

        private void btnSuppliers_Click(object sender, EventArgs e)
        {
            frmSuppliers formSup = new frmSuppliers();
            formSup.ShowDialog();
            Fill();
        }

        private void btnCustomers_Click(object sender, EventArgs e)
        {
            CustomersForm formCus = new CustomersForm();
            formCus.ShowDialog();
            Fill();
        }

        private void btnOrders_Click(object sender, EventArgs e)
        {
            OrdersForm formOrders = new OrdersForm();
            formOrders.ShowDialog();
            Fill();
        }

        private void frmHome_Load(object sender, EventArgs e)
        {
            Fill();
        }
        //פונקציה למילוי נתונים בטופס
        private void Fill()
        {
            tbTotal.Text = mySQL.GetTotalEarnings().ToString() + "$";
            tbCustomers.Text = mySQL.GetCustomersCount().ToString();
        }
        //פונקציה ליצירת דוח מכירות
        private void btnReport_Click(object sender, EventArgs e)
        {
            PDFClass report = new PDFClass();
            report.Title();
            report.CreateTable(DBSQL.Instance.GetOrders());
            MessageBox.Show(File.Exists("C:/temp/sales-report.pdf") ? "The report created in C:/temp folder" : "Error! cant create the report!");
            report.CloseReport();
        }
    }
}
