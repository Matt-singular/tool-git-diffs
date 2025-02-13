namespace Business.Domain.Reports;

using System.Linq;
using Business.Models.Reports.GetCleanedExcelReport;
using ClosedXML.Excel;
using Common.Shared.Models.Commits;

/// <inheritdoc cref="IGetCleanedExcelReport"/>
public class GetCleanedExcelReport : IGetCleanedExcelReport
{
  /// <inheritdoc/>
  public GetCleanedExcelReportResponse Process(GetCleanedExcelReportRequest request)
  {
    // Create a new Excel workbook
    using var workbook = new XLWorkbook();

    // Get cleaned commit details for Excel sheet
    var cleanedCommitDetails = request.RepositoryCleanedCommits
      .SelectMany(repo => repo.CleanedCommits)
      .GroupBy(repoCommits => repoCommits.RawDetails.Hash)
      .Select(grouping => grouping.FirstOrDefault()!) // TODO: using a basic GroupBy for now
      .ToList();

    // Create Excel sheet
    var worksheetName = request.RepositoryCleanedCommits.FirstOrDefault()!.RepositoryName;
    var worksheet = AddExcelSheet(workbook, worksheetName);
    worksheet = AddExcelSheetData(worksheet, cleanedCommitDetails);

    var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
    var worksheetPath = Path.Combine(desktopPath, "Diffs", $"{worksheetName}.xlsx");
    workbook.SaveAs(worksheetPath);

    // Save workbook and return
    return new GetCleanedExcelReportResponse
    {
      ExcelFile = SaveWorksheet(workbook)
    };
  }

  private static IXLWorksheet AddExcelSheet(XLWorkbook workbook, string repositoryName)
  {
    // Create excel worksheet
    var worksheet = workbook.Worksheets.Add(repositoryName);

    // Add headers to the sheet
    var headerIndex = 1;
    worksheet.Cell(headerIndex, 1).Value = "Commit Message";
    worksheet.Cell(headerIndex, 2).Value = "Author";
    worksheet.Cell(headerIndex, 3).Value = "Commit Hash";
    worksheet.Cell(headerIndex, 4).Value = "Ticket Reference(s)";
    worksheet.Cell(headerIndex, 5).Value = "Date Created";

    return worksheet;
  }

  private static IXLWorksheet AddExcelSheetData(IXLWorksheet worksheet, List<CleanedCommitDetails> cleanedCommitDetails)
  {
    for (int i = 0; i < cleanedCommitDetails.Count(); i++)
    {
      // Get commit details
      var commitDetail = cleanedCommitDetails[i];
      var rowIndex = i + 2;

      // Add row data
      worksheet.Cell(rowIndex, 1).Value = commitDetail.RawDetails.Message;
      worksheet.Cell(rowIndex, 2).Value = commitDetail.RawDetails.AuthorName;
      worksheet.Cell(rowIndex, 3).Value = commitDetail.RawDetails.Hash;
      worksheet.Cell(rowIndex, 4).Value = string.Join(',', commitDetail.JiraReferences.Select(x => x.JiraReference));
      worksheet.Cell(rowIndex, 5).Value = commitDetail.RawDetails.DateOfCommit;
    }

    return worksheet;
  }

  private static byte[] SaveWorksheet(XLWorkbook workbook)
  {
    using var stream = new MemoryStream();
    workbook.SaveAs(stream);
    stream.Seek(0, SeekOrigin.Begin);

    return stream.ToArray();
  }
}