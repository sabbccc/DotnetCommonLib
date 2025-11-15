namespace Common.Core.Services.Reporting
{
    public interface IExcelService
    {
        byte[] GenerateExcel(Action<ClosedXML.Excel.XLWorkbook> buildAction);
    }
}
