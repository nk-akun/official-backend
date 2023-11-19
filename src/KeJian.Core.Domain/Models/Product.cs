using System;
using KeJian.Core.Library.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace KeJian.Core.Domain.Models
{
    public class Product : Entity
    {
        /// <summary>
        ///     产品图片接口值
        /// </summary>
        [NotMapped]
        public string[] Imgs { get; set; }

        /// <summary>
        ///     产品图片数据库
        /// </summary>
        public string Img { get; set; }

        /// <summary>
        ///     产品标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        ///     产品内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        ///     产品类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        ///     产品详情
        /// </summary>
        public string Del { get; set; }

        /// <summary>
        ///     修改时间
        /// </summary>
        public DateTime? CreateTime { get; set; }
    }
}