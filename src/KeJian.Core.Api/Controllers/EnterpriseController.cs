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
    public class EnterpriseController : ControllerBase
    {
        private readonly IBaseApplication<Enterprise> _application;

        public EnterpriseController(IBaseApplication<Enterprise> application)
        {
            _application = application;
        }

        /// <summary>
        ///     合作企业列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<Enterprise>> GetAsync()
        {
            return await _application.GetAsync();
        }

        /// <summary>
        ///     合作企业详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<Enterprise> GetAsync(int id)
        {
            return await _application.GetAsync(id);
        }

        /// <summary>
        ///     创建或修改合作企业
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<Enterprise> CreateOrUpdateAsync(Enterprise input)
        {
            return await _application.CreateOrUpdateAsync(input);
        }

        /// <summary>
        ///     删除合作企业
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