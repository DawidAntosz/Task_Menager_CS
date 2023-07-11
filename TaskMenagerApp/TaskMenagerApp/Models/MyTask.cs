namespace TaskMenagerApp.Models
{
    public enum StatusTask
    {
        Not_start,
        Start,
        In_progres,
        Break,
        Completed
    }
    public class MyTask
    {
        public Guid TaskId { get; set; }
        public int task_Number { get; set; }
        public string? Topic { get; set; }
        public string? Description { get; set; }
        public DateTime Data { get; set; }
        public TimeSpan WorkTime { get; set; } = TimeSpan.Zero;
        public StatusTask Status { get; set; } = StatusTask.Not_start;
        public Guid FkUser { get; set; }

    }
}
