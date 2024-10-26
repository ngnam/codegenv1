using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGeneratorV1
{
    public class SqlConfiguration
    {
        //[Configuration]
        //SqlServerName=<your IP>
        //UserName=<sqluser>
        //Password =<sqlpassword>
        //DatabaseName=<databaseName>
        public string SqlServerName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string DatabaseName { get; set; }

    }
}
