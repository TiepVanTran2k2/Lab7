namespace Lab7.Controllers
{
    public class Account
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string UserName { get;set; }
        public string Password { get; set; }
    }
}