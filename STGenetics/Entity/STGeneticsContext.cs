using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using STGenetics.Models;

namespace STGenetics.Entity
{
    public class STGeneticsContext 
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        public STGeneticsContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("STGeneticsContext");
        }
        public IDbConnection CreateConnection()
            => new SqlConnection(_connectionString);
    }
}
