using Common.Base.Model;
using Common.Factorys;
using Common.IOC;
using Common.Models.Logins;
using Fly.System.Domain.IReportsitorys.Systems;
using Fly.System.Domain.Models.Entities.Systems;
using Fly.System.Domain.Models.Interfaces;
using Fly.System.Domain.Models.Requests.Users;
using SqlSugar;

namespace Fly.System.Infrastructure.Reportsitorys.Systems
{
    public class UserReport : IUserReport, ISystemReport
    {
        [Autowired]
        public SqlSugarFactory SqlSugarFactory { get; set; }

        public User GetByEmail(string email)
        {
            using (var db = SqlSugarFactory.GetMySqlConnection(DBNameModel.System))
            {
                return db.Queryable<User>().First(u => u.Email == email);
            }
        }

        public User GetByEmailPwd(LoginInfoReq req)
        {
            using (var db = SqlSugarFactory.GetMySqlConnection(DBNameModel.System))
            {
                return db.Queryable<User>().First(u => u.Email == req.Email && u.Password == req.Password);
            }
        }

        public User GetByUName(string userName)
        {
            using (var db = SqlSugarFactory.GetMySqlConnection(DBNameModel.System))
            {
                return db.Queryable<User>().First(u => u.UserName == userName);
            }
        }

        public User GetByUPwd(LoginInfoReq req)
        {
            using (var db = SqlSugarFactory.GetMySqlConnection(DBNameModel.System))
            {
                return db.Queryable<User>().First(u => u.UserName == req.UserName && u.Password == req.Password);
            }
        }

        public User GetUserById(int id)
        {
            using (var db = SqlSugarFactory.GetMySqlConnection(DBNameModel.System))
            {
                return db.Queryable<User>().Where(u => u.Id == id).First();
            }
        }

        public bool Register(RegisterReq req)
        {
            using (var db = SqlSugarFactory.GetMySqlConnection(DBNameModel.System))
            {
                try
                {
                    db.Ado.BeginTran();
                    User user = new User()
                    {
                        UserId = SnowFlakeSingle.Instance.NextId().ToString(),
                        UserName = req.UserName,
                        Password = req.Password,
                        Email = req.Email,
                        LoginStatus = '0',
                        State = '1',
                        CreateTime = DateTime.Now,
                        CreateName = "注册系统",
                    };
                    Role role = db.Queryable<Role>().First(r => r.Name == "普通用户");

                    bool addUserIsOk = db.Insertable(user).ExecuteCommand() > 0;
                    user = db.Queryable<User>().First(u => u.UserId == user.UserId);

                    if (!addUserIsOk)
                    {
                        Console.WriteLine("注册时，用户添加失败");
                        return false;
                    }
                    UserRole userRole = new UserRole()
                    {
                        Uid = user.Id,
                        Rid =  role == null ? 0 : role.Id,
                        CreateTime = DateTime.Now,
                        CreateName = "注册系统",
                    };

                    db.Insertable(userRole).ExecuteCommand();
                    db.Ado.CommitTran();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    db.Ado.RollbackTran();
                    return false;
                }
            }
        }
    }
}
