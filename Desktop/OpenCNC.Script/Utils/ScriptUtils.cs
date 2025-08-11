using Palitri.OpenCNC.Script;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palitri.OpenCNC.Script.Utils
{
    public static class ScriptUtils
    {
        public static string[] SplitParams(string commandInputString)
        {
            char[] splitters = new char[] { ' ', ',', '(', ')' };
            return commandInputString.Split(splitters, StringSplitOptions.RemoveEmptyEntries);
        }

        public static Tuple<string, int>[] SplitParamsWithDetails(string commandInputString)
        {
            char[] splitters = new char[] { ' ', ',', '(', ')' };
            List<Tuple<string, int>> parameters = new List<Tuple<string, int>>();

            int start = 0;
            int count = commandInputString.Length;
            bool isSplitterPrevious = false;
            for (int i = 0; i <= count; i++)
            {
                bool isSplitter = i == count || splitters.Contains(commandInputString[i]);

                if (isSplitter)
                {
                    if (!isSplitterPrevious)
                    {
                        int length = i - start;
                        if (length > 0)
                        {
                            parameters.Add(new Tuple<string, int>(commandInputString.Substring(start, length), start));
                        }
                    }

                    start = i + 1;
                }

                isSplitterPrevious = isSplitter;
            }

            return parameters.ToArray();
        }

        public static bool ParseBool(string parameter, out bool result)
        {
            string[] keywords = new string[] { "true", "false", "1", "0", "on", "off", "enable", "disable", "enabled", "disabled", "yes", "no" };
            int count = keywords.Length;

            for (int i = 0; i < count; i++)
            {
                if (keywords[i].Equals(parameter, StringComparison.OrdinalIgnoreCase))
                {
                    result = i % 2 == 0;
                    return true;
                }
            }

            result = false;
            return false;
        }

        public static CNCScriptCommandResult GetResultByParameterCount(int inputParametersCount, int requiredParametersCount, bool infiniteParameters = false)
        {
            bool isPlural = requiredParametersCount > 1;

            if (inputParametersCount < requiredParametersCount)
                return new CNCScriptCommandResult(CNCScriptCommandResultType.Error, string.Format("Command requires {2}{0} parameter{1}.", requiredParametersCount, isPlural ? "s" : string.Empty, infiniteParameters ? "at least " : string.Empty));

            if (requiredParametersCount == 0)
            {
                if (inputParametersCount > 0)
                    return new CNCScriptCommandResult(CNCScriptCommandResultType.Warning, "Command takes no parameters.\r\nRedundant input will be ignored.");
            }
            else
            {
                if (infiniteParameters)
                {
                    if (inputParametersCount % requiredParametersCount != 0)
                        return new CNCScriptCommandResult(CNCScriptCommandResultType.Warning, string.Format("Command takes parameters in groups of {0}.\r\nRedundant input will be ignored.", requiredParametersCount));
                }
                else
                    if (inputParametersCount > requiredParametersCount)
                        return new CNCScriptCommandResult(CNCScriptCommandResultType.Warning, string.Format("Command takes {0} parameter{1}.\r\nRedundant input will be ignored.", requiredParametersCount, isPlural ? "s" : string.Empty));
            }

            return new CNCScriptCommandResult();
        }

        public static bool TryParse<T>(string value, out T result, out string message)
        {
            message = null;

            Type type = typeof(T);

            if (type == typeof(bool))
            {
                bool bResult;
                if (ParseBool(value, out bResult))
                {
                    result = (T)(object)bResult;
                    return true;
                }
            }

            try
            {
                TypeConverter typeConverter = TypeDescriptor.GetConverter(type);
                result = (T)typeConverter.ConvertFromString(value);
                return true;
            }
            catch
            {
            }

            result = (T)(type.IsValueType ? Activator.CreateInstance(type) : null);
            message = string.Format("Could not parse parameter {0}.\r\nExpected type is {1}.", value, type.Name);
            return false;

        }
    }
}
