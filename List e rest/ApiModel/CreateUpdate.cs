using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace List_e_rest.ApiModel
{
    public class CreateUpdate<T>
    {
        public bool Success { get; set; }
        public T Model { get; set; }
        public ModelStateDictionary Errors { get; set; }
    }
}
