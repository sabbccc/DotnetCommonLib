using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Common.Core.Services.Reporting
{
    public class PdfService : IPdfService
    {
        public byte[] GeneratePdf(Action<Document> buildAction)
        {
            var doc = Document.Create(container =>
            {
                buildAction((Document)container);
            });

            return doc.GeneratePdf();
        }
    }
}
