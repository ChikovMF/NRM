using NRM.Models.DataModels;

namespace NRM.Models.AbstractModels
{
    public class AbstractLog
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }

        public int TypeId { get; set; }
        public LogType Type { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}