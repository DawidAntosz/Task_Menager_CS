using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskMenagerApp.Models;

namespace TaskMenagerApp.Controllers
{
    public class TaskController : Controller
    {
        public static List<MyTask> tasks = new List<MyTask>();

        // GET: TaskController
        public ActionResult Index()
        {
            return View(tasks);
        }

        // GET: Task/Details/5
        public ActionResult Details(int id)
        {
            return View(tasks.FirstOrDefault(x => x.Id == id));
        }

        // GET: Task/Create
        public ActionResult Create()
        {
            return View(new MyTask());
        }

        // POST: Task/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MyTask task)
        {
            task.Id = tasks.Count + 1;
            tasks.Add(task);
            return RedirectToAction(nameof(Index));
        }

        // GET: Task/Edit/5
        public ActionResult Edit(int id)
        {
            return View(tasks.FirstOrDefault(x => x.Id == id));
        }

        // POST: Task/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, MyTask editTask)
        {
            MyTask Newtask = tasks.FirstOrDefault(x => x.Id == id);
            Newtask.Topic = editTask.Topic;
            Newtask.Description = editTask.Description;
            Newtask.Data = editTask.Data;
            return RedirectToAction(nameof(Index));

        }

        // GET: Task/Delete/5
        public ActionResult Delete(int id)
        {
            return View(tasks.FirstOrDefault(x => x.Id == id));
        }

        // POST: Task/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, MyTask DelTask)
        {
            MyTask task = tasks.FirstOrDefault(x => x.Id == id);
            tasks.Remove(task);

            foreach (var rtask in tasks)
            {   
                if (task.Id <= rtask.Id)
                {
                    rtask.Id--;
                }
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
