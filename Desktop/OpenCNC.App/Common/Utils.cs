using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace Palitri.OpenCNC.App.Common
{
    internal class Utils
    {
        static public T Min<T>(params T[] values) where T : IComparable<T>
        {
            int length = values.Length;
            if (length == 0)
                return default(T);

            T result = values[0];
            for (int i = 1; i < length; i++)
            {
                T val = values[i];
                if (val.CompareTo(result) < 0)
                    result = val;
            }

            return result;
        }
    }
}
