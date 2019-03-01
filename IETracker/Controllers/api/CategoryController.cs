using AutoMapper;
using IETracker.DTO;
using IETracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IETracker.Controllers.api
{
    [Authorize]
    public class CategoryController : ApiController
    {
        private readonly ApplicationDbContext _context;
        public CategoryController()
        {
            _context = new ApplicationDbContext();
        }

        public IHttpActionResult Get(string query = null)
        {
            if (query != null)
            {
                return Ok(_context.Categories
                    .Where(c => c.Name.Contains(query))
                    .ToList()
                    .Select(Mapper.Map<Category, CategoryDto>));
            }                
            return Ok(_context.Categories.ToList().Select(Mapper.Map<Category, CategoryDto>));
        }

        public IHttpActionResult Get(int id)
        {
            return Ok(Mapper.Map<CategoryDto>(_context.Categories.SingleOrDefault(c => c.Id == id)));
        }

        [HttpPost]
        public IHttpActionResult Save(CategoryDto categoryDto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var category = Mapper.Map<Category>(categoryDto);

            _context.Categories.Add(category);
            _context.SaveChanges();

            return Created(Request.RequestUri + "/" + category.Id, category);
        }

        [HttpPut]
        public IHttpActionResult Update(int id, CategoryDto categoryDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var categoryInDb = _context.Categories.SingleOrDefault(c => c.Id == id);
            if (categoryInDb == null)
                return NotFound();
            categoryInDb = Mapper.Map(categoryDto, categoryInDb);

            _context.SaveChanges();

            return Ok();

        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var categoryInDb = _context.Categories.SingleOrDefault(c => c.Id == id);
            _context.Categories.Remove(categoryInDb);
            _context.SaveChanges();
            return Ok();
        }

    }
}
