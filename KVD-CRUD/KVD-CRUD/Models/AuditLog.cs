namespace KVD_CRUD.Models
{
    public class AuditLog
    {
        public int id { get; set; }
        public string description { get; set; }

        public string username { get; set; }
        public DateTime createdAt { get; set; } 
    }
}
