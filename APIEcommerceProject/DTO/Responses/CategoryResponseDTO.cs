using APIEcommerceProject.Controllers;

namespace APIEcommerceProject.DTO.Responses
{
    public class CategoryResponseDTO
    {
        public string Id { get; set; }
        public List<CategoryTransltionResponseDTO> CategoryTranslations { get; set; }
    }
}
