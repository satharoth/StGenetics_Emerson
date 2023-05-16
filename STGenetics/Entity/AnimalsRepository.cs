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
    public class AnimalsRepository :  IAnimals
    {
        private readonly STGeneticsContext _context;
        public AnimalsRepository(STGeneticsContext context)
        {
            _context = context;
        }


        public async Task<List<Animals>> GetAnimals()
        {
            var query = "SELECT * FROM Animals";
            using (var connection = _context.CreateConnection())
            {
                var animals = await connection.QueryAsync<Animals>(query);
                return animals.ToList();
            }
        }

        public async Task<Animals> GetAnimal(int AnimalId)
        {
            var query = $"SELECT * FROM Animals Where AnimalId = {AnimalId}";

            using (var connection = _context.CreateConnection())
            {
                var animals = await connection.QueryFirstAsync<Animals>(query);
                return animals;
            }
        }

        public async Task<Animals> CreateAnimals(Animals animals)
        {
            var query = "INSERT INTO Animals (Name, Breed, BirthDate, Sex, Price, Status) VALUES (@Name, @Breed, @BirthDate, @Sex, @Price, @Status)";
            var parameters = new DynamicParameters();
            parameters.Add("Name", animals.Name, DbType.String);
            parameters.Add("Breed", animals.Breed, DbType.String);
            parameters.Add("BirthDate", animals.BirthDate, DbType.DateTime);
            parameters.Add("Sex", animals.Sex, DbType.Int32);
            parameters.Add("Price", animals.Price, DbType.Double);
            parameters.Add("Status", animals.Status, DbType.Int32);
            using (var connection = _context.CreateConnection())
            {
                var id = await connection.QuerySingleAsync<int>(query, parameters);
                var created = new Animals
                {
                    AnimalId = id,
                    Name = animals.Name,
                    Breed = animals.Breed,
                    BirthDate = animals.BirthDate,
                    Sex = animals.Sex,
                    Price = animals.Price,
                    Status = animals.Status,
                };
                return created;
            }
        }

        public async Task<int> UpdateAnimals(int AnimalId, Animals animals)
        {
            var query = $"UPDATE Animals SET Name = @Name, Breed = @Breed, BirthDate = @BirthDate, Sex = @Sex, Price = @Price, Status = @Status  WHERE AnimalId = {AnimalId}";
            var parameters = new DynamicParameters();
            parameters.Add("Name", animals.Name, DbType.String);
            parameters.Add("Breed", animals.Breed, DbType.String);
            parameters.Add("BirthDate", animals.BirthDate, DbType.DateTime);
            parameters.Add("Sex", animals.Sex, DbType.Int32);
            parameters.Add("Price", animals.Price, DbType.Double);
            parameters.Add("Status", animals.Status, DbType.Int32);
            using (var connection = _context.CreateConnection())
            {
                return await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<int> DeleteAnimals(int AnimalId)
        {
            var query = $"DELETE FROM Animals WHERE AnimalId = {AnimalId}";
            using (var connection = _context.CreateConnection())
            {
                return await connection.ExecuteAsync(query, new { AnimalId });
            }
        }

        

        public async Task<List<Animals>> GetAnimalsByParameter(AnimalsForParameter animals)
        {
            var query = $"SELECT * FROM Animals WHERE 1=1  ";   
            query += $"OR Name like '{animals.Name}' ";
            query += $"OR Sex = { animals.Sex } ";
            query += $"OR Status = { animals.Status } ";
            using (var connection = _context.CreateConnection())
            {
                var result = await connection.QueryAsync<Animals>(query);
                return result.ToList();
            }
        }

        
    }
}
