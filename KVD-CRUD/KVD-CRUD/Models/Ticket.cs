namespace KVD_CRUD.Models
{
    public class Ticket
    {
        public int id { get; set; }
        public string? name { get; set; }
        public string? country { get; set; }
        public string phoneNumber { get; set; }
        public bool flagDelete { get; set; }
        public DateTime createdAt { get; set; } = DateTime.UtcNow;
        public DateTime? updatedAt { get; set; }
    }
}
