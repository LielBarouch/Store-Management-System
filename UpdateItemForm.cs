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
    public partial class UpdateItemForm : Form
    {
        private Item item = null;
        public UpdateItemForm()
        {
            InitializeComponent();
        }

        private void UpdateItemForm_Load(object sender, EventArgs e)
        {
            FillOldData();
        }
        //פונקציה למילוי הנתוני בסדות הבחירה של המוצר שאותו מעדכנים
        private void FillOldData()
        {
            cbSuppliers.Items.Clear();
            cbOldName.Items.Clear();
            Item[] items = DBSQL.Instance.GetAllItems();
            Supplier[] suppliers = DBSQL.Instance.GetSuppliers();
            if (suppliers != null)
            {
                for (int i = 0; i < suppliers.Length; i++)
                {
                    cbSuppliers.Items.Add(suppliers[i].Id);
                }
            }
            if (items != null)
            {
                for(int i = 0; i < items.Length; i++)
                {
                    cbOldName.Items.Add(items[i].Name);
                }
            }
        }
        //פונקציה לעדכון המוצר
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string oldName = Convert.ToString(cbOldName.Text);
            item = DBSQL.Instance.GetItemByName(oldName);

            if (oldName != string.Empty)
            {
                bool changeFlag = tbName.Text.Length >0;
                item.Name = changeFlag ? tbName.Text : oldName;
                item.Price = tbNewPrice.Text.Length > 0 ? Convert.ToDouble(tbNewPrice.Text) : item.Price;
                item.UnitsInStock = tbNewUnits.Text.Length > 0 ? Convert.ToInt32(tbNewUnits.Text) : item.UnitsInStock;
                item.SupplierId = cbSuppliers.SelectedItem != null ? Convert.ToInt64(cbSuppliers.SelectedItem) : item.SupplierId;

                DBSQL.Instance.UpdateItemByName(item, oldName, changeFlag);
                this.Close();
            }
        }
    }
}
