using Dapper;
using Npgsql;
using System.Threading.Tasks;
using TaskMenagerApp.Models;
using static System.Net.Mime.MediaTypeNames;

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
                var sql = "SELECT * FROM myusers WHERE user_name = @username AND user_password = @password";
                return connection.QueryFirstOrDefault<MyUser>(sql, new { username, password });
            }
        }

        public MyUser Get_User(Guid UserId)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var sql = "SELECT * FROM myusers WHERE user_id = @UserId";
                return connection.QueryFirstOrDefault<MyUser>(sql, new { UserId });
            }
        }

        public void RegisterUser(MyUser newUser)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var sql = @"INSERT INTO users (user_id, user_name, user_surname, email, user_password) 
                            VALUES (@UserId, @UserName, @UserSurname, @Email, @UserPassword)";
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
                var sql = "DELETE FROM mytask WHERE user_id = @UserId";
                connection.Execute(sql, new { UserId });
            }
        }

    }
}
