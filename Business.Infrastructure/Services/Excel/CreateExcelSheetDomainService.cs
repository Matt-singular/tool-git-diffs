namespace Business.Infrastructure.Services.Excel;

using Business.Domain.Services.Excel;
using ClosedXML.Excel;

public class CreateExcelSheetDomainService : ICreateExcelSheetDomainService
{
  public void Process()
  {
    // TODO: temporary POC
    using var workbook = new XLWorkbook();
    AddWorksheet(workbook, "PLACEHOLDER");
    ExportToExcel(workbook);
  }

  private static void AddWorksheet(XLWorkbook workbook, string sheetName)
  {
    // Temporary test 
    var worksheet = workbook.Worksheets.Add(sheetName);

    // Headers
    worksheet.Cell(1, 1).Value = "Feature";
    worksheet.Cell(1, 2).Value = "Ticket";
    worksheet.Cell(1, 3).Value = "Author";
    worksheet.Cell(1, 4).Value = "Date";
    worksheet.Cell(1, 5).Value = "Message";

    // Data
    worksheet.Cell(2, 1).Value = "FEAT-1000";
    worksheet.Cell(2, 2).Value = "TICKET-200";
    worksheet.Cell(2, 3).Value = "Matt";
    worksheet.Cell(2, 4).Value = DateTime.Now;
    worksheet.Cell(2, 5).Value = "FEAT-1000 TICKET-200 Added functionality to export data to an excel sheet";

    // Header Style & range
    var headerRange = worksheet.Range("A1:E1");
    headerRange.Style.Font.Bold = true;
    headerRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
  }

  private static void ExportToExcel(XLWorkbook workbook)
  {
    // Save to a memory stream
    using var stream = new MemoryStream();
    workbook.SaveAs(stream);
    var workbookContent = stream.ToArray();

    // Save the file to local machine
    var filePath = $"{AppContext.BaseDirectory.TrimEnd('\\')}\\exportedFile.xlsx";
    File.WriteAllBytes(filePath, workbookContent);
  }
}