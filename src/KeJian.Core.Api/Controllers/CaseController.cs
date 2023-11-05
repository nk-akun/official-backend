﻿using System.Collections.Generic;
using System.Threading.Tasks;
using KeJian.Core.Application.Interface;
using KeJian.Core.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KeJian.Core.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CasesController : ControllerBase
    {
        private readonly IBaseApplication<Case> _application;

        public CasesController(IBaseApplication<Case> application)
        {
            _application = application;
        }

        /// <summary>
        ///     案例列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<Case>> GetAsyncAll()
        {
            return await _application.GetAsync();
        }

        /// <summary>
        ///     案例列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("/GetCasesAll")]
        public async Task<List<Case>> GetAsync()
        {
            return await _application.GetAsync();
        }

        /// <summary>
        ///     案例详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetCasesById/{id}")]
        public async Task<Case> GetAsync(int id)
        {
            return await _application.GetAsync(id);
        }

        /// <summary>
        ///     创建或修改案例
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<Case> CreateOrUpdateAsync(Case input)
        {
            return await _application.CreateOrUpdateAsync(input);
        }

        /// <summary>
        ///     删除案例
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