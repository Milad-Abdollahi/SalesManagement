﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Dapper;
using System.Data;
using System.Data.SqlClient;


namespace SalesManagementLibrary.DataAccess.Dapper;

public class DapperDataAccess : IDapperDataAccess
{
    private readonly IConfiguration _config;

    public DapperDataAccess(IConfiguration config)
    {
        _config = config;
    }

    public async Task<List<T>> LoadData<T, U>(string storedProcedure, U parameters, string connectionStringName)
    {
        string connectionString = _config.GetConnectionString(connectionStringName);
        using IDbConnection connection = new SqlConnection(connectionString);

        var rows = await connection.QueryAsync<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure);

        return rows.ToList();
        
    }

    public async Task<List<T?>> LoadData<T>(string storedProcedure, string connectionStringName)
    {
        string connectionString = _config.GetConnectionString(connectionStringName);
        using IDbConnection connection = new SqlConnection(connectionString);

        var rows = await connection.QueryAsync<T>(storedProcedure, commandType: CommandType.StoredProcedure);

        return rows.ToList();
    }

    public async Task<List<T>> LoadDataWithMultiMapping<T, T1, T2, U>
        (string storedProcedure, Func<T, T1, T2, T> map, U parameters, string connectionStringName, string splitOn)
    {
        string connectionString = _config.GetConnectionString(connectionStringName);

        using IDbConnection connection = new SqlConnection(connectionString);

        var rows = await connection.QueryAsync<T, T1, T2, T>(
                storedProcedure,
                map,
                parameters,
                commandType: CommandType.StoredProcedure,
                splitOn: splitOn
            );

        return rows.ToList();
    }

    public async Task SaveData<T>(string storedProcedure, T parameters, string connectinStringName)
    {
        string connectionString = _config.GetConnectionString(connectinStringName);
        
        using IDbConnection connection = new SqlConnection(connectionString);

        await connection.ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
    }
}
