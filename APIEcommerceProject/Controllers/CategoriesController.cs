using APIEcommerceProject.Data;
using APIEcommerceProject.DTO.Requests;
using APIEcommerceProject.DTO.Responses;
using APIEcommerceProject.Models;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIEcommerceProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        ApplicationDbContext context = new ApplicationDbContext();
        [HttpGet("")]
        public IActionResult GetAll()
        {
            var cats=context.Categories.Where(c=>c.Status==Status.Active).ToList().Adapt<List<CategoryResponseDTO>>();
            return Ok(new { message = "", cats });
        }
        //admin
        [HttpGet("all")]
        public IActionResult Index()
        {
            var cats = context.Categories.OrderByDescending(c => c.CreatedAt).ToList().Adapt<List<CategoryResponseDTO>>();
            return Ok(new { message = "", cats });
        }
        [HttpGet("{id}")]
        public IActionResult Details(int id)
        {
            var cats = context.Categories.Find(id);
            if (cats is null)
            {
                return NotFound(new{ message= "category not found"});
            }
            return Ok( cats.Adapt<CategoryResponseDTO>() );
        }
        [HttpPost("")]
        public IActionResult Create(CategoryRequestDTO request)
        {
            var catDTO = request.Adapt<Category>();
            context.Add(catDTO);
            context.SaveChanges();
            return Ok(new { message = "success" });
        }
        [HttpPatch("{id}")]
        public IActionResult Update(int id, CategoryRequestDTO request)
        {
          var category= context.Categories.Find(id);
            if (category is null)
            {
                return NotFound(new { message = "category not found" });
            }

            category.Name=request.Name;
            context.SaveChanges();
            return Ok(new { message = "success update" });
        }
        [HttpPatch("{id}/toggle-status")]
        public IActionResult ToggleStatus(int id)
        {
            var category = context.Categories.Find(id);
            if (category is null)
            {
                return NotFound(new { message = "category not found" });
            }

            category.Status = category.Status == Status.Active ? Status.Inactive : Status.Active;
            context.SaveChanges();

            return Ok(new { message = "Status updated successfully" });
        }
        [HttpDelete("{id}")]
        public IActionResult Remove(int id)
        {
            var category = context.Categories.Find(id);
            if (category is null) { 
                return NotFound(new { message = "category not found" });
            }   
            context.Remove(category);
            context.SaveChanges();

            return Ok(new { message = "deleted successfully" });
        }
        [HttpDelete("delete-all")]
        public IActionResult RemoveAll()
        {
            var categories = context.Categories.ToList();
            if (!categories.Any())
            {
                return NotFound(new { message = "category not found" });

            }
            
            context.RemoveRange(categories);
            context.SaveChanges();

            return Ok(new { message = "deleted all categories successfully" });
        }




    }
}
