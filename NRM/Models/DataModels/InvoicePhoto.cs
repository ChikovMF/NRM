namespace NRM.Models.DataModels
{
    public class InvoicePhoto
    {
        public int Id { get; set; }        
        public string? FileName { get; set; }
        public int? IdParcel { get; set; }
        public int? UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Patronymic { get; set; }     
                
    }
}
