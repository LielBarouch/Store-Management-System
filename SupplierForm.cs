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
    public partial class frmSuppliers : Form
    {
        public frmSuppliers()
        {
            InitializeComponent();
        }

        private void SupplierForm_Load(object sender, EventArgs e)
        {
            Fill();
        }
        //פונקציית מעבר לטופס יצירת ספק
        private void btnAddSupplier_Click(object sender, EventArgs e)
        {
            AddSupplierForm addSup = new AddSupplierForm();
            addSup.ShowDialog();
            Fill();

        }
        //פונקציה למילוי הנתונים בטופס
        private void Fill()
        {
            cbSup.Items.Clear();
            Supplier[] suppliers = DBSQL.Instance.GetSuppliers();
            dgvSuppliers.DataSource = suppliers;
            for (int i = 0; i < suppliers.Length; i++)
            {
                cbSup.Items.Add(suppliers[i].Id);
            }
        }
        //פונקציה למחיקת ספק לפי המספר שלו
        private void btnDelSupplier_Click(object sender, EventArgs e)
        {
            Hashtable getId = new Hashtable();
            int id = Convert.ToInt32(cbSup.SelectedItem);
            getId.Add("Id", id);
            if (getId.Keys.Count > 0)
            {
                DBSQL.Instance.DeleteSupplier(getId);
                cbSup.Text = string.Empty;
                Fill();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

