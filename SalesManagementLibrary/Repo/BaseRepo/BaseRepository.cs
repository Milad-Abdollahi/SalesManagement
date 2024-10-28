using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalesManagementLibrary.DataAccess.Dapper;
using SalesManagementLibrary.Exceptions;

namespace SalesManagementLibrary.Repo.BaseRepo;

public class BaseRepository
{
    //private readonly IDapperDataAccess _dapperDataAccess;

    //public BaseRepository(IDapperDataAccess dapperDataAccess)
    //{
    //    _dapperDataAccess = dapperDataAccess;
    //}

    protected async Task ExecWithErrHandling(Func<Task> action)
    {
        try
        {
            await action();
        }
        catch (SqlException ex) when (ex.Number == 2627)
        {
            throw new DuplicateRecordException("A record with this identifier already exists.");
        }
        catch (Exception ex)
        {
            throw new GenericException("An unexpected error occurred.", ex);
        }
    }

    protected async Task<T> ExecWithErrHandling<T>(Func<Task<T>> action)
    {
        try
        {
            return await action();
        }
        catch (SqlException ex) when (ex.Number == 2627)
        {
            throw new DuplicateRecordException("A record with this identifier already exists.");
        }
        catch (Exception ex)
        {
            throw new GenericException("An unexpected error occurred.", ex);
        }
    }
}
