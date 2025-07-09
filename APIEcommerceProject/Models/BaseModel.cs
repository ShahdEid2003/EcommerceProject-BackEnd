namespace APIEcommerceProject.Models
{
    public enum Status
    {
        Active=1,
        Inactive=2
    }
    public class BaseModel
    {
        public int Id { get; set; }
        public Status Status { get; set; }= Status.Active;
        public DateTime CreatedAt { get; set; }= DateTime.Now;
    }
}
