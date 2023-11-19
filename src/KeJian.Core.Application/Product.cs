using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KeJian.Core.Application.Interface;
using KeJian.Core.Domain.Models;
using KeJian.Core.EntityFramework;
using KeJian.Core.Library.Exception;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace KeJian.Core.Application
{
    public class ProductApplication : IBaseApplication<Product>
    {
        private readonly DefaultDbContext _dbContext;

        private Dictionary<string, string> convertMap = new Dictionary<string, string>()
        {
            {"product1", "桥梁模板"},
            {"product2", "房建模板"},
            {"product3", "全铝模板"},
            {"product4", "建筑机械"},
            {"product5", "模板配件"},
            {"product6", "钢结构"}
        };

        public ProductApplication(IServiceProvider serviceProvider)
        {
            _dbContext = serviceProvider.GetService<DefaultDbContext>();
        }

        public async Task<List<Product>> GetAsync()
        {
            var products = await _dbContext.Product
                .Where(c => !c.IsDeleted)
                .OrderByDescending(c => c.CreateTime)
                .ToListAsync();
            
            for (int i = 0; i < products.Count; i++)
            {
                products[i].Imgs = products[i].Img.Split(',');
            }

            return products;
        }

        public async Task<List<Product>> GetAsyncByType(string type)
        {
            if (convertMap.ContainsKey(type)) 
            {
                type = convertMap[type];
            }
            else
            {
                throw new StringResponseException("该产品不存在");
            }
            var products = await _dbContext.Product
                .Where(c => !c.IsDeleted)
                .Where(c => c.Type == type)
                .OrderByDescending(c => c.CreateTime)
                .ToListAsync();
            
            for (int i = 0; i < products.Count; i++)
            {
                products[i].Imgs = products[i].Img.Split(',');
            }

            return products;
        }

        public async Task<Product> GetAsync(int id)
        {
            var entity = await _dbContext.Product
                .Where(c => c.Id == id)
                .FirstOrDefaultAsync();
            
            entity.Imgs = entity.Img.Split(',');
            return entity;
        }

        public async Task<Product> CreateOrUpdateAsync(Product input)
        {
            if (convertMap.ContainsKey(input.Type)) 
            {
                input.Type = convertMap[input.Type];
            }
            else
            {
                throw new StringResponseException("该产品不存在");
            }
            string img = string.Join(",", input.Imgs);
            input.Img = img;
            if (input.Id == 0)
            {
                input.CreateTime = DateTime.Now;
                var entity = await _dbContext.AddAsync(input);
                await _dbContext.SaveChangesAsync();
                return entity.Entity;
            }

            _dbContext.Update(input);
            await _dbContext.SaveChangesAsync();
            return input;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = new Product {Id = id, IsDeleted = true};
            _dbContext.Entry(entity).Property(e => e.IsDeleted).IsModified = true;
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}