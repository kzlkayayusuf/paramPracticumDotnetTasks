using System.Text;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;

namespace WebAPI.Utilities.Formatters;

public class CsvOutputFormatter : TextOutputFormatter
{
    public CsvOutputFormatter()
    {
        SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/csv"));
        SupportedEncodings.Add(Encoding.UTF8);
        SupportedEncodings.Add(Encoding.Unicode);
    }

    protected override bool CanWriteType(Type? type)
    {
        if (typeof(MovieDto).IsAssignableFrom(type) || typeof(IEnumerable<MovieDto>).IsAssignableFrom(type))
        {
            return base.CanWriteType(type);
        }

        return false;
    }

    private static void FormatCsv(StringBuilder buffer, MovieDto movie)
    {
        buffer.AppendLine($"{movie.Id},{movie.Name},{movie.Genre},{movie.ReleaseYear},{movie.Price}");
    }

    public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
    {
        var response = context.HttpContext.Response;
        var buffer = new StringBuilder();

        if (context.Object is IEnumerable<MovieDto>)
        {
            foreach (var movie in (IEnumerable<MovieDto>)context.Object)
            {
                FormatCsv(buffer, movie);
            }
        }
        else
        {
            FormatCsv(buffer, (MovieDto)context.Object);
        }

        await response.WriteAsync(buffer.ToString());
    }
}