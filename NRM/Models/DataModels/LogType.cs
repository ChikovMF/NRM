using NRM.Models.AbstractModels;

namespace NRM.Models.DataModels
{
    public class LogType
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public string Name { get; set; }

        public int RelationId { get; set; }
        public RelationLogType? Relation { get; set; }
        public List<AbstractLog>? Logs { get; set; }
    }
}
