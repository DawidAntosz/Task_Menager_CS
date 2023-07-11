using TaskMenagerApp.Models;

namespace TaskMenagerApp.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserMenagerContext _context;

        public UserRepository(UserMenagerContext context)
        {
            _context = context;
        }

        public MyUser Login(string username, string password)
        {
            return _context.Login_user(username, password);
        }

        public MyUser Get(Guid UserId)
        {
            return _context.Get_User(UserId);
        }

        public void Register(MyUser newUser)
        {
            _context.RegisterUser(newUser);
        }

        public void EditAccount(MyUser editUser)
        {
            _context.EditAccount(editUser);
        }

        public void Delete(Guid UserId)
        {
            _context.DeleteUser(UserId);
        }
    }
}
