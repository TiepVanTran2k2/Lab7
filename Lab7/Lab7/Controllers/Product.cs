namespace Lab7.Controllers
{
    public class Product
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public int Count { get; set; }
        public string Description { get; set; }
        public DateTime EndDate { get; set; }
    }
}
