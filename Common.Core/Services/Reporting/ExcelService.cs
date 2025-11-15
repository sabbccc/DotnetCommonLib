using ClosedXML.Excel;

namespace Common.Core.Services.Reporting
{
    public class ExcelService : IExcelService
    {
        public byte[] GenerateExcel(Action<XLWorkbook> buildAction)
        {
            using var workbook = new XLWorkbook();
            buildAction(workbook);

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            return stream.ToArray();
        }
    }
}
