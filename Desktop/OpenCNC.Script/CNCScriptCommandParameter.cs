using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palitri.OpenCNC.Script
{
    public class CNCScriptCommandParameter
    {
        public string Name { get; set; }
        public Type ValueType { get; set; }

        public CNCScriptCommandParameter(string name, Type valueType)
        {
            this.Name = name;
            this.ValueType = valueType;
        }
    }
}
