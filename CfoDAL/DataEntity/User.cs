using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SqlSugar;

namespace CfoDAL.DataEntity
{
    [SugarTable("tb_user")]
    public class tb_user
    {

        /// <summary>
        /// id
        /// </summary>
        /// 
        [SugarColumn(IsIdentity = true, IsPrimaryKey = true, ColumnDescription = "id")]
        public int Id { get; set; }

        [SugarColumn(ColumnDescription = "登录名")]
       
        /// <summary>
        /// 登录名
        /// </summary>
        public string LoginName { get; set; }

        [SugarColumn(ColumnDescription = "密码")]
        public string Password { get; set; }
        public string RealName { get; set; }
        public string EmailAddress { get; set; }
        public string Avatar { get; set; }
        public int Status { get; set; }
        public string Telephone { get; set; }
        public string Qq { get; set; }
        public string WebsiteUrl { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedIp { get; set; }
        public int LoginCount { get; set; }
        public DateTime? LatestLoginDate { get; set; }
        public string LatestLoginIp { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int Type { get; set; }
    }
}
