using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using TaskHub.API.Data;
using TaskHub.API.Models;

namespace TaskHub.API.Controllers
{
    /// <summary>
    /// Provides API endpoints for managing task items in the system.
    /// </summary>
    /// <remarks>This controller handles CRUD operations for task items, including retrieving all tasks,
    /// retrieving a specific task by ID, creating new tasks, updating existing tasks, and deleting tasks. The
    /// controller uses an <see cref="AppDbContext"/> instance to interact with the database.</remarks>
    [Route("api/[controller]")]
    [ApiController]
    public class TaskItemsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TaskItemsController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves all task items from the data source.
        /// </summary>
        /// <remarks>This method performs an asynchronous operation to fetch all task items from the
        /// underlying data source. The returned collection will be empty if no task items are found.</remarks>
        /// <returns>A task that represents the asynchronous operation. The task result contains an <see cref="ActionResult{T}"/>
        /// with an <see cref="IEnumerable{T}"/> of <see cref="TaskItem"/> objects representing the retrieved task
        /// items.</returns>
        // GET: api/TaskItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskItem>>> GetTaskItems()
        {
            return await _context.TaskItems.ToListAsync();
        }

        // GET: api/TaskItems/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(TaskItem), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TaskItem>> GetTaskItem(int id)
        {
            var taskItem = await _context.TaskItems.FindAsync(id);

            if (taskItem == null)
            {
                return NotFound();
            }

            return taskItem;
        }

        // PUT: api/TaskItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTaskItem(int id, TaskItem taskItem)
        {
            if (id != taskItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(taskItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TaskItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TaskItem>> PostTaskItem(TaskItem taskItem)
        {
            if(ModelState.IsValid is false)
            {
                return BadRequest(ModelState);
            }

            _context.TaskItems.Add(taskItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTaskItem", new { id = taskItem.Id }, taskItem);
        }

        // DELETE: api/TaskItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaskItem(int id)
        {
            var taskItem = await _context.TaskItems.FindAsync(id);
            if (taskItem == null)
            {
                return NotFound();
            }

            _context.TaskItems.Remove(taskItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TaskItemExists(int id)
        {
            return _context.TaskItems.Any(e => e.Id == id);
        }
    }
}
