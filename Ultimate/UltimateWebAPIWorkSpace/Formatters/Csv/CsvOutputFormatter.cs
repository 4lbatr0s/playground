

using System.Text;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using Shared.DataTransferObjects;

namespace UltimateWebAPIWorkSpace.Formatters.Csv;


//INFO: HOW TO WRITE A FORMATTER, FOR RESPONSE.
public class CsvOutputFormatter : TextOutputFormatter
{
    //INFO: In the constructor, we define which media type this formatter should parse as well as encodings.
    public CsvOutputFormatter()
    {
        SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/csv"));
        SupportedEncodings.Add(Encoding.UTF8);
        SupportedEncodings.Add(Encoding.Unicode);
    }

    // INFO: The CanWriteType method is overridden, and it indicates whether
    // or not the CompanyDto type can be written by this serializer.
    protected override bool CanWriteType(Type? type)
    {
        if (typeof(CompanyDto).IsAssignableFrom(type) ||
        typeof(IEnumerable<CompanyDto>).IsAssignableFrom(type))
        {
            return base.CanWriteType(type);
        }
        return false;
    }
    // INFO: The WriteResponseBodyAsync method constructs the response.

    public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
    {
        var response = context.HttpContext.Response;
        var buffer = new StringBuilder();

        if (context.Object is IEnumerable<CompanyDto>)
        {
            foreach (var company in (IEnumerable<CompanyDto>)context.Object)
            {
                FormatCsv(buffer, company);
            }
        }
        else
        {
            FormatCsv(buffer, (CompanyDto)context.Object);
        }

        await response.WriteAsync(buffer.ToString());
    }
    //INFO: And finally, we have the FormatCsv method that formats a response the way we want it.
    private static void FormatCsv(StringBuilder buffer, CompanyDto company)
    {
        buffer.AppendLine($"{company.Id},\"{company.Name},\"{company.FullAddress}\"");
    }
}