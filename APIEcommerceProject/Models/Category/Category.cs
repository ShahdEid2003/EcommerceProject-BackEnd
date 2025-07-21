namespace APIEcommerceProject.Models.Category
{
    public class Category : BaseModel
    {
        public List <CategoryTranslation> CategoryTranslations { get; set; }= new List<CategoryTranslation>();
    }
}
