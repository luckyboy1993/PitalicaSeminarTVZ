using System;
using System.Collections.Generic;

namespace PitalicaSeminar.DAL.Entities
{
    public partial class Schools
    {
        public Schools()
        {
            Users = new HashSet<Users>();
        }

        public int SchoolId { get; set; }
        public int AddressId { get; set; }
        public string Name { get; set; }

        public Addresses Address { get; set; }
        public ICollection<Users> Users { get; set; }
    }
}
