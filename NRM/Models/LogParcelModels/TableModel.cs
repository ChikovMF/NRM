namespace NRM.Models.LogParcelModels
{
    public class TableModel
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }
    }
}
