using System.Reflection;

namespace LAb7FE.Controllers
{
    public class Product
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public int Count { get; set; }
        public string Description { get; set; }
        public DateTime EndDate { get; set; }
    }
    public class Helper
    {
        public List<string> GetListNameFieldAsync<TEntity>()
        {
            var result = new List<string>();
            Type fieldsType = typeof(TEntity);

            PropertyInfo[] props = fieldsType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            for (int i = 0; i < props.Length; i++)
            {
                result.Add(props[i].Name);
            }
            return result.OrderBy(x => x.Length).ToList();
        }
    }
}
