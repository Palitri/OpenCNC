using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palitri.CNCDriver
{
    public class CNCProperty
    {
        private static Dictionary<CNCDataType, Type>dataTypes = new Dictionary<CNCDataType,Type>()
        {
            { CNCDataType.None, typeof(object) },
            { CNCDataType.Int8, typeof(sbyte) },
            { CNCDataType.UInt8, typeof(sbyte) },
            { CNCDataType.Int16, typeof(short) },
            { CNCDataType.UInt16, typeof(ushort) },
            { CNCDataType.Int32, typeof(int) },
            { CNCDataType.UInt32, typeof(uint) },
            { CNCDataType.Float32, typeof(float) },
            { CNCDataType.String, typeof(string) }
        };


        public int Id { get; set; }
        public string Name { get; set; }
        public CNCDataType DataType { get; private set; }
        public object Value { private get; set; }

        public Type SystemType { get { return CNCProperty.dataTypes[this.DataType]; } }

        public CNCProperty(int id, string name, CNCDataType type, object value)
        {
            this.Id = id;
            this.Name = name;
            this.DataType = CNCDataType.None;

            this.Set(type, value);
        }

        public void Set(CNCDataType type, object value)
        {
            this.DataType = type;

            this.Value = Convert.ChangeType(value, this.SystemType);
        }

        public void Set(object value)
        {
            this.Set(this.DataType, value);
        }

        public T Get<T>()
        {
            return (T)this.Value;
        }
    }
}
