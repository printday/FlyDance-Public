using Common.Base.Model;
using Common.Models.Configs;
using SqlSugar;

namespace Common.Factorys
{
    public class SqlSugarFactory
    {
        public readonly SqlServerConfig SqlServerConfig;

        public readonly MySqlConfig MySqlConfig;

        public SqlSugarFactory(SqlServerConfig sqlServerConfig, MySqlConfig mySqlConfig)
        {
            this.SqlServerConfig = sqlServerConfig;
            this.MySqlConfig = mySqlConfig;
        }

        /// <summary>
        /// 根据数据库名称获取连接
        /// </summary>
        /// <param name="dbName"></param>
        /// <returns></returns>
        public ISqlSugarClient GetSqlServerConnection(string dbName)
        {
            if (!string.IsNullOrWhiteSpace(dbName))
            {
                switch (dbName)
                {
                    case DBNameModel.System:
                        return GetSqlSugarClient(SqlServerConfig.SystemConnectionString);
                }
            }
            return null;
        }

        /// <summary>
        /// 根据数据库名称获取连接
        /// </summary>
        /// <param name="dbName"></param>
        /// <returns></returns>
        public ISqlSugarClient GetMySqlConnection(string dbName)
        {
            if (!string.IsNullOrWhiteSpace(dbName))
            {
                switch (dbName)
                {
                    case DBNameModel.System:
                        return GetSqlSugarClient(MySqlConfig.SystemConnectionString, DbType.MySql);
                }
            }
            return null;
        }

        /// <summary>
        /// 获取连接(默认SQLServer)
        /// </summary>
        /// <param name="sqlString"></param>
        /// <returns></returns>
        private SqlSugarClient GetSqlSugarClient(string sqlString, DbType dbType = DbType.SqlServer)
        {
            return new SqlSugarClient(
                    new ConnectionConfig()
                    {
                        ConnectionString = sqlString,
                        DbType = dbType,
                        IsAutoCloseConnection = true,
                        InitKeyType = InitKeyType.Attribute,    // 从特性读取
                    }
                );
        }
    }
}
