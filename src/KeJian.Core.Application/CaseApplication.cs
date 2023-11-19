﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KeJian.Core.Application.Interface;
using KeJian.Core.Domain.Models;
using KeJian.Core.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace KeJian.Core.Application
{
    public class CaseApplication : IBaseApplication<Case>
    {
        private readonly DefaultDbContext _dbContext;

        public CaseApplication(IServiceProvider serviceProvider)
        {
            _dbContext = serviceProvider.GetService<DefaultDbContext>();
        }

        public async Task<List<Case>> GetAsync()
        {
            var cases = await _dbContext.Case
                .Where(c => !c.IsDeleted)
                .OrderByDescending(c => c.CreateTime)
                .ToListAsync();
            
            for (int i = 0; i < cases.Count; i++)
            {
                cases[i].Imgs = cases[i].Img.Split(',');
            }

            return cases;
        }

        public async Task<Case> GetAsync(int id)
        {
            var entity = await _dbContext.Case
                .Where(c => c.Id == id)
                .FirstOrDefaultAsync();
            
            entity.Imgs = entity.Img.Split(',');
            return entity;
        }

        public async Task<Case> CreateOrUpdateAsync(Case input)
        {
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
            var entity = new Case {Id = id, IsDeleted = true};
            _dbContext.Entry(entity).Property(e => e.IsDeleted).IsModified = true;
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<List<Case>> GetAsyncByType(string type)
        {
            return new List<Case>();
        }
    }
}