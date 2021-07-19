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
    public partial class frmItems : Form
    {
        public frmItems()
        {
            InitializeComponent();
        }

        private void frmItems_Load(object sender, EventArgs e)
        {
            Fill();
        }
        //פונקציה למילוי הנתונים בטופס
        private void Fill()
        {
            cbId.Items.Clear();
            cbSuppliers.Items.Clear();
            Item[] items = DBSQL.Instance.GetAllItems();
            Supplier[] suppliers = DBSQL.Instance.GetSuppliers();
            dgvItems.DataSource = items;
            for(int i = 0; i < items.Length; i++)
            {
                cbId.Items.Add(items[i].Id);
            }
            for(int i = 0; i < suppliers.Length; i++)
            {
                cbSuppliers.Items.Add(suppliers[i].Id);
            }
            
        }
        //מעבר לטופס הוספת מוצר
        private void btnAddItem_Click(object sender, EventArgs e)
        {
            AddItemForm addF = new AddItemForm();
            addF.ShowDialog();
            Fill();
        }
        //פונקציה למחיקת מוצר לפי המספר שלו
        private void btnDelItem_Click(object sender, EventArgs e)
        {
            Hashtable getId = new Hashtable();
            int id = Convert.ToInt32(cbId.SelectedItem);
            getId.Add("Id", id);
            if (getId.Keys.Count > 0)
            {
                DBSQL.Instance.DeleteItem(getId);
                cbId.Text = string.Empty;
                Fill();
            }
        }
        //פונקציה מעבר לעדכון המוצר
        private void btnUpdateItem_Click(object sender, EventArgs e)
        {
            UpdateItemForm upF = new UpdateItemForm();
            upF.ShowDialog();
            Fill();
        }
        //פונקציה לסינון תצוגת המוצרים לפי ספק
        private void button1_Click(object sender, EventArgs e)
        {
            Hashtable getSupplier = new Hashtable();
            int supplierId = Convert.ToInt32(cbSuppliers.SelectedItem);
            getSupplier.Add("SupplierId", supplierId);
            if (getSupplier.Keys.Count > 0)
            {
                dgvItems.DataSource = DBSQL.Instance.GetAllItems(getSupplier);
            }
        }
        
        private void btnReset_Click(object sender, EventArgs e)
        {
            
            Fill();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
