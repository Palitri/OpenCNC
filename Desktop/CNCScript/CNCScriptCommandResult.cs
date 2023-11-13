using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palitri.CNCScript
{
    public class CNCScriptCommandResult
    {
        public CNCScriptCommandResultType ResultType { get; private set; }
        public string Text { get; private set; }

        public CNCScriptCommandResult()
        {
            this.ResultType = CNCScriptCommandResultType.Success;
            this.Text = string.Empty;
        }

        public CNCScriptCommandResult(CNCScriptCommandResultType resultType)
        {
            this.ResultType = resultType;
            this.Text = string.Empty;
        }

        public CNCScriptCommandResult(CNCScriptCommandResultType resultType, string text)
        {
            this.ResultType = resultType;
            this.Text = text;
        }
    }
}
