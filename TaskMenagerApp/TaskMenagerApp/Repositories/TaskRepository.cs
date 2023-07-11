using System.Threading.Tasks;
using TaskMenagerApp.Models;

namespace TaskMenagerApp.Repositories
{
    public class TaskRepository : ITaskRepository
    {

        private readonly TaskMenagerContext _context;

        public TaskRepository(TaskMenagerContext context)
        {
            _context = context;
        }

        public MyTask Get(Guid TaskId)
        {
            return _context.Get_Task(TaskId);
        }

        public IQueryable<MyTask> Get_TaskList(Guid UserId)
        {
            return _context.Get_AllTask(UserId);
        }

        public void Added(MyTask task, Guid UserId)
        {
            _context.AddTask(task, UserId);
        }
        public void Update(Guid TaskId, MyTask task)
        {
            _context.UpdateTask(TaskId, task);
        }

        public void Delete(Guid TaskId)
        {
            _context.DeleteTask(TaskId);
        }

    }
}
