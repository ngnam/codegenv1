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
        //SqlServerName=220.165.10.199
        //UserName=os_iso20022
        //Password = Abc@12345
        //DatabaseName=MizuhoData
        public string SqlServerName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string DatabaseName { get; set; }

    }
}
