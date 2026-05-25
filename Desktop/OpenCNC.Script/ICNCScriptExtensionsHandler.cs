using Palitri.OpenCNC.Driver.Settings;
using Palitri.OpenCNC.Script;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCNC.Script
{
    public interface ICNCScriptExtensionsHandler
    {
        public CNCScriptEngine ScriptEngine { get; }
        public OpenIoTBoardConfiguration BoardConfig { get; }

        public void Write(string text, bool newLine = true);
        public void Clear();
        public void Exit();
    }
}
