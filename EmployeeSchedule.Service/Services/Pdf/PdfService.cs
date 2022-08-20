using EmployeeSchedule.Data.Entities;
using EmployeeSchedule.Data.Interface.Pdf;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeSchedule.Service.Services.Pdf
{
    public class PdfService : IPdfService
    {
        public Task<string> GeneratePdfScheduleForEmployee(List<Schedule> schedules)
        {
            using (var pdfDocument = new Document(PageSize.A4))
            {
                try
                {
                    var employee = schedules[0].Employee;

                    var basePath = $@"C:\Users\Andrea\Desktop\FON\ITEH\Projekat\Pdf izvestaji";

                    if (!Directory.Exists(basePath))
                    {
                        Directory.CreateDirectory(basePath);
                    }
                    var path = $@"{basePath}\{employee.Name}{employee.Surname}-{employee.Id}";

                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    var fileName = $@"{path}\{employee.Name}{employee.Surname}{DateTime.Now.ToString("yyyyMMddTHHmmss")}.pdf";
                    var pdfWriter = PdfWriter.GetInstance(pdfDocument, new FileStream(fileName, FileMode.Create));
                    pdfDocument.Open();
                    pdfDocument.Add(CreateParagraph($"Izvestaj za zaposlenog", aligment: Element.ALIGN_CENTER, tipParagrafa: 4, spacing: 5));

                    pdfDocument.Add(CreateParagraph("Datum i vreme izdavanja izvestaja:", aligment: Element.ALIGN_LEFT, tipParagrafa: 2, spacing: 5));
                    pdfDocument.Add(CreateParagraph(DateTime.Now.ToString("dd / MM / yyyy HH: mm"), aligment: Element.ALIGN_LEFT, tipParagrafa: 3, spacing: 5));
                    pdfDocument.Add(CreateParagraph("Zaposleni:", aligment: Element.ALIGN_LEFT, tipParagrafa: 2, spacing: 5));
                    pdfDocument.Add(CreateParagraph($"{employee.Name} {employee.Surname} - {employee.Email}", aligment: Element.ALIGN_LEFT, tipParagrafa: 3, spacing: 5));
                    pdfDocument.Add(CreateParagraph($"{employee.Adress} - {employee.Number}", aligment: Element.ALIGN_LEFT, tipParagrafa: 3, spacing: 5));
                    pdfDocument.Add(CreateParagraph($"{employee.Company.Name} - {employee.Possition}", aligment: Element.ALIGN_LEFT, tipParagrafa: 3, spacing: 5));

                    pdfDocument.Add(CreateParagraph("Raspored:\n", aligment: Element.ALIGN_LEFT, tipParagrafa: 5, spacingBefore: 10));

                    if (schedules != null && schedules.Any())
                    {
                        var table = new PdfPTable(4);
                        //Header
                        table.AddCell(CreateCellTable("Datum"));
                        table.AddCell(CreateCellTable("Smena"));
                        table.AddCell(CreateCellTable("Vreme prijave"));
                        table.AddCell(CreateCellTable("Obavestenja"));

                        foreach (var schedule in schedules)
                        {
                            table.PaddingTop = 10;
                            table.AddCell(CreateCellTable(schedule.Date.ToString("dd/MM/yyyy"), true));
                            table.AddCell(CreateCellTable(schedule.ShiftWork, true));
                            table.AddCell(CreateCellTable(schedule.CheckInTime.ToString("HH:mm"), true));
                            table.AddCell(CreateCellTable(schedule.Notification, true));
                        }

                        pdfDocument.Add(table);

                    }
                   
                    return Task.FromResult(fileName);
                }
                catch (Exception e)
                {
                    return null;
                }
            }
        }

        private PdfPCell CreateCellTable(string text, bool row = false)
        {
            var font = FontFactory.GetFont("Times new roman", 12, row ? BaseColor.BLACK : BaseColor.WHITE);
            var chunk = new Chunk(text, font);
            var cell = new PdfPCell(new Phrase(chunk));
            if (!row)
            {
                cell.BackgroundColor = BaseColor.GRAY;
            }
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BorderColor = BaseColor.DARK_GRAY;
            return cell;
        }

        private Paragraph CreateParagraph(string tekst, int spacing = 15, int aligment = Element.ALIGN_LEFT, int tipParagrafa = 1, int spacingBefore = 5)
        {
            var paragraph = new Paragraph();
            paragraph.SpacingBefore = spacingBefore;
            paragraph.SpacingAfter = spacing;
            paragraph.Alignment = aligment;

            if (tipParagrafa == 1)
            {
                paragraph.Font = FontFactory.GetFont("Times New Roman", 12, Font.NORMAL, BaseColor.BLACK);
            }
            else if (tipParagrafa == 2)
            {
                paragraph.Font = FontFactory.GetFont("Times New Roman", 12, Font.ITALIC, BaseColor.BLACK);
            }
            else if (tipParagrafa == 3)
            {
                paragraph.Font = FontFactory.GetFont("Times New Roman", 15, Font.BOLD, BaseColor.BLUE);
            }
            else if (tipParagrafa == 5)
            {
                paragraph.Font = FontFactory.GetFont("Times New Roman", 15, Font.BOLD, BaseColor.DARK_GRAY);
            }
            else
            {
                paragraph.Font = FontFactory.GetFont("Times New Roman", 25, Font.BOLD, BaseColor.DARK_GRAY);
            }
            paragraph.Add(tekst);
            return paragraph;
        }
    }
}
