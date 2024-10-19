using Dapper;
using Microsoft.Data.SqlClient;
using Repository.Constants;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class RepositorySQL<T> : IRepositorySQL<T>
    {
        public async Task ExecCommand(string SP, object Params)
        {
            using (IDbConnection con = new SqlConnection(CommonConstants.ConnectionString))
            {
                if (con.State == ConnectionState.Closed) con.Open();
                await con.QueryAsync<T>(SP, Params, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<T> FindExecCommand(string SP, object Params)
        {
            using (IDbConnection con = new SqlConnection(CommonConstants.ConnectionString))
            {
                if (con.State == ConnectionState.Closed) con.Open();
                var result = await con.QuerySingleOrDefaultAsync<T>(SP, Params, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<int> IntExecCommand(string SP, object Params)
        {
            using (IDbConnection con = new SqlConnection(CommonConstants.ConnectionString))
            {
                if (con.State == ConnectionState.Closed) con.Open();
                var result = await con.ExecuteScalarAsync<int>(SP, Params, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<List<T>> ListData(string SP, object Params)
        {
            using (IDbConnection con = new SqlConnection(CommonConstants.ConnectionString))
            {
                if (con.State == ConnectionState.Closed) con.Open();
                var result = await con.QueryAsync<T>(SP, Params, commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
        }
    }
}
