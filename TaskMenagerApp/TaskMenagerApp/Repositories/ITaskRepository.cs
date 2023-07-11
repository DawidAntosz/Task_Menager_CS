using TaskMenagerApp.Models;

namespace TaskMenagerApp.Repositories
{
    public interface ITaskRepository
    {
        MyTask Get(Guid TaskId);
        IQueryable<MyTask> Get_TaskList(Guid UserId);
        void Added(MyTask task, Guid UserId);
        void Update(Guid TaskId, MyTask task);
        void Delete(Guid TaskId);
    }
}
