using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPRPGtest
{
    public class Special
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public int Boost { get; set; }
        public int Value { get; set; }

        public Special(string name, string type, int boost, int value)
        {
            this.Name = name;
            this.Type = type;
            this.Boost = boost;
            this.Value = value;
        }
    }
}
