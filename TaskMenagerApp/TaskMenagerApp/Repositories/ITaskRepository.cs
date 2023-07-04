using TaskMenagerApp.Models;

namespace TaskMenagerApp.Repositories
{
    public interface ITaskRepository
    {
        MyTask Get(int taskID); // pobieram konkretne zadanie
        IQueryable<MyTask> GetAllActive(); // sprawdzenie statusu aktywnosci zadania 
        void Added(MyTask task); // dodawanie do bazy
        void Update(int taskID, MyTask task); // educja taska 
        void Delete(int taskID); // usuwanie 
    }
}
