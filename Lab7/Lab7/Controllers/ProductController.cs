using AutoMapper;
using Lab7.AppDbContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IMapper _iMapper;
        private readonly DbContextApp _dbContextApp;
        public ProductController(IMapper mapper, DbContextApp dbContextApp)
        {
            _iMapper = mapper;
            _dbContextApp = dbContextApp;
        }
        [HttpGet]
        public async Task<List<Product>> GetAsync()
        {
            return await _dbContextApp.Product.ToListAsync();
        }
        [HttpPost]
        public async Task<bool> CreateAsync(ProductDto input)
        {
            var product = _iMapper.Map<Product>(input);
            await _dbContextApp.Product.AddAsync(product);
            await _dbContextApp.SaveChangesAsync();
            return true;
        }
        [HttpPut]
        public async Task<bool> UpdateAsync(ModelProductUpdate input)
        {
            var product = await _dbContextApp.Product.FindAsync(input.Id);
            if(product == null)
            {
                return false;
            }
            var result = _iMapper.Map(input, product);
            _dbContextApp.Product.Update(result);
            await _dbContextApp.SaveChangesAsync();
            return true;
        }
    }
    public class ProductDto
    {
        public string Name { get; set; }
        public int Count { get; set; }
        public string Description { get; set; }
        public DateTime EndDate { get; set; }
    }
    public class ModelProductUpdate
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public string Description { get; set; }
        public DateTime EndDate { get; set; }
    }
}
