using API_KETNOIGIAOTHUONG.Data;
using API_KETNOIGIAOTHUONG.DTOs;
using API_KETNOIGIAOTHUONG.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly KNGTContext _context;

    public CategoryController(KNGTContext context)
    {
        _context = context;
    }

    // 1. Lấy tất cả danh mục (không lọc)
    // GET: api/Category/all
    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<CategoryDto>>> GetAllCategories()
    {
        var categories = await _context.Categories
            .Select(c => new CategoryDto
            {
                CategoryID = c.CategoryID,
                CategoryName = c.CategoryName,
                ParentCategoryID = c.ParentCategoryID,
                ImageCategoly = c.ImageCategoly
            })
            .ToListAsync();

        return Ok(categories);
    }

    

    // 3. Tìm kiếm danh mục theo keyword (CategoryName)
    // GET: api/Category/search?keyword=someName
    [HttpGet("search")]
    public async Task<ActionResult<IEnumerable<CategoryDto>>> SearchCategories([FromQuery] string keyword)
    {
        if (string.IsNullOrEmpty(keyword))
            return BadRequest("Keyword không được để trống.");

        var categories = await _context.Categories
            .Where(c => c.CategoryName.Contains(keyword))
            .Select(c => new CategoryDto
            {
                CategoryID = c.CategoryID,
                CategoryName = c.CategoryName,
                ParentCategoryID = c.ParentCategoryID,
                ImageCategoly = c.ImageCategoly
            })
            .ToListAsync();

        return Ok(categories);
    }
    // GET: api/Category/5
    [HttpGet("{id}")]
    public async Task<ActionResult<API_KETNOIGIAOTHUONG.DTOs.CategoryDto>> GetCategory(int id)
    {
        var category = await _context.Categories.FindAsync(id);

        if (category == null)
        {
            return NotFound();
        }

        var dto = new API_KETNOIGIAOTHUONG.DTOs.CategoryDto
        {
            CategoryID = category.CategoryID,
            CategoryName = category.CategoryName,
            ParentCategoryID = category.ParentCategoryID,
            ImageCategoly = category.ImageCategoly
        };

        return Ok(dto);
    }

    // POST: api/Category
    [HttpPost]
    public async Task<ActionResult<API_KETNOIGIAOTHUONG.DTOs.CategoryDto>> CreateCategory(API_KETNOIGIAOTHUONG.DTOs.CategoryDto categoryDto)
    {
        var category = new Category
        {
            CategoryName = categoryDto.CategoryName,
            ParentCategoryID = categoryDto.ParentCategoryID,
            ImageCategoly = categoryDto.ImageCategoly
        };

        _context.Categories.Add(category);
        await _context.SaveChangesAsync();

        categoryDto.CategoryID = category.CategoryID;

        return CreatedAtAction(nameof(GetCategory), new { id = category.CategoryID }, categoryDto);
    }

    // PUT: api/Category/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCategory(int id, API_KETNOIGIAOTHUONG.DTOs.CategoryDto categoryDto)
    {
        if (id != categoryDto.CategoryID)
        {
            return BadRequest("Category ID không khớp");
        }

        var category = await _context.Categories.FindAsync(id);
        if (category == null)
        {
            return NotFound();
        }

        category.CategoryName = categoryDto.CategoryName;
        category.ParentCategoryID = categoryDto.ParentCategoryID;
        category.ImageCategoly = categoryDto.ImageCategoly;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CategoryExists(id))
                return NotFound();
            else
                throw;
        }

        return NoContent();
    }

    // DELETE: api/Category/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category == null)
        {
            return NotFound();
        }

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool CategoryExists(int id)
    {
        return _context.Categories.Any(e => e.CategoryID == id);
    }
}
