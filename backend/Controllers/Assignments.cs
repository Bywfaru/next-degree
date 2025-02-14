using System.Security.Claims;
using DegreePlanner.Data;
using DegreePlanner.Entities;
using DegreePlanner.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DegreePlanner.Controllers;

[Route("[controller]")]
[ApiController]
[Authorize]
public class Assignments(DegreePlannerContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<Response<List<Assignment>>>> GetAllAssignments()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var assignments = await context.Assignments
            .Where(a => a.UserId == userId)
            .ToListAsync();

        return Ok(new Response<List<Assignment>>(assignments));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Response<Assignment>>> GetAssignment(string id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var assignment = await context.Assignments
            .Where(a => a.Id == id && a.UserId == userId)
            .FirstAsync();

        return Ok(new Response<Assignment>(assignment));
    }

    [HttpPost]
    public async Task<ActionResult<Response<Assignment>>> CreateAssignment([FromBody] CreateAssignmentDto assignment)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId == null) return Unauthorized();

        var assignmentCategory = await context.AssignmentCategories
            .Where(ac => ac.Id == assignment.AssignmentCategoryId && ac.UserId == userId)
            .FirstAsync();

        if (assignmentCategory == null) return NotFound();

        var newAssignment = new Assignment
        {
            CourseId = assignment.CourseId,
            Name = assignment.Name,
            UserId = userId,
            DueDate = assignment.DueDate,
            GradePossible = assignment.GradePossible,
            Description = assignment.Description,
            IsCompleted = assignment.IsCompleted,
            CompletedDate = assignment.CompletedDate,
            GradeReceived = assignment.GradeReceived,
            AssignmentCategoryId = assignment.AssignmentCategoryId
        };

        context.Add(newAssignment);

        await context.SaveChangesAsync();

        return Ok(new Response<Assignment>(newAssignment));
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<Response<Assignment>>> DeleteAssignment(string id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var assignment = await context.Assignments
            .Where(a => a.Id == id && a.UserId == userId)
            .FirstAsync();

        context.Remove(assignment);

        await context.SaveChangesAsync();

        return Ok(new Response<Assignment>(assignment));
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<Response<Assignment>>> UpdateAssignment(string id,
        [FromBody] UpdateAssignmentDto assignment)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var assignmentToUpdate = await context.Assignments
            .Where(a => a.Id == id && a.UserId == userId)
            .FirstAsync();

        assignmentToUpdate.CourseId = assignment.CourseId ?? assignmentToUpdate.CourseId;
        assignmentToUpdate.AssignmentCategoryId =
            assignment.AssignmentCategoryId ?? assignmentToUpdate.AssignmentCategoryId;
        assignmentToUpdate.Name = assignment.Name ?? assignmentToUpdate.Name;
        assignmentToUpdate.Description = assignment.Description ?? assignmentToUpdate.Description;
        assignmentToUpdate.GradeReceived = assignment.GradeReceived ?? assignmentToUpdate.GradeReceived;
        assignmentToUpdate.GradePossible = assignment.GradePossible ?? assignmentToUpdate.GradePossible;
        assignmentToUpdate.DueDate = assignment.DueDate ?? assignmentToUpdate.DueDate;
        assignmentToUpdate.CompletedDate = assignment.CompletedDate ?? assignmentToUpdate.CompletedDate;
        assignmentToUpdate.IsCompleted = assignment.IsCompleted ?? assignmentToUpdate.IsCompleted;

        await context.SaveChangesAsync();

        return Ok(new Response<Assignment>(assignmentToUpdate));
    }
}