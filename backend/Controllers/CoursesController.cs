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
public class CoursesController(DegreePlannerContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<Response<List<Course>>>> GetAllCourses()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        // var courses = await context.Courses.Where(c => c.UserId == userId).ToListAsync();

        return Ok(new Response<List<Course>>([]));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Response<Course>>> GetCourse(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var course = await context.Courses.Where(c => c.Id == id && c.UserId == userId).FirstAsync();

        return Ok(new Response<Course>(course));
    }

    [HttpPost]
    public async Task<ActionResult<Response<Course>>> CreateCourse([FromBody] CreateCourseDto course)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId == null) return Unauthorized();

        var newCourse = new Course
        {
            Name = course.Name,
            UserId = userId,
            Code = course.Code,
            Credits = course.Credits,
            PassingGrade = course.PassingGrade,
            Status = course.Status
        };

        context.Add(newCourse);

        await context.SaveChangesAsync();

        return Ok(new Response<Course>(newCourse));
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<Response<Course>>> DeleteCourse(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var course = await context.Courses.Where(c => c.Id == id && c.UserId == userId).FirstAsync();

        context.Remove(course);

        await context.SaveChangesAsync();

        return Ok(new Response<Course>(course));
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<Response<Course>>> UpdateCourse(int id, [FromBody] UpdateCourseDto course)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var courseToUpdate = await context.Courses.Where(c => c.Id == id && c.UserId == userId).FirstAsync();

        courseToUpdate.Name = course.Name ?? courseToUpdate.Name;
        courseToUpdate.Code = course.Code ?? courseToUpdate.Code;
        courseToUpdate.Credits = course.Credits ?? courseToUpdate.Credits;
        courseToUpdate.PassingGrade = course.PassingGrade ?? courseToUpdate.PassingGrade;
        courseToUpdate.Status = course.Status ?? courseToUpdate.Status;

        await context.SaveChangesAsync();

        return Ok(new Response<Course>(courseToUpdate));
    }

    [HttpGet("{id:int}/prerequisites")]
    public async Task<ActionResult<Response<List<Course>>>> GetPrerequisites(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var prerequisiteCourses = await context.Prerequisites
            .Where(p => p.UserId == userId && p.CourseId == id)
            .Select(p => p.PrerequisiteCourse)
            .ToListAsync();

        return Ok(new Response<List<Course>>(prerequisiteCourses));
    }

    [HttpPost("{id:int}/prerequisites")]
    public async Task<ActionResult<Response<Course>>> AddPrerequisite(int id,
        [FromBody] CreatePrerequisiteDto prerequisite)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId == null) return Unauthorized();

        var existingPrerequisite = await context.Prerequisites
            .Where(p => p.UserId == userId && p.CourseId == id &&
                        p.PrerequisiteCourseId == prerequisite.PrerequisiteCourseId)
            .FirstOrDefaultAsync();

        if (existingPrerequisite != null) return BadRequest("Prerequisite already exists.");

        var course = await context.Courses
            .Where(c => c.Id == id && c.UserId == userId)
            .FirstAsync();
        var prerequisiteCourse = await context.Courses
            .Where(c => c.Id == prerequisite.PrerequisiteCourseId && c.UserId == userId)
            .FirstAsync();

        if (course == null || prerequisiteCourse == null) return NotFound();

        var newPrerequisite = new Prerequisite
        {
            CourseId = id,
            PrerequisiteCourseId = prerequisite.PrerequisiteCourseId,
            UserId = userId
        };

        context.Add(newPrerequisite);

        await context.SaveChangesAsync();

        return Ok(new Response<Course>(course));
    }

    [HttpDelete("{id:int}/prerequisites/{prerequisiteCourseId:int}")]
    public async Task<ActionResult<Response<Prerequisite>>> RemovePrerequisite(int id, int prerequisiteCourseId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var prerequisite = await context.Prerequisites
            .Where(p => p.UserId == userId && p.CourseId == id && p.PrerequisiteCourseId == prerequisiteCourseId)
            .Include(prerequisite => prerequisite.Course)
            .FirstAsync();

        context.Remove(prerequisite);

        await context.SaveChangesAsync();

        return Ok(new Response<Prerequisite>(prerequisite));
    }
}