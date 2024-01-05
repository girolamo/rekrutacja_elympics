namespace Aspnet_for_elympics.Entities
{
    public class Number
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public DateTime CreationTime { get; set; } = DateTime.UtcNow;
    }
}
