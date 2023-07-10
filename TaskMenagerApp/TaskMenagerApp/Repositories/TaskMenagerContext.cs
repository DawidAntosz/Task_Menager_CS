using Dapper;
using Npgsql;
using TaskMenagerApp.Models;

namespace TaskMenagerApp.Repositories
{
    public class TaskMenagerContext
    {
        private readonly string _connectionString;
        public TaskMenagerContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public MyTask Get_Task(int taskId)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var sql = "SELECT * FROM mytask WHERE id = @taskId";
                return connection.QueryFirstOrDefault<MyTask>(sql, new { taskId });
            }
        }

        public IQueryable<MyTask> Get_AllTask()
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var sql = @"SELECT * FROM mytask ORDER BY id";
                return connection.Query<MyTask>(sql).AsQueryable();
            }
        }

        public void AddTask(MyTask task)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var sqlCount = @"SELECT COUNT(*) FROM mytask";
                int count = connection.ExecuteScalar<int>(sqlCount);

                task.Id = count + 1;
                task.Topic ??= string.Empty;
                task.Description ??= string.Empty;

                var sql = @"INSERT INTO mytask (id, topic, description, data, status) 
                            VALUES (@Id, @Topic, @Description, @Data, @Status)";
                connection.Execute(sql, task);
            }
        }

        public void UpdateTask(int Id, MyTask task) 
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var sql = @"UPDATE mytask 
                            SET topic = @Topic, description = @Description, data = @Data, status = @Status 
                            WHERE id = @Id";
                connection.Execute(sql, task);
            }
        }

        public void DeleteTask(int taskId) 
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var sql = "DELETE FROM mytask WHERE id = @taskId";
                connection.Execute(sql, new { taskId });

                var updateSql = "UPDATE mytask SET id = id - 1 WHERE id > @taskId";
                connection.Execute(updateSql, new { taskId });
            }
        }

    }
}
