using Blog.web.Data;
using Blog.web.Models.Domain;
using Blog.web.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace Blog.web.Controllers
{
    public class AdminTagController : Controller
    {
        private BlogDbContext _blogDbContext;
        public AdminTagController(BlogDbContext blogDbContext)
        {
            _blogDbContext = blogDbContext;
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ActionName("Add")]
        public IActionResult Add(AddTagRequest addTagRequest)
        {
            //Mapping AddTagRequest to Tag domain model
            var tag = new Tag
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName
            };
            _blogDbContext.Tags.Add(tag);
            _blogDbContext.SaveChanges();

            return RedirectToAction("List");
        }
        [HttpGet]
        [ActionName("List")]
        public IActionResult List()
        {
            // use DbContext to read the tags
            var tags = _blogDbContext.Tags.ToList();
            return View(tags);
        }
        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            // var tag = _blogDbContext.Tags.Find(id);
            var tag = _blogDbContext.Tags.FirstOrDefault(x => x.Id == id);

            if (tag != null)
            {
                var editTagRequest = new EditTagRequest
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    DisplayName = tag.DisplayName
                };
                return View(editTagRequest);
            }
            return View(null);
        }
        [HttpPost]
        public IActionResult Edit(EditTagRequest editTagRequest)
        {
            var tag = new Tag
            {
                Id = editTagRequest.Id,
                Name = editTagRequest.Name,
                DisplayName = editTagRequest.DisplayName
            };
            var existingTag = _blogDbContext.Tags.Find(tag.Id);
            if (existingTag != null)
            {
                existingTag.Name = tag.Name;
                existingTag.DisplayName = tag.DisplayName;
                // save changes
                _blogDbContext.SaveChanges();
                return RedirectToAction("List");
            }
            return RedirectToAction("Edit", new { id = editTagRequest.Id });

        }
        [HttpPost]
        public IActionResult Delete(EditTagRequest editTagRequest) { 
            var tag = _blogDbContext.Tags.Find(editTagRequest.Id);
            if (tag != null)
            {
                _blogDbContext.Tags.Remove(tag);
                _blogDbContext.SaveChanges();
                return RedirectToAction("List");

            }
            return RedirectToAction("Edit", new { id = editTagRequest.Id });
        }
    }
}