﻿using System;
using System.Collections.Generic;
using System.Text;

namespace KeJian.Core.Domain.Dtos
{
    public class LoginInputDto
    {
        /// <summary>
        /// 登陆名
        /// </summary>
        public string LoginName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
    }
}
