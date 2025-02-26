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
public class DegreesController(DegreePlannerContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<Response<List<DegreeResponseDto>>>> GetAllDegrees()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId == null) return Unauthorized();

        var degrees = await context.Degrees.Where(d => d.UserId == userId).ToListAsync();
        var degreeResponses = degrees
            .Select(d => new DegreeResponseDto
            {
                UserId = userId,
                Id = d.Id,
                Name = d.Name,
                Status = d.Status,
                CompletedCredits = d.Courses.Sum(c => c.Status == Status.Completed ? c.Credits : 0),
                TotalCredits = d.Courses.Sum(c => c.Credits)
            })
            .ToList();

        return Ok(new Response<List<DegreeResponseDto>>(degreeResponses));
    }

    [HttpGet("{degreeId}")]
    public async Task<ActionResult<Response<DegreeResponseDto>>> GetDegree(string degreeId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId == null) return Unauthorized();

        var degree = await context.Degrees.Where(d => d.Id == degreeId && d.UserId == userId).FirstAsync();
        var degreeResponse = new DegreeResponseDto
        {
            UserId = userId,
            Id = degree.Id,
            Name = degree.Name,
            Status = degree.Status,
            CompletedCredits = degree.Courses.Sum(c => c.Status == Status.Completed ? c.Credits : 0),
            TotalCredits = degree.Courses.Sum(c => c.Credits)
        };

        return Ok(new Response<DegreeResponseDto>(degreeResponse));
    }

    [HttpPost]
    public async Task<ActionResult<Response<Degree>>> CreateDegree
        ([FromBody] CreateDegreeRequestDto createDegreeRequest)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId == null) return Unauthorized();

        var newDegree = new Degree
        {
            Name = createDegreeRequest.Name,
            UserId = userId,
            Status = createDegreeRequest.Status
        };

        context.Add(newDegree);

        await context.SaveChangesAsync();

        return Ok(new Response<Degree>(newDegree));
    }

    [HttpDelete("{degreeId}")]
    public async Task<ActionResult<Response<Degree>>> DeleteDegree(string degreeId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var degree = await context.Degrees.Where(d => d.Id == degreeId && d.UserId == userId).FirstAsync();

        context.Degrees.Remove(degree);

        await context.SaveChangesAsync();

        return Ok(new Response<Degree>(degree));
    }

    [HttpPut("{degreeId}")]
    public async Task<ActionResult<Response<Degree>>> UpdateDegree
    (
        string degreeId,
        [FromBody] UpdateDegreeRequestDto updateDegreeRequest
    )
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var degreeToReplace =
            await context.Degrees.Where(d => d.Id == degreeId && d.UserId == userId).FirstAsync();

        degreeToReplace.Name = updateDegreeRequest.Name ?? degreeToReplace.Name;
        degreeToReplace.Status = updateDegreeRequest.Status ?? degreeToReplace.Status;

        await context.SaveChangesAsync();

        return Ok(new Response<Degree>(degreeToReplace));
    }

    [HttpGet("{degreeId}/courses")]
    public async Task<ActionResult<Response<DegreeCoursesResponseDto>>> GetCoursesForDegree(string degreeId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId == null) return Unauthorized();

        var degreeCourses = await context
            .Courses
            .Where(c => c.DegreeId == degreeId && c.UserId == userId)
            .Include(c => c.Assignments)
            .Include(c => c.AssignmentCategories)
            .ToListAsync();

        var courseResponses = degreeCourses
            .Select(dc => new DegreeCoursesResponseDto
            {
                UserId = userId,
                Id = dc.Id,
                Name = dc.Name,
                Code = dc.Code,
                Credits = dc.Credits,
                PassingGrade = dc.PassingGrade,
                Status = dc.Status,
                DegreeId = dc.DegreeId,
                CurrentGrade =
                    dc.Assignments.Sum(a => a.GradeReceived / a.GradePossible * a.AssignmentCategory.Weight) ?? 1
            })
            .ToList();

        return Ok(new Response<List<DegreeCoursesResponseDto>>(courseResponses));
    }

    [HttpPost("{degreeId}/courses")]
    public async Task<ActionResult<Response<Course>>> AddCourseToDegree
    (
        string degreeId,
        [FromBody] CreateCourseDto createDegreeCourseRequest
    )
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId == null) return Unauthorized();

        var degree = await context.Degrees.Where(d => d.Id == degreeId && d.UserId == userId).FirstAsync();

        if (degree == null) return NotFound();

        var newCourse = new Course
        {
            Code = createDegreeCourseRequest.Code,
            Credits = createDegreeCourseRequest.Credits,
            Name = createDegreeCourseRequest.Name,
            DegreeId = degreeId,
            UserId = userId,
            PassingGrade = createDegreeCourseRequest.PassingGrade,
            Status = createDegreeCourseRequest.Status
        };

        context.Courses.Add(newCourse);

        await context.SaveChangesAsync();

        return Ok(new Response<Course>(newCourse));
    }

    [HttpDelete("{degreeId}/courses/{courseId}")]
    public async Task<ActionResult<Response<Course>>> DeleteCourseFromDegree(string degreeId, string courseId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId == null) return Unauthorized();

        var degreeCourse = await context
            .Courses
            .Where(dc => dc.DegreeId == degreeId && dc.Id == courseId && dc.UserId == userId)
            .FirstAsync();

        context.Courses.Remove(degreeCourse);

        await context.SaveChangesAsync();

        return Ok(new Response<Course>(degreeCourse));
    }
}
