using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palitri.OpenCNC.Driver.Utils
{
    internal class BitUtils
    {
        static public void SetBits(byte[] bits, byte[] bitMask, byte[] bitValues)
        {
            int byteCount = Utils.Min(bits.Length, bitMask.Length, bitValues.Length);
            for (int i = 0; i < byteCount; i++)
                bits[i] = (byte)(bits[i] & ~bitMask[i] | bitValues[i]);
        }

        static public void SetBits(byte[] bits, byte[] bitMask, bool value)
        {
            int byteCount = Utils.Min(bits.Length, bitMask.Length);
            for (int i = 0; i < byteCount; i++)
                bits[i] = (byte)(bits[i] & ~bitMask[i] | (value ? bitMask[i] : 0));
        }
        static public byte[] ParseString(string bits, int numericBase = 10)
        {
            bits = bits.Replace(" ", string.Empty);

            if (bits.StartsWith("0b"))
            {
                numericBase = 2;
                bits = bits.Substring(2);
            }
            else if (bits.StartsWith("0x"))
            {
                numericBase = 16;
                bits = bits.Substring(2);
            }
            else if (bits.StartsWith("0d"))
            {
                numericBase = 10;
                bits = bits.Substring(2);
            }
            else if (bits.Contains('#'))
            {
                int separatorIndex = bits.IndexOf('#') + 1;
                numericBase = int.Parse(bits.Substring(0, separatorIndex - 1));
                bits = bits.Substring(separatorIndex + 1);
            }

            ulong intValue = Convert.ToUInt64(bits, numericBase);
            byte[] result = BitConverter.GetBytes(intValue);

            return result;
        }
    }
}
