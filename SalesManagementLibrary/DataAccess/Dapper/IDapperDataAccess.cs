using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagementLibrary.DataAccess.Dapper;

public interface IDapperDataAccess
{
    Task<List<T>> LoadData<T, U>(string storedProcedure, U parameters, string connectionStringName);

    Task<List<T?>> LoadData<T>(string storedProcedure, string connectionStringName);

    Task<List<T>> LoadDataWithMultiMapping<T, T1, T2, U>
        (string storedProcedure, Func<T, T1, T2, T> map, U parameters, string connectionStringName, string splitOn);


    Task SaveData<T>(string storedProcedure, T parameters, string connectinStringName);
}
