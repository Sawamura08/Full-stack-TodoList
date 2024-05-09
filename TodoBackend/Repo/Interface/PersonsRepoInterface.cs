using TodoBackend.Models;

namespace TodoBackend.Repo.Interface
{
    public interface PersonsRepoInterface
    {
        Task<List<Persons>> GetAll();

        Task<Persons> GetById(int id);

        Task<Persons> CreatePersonInfo(Persons person);

        Task<LoginCredentials> Login(LoginCredentials credentials);

        Task<AddType> AddType(AddType type);

        Task<AddTask> AddTask(AddTask task);
    }
}
