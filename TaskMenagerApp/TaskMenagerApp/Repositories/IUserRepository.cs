using TaskMenagerApp.Models;

namespace TaskMenagerApp.Repositories
{
    public interface IUserRepository
    {
        MyUser Get(Guid UserId);
        void Register(MyUser newUser);
        void EditAccount(MyUser editUser);
        void Delete(Guid UserId);
    }
}
