using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace FInalP
{
    class PDFClass
    {
        private Document doc;
        //Constructor
        public PDFClass()
        {
            doc = new Document();
            iTextSharp.text.pdf.PdfWriter.GetInstance(doc, new FileStream("C:/temp/sales-report.pdf", FileMode.Create,FileAccess.ReadWrite));
            doc.Open();
        }
        //פונקציה ליצירת הכותרת
        public void Title()
        {
            Font titleFont = new Font(Font.FontFamily.HELVETICA, 20, Font.BOLD);
            titleFont.Color = BaseColor.GREEN;
            Paragraph title = new Paragraph("Sale Report", titleFont);
            title.Alignment = Element.ALIGN_CENTER;
            doc.Add(title);
            doc.Add(new Paragraph("\n\n"));
        }
        //פונקציה ליצירת הטבלה
        public void CreateTable(Order[] table)
        {
            Font font = new Font(Font.FontFamily.HELVETICA, 10, Font.NORMAL);
            PdfPTable pdfTable = new PdfPTable(6);
            pdfTable.HorizontalAlignment = Element.ALIGN_CENTER;
            PdfPCell cell = new PdfPCell();
            cell.BorderColor = BaseColor.DARK_GRAY;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;

            CreateTableHeader(cell, pdfTable, font);
            CreateTableData(cell, pdfTable, font, table);
            doc.Add(pdfTable);
        }
        //פוקנציה ליצירת כותרות העמודות
        private void CreateTableHeader(PdfPCell cell,PdfPTable pdfPTable,Font font)
        {
            cell.BackgroundColor = BaseColor.GRAY;
            cell.Phrase = new Phrase("ID", font);
            pdfPTable.AddCell(cell);
            cell.Phrase = new Phrase("PRICE", font);
            pdfPTable.AddCell(cell);
            cell.Phrase = new Phrase("DATE", font);
            pdfPTable.AddCell(cell);
            cell.Phrase = new Phrase("COUNT", font);
            pdfPTable.AddCell(cell);
            cell.Phrase = new Phrase("COSTUMER", font);
            pdfPTable.AddCell(cell);
            cell.Phrase = new Phrase("ITEM", font);
            pdfPTable.AddCell(cell);
        }
        //פונקציה להצגת הנתונים בטלה
        private void CreateTableData(PdfPCell cell, PdfPTable pdfPTable, Font font,Order[] table)
        {
            cell.BackgroundColor = BaseColor.WHITE;
            for(int i = 0; i < table.Length; i++)
            {
                cell.Phrase = new Phrase((table[i]).Id.ToString(), font);
                pdfPTable.AddCell(cell);
                cell.Phrase = new Phrase((table[i]).Price.ToString(), font);
                pdfPTable.AddCell(cell);
                cell.Phrase = new Phrase((table[i]).OrderDate.ToString(), font);
                pdfPTable.AddCell(cell);
                cell.Phrase = new Phrase((table[i]).Count.ToString(), font);
                pdfPTable.AddCell(cell);
                cell.Phrase = new Phrase((table[i]).CustomerId.ToString(), font);
                pdfPTable.AddCell(cell);
                cell.Phrase = new Phrase((table[i]).ItemId.ToString(), font);
                pdfPTable.AddCell(cell);
            }
        }

        public void CloseReport()
        {
            doc.Close();
        }
    }
}
