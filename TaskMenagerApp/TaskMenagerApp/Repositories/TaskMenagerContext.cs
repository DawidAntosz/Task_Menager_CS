﻿using Dapper;
using Npgsql;
using System.Threading.Tasks;
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

        public MyTask Get_Task(Guid TaskId)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var sql = "SELECT * FROM tasks WHERE task_id = @TaskId ";
                return connection.QueryFirstOrDefault<MyTask>(sql, new { TaskId });
            }
        }

        public IQueryable<MyTask> Get_AllTask(Guid UserId)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var sql = @"SELECT * FROM tasks WHERE fk_user = @UserId ORDER BY task_number";
                return connection.Query<MyTask>(sql, new { UserId }).AsQueryable();
            }
        }

        public void AddTask(MyTask task, Guid UserId)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var sqlCount = @"SELECT COUNT(*) FROM tasks WHERE fk_user = @UserId";
                int count = connection.ExecuteScalar<int>(sqlCount, new { UserId });

                task.task_Number = count + 1;
                task.Topic ??= string.Empty;
                task.Description ??= string.Empty;
                task.FkUser = UserId;

                var sql = @"INSERT INTO tasks ( task_Number, topic, description, start_time, work_time, status, fk_user ) 
                                    VALUES ( @task_Number, @Topic, @Description, @Data, @WorkTime, @Status, @FkUser )";
                connection.Execute(sql, task);
            }
        }

        public void UpdateTask(MyTask task, Guid TaskId) 
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var sql = @"UPDATE tasks 
                            SET topic = @Topic, description = @Description, data = @Data, status = @Status 
                            WHERE task_id = @TaskId";
                connection.Execute(sql, task);
            }
        }

        public void DeleteTask(Guid TaskId) 
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var sql = "DELETE FROM tasks WHERE task_id = @TaskId";
                connection.Execute(sql, new { TaskId });

                var updateSql = "UPDATE tasks SET task_Number = task_Number - 1 WHERE task_id > @TaskId";
                connection.Execute(updateSql, new { TaskId });
            }
        }

    }
}
