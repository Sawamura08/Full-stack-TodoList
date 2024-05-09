using TodoBackend.DTOs.Request;
using TodoBackend.Models;

namespace TodoBackend.Repo.Interface
{
    public interface TaskRepoInterface
    {
        Task<TaskModel> Update(TaskModel model);

        Task<TaskModel> Delete(int id);

    }
}
