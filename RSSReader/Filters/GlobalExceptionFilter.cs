using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;


namespace RSSReader.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<GlobalExceptionFilter> _logger;

        public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> exceptionLogger)
        {
            _logger = exceptionLogger;
        }

        public void OnException(ExceptionContext context)
        {
            _logger.LogError(0, context.Exception.GetBaseException(), "Exception occurred.");
        }
    }
}
