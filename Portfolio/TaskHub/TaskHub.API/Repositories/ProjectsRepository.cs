using Microsoft.EntityFrameworkCore;

using TaskHub.API.Data;
using TaskHub.API.Dtos;
using TaskHub.API.Models;

namespace TaskHub.API.Repositories
{
    public class ProjectsRepository
    {

        private readonly AppDbContext dbContext;

        public ProjectsRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        internal async Task<Project> CreateProjectAsync(ProjectDto dto)
        {
            if (dto is null)
            {
                throw new ArgumentNullException(nameof(dto), "Project cannot be null.");
            }

            if (await dbContext.Users.AnyAsync(u => u.Id == dto.OwnerId) is false)
            {
                throw new ArgumentException("Owner does not exist.", nameof(dto.OwnerId));
            }

            var project = new Project
            {
                Name = dto.Name,
                Description = dto.Description,
                OwnerId = dto.OwnerId
            };

            var addedPRoject = await dbContext.Projects.AddAsync(project);
            await dbContext.SaveChangesAsync();

            return addedPRoject.Entity;
        }

        internal async Task<bool> DeleteProjectAsync(int id)
        {
            var project = await GetProjectByIdAsync(id);
            if (project is null)
            {
                return false;
            }

            dbContext.Projects.Remove(project);
            await dbContext.SaveChangesAsync();

            return true;
        }

        internal async Task<List<Project>> GetAllProjectsAsync()
        {
            return await dbContext.Projects
                .Include(p => p.Owner)
                .Include(p => p.Tasks)
                .ToListAsync();
        }

        internal async Task<Project?> GetProjectByIdAsync(int id)
        {
            return await dbContext.Projects
                .Include(p => p.Owner)
                .Include(p => p.Tasks)
                .FirstOrDefaultAsync(p => p.Id == id);

        }

        internal async Task<Project?> UpdateProjectAsync(int id, ProjectDto dto)
        {
            var existingProject = await dbContext.Projects.FirstOrDefaultAsync(p => p.Id == id);

            if (existingProject is null)
            {
                return null;
            }

            existingProject.Name = dto.Name;
            existingProject.Description = dto.Description;

            await dbContext.SaveChangesAsync();

            return existingProject;
        }

        internal async Task<Project?> UpdateProjectOwnerAsync(int id, ProjectOwnerUpdateDto dto)
        {
            var project = await dbContext.Projects.FindAsync(id);
            if (project is null)
            {
                return null;
            }

            if (await dbContext.Users.AnyAsync(u => u.Id == dto.OwnerId) is false)
            {
                return null;
            }

            project.OwnerId = dto.OwnerId;

            await dbContext.SaveChangesAsync();

            return project;
        }

        internal async Task<Project?> UpdateProjectTasksAsync(int id, ProjectTasksUpdateDto dto)
        {
            var project = await dbContext.Projects
                .Include(p => p.Tasks)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (project is null)
            {
                return null;
            }

            project.Tasks.Clear();

            foreach (var task in dto.Tasks)
            {
                var newTask = new TaskItem
                {
                    Title = task.Title,
                    Description = task.Description,
                    IsCompleted = task.IsCompleted,
                    ProjectId = id
                };

                project.Tasks.Add(newTask);
            }

            await dbContext.SaveChangesAsync();

            return project;
        }

        internal async Task<Project?> AddNewTaskToProjectASync(int id, TaskDto dto)
        {
            var project = await dbContext.Projects
                .Include(p => p.Tasks)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (project is null)
            {
                return null;
            }

            if (await dbContext.Users.AnyAsync(u => u.Id == dto.AssignedUserId) is false)
            {
                return null;
            }

            var newTask = new TaskItem
            {
                Title = dto.Title,
                Description = dto.Description,
                IsCompleted = dto.IsCompleted,
                AssignedUserId = dto.AssignedUserId,
                ProjectId = id
            };

            project.Tasks.Add(newTask);

            await dbContext.SaveChangesAsync();

            return project;
        }
    }
}
