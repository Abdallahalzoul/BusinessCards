using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public interface IRepositorySQL<T>
    {
        Task<List<T>> ListData(string SP, object Params);
        Task<T> FindExecCommand(string SP, object Params);
        Task<int> IntExecCommand(string SP, object Params);
        Task ExecCommand(string SP, object Params);
    }
}
