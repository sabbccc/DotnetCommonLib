namespace Common.Core.Services.Reporting
{
    public interface IPdfService
    {
        byte[] GeneratePdf(Action<QuestPDF.Fluent.Document> buildAction);
    }
}
