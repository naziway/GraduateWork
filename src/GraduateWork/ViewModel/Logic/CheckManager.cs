using DatabaseService;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Model;
using System;
using System.Collections.Generic;
using System.Globalization;
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



        public string CreateSellCheck(List<Selling> sellings)
        {

            FileStream fs = new FileStream($"продаж_{sellings.First().Kod}.pdf", FileMode.Create, FileAccess.Write, FileShare.None);

            Document doc = new Document(PageSize.A5, 2, 2, 2, 2);

            PdfWriter writer = PdfWriter.GetInstance(doc, fs);

            String FONT_LOCATION = @"C:\Windows\Fonts\arial.ttf";
            BaseFont baseFont = BaseFont.CreateFont(FONT_LOCATION, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            iTextSharp.text.Font font = new iTextSharp.text.Font(baseFont, 14, iTextSharp.text.Font.NORMAL);
            iTextSharp.text.Font boltFont = new iTextSharp.text.Font(baseFont, 16, iTextSharp.text.Font.BOLD);


            doc.Open();
            var header = new Paragraph($"Покупка: {sellings.First().Kod}", boltFont);
            header.IndentationLeft = 42;
            doc.Add(header);
            doc.Add(new Paragraph($"Клієнт: {sellings.First().Client.FirstName} {sellings.First().Client.LastName}", boltFont) { IndentationLeft = 42, SpacingAfter = 5 });

            var tableWidth = new float[] { 240, 70, 120, 130, 120 };

            var table = new PdfPTable(tableWidth);
            table.TotalWidth = 15;
            table.AddCell(GetCursiveUkraineText("Запчастина"));
            table.AddCell(GetCursiveUkraineText("Кілк."));
            table.AddCell(GetCursiveUkraineText("Ціна"));
            table.AddCell(GetCursiveUkraineText("Cума"));
            table.AddCell(GetCursiveUkraineText("Дата"));

            var sum = 0.0;
            foreach (var item in sellings)
            {
                table.AddCell(GetUkraineText(item.Part.Title));
                table.AddCell(GetUkraineText(item.Count.ToString()));
                table.AddCell(GetUkraineText($"{item.Part.Price}грн."));
                table.AddCell(GetUkraineText($"{item.Count * item.Part.Price}грн."));
                table.AddCell(item.OrderDate.ToShortDateString());
                sum += item.Count * item.Part.Price;
            }
            doc.Add(table);

            doc.Add(new Paragraph($"Загальна вартість: {sum} гривень", boltFont) { IndentationLeft = 42, SpacingAfter = 5 });

            doc.Close();
            return fs.Name;
        }
        public string CreateRepairCheck(List<Repair> repairs)
        {

            FileStream fs = new FileStream($"ремонт_{repairs.First().Kod}.pdf", FileMode.Create, FileAccess.Write, FileShare.None);

            Document doc = new Document(PageSize.A5, 2, 2, 2, 2);

            PdfWriter writer = PdfWriter.GetInstance(doc, fs);

            String FONT_LOCATION = @"C:\Windows\Fonts\arial.ttf";
            BaseFont baseFont = BaseFont.CreateFont(FONT_LOCATION, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            iTextSharp.text.Font font = new iTextSharp.text.Font(baseFont, 14, iTextSharp.text.Font.NORMAL);
            iTextSharp.text.Font boltFont = new iTextSharp.text.Font(baseFont, 16, iTextSharp.text.Font.BOLD);


            doc.Open();
            var header = new Paragraph($"Ремонт: {repairs.First().Kod}", boltFont);
            header.IndentationLeft = 42;
            doc.Add(header);
            doc.Add(new Paragraph($"Клієнт: {repairs.First().Device.Client.FirstName} {repairs.First().Device.Client.LastName}", boltFont) { IndentationLeft = 42, SpacingAfter = 5 });
            doc.Add(new Paragraph($"Пристрій: {repairs.First().Device.Marka} {repairs.First().Device.Model} {repairs.First().Device.SerialNumber}", boltFont) { IndentationLeft = 42, SpacingAfter = 5 });
            doc.Add(new Paragraph($"Робітник: {repairs.First().Worker.UserData.FirstName} {repairs.First().Worker.UserData.LastName} ", boltFont) { IndentationLeft = 42, SpacingAfter = 5 });

            var tableWidth = new float[] { 120, 70, 120, 120 };

            var table = new PdfPTable(tableWidth);
            table.TotalWidth = 15;
            table.AddCell(GetCursiveUkraineText("Робота"));
            table.AddCell(GetCursiveUkraineText("Ціна"));
            table.AddCell(GetCursiveUkraineText("Частина"));
            table.AddCell(GetCursiveUkraineText("Ціна"));

            var sum = 0.0;
            for (var i = 0; i < repairs.Count; i++)
            {
                var repair = repairs[i];
                table.AddCell(GetUkraineText(repair.Work.Title));
                table.AddCell(GetUkraineText(repair.Work.Price.ToString(CultureInfo.CurrentCulture)));
                sum += repairs[i].Work.Price;
                if (repairs[i].Part == null)
                {
                    table.AddCell(GetUkraineText("не має"));
                    table.AddCell(GetUkraineText(0.ToString(CultureInfo.CurrentCulture)));
                    continue;
                }

                table.AddCell(GetUkraineText(repair.Part.Title));
                table.AddCell(GetUkraineText(repair.Part.Price.ToString(CultureInfo.CurrentCulture)));
                sum += repairs[i].Part.Price;
            }
            doc.Add(table);

            doc.Add(new Paragraph($"Загальна вартість: {sum} гривень", boltFont) { IndentationLeft = 42, SpacingAfter = 5 });

            doc.Close();
            return fs.Name;
        }
        public string CreateReviewCheck(Review review)
        {

            FileStream fs = new FileStream($"обстеження_{review.Kod}.pdf", FileMode.Create, FileAccess.Write, FileShare.None);
            Document doc = new Document(PageSize.A5, 2, 2, 2, 2);
            PdfWriter writer = PdfWriter.GetInstance(doc, fs);
            String FONT_LOCATION = @"C:\Windows\Fonts\arial.ttf";
            BaseFont baseFont = BaseFont.CreateFont(FONT_LOCATION, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            iTextSharp.text.Font font = new iTextSharp.text.Font(baseFont, 14, iTextSharp.text.Font.NORMAL);
            iTextSharp.text.Font boltFont = new iTextSharp.text.Font(baseFont, 16, iTextSharp.text.Font.BOLD);


            doc.Open();
            var header = new Paragraph($"Обстеження: {review.Kod}", boltFont);
            header.IndentationLeft = 42;
            doc.Add(header);
            doc.Add(new Paragraph($"Пристрій ", boltFont) { IndentationLeft = 42, SpacingAfter = 5 });
            doc.Add(new Paragraph($"Марка:{review.Device.Marka}", font) { IndentationLeft = 42, SpacingAfter = 5 });
            doc.Add(new Paragraph($"Модель: {review.Device.Model}", font) { IndentationLeft = 42, SpacingAfter = 5 });
            doc.Add(new Paragraph($"Серійний номер: {review.Device.SerialNumber}", font) { IndentationLeft = 42, SpacingAfter = 5 });

            doc.Add(new Paragraph($"Клієнт", boltFont) { IndentationLeft = 42, SpacingAfter = 5 });
            doc.Add(new Paragraph($"Прізвище: {review.Device.Client.FirstName} ", font) { IndentationLeft = 42, SpacingAfter = 5 });
            doc.Add(new Paragraph($"Імя: {review.Device.Client.LastName} ", font) { IndentationLeft = 42, SpacingAfter = 5 });
            doc.Add(new Paragraph($"Серія та номер паспорта: {review.Device.Client.PasportData}", font) { IndentationLeft = 42, SpacingAfter = 5 });

            doc.Close();
            return fs.Name;
        }
        public Paragraph GetUkraineText(string text)
        {
            String FONT_LOCATION = @"C:\Windows\Fonts\arial.ttf";
            BaseFont baseFont = BaseFont.CreateFont(FONT_LOCATION, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            iTextSharp.text.Font font = new iTextSharp.text.Font(baseFont, 14, iTextSharp.text.Font.NORMAL);

            return new Paragraph(text, font);
        }
        public Paragraph GetCursiveUkraineText(string text)
        {
            String FONT_LOCATION = @"C:\Windows\Fonts\arial.ttf";
            BaseFont baseFont = BaseFont.CreateFont(FONT_LOCATION, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            iTextSharp.text.Font font = new iTextSharp.text.Font(baseFont, 14, iTextSharp.text.Font.BOLD);

            return new Paragraph(text, font);
        }
    }
}