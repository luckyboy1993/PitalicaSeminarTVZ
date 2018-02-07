using System;
using System.Collections.Generic;

namespace PitalicaSeminar.DAL.Entities
{
    public partial class Addresses
    {
        public Addresses()
        {
            Schools = new HashSet<Schools>();
        }

        public int AddressId { get; set; }
        public string CityName { get; set; }
        public string Country { get; set; }
        public string StreetName { get; set; }

        public ICollection<Schools> Schools { get; set; }
    }
}
