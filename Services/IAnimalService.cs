using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cw4.Models;

namespace cw4.Services
{
    public interface IAnimalService
    {
        public List<Animal> GetAnimals(string orderBy);
        public int AddAnimal(Animal animal);
        public int UpdateAnimal(Animal animal, int id);
        public int DeleteAnimal(int id);
    }
}