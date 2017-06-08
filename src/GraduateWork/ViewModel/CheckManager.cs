using DatabaseService;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ViewModel
{
    public class CheckManager
    {
        public DataService Service { get; set; }

        public CheckManager(DatabaseService.DataService service)
        {
            Service = service;
        }



        public void CreateSellCheck(List<Selling> sellings)
        {

            FileStream fs = new FileStream($"продаж_{sellings.First().Kod}.pdf", FileMode.Create, FileAccess.Write, FileShare.None);

            Document doc = new Document(PageSize.A6);

            PdfWriter writer = PdfWriter.GetInstance(doc, fs);

            String FONT_LOCATION = @"C:\Windows\Fonts\arial.ttf";
            BaseFont baseFont = BaseFont.CreateFont(FONT_LOCATION, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            iTextSharp.text.Font font = new iTextSharp.text.Font(baseFont, iTextSharp.text.Font.DEFAULTSIZE, iTextSharp.text.Font.NORMAL);
            iTextSharp.text.Font italicFont = new iTextSharp.text.Font(baseFont, iTextSharp.text.Font.DEFAULTSIZE, iTextSharp.text.Font.ITALIC);


            doc.Open();

            doc.Add(new Paragraph($"Код покупки: {sellings.First().Kod}", font));
            doc.Add(new Paragraph($"Клієнт: {sellings.First().Client.FirstName} {sellings.First().Client.LastName}", font));
            doc.Add(new Paragraph("jnhj"));

            var tableWidth = new float[] { 2, 2, 2, 2, 2 };

            var table = new PdfPTable(tableWidth);


            table.AddCell("gfdgdfg");
            table.AddCell("fsdf");
            table.AddCell("лдваплд");
            table.AddCell("fsdf");
            table.AddCell("fsdf");
            table.AddCell("fsdf");
            table.AddCell("fsdf");
            table.AddCell("fsdf");
            table.AddCell("fsdf");
            table.AddCell("fsdf");
            table.AddCell("fsdf");
            table.AddCell("fsdf");
            table.AddCell("fsdf");
            table.AddCell("fsdf");
            table.AddCell("fsdf");
            table.AddCell("fsdf");
            doc.Add(table);
            doc.Close();

        }
    }
}