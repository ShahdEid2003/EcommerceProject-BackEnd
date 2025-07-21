using APIEcommerceProject.Data;
using APIEcommerceProject.DTO.Requests;
using APIEcommerceProject.DTO.Responses;
using APIEcommerceProject.Models;
using APIEcommerceProject.Models.Category;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace APIEcommerceProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
       
        private readonly IStringLocalizer<SharedResource> _localizer;
        private readonly ApplicationDbContext context;

        public CategoriesController(IStringLocalizer<SharedResource> localizer , ApplicationDbContext context)
        {
            _localizer = localizer;
            this.context = context;
        }

        [HttpGet("")]
        public IActionResult GetAll([FromQuery]string lang="en")
        {
           
            var cats = context.Categories.Include(c => c.CategoryTranslations)
                .Where(c => c.Status == Status.Active)
                .ToList()
                .Adapt<List<CategoryResponseDTO>>();
            var result = cats.Select(cat => new
            {
                Id = cat.Id,
                Name = cat.CategoryTranslations.FirstOrDefault(t => t.Language == lang).Name
            });


            return Ok(new { message = _localizer["success"].Value, result });
        }

        // admin
        [HttpGet("all")]
        public IActionResult Index()
        {
            var cats = context.Categories
                .OrderByDescending(c => c.CreatedAt)
                .ToList()
                .Adapt<List<CategoryResponseDTO>>();

            return Ok(new { message = _localizer["success"].Value, cats });
        }

        [HttpGet("{id}")]
        public IActionResult Details([FromRoute] int id)
        {
            var category = context.Categories.Find(id);
            if (category is null)
            {
                return NotFound(new { message = _localizer["not-found"].Value });
            }

            return Ok(category.Adapt<CategoryResponseDTO>());
        }

        [HttpPost("")]
        public IActionResult Create([FromBody] CategoryRequestDTO request)
        {
            var cat = request.Adapt<Category>();
            context.Categories.Add(cat);
            context.SaveChanges();

            return Ok(new { message = _localizer["added-success"].Value });
        }

        [HttpPatch("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] CategoryRequestDTO request)
        {
            var category = context.Categories.Find(id);
            if (category is null)
            {
                return NotFound(new { message = _localizer["not-found"].Value });
            }

           //category.Name = request.Name;
           context.SaveChanges();

            return Ok(new { message = _localizer["updated-success"].Value });
        }

        [HttpPatch("{id}/toggle-status")]
        public IActionResult ToggleStatus([FromRoute] int id)
        {
            var category = context.Categories.Find(id);
            if (category is null)
            {
                return NotFound(new { message = _localizer["not-found"].Value });
            }

            category.Status = category.Status == Status.Active ? Status.Inactive : Status.Active;
            context.SaveChanges();

            return Ok(new { message = _localizer["status-updated"].Value });
        }

        [HttpDelete("{id}")]
        public IActionResult Remove([FromRoute] int id)
        {
            var category = context.Categories.Find(id);
            if (category is null)
            {
                return NotFound(new { message = _localizer["not-found"].Value });
            }

           context.Categories.Remove(category);
           context.SaveChanges();

            return Ok(new { message = _localizer["deleted-success"].Value });
        }

        [HttpDelete("delete-all")]
        public IActionResult RemoveAll()
        {
            var categories = context.Categories.ToList();
            if (!categories.Any())
            {
                return NotFound(new { message = _localizer["not-found"].Value });
            }

            context.Categories.RemoveRange(categories);
            context.SaveChanges();

            return Ok(new { message = _localizer["deleted-all-success"].Value });
        }
    }
}
