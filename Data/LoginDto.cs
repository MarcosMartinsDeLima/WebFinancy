using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebFinancy.Data
{
    public record LoginDto
    {
        public string senha {get;set;} 
        public string email {get;set;}
    }
}