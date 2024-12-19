using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace lesavrilshop_be.Helpers
{
    public class SwaggerFileOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var fileUploadMime = "multipart/form-data";
            if (operation.RequestBody == null || !operation.RequestBody.Content.Any(x => x.Key.Equals(fileUploadMime, StringComparison.InvariantCultureIgnoreCase)))
                return;

            var fileParams = context.MethodInfo.GetParameters()
                .SelectMany(p => p.ParameterType.GetProperties())
                .Where(p => p.PropertyType == typeof(IFormFile) || p.PropertyType == typeof(IFormFile[]));

            foreach (var parameter in fileParams)
            {
                operation.RequestBody.Content[fileUploadMime].Schema.Properties[parameter.Name].Type = "array";
                operation.RequestBody.Content[fileUploadMime].Schema.Properties[parameter.Name].Items = new OpenApiSchema
                {
                    Type = "string",
                    Format = "binary"
                };
            }
        }
    }
}