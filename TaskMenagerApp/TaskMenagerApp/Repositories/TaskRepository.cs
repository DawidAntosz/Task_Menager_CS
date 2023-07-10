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

        public MyTask Get(int taskId)
        {
            return _context.Get_Task(taskId);
        }

        public IQueryable<MyTask> Get_TaskList()
        {
            return _context.Get_AllTask();
        }

        public void Added(MyTask task)
        {
            _context.AddTask(task);
        }
        public void Update(int taskId, MyTask task)
        {
            _context.UpdateTask(taskId, task);
        }

        public void Delete(int taskID)
        {
            _context.DeleteTask(taskID);
        }

    }
}
