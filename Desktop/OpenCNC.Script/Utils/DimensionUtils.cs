using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palitri.OpenCNC.Script.Utils
{
    internal class DimensionUtils
    {
        static private char[] dimensionNames = new char[] { 'X', 'Y', 'Z', 'W', 'V', 'U' };
        static private Dictionary<char, int> dimensionIndices = dimensionNames.ToDictionary(c => c, c => Array.IndexOf(dimensionNames, c));

        static public string GetName(int index)
        {
            if ((index < 0) || (index >= dimensionNames.Length))
                return "d" + index.ToString();

            return dimensionNames[index].ToString();
        }
        static public int GetIndex(string name)
        {
            if (name.Length == 1)
            {
                if (dimensionIndices.TryGetValue(name[0], out int result))
                    return result;
            }

            return -1;
        }
    }
}
