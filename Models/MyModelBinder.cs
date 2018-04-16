
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;
namespace webmva.Models
{
    public class MyModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null) throw new ArgumentNullException(nameof(bindingContext));

            ValueProviderResult values = bindingContext.ValueProvider.GetValue("ModelType");
            if (values.Length == 0) return Task.CompletedTask;

            string typeString = values.FirstValue;
            Type type = Type.GetType(
                "webmva.Models" + typeString + ", webmva.Models",
                true);

            object model = Activator.CreateInstance(type);

            var metadataProvider = (IModelMetadataProvider)bindingContext.HttpContext.RequestServices.GetService(typeof(IModelMetadataProvider));
            bindingContext.ModelMetadata = metadataProvider.GetMetadataForType(type);
            bindingContext.Result = ModelBindingResult.Success(model);
            return Task.CompletedTask;
        }
    }
        
}
