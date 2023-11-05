using System.Diagnostics;
using System.Collections.Generic;
using System.Threading.Tasks;
using KeJian.Core.Application.Interface;
using KeJian.Core.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using KeJian.Core.Library.Filter;

namespace KeJian.Core.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsApplication _application;

        public NewsController(INewsApplication application)
        {
            _application = application;
        }

        /// <summary>
        ///     新闻动态列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<News>> GetAsync()
        {
            return await _application.GetAsync();
        }

        /// <summary>
        ///     新闻动态列表
        /// </summary>
        /// <param name="type">类型：1：新闻资讯 2：行业动态</param>
        /// <param name="count">条数</param>
        /// <returns></returns>
        [HttpGet("Search")]
        public async Task<List<News>> GetAsyncWithType(int type, int count)
        {
            
            // Trace.WriteLine(type);
            // Trace.WriteLine(count);
            return await _application.GetAsync(type, count);
        }

        /// <summary>
        ///     新闻动态详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetNewsById/{id}")]
        public async Task<News> GetAsync(int id)
        {
            return await _application.GetAsync(id);
        }

        /// <summary>
        ///     创建或修改新闻动态
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<News> CreateOrUpdateAsync(News input)
        {
            return await _application.CreateOrUpdateAsync(input);
        }

        /// <summary>
        ///     删除新闻动态
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("{id}")]
        [Authorize]
        public async Task<bool> DeleteAsync(int id)
        {
            return await _application.DeleteAsync(id);
        }
    }
}