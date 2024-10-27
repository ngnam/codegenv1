using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGeneratorV1
{
    public class Table
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Column> Columns { get; set; }
        public Table()
        {
            Columns = new HashSet<Column>();
        }
    }
}
