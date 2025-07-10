using Microsoft.AspNetCore.Mvc;

using TaskHub.API.Dtos;
using TaskHub.API.Models;
using TaskHub.API.Repositories;

namespace TaskHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly ProjectsRepository repository;

        public ProjectsController(ProjectsRepository repository)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpGet] // GET: api/projects
        public async Task<IActionResult> GetAllProjectsAsync()
        {
            var projects = await repository.GetAllProjectsAsync();
            return Ok(projects);
        }

        [HttpGet("{id}")] // GET: api/projects/{id}
        public async Task<IActionResult> GetProjectByIdAsync(int id)
        {
            var project = await repository.GetProjectByIdAsync(id);
            if (project is null)
            {
                return NotFound();
            }

            return Ok(project);
        }

        [HttpPost] // POST: api/projects
        public async Task<IActionResult> CreateProjectAsync([FromBody] ProjectDto dto)
        {
            if (dto is null)
            {
                return BadRequest("Project cannot be null.");
            }

            var createdProject = await repository.CreateProjectAsync(dto);
            if (createdProject is null)
            {
                return BadRequest("Project creation failed.");
            }

            return CreatedAtAction(nameof(GetProjectByIdAsync), new { id = createdProject.Id }, createdProject);
        }

        [HttpPut("{id}")] // PUT: api/projects/{id}
        public async Task<IActionResult> UpdateProjectAsync(int id, [FromBody] ProjectDto dto)
        {
            if (dto is null)
            {
                return BadRequest("Project data is invalid.");
            }

            var updatedProject = await repository.UpdateProjectAsync(id, dto);
            if (updatedProject is null)
            {
                return NotFound();
            }

            return Ok(updatedProject);
        }

        [HttpPut("{id}/owner")]
        public async Task<IActionResult> UpdateProjectOwnerAsync(int id, [FromBody] ProjectOwnerUpdateDto dto)
        {
            if (dto is null || dto.OwnerId != id)
            {
                return BadRequest("Project data is invalid.");
            }

            var updatedProject = await repository.UpdateProjectOwnerAsync(id, dto);

            if (updatedProject is null)
            {
                return NotFound();
            }

            return Ok(updatedProject);
        }

        [HttpPut("{id}/tasks")]
        public async Task<IActionResult> UpdateProjectTasksAsync(int id, [FromBody] ProjectTasksUpdateDto dto)
        {
            if (dto is null || dto.Tasks.Any() is false)
            {
                return BadRequest("Task data is invalid.");
            }

            var updatedProject = await repository.UpdateProjectTasksAsync(id, dto);
            if (updatedProject is null)
            {
                return NotFound();
            }
            return Ok(updatedProject);
        }

        [HttpDelete("{id}")] // DELETE: api/projects/{id}
        public async Task<IActionResult> DeleteProjectAsync(int id)
        {
            await repository.DeleteProjectAsync(id);

            return NoContent(); // 204 No Content
        }
    }
}
