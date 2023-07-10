using TaskMenagerApp.Models;

namespace TaskMenagerApp.Repositories
{
    public interface ITaskRepository
    {
        MyTask Get(int taskID);
        IQueryable<MyTask> Get_TaskList();
        void Added(MyTask task);
        void Update(int taskID, MyTask task);
        void Delete(int taskID);
    }
}
