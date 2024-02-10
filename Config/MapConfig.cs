using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using WebFinancy.Data;
using WebFinancy.Model;

namespace WebFinancy.Config
{
    public class MapConfig
    {
        public static MapperConfiguration RegisterMapping(){
            var mappconfig = new MapperConfiguration(config => 
            {
                config.CreateMap<FinancyRecord,Financy>();
                config.CreateMap<Financy,FinancyRecord>();
            });
            return mappconfig;
        }
    }
}