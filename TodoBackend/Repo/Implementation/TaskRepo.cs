using TodoBackend.Models.Data;
using TodoBackend.Repo.Interface;
using Dapper;
using TodoBackend.Models;
using System.Data;

namespace TodoBackend.Repo.Implementation
{
    public class TaskRepo : TaskRepoInterface
    {
        private readonly AppDbContext _appDbContext;
        public TaskRepo(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<TaskModel> Update(TaskModel model)
        {
            var parameters = new DynamicParameters();
            parameters.Add("taskid", model.taskId, DbType.Int64);
            parameters.Add("title", model.title, DbType.String);
            parameters.Add("typeid", model.typeId, DbType.Int64);
            parameters.Add("description", model.description, DbType.String);
            parameters.Add("dueDate", model.dueDate, DbType.Date);
            parameters.Add("subTasks", model.subTasks, DbType.String);

            var query = @"UPDATE taskList SET title = @title, typeId = @typeId,
            description = @description, dueDate = @dueDate, subTasks = @subTasks
            WHERE ""taskid"" = @taskid RETURNING *";

            using (var conn = _appDbContext.CreateConnection())
            {
                var result = await conn.QueryFirstOrDefaultAsync<TaskModel>(query, parameters);

                result ??= new TaskModel();
                return result;
            }
        }

        public async Task<TaskModel> Delete(int id)
        {

            var query = @"DELETE FROM taskList WHERE ""taskid"" = @id";

            using (var conn = _appDbContext.CreateConnection())
            {
                var result = await conn.QueryFirstOrDefaultAsync<TaskModel>(query, new { id });

                result ??= new TaskModel();

                return result;
            }
        }
    }
}
