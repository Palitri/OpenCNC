using Palitri.Graphics;
using Palitri.SVG.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Palitri.SVG
{
    /// <summary>
    /// Contains other elements and can define a viewport and coordinate system, which applies to its contents.
    /// A whole individual SVG can be embedded in another.
    /// </summary>
    public class SVGContainer : SVGGroup, IGraphicsObject
    {
        public override string NodeName { get { return "svg"; } }

        public float x;
        public float y;
        public float width;
        public float height;
        public float viewBoxX, viewBoxY, viewBoxWidth, viewBoxHeight;

        public SVGContainer()
        {
        }

        public SVGContainer(string fileName)
        {
            XmlTextReader xmlReader = new XmlTextReader(fileName);
            xmlReader.DtdProcessing = DtdProcessing.Ignore;
            while (!xmlReader.EOF && xmlReader.Read())
                if ((xmlReader.NodeType == XmlNodeType.Element) && (xmlReader.Name == NodeName))
                    break;

            this.Load(xmlReader);
        }

        public SVGContainer(XmlTextReader xmlReader)
        {
            this.Load(xmlReader);
        }

        public override void UpdateProperties()
        {
            try
            {
                this.x = this.GetAttribute<float>("x");
                this.y = this.GetAttribute<float>("y");
                this.width = this.GetAttribute<float>("width", 100.0f);
                this.height = this.GetAttribute<float>("height", 100.0f);

                string viewBox = this.GetAttribute<string>("viewbox");
                if (!string.IsNullOrWhiteSpace(viewBox))
                {
                    string[] values = viewBox.Split(new char[] { ' ', ',' });
                    if (values.Length == 4)
                    {
                        float.TryParse(values[0], out this.viewBoxX);
                        float.TryParse(values[1], out this.viewBoxY);
                        float.TryParse(values[2], out this.viewBoxWidth);
                        float.TryParse(values[3], out this.viewBoxHeight);

                        if (!this.HasAttribute("width"))
                            this.width = this.viewBoxWidth;
                        if (!this.HasAttribute("height"))
                            this.height = this.viewBoxHeight;
                    }
                }
                else
                {
                    this.viewBoxX = 0;
                    this.viewBoxY = 0;
                    this.viewBoxWidth = this.width;
                    this.viewBoxHeight = this.height;
                }
            }
            catch
            {
                return;
            }
        }

        public float Width => this.width;

        public float Height => this.height;

        public void Render(IGraphicsDevice g)
        {
            this.Render(this.x, this.y, this.width, this.height, g);
        }

        public void Render(float x, float y, IGraphicsDevice g)
        {
            this.Render(x, y, this.width, this.height, g);
        }

        public void Render(float x, float y, float scale, IGraphicsDevice g)
        {
            this.Render(x, y, this.width * scale, this.height * scale, g);
        }

        public void Render(float x, float y, float width, float height, IGraphicsDevice g)
        {
            this.Render(x, y, width, height, 0.0f, g);
        }

        public void Render(float x, float y, float width, float height, float angle, IGraphicsDevice g)
        {
            g.Begin();

            Matrix3 t = new Matrix3();
            Matrix3.CreatePlot(t, x, y, width / this.width, height / this.height, angle);

            this.Render(t, g);

            g.End();
        }

        public override void Render(Matrix3 transform, IGraphicsDevice g)
        {
            Matrix3 t1 = new Matrix3();
            Matrix3 t2 = new Matrix3();

            Matrix3.CreateViewport(t1, this.viewBoxX, this.viewBoxY, this.viewBoxWidth, this.viewBoxHeight);
            Matrix3.CreatePlot(t2, this.x, this.y, this.width, this.height);
            Matrix3.Multiply(t1, t1, t2);

            Matrix3.Multiply(t1, t1, transform);

            base.Render(t1, g);
        }

    }
}
