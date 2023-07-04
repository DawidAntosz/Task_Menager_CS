using TaskMenagerApp.Models;

namespace TaskMenagerApp.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        public MyTask Get(int taskID)
        {
            throw new NotImplementedException();
        }

        public IQueryable<MyTask> GetAllActive()
        {
            throw new NotImplementedException();
        }

        public void Added(MyTask task)
        {
            throw new NotImplementedException();
        }
        public void Update(int taskID, MyTask task)
        {
            throw new NotImplementedException();
        }

        public void Delete(int taskID)
        {
            throw new NotImplementedException();
        }
    }
}
