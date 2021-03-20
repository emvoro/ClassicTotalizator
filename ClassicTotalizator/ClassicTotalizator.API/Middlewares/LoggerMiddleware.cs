using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace ClassicTotalizator.API.Middlewares
{
    public class LoggerMiddleware
    {
        private readonly ILogger<LoggerMiddleware> _logger;
        
        private readonly RequestDelegate _next;
        
        public LoggerMiddleware(RequestDelegate next, ILogger<LoggerMiddleware> logger)
        {
            _logger = logger;
            _next = next;
        }
        
        public async Task InvokeAsync(HttpContext context)
        {
            _logger.LogInformation(await PrintSomeInfoAboutRequest(context));

            var originalBodyStream = context.Response.Body;

            await using var responseBody = new MemoryStream();
            context.Response.Body = responseBody;
            await _next(context);

            _logger.LogInformation(await PrintSomeInfoAboutResponse(context));
            await responseBody.CopyToAsync(originalBodyStream);
        }

        private static async Task<string> ObtainRequestBody(HttpRequest request)
        {
            request.EnableBuffering();

            var encoding = GetEncodingFromContentType(request.ContentType);
            string bodyStr;

            using (var reader = new StreamReader(request.Body, encoding, true, 1024, true))
            {
                bodyStr = await reader.ReadToEndAsync().ConfigureAwait(false);
            }
            
            request.Body.Seek(0, SeekOrigin.Begin);
            
            return bodyStr;
        }
        
        private static async Task<string> ObtainResponseBody(HttpContext context)
        {
            var response = context.Response;
            response.Body.Seek(0, SeekOrigin.Begin);

            var encoding = GetEncodingFromContentType(response.ContentType);
            using var reader = new StreamReader(response.Body, encoding,
                detectEncodingFromByteOrderMarks: false, bufferSize: 4096, leaveOpen: true);
            
            var text = await reader.ReadToEndAsync().ConfigureAwait(false);
            response.Body.Seek(0, SeekOrigin.Begin);
            
            return text;
        }
        
        private async Task<string> PrintSomeInfoAboutRequest(HttpContext context)
        {
            var builder = new StringBuilder();
            
            builder.AppendLine($"[REACHED REQUEST INFO]");
            builder.AppendLine($"Method\t\t[{context.Request.Method}]");
            
            foreach (var m in context.Request.Headers)
                builder.AppendLine($"[{m.Key}]-->{m.Value}");
            
            builder.AppendLine($"[Request body]\n{await ObtainRequestBody(context.Request)}");
            
            return builder.ToString();
        }
        
        private async Task<string> PrintSomeInfoAboutResponse(HttpContext context)
        {
            var builder = new StringBuilder();
            builder.AppendLine($"[REACHED RESPONSE INFO]\t\tSTATUS CODE<{context.Response.StatusCode}>\n" +
                $"[Response body]\t\t{await ObtainResponseBody(context)}");
            
            return builder.ToString();
        }
        
        private static Encoding GetEncodingFromContentType(string contentTypeStr)
        {
            if (string.IsNullOrEmpty(contentTypeStr))          
                return Encoding.UTF8;
            
            ContentType contentType;
            try
            {
                contentType = new ContentType(contentTypeStr);
            }
            catch (FormatException)
            {
                return Encoding.UTF8;
            }
            
            if (string.IsNullOrEmpty(contentType.CharSet))
                return Encoding.UTF8;
            
            return Encoding.GetEncoding(contentType.CharSet, EncoderFallback.ExceptionFallback, DecoderFallback.ExceptionFallback);
        }
    }
}
