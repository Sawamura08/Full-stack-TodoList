using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoBackend.DTOs.Request;
using TodoBackend.DTOs.Response;
using TodoBackend.Models;
using TodoBackend.Repo.Interface;

namespace TodoBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly TaskRepoInterface _taskRepoInterface;

        public TasksController(TaskRepoInterface taskRepoInterface)
        {
            _taskRepoInterface = taskRepoInterface;
        }

        [HttpPut("Update")]

        public async Task<IActionResult> Update(TaskRequest req)
        {
            var information = new TaskModel
            {
                taskId = req.taskId,
                title = req.title,
                typeId = req.typeId,
                description = req.description,
                dueDate = req.dueDate,
                subTasks = req.subTasks
            };

            var result = await _taskRepoInterface.Update(information);

            if (result != null)
            {
                var res = new TaskResponse
                {
                    taskId = result.taskId,
                    title = result.title,
                    typeId = result.typeId,
                    description = result.description,
                    dueDate = result.dueDate,
                    subTasks = result.subTasks
                };

                return Ok(res);
            }
            else
            {
                return NotFound();

            }
        }

        [HttpDelete("Delete")]

        public async Task<IActionResult> Delete(int id)
        {
            var result = _taskRepoInterface.Delete(id);

            if (result != null)
            {
                return Ok("TASK SUCESSFULLY DELETED!");
            }
            else
            {
                return NotFound("DELETION FAILED!");
            }
        }
    }
}
