using APIEcommerceProject.Models;

namespace APIEcommerceProject.DTO.Requests
{
    public class CategoryRequestDTO
    {
        public Status Status { get; set; } = Status.Active;
    public List<CategoryTranslationRequestDTO> CategoryTranslations { get; set; }
    }
}
