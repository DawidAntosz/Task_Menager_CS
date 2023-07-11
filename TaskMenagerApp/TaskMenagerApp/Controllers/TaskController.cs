using Microsoft.AspNetCore.Mvc;
using TaskMenagerApp.Models;
using TaskMenagerApp.Repositories;


namespace TaskMenagerApp.Controllers
{
    public class TaskController : Controller
    {
        private readonly ITaskRepository _taskRepository;
        public MyUser CurrentUserr = new MyUser();

        public TaskController(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }


        // GET: TaskController
        [HttpGet]
        public IActionResult Index()
        {
            CurrentUserr.UserId = Guid.NewGuid();
            var tasks = _taskRepository.Get_TaskList(CurrentUserr.UserId).ToList();
            return View(tasks);
        }



        // GET: Task/Details/5
        public IActionResult Details(Guid TaskId)
        {
            var task = _taskRepository.Get(TaskId);
            if (task != null)
            {
                return View(task);
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Task/Create
        public IActionResult Create()
        {
            return View(new MyTask());
        }

        // POST: Task/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(MyTask task)
        {
            if (ModelState.IsValid)
            {
                _taskRepository.Added(task, CurrentUserr.UserId);
                return RedirectToAction(nameof(Index));
            }
            return View(task);
        }

        // GET: Task/Edit/5
        public IActionResult Edit(Guid TaskId)
        {
            var task = _taskRepository.Get(TaskId);
            if (task != null)
            {
                return View(task);
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: Task/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid TaskId, MyTask editTask)
        {
            if (ModelState.IsValid)
            {
                _taskRepository.Update(TaskId, editTask);
                return RedirectToAction(nameof(Index));
            }
            return View(editTask);
        }

        // GET: Task/Delete/5
        public IActionResult Delete(Guid TaskId)
        {
            var Delatetask = _taskRepository.Get(TaskId);
            if (Delatetask != null)
            {
                return View(Delatetask);
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: Task/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(MyTask DelTask)
        {
            _taskRepository.Delete(DelTask.TaskId);
            return RedirectToAction(nameof(Index));
        }

    }
}
