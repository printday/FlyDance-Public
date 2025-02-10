using SqlSugar;

namespace Fly.System.Domain.Models.Entities.Systems
{
    [SugarTable("t_sys_user")]
    public class User
    {
        /// <summary>
        /// Id
        /// </summary>
        [SugarColumn(ColumnName = "uid", IsPrimaryKey = true)]
        public int Id { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        [SugarColumn(ColumnName = "user_id")]
        public string UserId { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        [SugarColumn(ColumnName = "user_name")]
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [SugarColumn(ColumnName = "user_password")]
        public string Password { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        [SugarColumn(ColumnName = "nick_name")]
        public string NickName { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        [SugarColumn(ColumnName = "phone_number")]
        public string Phone { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [SugarColumn(ColumnName = "email")]
        public string Email { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        [SugarColumn(ColumnName = "birth")]
        public DateTime? Birth { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        [SugarColumn(ColumnName = "sex")]
        public char? Sex { get; set; }

        /// <summary>
        /// 登录状态
        /// </summary>
        [SugarColumn(ColumnName = "login_status")]
        public char LoginStatus { get; set; }

        /// <summary>
        /// 用户状态
        /// </summary>
        [SugarColumn(ColumnName = "state")]
        public char State { get; set; }

        /// <summary>
        /// 连接Id(Websocket使用)
        /// </summary>
        [SugarColumn(ColumnName = "connection_id")]
        public string ConnectionId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [SugarColumn(ColumnName = "create_time")]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        [SugarColumn(ColumnName = "create_name")]
        public string CreateName { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        [SugarColumn(ColumnName = "update_time")]
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>
        [SugarColumn(ColumnName = "update_name")]
        public string UpdateName { get; set; }
    }
}
