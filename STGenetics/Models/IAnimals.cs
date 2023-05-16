using STGenetics.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STGenetics.Models
{
    public interface IAnimals
    {
        public Task<List<Animals>> GetAnimals();
        public Task<Animals> GetAnimal(int id);
        public Task<Animals> CreateAnimals(Animals animals);
        public Task<int> UpdateAnimals(int id, Animals animals);
        public Task<int> DeleteAnimals(int id);
        public Task<List<Animals>> GetAnimalsByParameter(AnimalsForParameter animals);
    }
}
