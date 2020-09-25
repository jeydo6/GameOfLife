using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;

namespace GameOfLife.Common.Filters
{
    public class ValidationExceptionFilter : IAsyncExceptionFilter
    {
        public async Task OnExceptionAsync(ExceptionContext context)
        {
            if (context.Exception is ValidationException ex)
            {
                var messages = ex.Errors
                    .Where(error => error.Severity == Severity.Error)
                    .Select(error => error.ErrorMessage)
                    .ToArray();

                context.Result = new BadRequestObjectResult(
                    JsonConvert.SerializeObject(messages)
                );
            }

            await Task.CompletedTask;
        }
    }
}