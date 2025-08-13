using Palitri.OpenCNC.App.Common;
using OrderedPropertyGrid;
using Palitri.OpenCNC.Driver;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace Palitri.OpenCNC.App.Settings
{
    [TypeConverter(typeof(PropertySorter))]
    public class OpenCNCAppSettings
    {
        [Category("Application"), PropertyOrder(0)]
        [Description("Set the stepping mode of the motors")]
        public CNCMotorStepMode StepMode { get; set; }

        [Category("Application"), PropertyOrder(1)]
        [Description("Power value when machine is working and actively utilizing the power tool. Range [0, 1]")]
        public float OnPower { get; set; }

        [Category("Application"), PropertyOrder(2)]
        [Description("Power value when machine is working, but the power tool is not in use. For example when only moving. Range [0, 1]")]
        public float OffPower { get; set; }

        [Category("Application"), PropertyOrder(3)]
        [Description("Power value when machine is idle. Range [0, 1]")]
        public float IdlePower { get; set; }

        [Category("Application"), PropertyOrder(4)]
        [Description("Power value for testing. Usually, this is a very low value, close to minimum. Range [0, 1]")]
        public float TestingPower { get; set; }

        [Category("Application"), PropertyOrder(5)]
        [Description("For tools which have to disengage while tool is not actively working, this property lifts the tool. In units")]
        public float DisengagementDistance { get; set; }

        [Category("Application"), PropertyOrder(6)]
        [Description("For tools, which have to 'dig in' before start active work, this property brings the tool close to the workpiece and then back. In units.")]
        public float EngagementDistance { get; set; }

        [Category("Application"), PropertyOrder(7)]
        [Description("The speed at which the power tool should move while actively working. In units per second")]
        public float WorkSpeed { get; set; }

        [Category("Application"), PropertyOrder(8)]
        [Description("The speed at which the power tool should move while not actively working, when only transporting the power tool from one location to another. In units per second")]
        public float MoveSpeed { get; set; }

        [Category("Application"), PropertyOrder(9)]
        [Description("The normal speed at which the power tool should move when a manual translation is issued. In units per second")]
        public float ManualSpeed { get; set; }

        [Category("Application"), PropertyOrder(10)]
        [Description("The fine speed at which the power tool should move when a manual translation is issued. In units per second")]
        public float ManualSpeedFine { get; set; }

        [Category("Application"), PropertyOrder(11)]
        [Description("Determines whether motors should be powered off while the machine is idle")]
        public bool PowerOffMotorsWhenIdle { get; set; }

        private CNCLengthUnitType unitType;
        [Category("Application"), PropertyOrder(12)]
        [Description("Type of length measurement / dimension unit")]
        public CNCLengthUnitType UnitType {
            get
            {
                return unitType;
            }

            set 
            {
                if (unitType != value)
                {
                    unitType = value;
                    switch (unitType)
                    {
                        case CNCLengthUnitType.Millimeters:
                            UnitSize = 1.0f;
                            break;

                        case CNCLengthUnitType.Inches:
                            UnitSize = 25.4f;
                            break;
                    }
                }
            }
        }

        private float unitSize;
        [Category("Application"), PropertyOrder(12)]
        [Description("Size of a length measurement / dimension unit in millimeters")]
        public float UnitSize
        {
            get
            {
                return unitSize;
            }

            set
            {
                if (unitSize != value)
                {
                    unitSize = value;
                    if (unitSize == 1.0f)
                        unitType = CNCLengthUnitType.Millimeters;
                    else if (unitSize == 25.4f)
                        unitType = CNCLengthUnitType.Inches;
                }
            }
        }

        [Category("Application"), PropertyOrder(13)]
        [Description("Type of temperature unit")]
        public CNCTemperatureUnitType TemperatureUnitType { get; set; }

        [Category("Application"), PropertyOrder(14)]
        [Description("Determines whether to get back to original position when done plotting")]
        public bool ReturnToOrigin { get; set; }

        [Category("Display"), PropertyOrder(100)]
        [Description("The length of the diagonal of your display in inches. Relevant when displaying objects on the screen in aspect to their actual physical size. Units: inches")]
        public float DisplaySize { get; set; }


        
        [Category("Device"), PropertyOrder(200)]
        [Description("The number of full steps the first motor makes for exactly one turn")]
        public int Motor1FullStepsPerTurn { get; set; }

        [Category("Device"), PropertyOrder(201)]
        [Description("The the distance on the axis, which the first motor travels per one turn. In units.")]
        public float Motor1UnitsPerTurn { get; set; }

        [Category("Device"), PropertyOrder(202)]
        [Description("The number of full steps the second motor makes for exactly one turn")]
        public int Motor2FullStepsPerTurn { get; set; }

        [Category("Device"), PropertyOrder(203)]
        [Description("The the distance on the axis, which the second motor travels per one turn. In units.")]
        public float Motor2UnitsPerTurn { get; set; }
        
        [Category("Device"), PropertyOrder(204)]
        [Description("The number of full steps the third motor makes for exactly one turn")]
        public int Motor3FullStepsPerTurn { get; set; }

        [Category("Device"), PropertyOrder(205)]
        [Description("The the distance on the axis, which the third motor travels per one turn. In units.")]
        public float Motor3UnitsPerTurn { get; set; }


        [Category("3D Printing"), PropertyOrder(300)]
        [Description("Height of print layers. In units")]
        public float LayerHeight { get; set; }

        [Category("3D Printing"), PropertyOrder(301)]
        [Description("Temperature of the filler in the hot end. In Celsius")]
        public float FillerTemp { get; set; }

        [Category("3D Printing"), PropertyOrder(302)]
        [Description("Temperature of the hot bed. In Celsius")]
        public float BaseTemp { get; set; }

        [Category("3D Printing"), PropertyOrder(303)]
        [Description("The speed of extruding the filler while printing. In units per speed per second")]
        public float FillSpeed { get; set; }

        [Category("3D Printing"), PropertyOrder(304)]
        [Description("Allows for retraction of the filler material immediately after filling, in order to eliminate tension in the fill pool and unwanted discharge of filler. In units")]
        public float Retraction { get; set; }

        [Category("3D Printing"), PropertyOrder(305)]
        [Description("The speed of retraction. Usually faster than the working fill speed. In units per speed per second")]
        public float RetractionSpeed { get; set; }

        [Category("Miscellaneous"), PropertyOrder(901)]
        [Description("Play initialization sound")]
        public bool InitSound { get; set; }


        //[Category("Cutting"), PropertyOrder(304)]
        //[Category("Milling"), PropertyOrder(304)]




        public OpenCNCAppSettings()
        {
            SetDefaults();
        }

        public OpenCNCAppSettings(string fileName)
        {
            SetDefaults();

            LoadFromFile(fileName);
        }

        public void LoadFromFile(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                return;

            try
            {
                using (Stream fileStream = new FileStream(fileName, FileMode.Open))
                {
                    Type type = GetType();
                    XmlSerializer serializer = new XmlSerializer(type);
                    OpenCNCAppSettings settings = (OpenCNCAppSettings)serializer.Deserialize(fileStream);

                    PropertyInfo[] properties = type.GetProperties();
                    foreach (PropertyInfo property in properties)
                    {
                        property.SetValue(this, property.GetValue(settings));
                    }
                }
            }
            catch
            {
            }
        }

        public void SaveToFile(string fileName)
        {
            try
            {
                using (Stream file = File.Create(fileName))
                {
                    XmlSerializer writer = new XmlSerializer(GetType());
                    writer.Serialize(file, this);
                    file.Close();
                }
            }
            catch (Exception ex)
            {
            }
        }

        public void SetDefaults()
        {
            StepMode = CNCMotorStepMode.Quarter;
            OnPower = 0.25f;
            OffPower = 0.0f;
            IdlePower = 0.0f;
            TestingPower = 0.001f;
            DisengagementDistance = 20.0f;
            EngagementDistance = 0.0f;
            WorkSpeed = 10.0f;
            MoveSpeed = 15.0f;
            ManualSpeed = 30.0f;
            ManualSpeedFine = 2.0f;
            PowerOffMotorsWhenIdle = true;

            DisplaySize = 23.8f;

            Motor1FullStepsPerTurn = 200;
            Motor1UnitsPerTurn = 8.0f;
            Motor2FullStepsPerTurn = 200;
            Motor2UnitsPerTurn = 8.0f;
            Motor3FullStepsPerTurn = 200;
            Motor3UnitsPerTurn = 8.0f;
        }

        public float GetPhysicalSizeAspectRatio(nint handle)
        {
            const double millimetersPerInch = 25.4;

            //Point resolution = new Point(SystemInformation.VirtualScreen.Width, SystemInformation.VirtualScreen.Height);

            var screen = Screen.FromHandle(handle);
            Point resolution = new Point(screen.Bounds.Left - screen.Bounds.Right, screen.Bounds.Top - screen.Bounds.Bottom);

            double diagonalPixels = Math.Sqrt(resolution.X * resolution.X + resolution.Y * resolution.Y);
            double pixelSizeMillimeters = millimetersPerInch * DisplaySize / diagonalPixels;
            return (float)(1.0 / pixelSizeMillimeters);
        }
    }
}
