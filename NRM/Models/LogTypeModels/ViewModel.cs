namespace NRM.Models.LogTypeModels
{
    public class ViewModel
    {
        public string Name { get; set; }
        public List<ItemLog>? Logs { get; set; }

        public class ItemLog
        {
            public int Id { get; set; }
            public string Message { get; set; }
            public DateOnly Date { get; set; }
            public TimeOnly Time { get; set; }
        }
    }
}
