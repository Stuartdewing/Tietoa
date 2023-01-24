﻿using Services.Database;
using Services.Database.Interface;
using System.Data;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using Tietoa.Domain;

namespace Services.Database.GetData
{
    public class GetData : IGetData
    {
        private readonly IGetData _GetData;
        private readonly IConfiguration _configuration;

        public GetData(IGetData getData, IConfiguration configuration)
        {
            _GetData = getData;
            _configuration = configuration;
        }

        public DataTable GetDataFromTable(string TableName)
        {
            var config = _configuration.GetConnectionString("TietoaConnectionString");

            SqlConnection connection = new SqlConnection(config);
            connection.Open();
            string query = SqlConstants.SelectEverythingFrom + "[" + TableName + "]";
            SqlCommand command = new SqlCommand(query, connection);

            DataTable datatable = new DataTable();
            datatable.Load(command.ExecuteReader());
            connection.Close();

            return datatable;
        }
    }
}