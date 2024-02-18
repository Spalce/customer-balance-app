using System.Globalization;
using CustomerBalance.Core.ViewModels;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;
using SizeF = Syncfusion.Drawing.SizeF;
using Color = Syncfusion.Drawing.Color;
using PointF = Syncfusion.Drawing.PointF;
using RectangleF = Syncfusion.Drawing.RectangleF;

namespace CustomerBalance.App.Services
{
    public static class ReportService
    {
        public static MemoryStream FinanceReport(ReportDataModel model, ReportDataActual<List<FinanceModel>> records)
        {
            var pdf = GetPdfSetting(model);

            // font = new PdfStandardFont(PdfFontFamily.Helvetica, 10, PdfFontStyle.Bold);
            // headerText = new PdfTextElement("Invoice No.#23698720 ", font);
            // headerText.StringFormat = new PdfStringFormat(PdfTextAlignment.Right);
            // headerText.Draw(currentPage, new PointF(clientSize.Width - 25, result.Bounds.Y - 20));

            PdfGrid grid = new PdfGrid();
            pdf.Item2 = new PdfStandardFont(PdfFontFamily.Helvetica, 10, PdfFontStyle.Regular);
            grid.Style.Font = pdf.Item2;
            grid.Columns.Add(6);
            grid.Columns[0].Width = 100;
            grid.Columns[1].Width = 200;
            grid.Columns[2].Width = 120;
            // grid.Columns[3].Width = 80;
            grid.Columns[4].Width = 70;
            grid.Columns[5].Width = 130;

            grid.Headers.Add(1);
            PdfStringFormat stringFormatR = new PdfStringFormat(PdfTextAlignment.Right, PdfVerticalAlignment.Middle);
            PdfStringFormat stringFormatL = new PdfStringFormat(PdfTextAlignment.Left, PdfVerticalAlignment.Middle);
            PdfGridRow header = grid.Headers[0];

            header.Cells[0].Value = "Date/Time";
            header.Cells[0].StringFormat.LineAlignment = PdfVerticalAlignment.Middle;
            header.Cells[1].Value = "Customer";
            header.Cells[1].StringFormat = stringFormatL;
            header.Cells[2].Value = "Amount";
            header.Cells[2].StringFormat = stringFormatR;
            header.Cells[3].Value = "Remarks";
            header.Cells[3].StringFormat = stringFormatL;
            header.Cells[4].Value = "Type";
            header.Cells[4].StringFormat = stringFormatL;
            header.Cells[5].Value = "Number";
            header.Cells[5].StringFormat = stringFormatL;

            PdfGridCellStyle cellStyle = new PdfGridCellStyle();
            cellStyle.Borders.All = PdfPens.Transparent;
            cellStyle.TextBrush = PdfBrushes.White;
            cellStyle.BackgroundBrush = new PdfSolidBrush(Color.FromArgb(1, 53, 67, 168));

            for (int j = 0; j < header.Cells.Count; j++)
            {
                PdfGridCell cell = header.Cells[j];
                cell.Style = cellStyle;
            }

            foreach (var item in records?.Details!)
            {
                PdfGridRow row = grid.Rows.Add();
                row.Cells[0].Value = item.Date! ?? "";
                row.Cells[0].StringFormat.LineAlignment = PdfVerticalAlignment.Middle;
                row.Cells[1].Value = item.Customer ?? "";
                row.Cells[1].StringFormat = stringFormatL;
                row.Cells[2].Value = item.Amount.ToString("N", CultureInfo.InvariantCulture) ?? "";
                row.Cells[2].StringFormat = stringFormatR;
                row.Cells[3].Value = item.Remarks ?? "";
                row.Cells[3].StringFormat = stringFormatL;
                row.Cells[4].Value = item.Type ?? "";
                row.Cells[4].StringFormat = stringFormatL;
                row.Cells[5].Value = item.Number ?? "";
                row.Cells[5].StringFormat = stringFormatL;
            }

            grid.ApplyBuiltinStyle(PdfGridBuiltinStyle.PlainTable3);
            PdfGridStyle gridStyle = new PdfGridStyle();
            gridStyle.CellPadding = new PdfPaddings(3, 3, 3, 3);
            grid.Style = gridStyle;

            PdfGridLayoutFormat layoutFormat = new PdfGridLayoutFormat();
            layoutFormat.Layout = PdfLayoutType.Paginate;
            pdf.Item1 = grid.Draw(pdf.Item3, 0, pdf.Item1.Bounds.Bottom + 10, pdf.Item5.Width, layoutFormat);

            pdf.Item3.Graphics.DrawRectangle(new PdfSolidBrush(Color.FromArgb(255, 239, 242, 255)),
                new RectangleF(pdf.Item1.Bounds.Right - 100, pdf.Item1.Bounds.Bottom + 20, 100, 20));

            MemoryStream stream = new MemoryStream();
            pdf.Item4.Save(stream);
            pdf.Item4.Close(true);
            stream.Position = 0;
            return stream;
        }

