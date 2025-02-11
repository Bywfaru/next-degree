using System.Security.Claims;
using DegreePlanner.Data;
using DegreePlanner.Entities;
using DegreePlanner.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DegreePlanner.Controllers;

[Route("assignments/categories")]
[ApiController]
[Authorize]
public class AssignmentCategories(DegreePlannerContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<Response<List<AssignmentCategory>>>> GetAllAssignmentCategories()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var assignmentCategories = await context.AssignmentCategories
            .Where(ac => ac.UserId == userId)
            .ToListAsync();

        return Ok(new Response<List<AssignmentCategory>>(assignmentCategories));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Response<AssignmentCategory>>> GetAssignmentCategory(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var assignmentCategory = await context.AssignmentCategories
            .Where(ac => ac.Id == id && ac.UserId == userId)
            .FirstAsync();

        return Ok(new Response<AssignmentCategory>(assignmentCategory));
    }

    [HttpPost]
    public async Task<ActionResult<Response<AssignmentCategory>>> CreateAssignmentCategory(
        [FromBody] CreateAssignmentCategoryDto assignmentCategory)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId == null) return Unauthorized();

        var newAssignmentCategory = new AssignmentCategory
        {
            Name = assignmentCategory.Name,
            Weight = assignmentCategory.Weight,
            CourseId = assignmentCategory.CourseId,
            UserId = userId
        };

        context.Add(newAssignmentCategory);

        await context.SaveChangesAsync();

        return Ok(new Response<AssignmentCategory>(newAssignmentCategory));
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<Response<AssignmentCategory>>> DeleteAssignmentCategory(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var assignmentCategory = await context.AssignmentCategories
            .Where(ac => ac.Id == id && ac.UserId == userId)
            .FirstAsync();

        context.Remove(assignmentCategory);

        await context.SaveChangesAsync();

        return Ok(new Response<AssignmentCategory>(assignmentCategory));
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<Response<AssignmentCategory>>> UpdateAssignmentCategory(int id,
        [FromBody] UpdateAssignmentCategoryDto assignmentCategory)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var assignmentCategoryToUpdate = await context.AssignmentCategories
            .Where(ac => ac.Id == id && ac.UserId == userId)
            .FirstAsync();

        assignmentCategoryToUpdate.Name = assignmentCategory.Name ?? assignmentCategoryToUpdate.Name;
        assignmentCategoryToUpdate.Weight = assignmentCategory.Weight ?? assignmentCategoryToUpdate.Weight;
        assignmentCategoryToUpdate.CourseId = assignmentCategory.CourseId ?? assignmentCategoryToUpdate.CourseId;

        await context.SaveChangesAsync();

        return Ok(new Response<AssignmentCategory>(assignmentCategoryToUpdate));
    }
}