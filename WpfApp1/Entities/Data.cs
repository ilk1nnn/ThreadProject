using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Entities
{
    public class Data
    {

        public string Name { get; set; }

        public static List<Data> GetAll()
        {
            return new List<Data>()
            {
                new Data
                {
                    Name="Apple"
                },
                 new Data
                {
                    Name="Pomegranate"
                },
                  new Data
                {
                    Name="Pineapple"
                },
                   new Data
                {
                    Name="Salam"
                },
                    new Data
                {
                    Name="Hello"
                },
                     new Data
                {
                    Name="Strawberry"
                }
            };
        }
    }
}
