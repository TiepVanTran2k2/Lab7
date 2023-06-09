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
        [Authorize]
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
    }
    public class ProductDto
    {
        public string Name { get; set; }
        public int Count { get; set; }
        public string Description { get; set; }
        public DateTime EndDate { get; set; }
    }
}
