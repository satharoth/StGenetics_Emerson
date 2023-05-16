using Dapper;
using STGenetics.DTO;
using STGenetics.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace STGenetics.Entity
{
    public class AnimalsPurchaseRepository : IAnimalPruchase
    {
        private readonly STGeneticsContext _context;
        public AnimalsPurchaseRepository(STGeneticsContext context)
        {
            _context = context;
        }

        public async Task<AnimalPruchase> CreateAnimalsPurchase(AnimalPruchase animalpruchase)
        {
            var totalpruchase = 0.0;
            animalpruchase.Freight = 1000;

            if (animalpruchase.Quantity>50)
            {
                totalpruchase = animalpruchase.TotalPrice - (animalpruchase.TotalPrice * 0.05);

            }else if (animalpruchase.Quantity > 200)
            {
                totalpruchase = animalpruchase.TotalPrice - (animalpruchase.TotalPrice * 0.03);
            }
            else if (animalpruchase.Quantity > 300)
            {
                animalpruchase.Freight = 0.0;
            }
            else 
            {
                totalpruchase = animalpruchase.TotalPrice;
            }
            var query = "INSERT INTO Purchase (AnimalId, UserName, Quantity, TotalPrice, Status,Freight) VALUES (@AnimalId, @UserName, @Quantity, @TotalPrice, @Status,@Freight)";
            var parameters = new DynamicParameters();
            parameters.Add("AnimalId", animalpruchase.AnimalId, DbType.Int32);
            parameters.Add("UserName", animalpruchase.UserName, DbType.String);
            parameters.Add("Quantity", animalpruchase.Quantity, DbType.Int32);
            parameters.Add("TotalPrice", totalpruchase, DbType.Double);
            parameters.Add("Freight", animalpruchase.Freight, DbType.Double);
            parameters.Add("Status", animalpruchase.Status, DbType.Int32);
            using (var connection = _context.CreateConnection())
            {
                var id = await connection.QuerySingleAsync<int>(query, parameters);
                var created = new AnimalPruchase
                {
                    PurchaseId = id,
                    UserName = animalpruchase.UserName,
                    AnimalId = animalpruchase.AnimalId,
                    Quantity = animalpruchase.Quantity,
                    TotalPrice = animalpruchase.TotalPrice,
                    Freight = animalpruchase.Freight,
                    Status = animalpruchase.Status,
                };
                return created;
            }
        }
        
    }
}
