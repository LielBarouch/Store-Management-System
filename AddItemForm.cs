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
    public partial class AddItemForm : Form
    {

        private Item item = null;
        public AddItemForm()
        {
            InitializeComponent();
        }
        
        private void AddItemForm_Load(object sender, EventArgs e)
        {
            FillSuppliers();
        }
        //פונקציה למילוי אופציות הספק בטופס
        private void FillSuppliers()
        {
            cbSuppliers.Items.Clear();
            Supplier[] suppliers = DBSQL.Instance.GetSuppliers();
            if (suppliers != null)
            {
                for(int i=0; i < suppliers.Length; i++)
                {
                    cbSuppliers.Items.Add(suppliers[i].Id);
                }
            }
        }

        
        //פונקציה ליצירת אובייקט מוצר חדש
        private void createNewItem()
        {
            item = new Item();
            item.Name = Convert.ToString(tbName.Text);
            item.Price = Convert.ToDouble(tbPrice.Text);
            item.UnitsInStock = Convert.ToInt32(tbUnits.Text);
            item.QtySold = 0;
            item.SupplierId = Convert.ToInt64(cbSuppliers.SelectedItem);
        }
        //פונקצייה לבדיקת הקלט
        private void notEmptyCheck(object sender, EventArgs e)
        {
            if (validationCheck())
            {
                createNewItem();
                btnAddNew.Enabled = true;
            }
            else
            {
                btnAddNew.Enabled = false;
                item = null;
            }
        }
        //פונקציית עזר לבדיקת תקינות הקלט
        private bool validationCheck()
        {
            string name = tbName.Text;
            string price = tbPrice.Text;
            string units = tbUnits.Text;
            object suppliers = cbSuppliers.SelectedItem;

            return name != string.Empty && price != string.Empty && price.All(char.IsDigit)
                && Convert.ToDouble(price) > 0 && units != string.Empty && units.All(char.IsDigit)
                && Convert.ToDouble(units) > 0 && suppliers != null;
        }
        //פונקציה ליצירת מוצר
        private void btnAddNew_Click(object sender, EventArgs e)
        {
            DBSQL.Instance.InsertItemToDB(item);
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
