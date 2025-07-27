using Istijara.Core.Entities;

namespace Istijara.API.Dtos
{
    public class CategoryDto
    {


        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? IconUrl { get; set; }
        public bool IsActive { get; set; } = true;
        

    }
}
