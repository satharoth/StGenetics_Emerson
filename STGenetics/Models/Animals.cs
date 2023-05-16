using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STGenetics.Models
{
    public class Animals
    {
        public int AnimalId { get; set; }
        public string Name { get; set; }
        public string Breed { get; set; }
        public DateTime BirthDate { get; set; }
        public int Sex { get; set; }
        public double Price { get; set; }
        public int Status { get; set; }

        internal string Dump()
        {
            return "AnimalId:" + this.AnimalId +
                "Name:" + this.Name +
                "Breed:" + this.Breed +
                "BirthDate:" + this.BirthDate +
                "Sex:" + this.Sex +
                "Price:" + this.Price +
                "Status:" + this.Status;
        }
    }
}
