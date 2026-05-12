using Palitri.OpenCNC.Driver.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Palitri.OpenCNC.Driver.Settings
{
    public class OpenIoTBoardConfiguration
    {
        public class AsyncChannelConfiguration
        {
            public int PeripheralId { get; set; }
            public int StepsPerUnit { get; set; }

            public int PropertyIdSpeed { get; set; }
            public int PropertyIdTurn { get; set; }

            public SwitchConfiguration? Enable { get; set; }

            public SwitchConfiguration? Sleep { get; set; }

            public StepModeConfiguration? StepMode { get; set; }
        }

        public class SwitchConfiguration
        {
            [JsonConverter(typeof(StringToBytesConverter))]
            public byte[] Bitmask { get; set; }

            [JsonConverter(typeof(StringToBytesConverter))]
            public byte[] On { get; set; }

            [JsonConverter(typeof(StringToBytesConverter))]
            public byte[] Off { get; set; }
        }

        public class StepModeConfiguration
        {
            [JsonConverter(typeof(StringToBytesConverter))]
            public byte[] Bitmask { get; set; }

            [JsonConverter(typeof(StringToBytesConverter))]
            public byte[] Full { get; set; }

            [JsonConverter(typeof(StringToBytesConverter))]
            public byte[] Half { get; set; }

            [JsonConverter(typeof(StringToBytesConverter))]
            public byte[] Quarter { get; set; }

            [JsonConverter(typeof(StringToBytesConverter))]
            public byte[] Eighth { get; set; }

            [JsonConverter(typeof(StringToBytesConverter))]
            public byte[] Sixteenth { get; set; }
        }

        public int AsyncDriverPeripheralID { get; set; }
        public int CNCPeripheralID { get; set; }
        public int ToolPeripheralID { get; set; }
        public int ShiftRegPeripheralID { get; set; }
        public int ControlBitsCount { get; set; }

        public SwitchConfiguration ToolEnable { get; set; }

        public List<AsyncChannelConfiguration> Axes { get; set; }

        public List<SwitchConfiguration> SwitchesSettings { get; set; }
        public static OpenIoTBoardConfiguration LoadJSON(string fileName)
        {
            string jsonContent = File.ReadAllText(fileName);
            return JsonSerializer.Deserialize<OpenIoTBoardConfiguration>(jsonContent);
        }

        
        public string ToJson()
        {
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
            };

            return JsonSerializer.Serialize(this, options);
        }

        public void SaveToJson(string fileName)
        {
            File.WriteAllText(fileName, this.ToJson());
        }
    }

    public class StringToBytesConverter : JsonConverter<byte[]>
    {
        public override byte[] Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                string value = reader.GetString();
                return BitUtils.ParseString(value, 2);
            }

            if (reader.TokenType == JsonTokenType.Number)
            {
                long value = reader.GetInt64();
                return BitConverter.GetBytes(value);
            }

            throw new JsonException("Unable to convert to int");
        }

        public override void Write(Utf8JsonWriter writer, byte[] value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }


    }
}
