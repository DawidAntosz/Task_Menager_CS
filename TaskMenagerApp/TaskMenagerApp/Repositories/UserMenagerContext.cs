using Dapper;
using Npgsql;
using TaskMenagerApp.Models;

namespace TaskMenagerApp.Repositories
{
    public class UserMenagerContext
    {
        private readonly string _connectionString;
        public UserMenagerContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public MyUser Login_user(string username, string password)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var sql = @"SELECT * FROM users WHERE user_name = @username AND user_password = @password";
                var temp = connection.QueryFirstOrDefault<MyUser>(sql, new { username, password });
                return temp;
            }
        }




        public MyUser Get_User(Guid UserId)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var sql = "SELECT * FROM users WHERE user_id = @UserId";
                return connection.QueryFirstOrDefault<MyUser>(sql, new { UserId });
            }
        }

        public void RegisterUser(MyUser newUser)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var sql = @"INSERT INTO users (user_name, user_surname, email, user_password) 
                            VALUES (@UserName, @UserSurname, @Email, @UserPassword)";
                connection.Execute(sql, newUser);
            }
        }

        public void EditAccount(MyUser editUser)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var sql = @"UPDATE users 
                            SET user_name = @UserName, user_surname = @UserSurname, email = @Email, user_password = @UserPassword 
                            WHERE user_id = @UserId";
                connection.Execute(sql, editUser);
            }
        }

        public void DeleteUser(Guid UserId)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var sql = "DELETE FROM users WHERE user_id = @UserId";
                connection.Execute(sql, new { UserId });
            }
        }

    }
}
