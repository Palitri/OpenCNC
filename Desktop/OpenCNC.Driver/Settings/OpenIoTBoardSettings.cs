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
    public class OpenIoTBoardSettings
    {
        public class AsyncChannelSetting
        {
            public int PeripheralId { get; set; }
            public int StepsPerUnit { get; set; }

            [JsonConverter(typeof(StringToBytesConverter))]
            public byte[] EnableBitmask { get; set; }

            [JsonConverter(typeof(StringToBytesConverter))]
            public byte[] EnableValueOn { get; set; }
            [JsonConverter(typeof(StringToBytesConverter))]
            public byte[] EnableValueOff { get; set; }

            [JsonConverter(typeof(StringToBytesConverter))]
            public byte[] SleepBitmask { get; set; }

            [JsonConverter(typeof(StringToBytesConverter))]
            public byte[] SleepValueOn { get; set; }

            [JsonConverter(typeof(StringToBytesConverter))]
            public byte[] SleepValueOff { get; set; }

            [JsonConverter(typeof(StringToBytesConverter))]
            public byte[] SleepValue { get; set; }

            [JsonConverter(typeof(StringToBytesConverter))]
            public byte[] StepModeBitmask { get; set; }

            [JsonConverter(typeof(StringToBytesConverter))]
            public byte[] StepModeValueFull { get; set; }

            [JsonConverter(typeof(StringToBytesConverter))]
            public byte[] StepModeValueHalf { get; set; }

            [JsonConverter(typeof(StringToBytesConverter))]
            public byte[] StepModeValueQuarter { get; set; }

            [JsonConverter(typeof(StringToBytesConverter))]
            public byte[] StepModeValueEighth { get; set; }

            [JsonConverter(typeof(StringToBytesConverter))]
            public byte[] StepModeValueSixteenth { get; set; }
        }


        public int AsyncDriverPeripheralID { get; set; }
        public int CNCPeripheralID { get; set; }
        public int ToolPeripheralID { get; set; }
        public int ShiftRegPeripheralID { get; set; }

        [JsonConverter(typeof(StringToBytesConverter))]
        public byte[] ToolEnableBitmask { get; set; }

        [JsonConverter(typeof(StringToBytesConverter))]
        public byte[] ToolEnableValueOn { get; set; }

        [JsonConverter(typeof(StringToBytesConverter))]
        public byte[] ToolEnableValueOff { get; set; }

        public List<AsyncChannelSetting> AxesSettings { get; set; }

        public static OpenIoTBoardSettings LoadSettings(string fileName)
        {
            string jsonContent = File.ReadAllText(fileName);
            return JsonSerializer.Deserialize<OpenIoTBoardSettings>(jsonContent);
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
