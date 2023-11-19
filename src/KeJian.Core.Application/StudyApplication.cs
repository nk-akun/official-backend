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
    public class StudyApplication : IBaseApplication<Study>
    {
        private readonly DefaultDbContext _dbContext;

        public StudyApplication(IServiceProvider serviceProvider)
        {
            _dbContext = serviceProvider.GetService<DefaultDbContext>();
        }

        public async Task<List<Study>> GetAsync()
        {
            return await _dbContext.Study
                .Where(c => !c.IsDeleted)
                .OrderByDescending(c => c.CreateTime)
                .ToListAsync();
        }

        public async Task<Study> GetAsync(int id)
        {
            var entity = await _dbContext.Study
                .Where(c => c.Id == id)
                .FirstOrDefaultAsync();

            return entity;
        }

        public async Task<Study> CreateOrUpdateAsync(Study input)
        {
            input.CreateTime = DateTime.Now;

            if (input.Id == 0)
            {
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
            var entity = new Study {Id = id, IsDeleted = true};
            _dbContext.Entry(entity).Property(e => e.IsDeleted).IsModified = true;
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<List<Study>> GetAsyncByType(string type)
        {
            return new List<Study>();
        }
    }
}