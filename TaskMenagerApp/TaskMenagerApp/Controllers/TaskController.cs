using Microsoft.AspNetCore.Mvc;
using Npgsql;
using TaskMenagerApp.Models;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Common;
using System.Data;

/*

var connectionString = "Host=myserver;Username=mylogin;Password=mypass;Database=mydatabase";
await using var dataSource = NpgsqlDataSource.Create(connectionString);

// Insert some data
await using (var cmd = dataSource.CreateCommand("INSERT INTO data (some_field) VALUES ($1)"))
{
    cmd.Parameters.AddWithValue("Hello world");
    await cmd.ExecuteNonQueryAsync();
}

// Retrieve all rows
await using (var cmd = dataSource.CreateCommand("SELECT some_field FROM data"))
await using (var reader = await cmd.ExecuteReaderAsync())
{
    while (await reader.ReadAsync())
    {
        Console.WriteLine(reader.GetString(0));
    }
}

===================== wzorzez reposytorium , dependencinjectiony  !!!!
// laczenie kontrolerow// relacyjne bazy danych  - 3 typy relacji 
// uml entity erd
// dwa klucze glowne w relacyjnych bazach - laczenia sie na tej podstawie robi 1* na query polaczyc dwie tablee - uzytkownik - zadania
-- uuid uniwersal unit identifire

// linq to sql - stara bilbiteka - object maper

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
            await using var dataSource = NpgsqlDataSource.Create(connectionString);

            List<MyTask> tasks = new List<MyTask>();
                            
            await using (var cmd = dataSource.CreateCommand("SELECT * FROM mytask ORDER BY id"))
            {
                var reader = await cmd.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {                
                    var DBtask = new MyTask
                    {
                        Id = (int)reader["id"],
                        Topic = (string)reader["topic"],
                        Description = (string)reader["description"],
                        Data = reader.GetDateTime("data"),
                        Status = (StatusTask)Enum.Parse(typeof(StatusTask), reader["status"].ToString())
                    };

                    tasks.Add(DBtask);
                }
            }
            
            return View(tasks);
        }



        // GET: Task/Details/5
        public async Task<ActionResult> Details(int id)
        {
            await using var dataSource = NpgsqlDataSource.Create(connectionString);
            using (var cmd = dataSource.CreateCommand("SELECT * FROM mytask WHERE id = @id"))
            {
                cmd.Parameters.AddWithValue("id", id);
                await using var reader = await cmd.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    var task = new MyTask
                    {
                        Id = (int)reader["id"],
                        Topic = reader.GetString("topic"),
                        Description = reader.GetString("description"),
                        Data = reader.GetDateTime("data"),
                        Status = (StatusTask)Enum.Parse(typeof(StatusTask), reader.GetString("status"))
                    };
                    return View(task);
                }
            }
            return RedirectToAction(nameof(Index));
        }




        // GET: Task/Create
        public ActionResult Create()
        {
            return View(new MyTask());
        }

        // POST: Task/Create
        [HttpPost] // mapowanie metody na odpowiednie żadanie HTTP-kominikacja przegladarka-serwer
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(MyTask task)
        {
            await using var dataSource = NpgsqlDataSource.Create(connectionString);

            using (var countCmd = dataSource.CreateCommand("SELECT COUNT(*) FROM mytask"))
            {
                object? taskCount = await countCmd.ExecuteScalarAsync();
                int count = Convert.ToInt32(taskCount);
                task.Id = count + 1;
            }

            if (string.IsNullOrEmpty(task.Topic))
            {
                task.Topic = string.Empty;
            }
            if (string.IsNullOrEmpty(task.Description))
            {
                task.Description = string.Empty;
            }

            using (var cmd = dataSource.CreateCommand("INSERT INTO mytask (id, topic, description, data, status) VALUES (@id, @topic, @description, @data, @status)"))
            {
                cmd.Parameters.AddWithValue("id", task.Id);
                cmd.Parameters.AddWithValue("topic", task.Topic);
                cmd.Parameters.AddWithValue("description", task.Description);
                cmd.Parameters.AddWithValue("data", task.Data);
                cmd.Parameters.AddWithValue("status", task.Status.ToString());
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction(nameof(Index));
        }




        // GET: Task/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            await using var dataSource = NpgsqlDataSource.Create(connectionString);
            var edittask = new MyTask();
            using (var cmd = dataSource.CreateCommand("SELECT * FROM mytask WHERE id = @id"))
            {
                cmd.Parameters.AddWithValue("id", id);
                await using var reader = await cmd.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    edittask.Id = (int)reader["id"];
                    edittask.Topic = reader.GetString("topic");
                    edittask.Description = reader.GetString("description");
                    edittask.Data = reader.GetDateTime("data");
                    edittask.Status = (StatusTask)Enum.Parse(typeof(StatusTask), reader.GetString("status"));
                }
            }
            return View(edittask);
        }

        // POST: Task/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, MyTask editTask)
        {
            await using var dataSource = NpgsqlDataSource.Create(connectionString);
            var Newtask = new MyTask();
            using (var cmd = dataSource.CreateCommand("SELECT * FROM mytask WHERE id = @id"))
            {
                cmd.Parameters.AddWithValue("id", id);
                await using var reader = await cmd.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    Newtask.Id = (int)reader["id"];
                    Newtask.Topic = reader.GetString("topic");
                    Newtask.Description = reader.GetString("description");
                    Newtask.Data = reader.GetDateTime("data");
                    Newtask.Status = (StatusTask)Enum.Parse(typeof(StatusTask), reader.GetString("status"));
                }
            }

            Newtask.Topic = editTask.Topic;
            Newtask.Description = editTask.Description;
            Newtask.Data = editTask.Data;

            if (string.IsNullOrEmpty(Newtask.Topic))
            {
                Newtask.Topic = string.Empty;
            }
            if (string.IsNullOrEmpty(Newtask.Description))
            {
                Newtask.Description = string.Empty;
            }

            using (var cmd = dataSource.CreateCommand("UPDATE mytask SET topic = @topic, description = @description, data = @data WHERE id = @id"))
            {
                cmd.Parameters.AddWithValue("topic", Newtask.Topic);
                cmd.Parameters.AddWithValue("description", Newtask.Description);
                cmd.Parameters.AddWithValue("data", Newtask.Data);
                cmd.Parameters.AddWithValue("id", Newtask.Id);
                await cmd.ExecuteNonQueryAsync();
            }
            return RedirectToAction(nameof(Index));
        }






        // GET: Task/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            await using var dataSource = NpgsqlDataSource.Create(connectionString);
            var deletetask = new MyTask();
            using (var cmd = dataSource.CreateCommand("SELECT * FROM mytask WHERE id = @id"))
            {
                cmd.Parameters.AddWithValue("id", id);
                await using var reader = await cmd.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    deletetask.Id = (int)reader["id"];
                    deletetask.Topic = reader.GetString("topic");
                    deletetask.Description = reader.GetString("description");
                    deletetask.Data = reader.GetDateTime("data");
                    deletetask.Status = (StatusTask)Enum.Parse(typeof(StatusTask), reader.GetString("status"));
                }
            }
            return View(deletetask);
        }

        // POST: Task/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, MyTask DelTask)
        {
            await using var dataSource = NpgsqlDataSource.Create(connectionString);

            using (var cmd = dataSource.CreateCommand("DELETE FROM mytask WHERE id = @id"))
            {
                cmd.Parameters.AddWithValue("id", id);
                cmd.ExecuteNonQuery();
            }
           
            using (var cmd = dataSource.CreateCommand("UPDATE mytask SET id = id - 1 WHERE id > @id"))
            {
                cmd.Parameters.AddWithValue("id", id);
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction(nameof(Index));
        }



    }
}
