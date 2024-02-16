using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2
{
    public class todo
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }

        public todo(string name, string description, DateTime date)
        {
            Name = name;
            Description = description;
            Date = date;

        }
    }
}
