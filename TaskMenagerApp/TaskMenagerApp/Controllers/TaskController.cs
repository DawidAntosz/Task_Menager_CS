using Microsoft.AspNetCore.Mvc;
using Npgsql;
using TaskMenagerApp.Models;
using Dapper;

/*
 * 
===================== wzorzez reposytorium , dependencinjectiony  !!!!
laczenie kontrolerow// relacyjne bazy danych  - 3 typy relacji 
uml entity erd
dwa klucze glowne w relacyjnych bazach - laczenia sie na tej podstawie robi 1* na query polaczyc dwie tablee - uzytkownik - zadania

*
*/

namespace TaskMenagerApp.Controllers
{
    public class TaskController : Controller
    {
        private string connectionString = "Server=127.0.0.1;Port=5432;Database=TaskMenagerDB;" +
                                    "User Id=postgres;Password=password;";

        // GET: TaskController
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            await using var connection = new NpgsqlConnection(connectionString);
            List<MyTask> tasks = new List<MyTask>();
            string sql = @"SELECT * FROM mytask ORDER BY id";
            var dbTasks = await connection.QueryAsync<MyTask>(sql);
            tasks.AddRange(dbTasks);
            return View(tasks);
        }



        // GET: Task/Details/5
        public async Task<ActionResult> Details(int id)
        {
            using var connection = new NpgsqlConnection(connectionString);
            var sql = "SELECT * FROM mytask WHERE id = @id";
            var task = await connection.QueryFirstOrDefaultAsync<MyTask>(sql, new { id });

            if (task != null)
            {

                return View(task);
            }

            return RedirectToAction(nameof(Index));
        }




        // GET: Task/Create
        public ActionResult Create()
        {
            return View(new MyTask());
        }

        // POST: Task/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(MyTask task)
        {
            using var connection = new NpgsqlConnection(connectionString);
            var sql = @"SELECT COUNT(*) FROM mytask";
            int count = await connection.ExecuteScalarAsync<int>(sql);
            task.Id = count + 1;
            task.Topic ??= string.Empty;
            task.Description ??= string.Empty;
            sql = @"INSERT INTO mytask (id, topic, description, data, status) VALUES (@id, @topic, @description, @data, @status)";
            await connection.ExecuteAsync(sql, task);
            return RedirectToAction(nameof(Index));
        }




        // GET: Task/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            using var connection = new NpgsqlConnection(connectionString);
            var sql = "SELECT * FROM mytask WHERE id = @id";
            var task = await connection.QueryFirstOrDefaultAsync<MyTask>(sql, new { id });
            if (task != null)
            {
                return View(task);
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: Task/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, MyTask editTask)
        {
            using var connection = new NpgsqlConnection(connectionString);
            var sql = "SELECT * FROM mytask WHERE id = @id";
            var Existtask = await connection.QueryFirstOrDefaultAsync<MyTask>(sql, new { id });

            if (Existtask == null)
            {
                return RedirectToAction(nameof(Index));
            }

            Existtask.Topic = editTask.Topic;
            Existtask.Description = editTask.Description;
            Existtask.Data = editTask.Data;

            if (string.IsNullOrEmpty(Existtask.Topic))
            {
                Existtask.Topic = string.Empty;
            }
            if (string.IsNullOrEmpty(Existtask.Description))
            {
                Existtask.Description = string.Empty;
            }

            sql = @"UPDATE mytask SET topic = @topic, description = @description, data = @data WHERE id = @id";
            await connection.ExecuteAsync(sql, Existtask);

            return RedirectToAction(nameof(Index));
        }






        // GET: Task/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            using var connection = new NpgsqlConnection(connectionString);
            var sql = @"SELECT * FROM mytask WHERE id = @id";
            var Delatetask = await connection.QueryFirstOrDefaultAsync<MyTask>(sql, new { id });
            if (Delatetask != null)
            {
                return View(Delatetask);
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: Task/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, MyTask DelTask)
        {
            using var connection = new NpgsqlConnection(connectionString);
            
            var sql = @"DELETE FROM mytask WHERE id = @id";
            await connection.ExecuteAsync(sql, new { id });

            sql = @"UPDATE mytask SET id = id - 1 WHERE id > @id";
            await connection.ExecuteAsync(sql, new { id }); 
            return RedirectToAction(nameof(Index));
        }



    }
}
