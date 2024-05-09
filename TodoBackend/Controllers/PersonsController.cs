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
    public class PersonsController : ControllerBase
    {
        private readonly PersonsRepoInterface _personsRepo;
        public PersonsController(PersonsRepoInterface personsRepo)
        {
            _personsRepo = personsRepo;
        }

        [HttpGet("GetAll")]

        public async Task<IActionResult> GetAll()
        {
            // call the query and get value
            var result = await _personsRepo.GetAll();

            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }

        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int Id)
        {
            var result = await _personsRepo.GetById(Id);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost("CreatePersonInfo")]

        public async Task<IActionResult> CreatePersonInfo(PersonsRequest req)
        {
            var information = new Persons
            {
                first_name = req.first_name,
                last_name = req.last_name,
                email = req.email,
                username = req.username,
                password = req.password
            };


            var result = await _personsRepo.CreatePersonInfo(information);
            if (result != null)
            {
                var res = new PersonResponse
                {
                    id = result.id,
                    first_name = result.first_name,
                    last_name = result.last_name,
                    email = result.email,
                    username = result.username,
                    password = result.password

                };

                return Ok(res);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost("Login")]

        public async Task<IActionResult> Login(LoginRequest req)
        {
            var information = new LoginCredentials
            {
                username = req.username,
                password = req.password
            };

            var result = await this._personsRepo.Login(information);


            if (result != null)
            {
                var res = new PersonResponse
                {
                    id = result.id,
                    username = result.username,
                    password = result.password
                };

                return Ok(res);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost("AddType")]

        public async Task<IActionResult> AddType(AddTypeRequest req)
        {
            var information = new AddType
            {
                typeNames = req.typeNames,
                color = req.color
            };

            var result = await this._personsRepo.AddType(information);

            if (result != null)
            {
                var res = new AddTypeResponse
                {
                    typeId = result.typeId,
                    typeNames = result.typeNames,
                    color = result.color
                };

                return Ok(res);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost("AddTask")]

        public async Task<IActionResult> AddTask(AddTaskRequest req)
        {
            var information = new AddTask
            {
                title = req.title,
                typeId = req.typeId,
                description = req.description,
                dueDate = req.dueDate,
                subTasks = req.subTasks
            };

            var result = await this._personsRepo.AddTask(information);

            if (result != null)
            {
                var res = new AddTaskResponse
                {
                    taskId = result.taskId,
                    title = result.title,
                    description = result.description,
                    typeId = result.typeId,
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


    }
}
