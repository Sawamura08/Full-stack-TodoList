using Dapper;
using System.Data;
using System.Reflection.Metadata;
using TodoBackend.Models;
using TodoBackend.Models.Data;
using TodoBackend.Repo.Interface;

namespace TodoBackend.Repo.Implementation
{
    public class PersonRepo : PersonsRepoInterface
    {
        private readonly AppDbContext _appDbContext;
        public PersonRepo(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<List<Persons>> GetAll()
        {
            string query = "SELECT * FROM employee_info";
            using (var conn = this._appDbContext.CreateConnection())
            {
                var result = await conn.QueryAsync<Persons>(query);
                return result.ToList();
            }
        }

        public async Task<Persons> GetById(int id)
        {
            string query = "SELECT * FROM employee_info WHERE id=@id";
            using (var conn = this._appDbContext.CreateConnection())
            {
                var result = await conn.QueryFirstOrDefaultAsync<Persons>(query, new { id });
                return result;
            }
        }

        public async Task<Persons> CreatePersonInfo(Persons persons)
        {
            var parameters = new DynamicParameters();
            parameters.Add("first_name", persons.first_name, DbType.String);
            parameters.Add("last_name", persons.last_name, DbType.String);
            parameters.Add("email", persons.email, DbType.String);
            parameters.Add("username", persons.username, DbType.String);
            parameters.Add("password", persons.password, DbType.String);
            string query = @"INSERT INTO employee_info (first_name,last_name,email,username,password) 
            VALUES (@first_name,@last_name,@email,@username,@password)
            RETURNING *
               ";
            using (var conn = this._appDbContext.CreateConnection())
            {
                var result = await conn.QueryFirstOrDefaultAsync<Persons>(query, parameters);
                return result;
            }

        }

        public async Task<LoginCredentials> Login(LoginCredentials credentials)
        {
            var parameters = new DynamicParameters();
            parameters.Add("username", credentials.username, DbType.String);
            parameters.Add("password", credentials.password, DbType.String);
            string query = @"SELECT username,password,id FROM employee_info 
                WHERE username = @username AND password = @password";
            using (var conn = this._appDbContext.CreateConnection())
            {
                var result = await conn.QueryFirstOrDefaultAsync<LoginCredentials>(query, parameters);
                return result;
            }
        }

        public async Task<AddType> AddType(AddType type)
        {
            var parameters = new DynamicParameters();
            parameters.Add("typeNames", type.typeNames, DbType.String);
            parameters.Add("color", type.color, DbType.String);
            var query = @"INSERT INTO taskTypes (typeNames, color) VALUES (@typeNames, @color) RETURNING *";



            using (var conn = this._appDbContext.CreateConnection())
            {
                var result = await conn.QueryFirstOrDefaultAsync<AddType>(query, parameters);

                result ??= new AddType();

                return result;
            }
        }


        public async Task<AddTask> AddTask(AddTask task)
        {
            var parameters = new DynamicParameters();
            parameters.Add("title", task.title, DbType.String);
            parameters.Add("description", task.description, DbType.String);
            parameters.Add("typeId", task.typeId, DbType.Int64);
            parameters.Add("dueDate", task.dueDate, DbType.Date);
            parameters.Add("subTasks", task.subTasks, DbType.String);

            var query = @"INSERT INTO taskList (title,description,typeId,dueDate,subTasks) 
            VALUES (@title,@description,@typeId,@dueDate,@subTasks) RETURNING *";

            using (var conn = this._appDbContext.CreateConnection())
            {
                var result = await conn.QueryFirstOrDefaultAsync<AddTask>(query, parameters);

                result ??= new AddTask(); // checking whether it's null or not

                return result;
            }
        }
    }
}
