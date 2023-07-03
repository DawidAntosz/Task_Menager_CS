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
        public int Id { get; set; }
        public string? Topic { get; set; }
        public string? Description { get; set; }
        public DateTime Data { get; set; }
        public StatusTask Status { get; set; } = StatusTask.Not_start;
    }
}