        private static PdfDocument GetDocument(SetDocument d, SetMargin m)
        {
            PdfDocument document = new PdfDocument();

            document.DocumentInformation.Author = d.Author!;
            document.DocumentInformation.CreationDate = (DateTime)d.CreationDate!;
            document.DocumentInformation.Creator = d.Creator!;
            document.DocumentInformation.Keywords = d.Keywords!;
            document.DocumentInformation.Subject = d.Subject!;
            document.DocumentInformation.Title = d.Title!;
            //Enable memory optimization
            document.EnableMemoryOptimization = true;
            // Set the page size.
            if (m.Width == 0)
            {
                document.PageSettings.Size = PdfPageSize.A4;
            }
            else
            {
                document.PageSettings.Width = m.Width;
            }

            // Set the custom page size.
            // document.PageSettings.Size = new SizeF(200, 300);
            document.PageSettings.Orientation =
                m.IsLandscape ? PdfPageOrientation.Landscape : PdfPageOrientation.Portrait;

            if (m.IsSameMargin)
            {
                document.PageSettings.Margins.All = m.Margin;
            }
            else
            {
                // Set the Left Margin for a PdfDocument page.
                document.PageSettings.Margins.Left = m.Left;
                // Set the Right margin for a PdfDocument page.
                document.PageSettings.Margins.Right = m.Right;
                // Set the Top margin for a PdfDocument page.
                document.PageSettings.Margins.Top = m.Top;
                // Set the Bottom margin for a PdfDocument page.
                document.PageSettings.Margins.Bottom = m.Down;
            }

            return document;
        }

        private static (PdfLayoutResult, PdfFont, PdfPage, PdfDocument, SizeF) GetPdfSetting(ReportDataModel model)
        {
            PdfDocument document = new PdfDocument();

            document.DocumentInformation.Author = model.Document?.Author!;
            document.DocumentInformation.CreationDate = (DateTime)model.Document?.CreationDate!;
            document.DocumentInformation.Creator = model.Document?.Creator!;
            document.DocumentInformation.Keywords = model.Document?.Keywords!;
            document.DocumentInformation.Subject = model.Document?.Subject!;
            document.DocumentInformation.Title = model.Document?.Title!;
            //Enable memory optimization
            document.EnableMemoryOptimization = true;
            // Set the page size.
            if (model.Margin?.Width == 0)
            {
                document.PageSettings.Size = PdfPageSize.A4;
            }
            else
            {
                document.PageSettings.Width = model.Margin!.Width!;
            }

            // Set the custom page size.
            // document.PageSettings.Size = new SizeF(200, 300);
            document.PageSettings.Orientation =
                model.Margin.IsLandscape ? PdfPageOrientation.Landscape : PdfPageOrientation.Portrait;

            if (model.Margin.IsSameMargin)
            {
                document.PageSettings.Margins.All = model.Margin.Margin;
            }
            else
            {
                // Set the Left Margin for a PdfDocument page.
                document.PageSettings.Margins.Left = model.Margin.Left;
                // Set the Right margin for a PdfDocument page.
                document.PageSettings.Margins.Right = model.Margin.Right;
                // Set the Top margin for a PdfDocument page.
                document.PageSettings.Margins.Top = model.Margin.Top;
                // Set the Bottom margin for a PdfDocument page.
                document.PageSettings.Margins.Bottom = model.Margin.Down;
            }

            PdfPage currentPage = document.Pages.Add();
            SizeF clientSize = currentPage.GetClientSize();

            string image;
            var dummy =
                    "data:image/jpeg;base64,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";
             string[] parts = dummy.Split(',');
             image = parts[1];

            byte[] imageBytes = Convert.FromBase64String(image);
            using MemoryStream imageStream = new MemoryStream(imageBytes);
            PdfImage icon = new PdfBitmap(imageStream);
            SizeF iconSize = new SizeF(40, 40);
            PointF iconLocation = new PointF(5, 13);

            /*string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string wwwrootPath = Path.Combine(baseDirectory, "wwwroot");
            string imagePath = Path.Combine(wwwrootPath, "images");
            string logoPath = Path.Combine(imagePath, "queendafred.png");*/

            //FileStream imageStream = new FileStream(logoPath, FileMode.Open, FileAccess.Read);
            //PdfImage icon = new PdfBitmap(imageStream);
            //SizeF iconSize = new SizeF(40, 40);
            //PointF iconLocation = new PointF(5, 13);
            PdfGraphics graphics = currentPage.Graphics;
            graphics.DrawImage(icon, iconLocation, iconSize);
            PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 20, PdfFontStyle.Bold);
            var headerText = new PdfTextElement(model.Title! ?? "No Title", font,
                new PdfSolidBrush(Color.FromArgb(1, 53, 67, 168)));
            headerText.StringFormat = new PdfStringFormat(PdfTextAlignment.Right);
            PdfLayoutResult result = headerText.Draw(currentPage, new PointF(clientSize.Width, iconLocation.Y));

            font = new PdfStandardFont(PdfFontFamily.Helvetica, 10);
            headerText = new PdfTextElement(model.Query! ?? "No Query", font,
                new PdfSolidBrush(Color.FromArgb(1, 53, 67, 168)));
            headerText.StringFormat = new PdfStringFormat(PdfTextAlignment.Right);
            result = headerText.Draw(currentPage, new PointF(clientSize.Width, iconLocation.Y + 25));

            font = new PdfStandardFont(PdfFontFamily.Helvetica, 10);
            headerText = new PdfTextElement(model.Company?.Branch! ?? "No Branch", font);
            result = headerText.Draw(currentPage, new PointF(0, result.Bounds.Bottom + 15));
            font = new PdfStandardFont(PdfFontFamily.Helvetica, 14, PdfFontStyle.Bold);
            headerText = new PdfTextElement(model.Company?.Name! ?? "No Company", font);
            result = headerText.Draw(currentPage, new PointF(0, result.Bounds.Bottom + 3));
            font = new PdfStandardFont(PdfFontFamily.Helvetica, 10);
            headerText =
                new PdfTextElement(
                    $"{model.Company?.Address ?? "No Address"},\n{model.Company?.Contact ?? "No Company"},\n{model.Company?.Website ?? "No Website"}",
                    font);
            result = headerText.Draw(currentPage, new PointF(0, result.Bounds.Bottom + 3));

            return (result, font, currentPage, document, clientSize);
        }

    }
}
