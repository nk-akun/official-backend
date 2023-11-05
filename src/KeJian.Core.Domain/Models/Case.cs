using System;
using KeJian.Core.Library.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace KeJian.Core.Domain.Models
{
    public class Case : Entity
    {
        /// <summary>
        ///     案例图片接口值
        /// </summary>
        [NotMapped]
        public string[] Imgs { get; set; }

        /// <summary>
        ///     案例图片数据库
        /// </summary>
        public string Img { get; set; }

        /// <summary>
        ///     案例标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        ///     案例内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        ///     案例详情
        /// </summary>
        public string Del { get; set; }

        /// <summary>
        ///     修改时间
        /// </summary>
        public DateTime? CreateTime { get; set; }
    }
}