using Microsoft.AspNetCore.Mvc;
using Npgsql;
using TaskMenagerApp.Models;
using Dapper;
using TaskMenagerApp.Repositories;

/*
 * 
 * 
laczenie kontrolerow// relacyjne bazy danych  - 3 typy relacji 
uml entity erd
dwa klucze glowne w relacyjnych bazach - laczenia sie na tej podstawie robi 1* na query polaczyc dwie tablee - uzytkownik - zadania

*
*/

namespace TaskMenagerApp.Controllers
{
    public class TaskController : Controller
    {
        private readonly ITaskRepository _taskRepository;

        public TaskController(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        // GET: TaskController
        [HttpGet]
        public IActionResult Index()
        {
            var tasks = _taskRepository.Get_TaskList().ToList();
            return View(tasks);
        }



        // GET: Task/Details/5
        public IActionResult Details(int id)
        {
            var task = _taskRepository.Get(id);
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
                _taskRepository.Added(task);
                return RedirectToAction(nameof(Index));
            }
            return View(task);
        }




        // GET: Task/Edit/5
        public IActionResult Edit(int id)
        {
            var task = _taskRepository.Get(id);
            if (task != null)
            {
                return View(task);
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: Task/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, MyTask editTask)
        {
            if (ModelState.IsValid)
            {
                _taskRepository.Update(id, editTask);
                return RedirectToAction(nameof(Index));
            }
            return View(editTask);
        }






        // GET: Task/Delete/5
        public IActionResult Delete(int id)
        {
            var Delatetask = _taskRepository.Get(id);
            if (Delatetask != null)
            {
                return View(Delatetask);
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: Task/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, MyTask DelTask)
        {
            _taskRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }



    }
}
