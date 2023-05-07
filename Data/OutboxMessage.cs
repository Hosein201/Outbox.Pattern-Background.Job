namespace Data
{
    public class OutboxMessage
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string Type { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? ProcessdDate { get; set; }
    }
}