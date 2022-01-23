using demo.Model.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace demo.api.Filters
{
    public class ValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                ErrorDto errorDto = new ErrorDto
                {
                    StatusCode = 400
                };
                IEnumerable<ModelError> modelErrors = context.ModelState.Values.SelectMany(e => e.Errors);
                modelErrors.ToList().ForEach(x =>
                                                {
                                                    errorDto.Errors.Add(x.ErrorMessage);

                                                });
                context.Result = new BadRequestObjectResult(errorDto);
                return;
            }

            await next();
        }
    }
}
