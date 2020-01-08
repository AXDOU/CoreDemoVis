using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using SqlSugar;

namespace CfoDAL.DataEntity
{
    [SugarTable("CoreUser")]
    public class CoreUser
    {
        [SugarColumn(IsPrimaryKey =true,IsIdentity =true)]
        public int ID { get; set; }

        [DisplayName("姓名")]
        ///<summary>        ///姓名        ///</summary>
        public string FullName { get; set; }

        [DisplayName("密码")]
        ///<summary>        ///密码        ///</summary>
        public string Password { get; set; }

        [DisplayName("地址")]
        ///<summary>        ///地址        ///</summary>
        public string Address { get; set; }

        [DisplayName("性别")]
        ///<summary>        ///性别 0  女 1 男  -1 未知         ///</summary>
        public int? Sex { get; set; }

        [DisplayName("邮箱")]
        ///<summary>        ///邮箱        ///</summary>
        public string Email { get; set; }

        [MinLength(11)]
        [DisplayName("手机号")]
        ///<summary>        ///手机号        ///</summary>
        public string Phone { get; set; }

        //[DisplayName("key")]
        /////<summary>        /////key        /////</summary>
        //public string CoKey { get; set; }

    }
}
