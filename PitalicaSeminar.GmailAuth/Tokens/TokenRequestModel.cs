using PitalicaSeminar.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PitalicaSeminar.GmailAuth.Tokens
{
    public class TokenRequestModel
    {
        public string UserName { get; set; }       
        public string DoubleName { get; set; }

        public TokenRequestModel()
        { }
    }
}
