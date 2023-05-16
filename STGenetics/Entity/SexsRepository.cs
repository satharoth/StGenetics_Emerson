using STGenetics.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace STGenetics.Entity
{
    public class SexsRepository
    {
        protected readonly DbConnection _conection;
        public string TableName { get; }
        public SexsRepository(DbConnection conection)
        {
            _conection = conection;
        }

        public async Task<List<Sexs>> GetSexs(int sexId)
        {
            using (var con = _conection)
            {
                con.Open();
                return (await con.QueryAsync<Sexs>($"select * from { TableName }")).ToList();
            }
        }

    }
}
