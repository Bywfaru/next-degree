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
        var degreeResponses = degrees.Select(d => new DegreeResponseDto
        {
            UserId = userId,
            Id = d.Id,
            Name = d.Name,
            Status = d.Status,
            CompletedCredits = d.DegreeCourses.Sum(dc =>
            {
                if (dc.Course is not { Status: Status.Completed }) return 0;

                return dc.Course.Credits;
            }),
            TotalCredits = d.DegreeCourses.Sum(dc =>
            {
                if (dc.Course == null) return 0;

                return dc.Course.Credits;
            })
        }).ToList();

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
            CompletedCredits = degree.DegreeCourses.Sum(dc =>
            {
                if (dc.Course is not { Status: Status.Completed }) return 0;

                return dc.Course.Credits;
            }),
            TotalCredits = degree.DegreeCourses.Sum(dc =>
            {
                if (dc.Course == null) return 0;

                return dc.Course.Credits;
            })
        };

        return Ok(new Response<DegreeResponseDto>(degreeResponse));
    }

    [HttpPost]
    public async Task<ActionResult<Response<Degree>>> CreateDegree(
        [FromBody] CreateDegreeRequestDto createDegreeRequest)
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

    [HttpPut("{id}")]
    public async Task<ActionResult<Response<Degree>>> UpdateDegree(string id,
        [FromBody] UpdateDegreeRequestDto updateDegreeRequest)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var degreeToReplace =
            await context.Degrees.Where(d => d.Id == id && d.UserId == userId).FirstAsync();

        degreeToReplace.Name = updateDegreeRequest.Name ?? degreeToReplace.Name;
        degreeToReplace.Status = updateDegreeRequest.Status ?? degreeToReplace.Status;

        await context.SaveChangesAsync();

        return Ok(new Response<Degree>(degreeToReplace));
    }

    [HttpPost("{id}/courses")]
    public async Task<ActionResult<Response<DegreeCourse>>> AddCourseToDegree(string id,
        [FromBody] CreateDegreeCourseDto createDegreeCourseRequest)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId == null) return Unauthorized();

        var degree = await context.Degrees.Where(d => d.Id == id && d.UserId == userId).FirstAsync();

        if (degree == null) return NotFound();

        var course = await context.Courses.Where(c => c.Id == createDegreeCourseRequest.CourseId && c.UserId == userId)
            .FirstAsync();

        if (course == null) return NotFound();

        var newDegreeCourse = new DegreeCourse
        {
            DegreeId = id,
            CourseId = createDegreeCourseRequest.CourseId,
            UserId = userId
        };

        context.DegreeCourses.Add(newDegreeCourse);

        await context.SaveChangesAsync();

        return Ok(new Response<DegreeCourse>(newDegreeCourse));
    }

    [HttpDelete("{degreeId}/courses/{courseId}")]
    public async Task<ActionResult<Response<DegreeCourse>>> RemoveCourseFromDegree(string degreeId, string courseId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId == null) return Unauthorized();

        var degreeCourse = await context.DegreeCourses
            .Where(dc => dc.DegreeId == degreeId && dc.CourseId == courseId && dc.UserId == userId)
            .FirstAsync();

        context.DegreeCourses.Remove(degreeCourse);

        await context.SaveChangesAsync();

        return Ok(new Response<DegreeCourse>(degreeCourse));
    }
}