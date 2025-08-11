using Palitri.Graphics;
using Palitri.SVG;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Palitri.SVG.Elements
{
    /// <summary>
    /// Contains other elements
    /// Used for grouping other elements together.
    /// The attributes of the group get inherited by its children
    /// </summary>
    public class SVGGroup : SVGElement
    {
        public override string NodeName { get { return "g"; } }

        public List<SVGElement> elements = new List<SVGElement>();

        public override void Render(Matrix3 transform, IGraphicsDevice g) 
        {
            Matrix3 transformation = new Matrix3();

            foreach (SVGElement element in this.elements)
            {
                Matrix3.Multiply(transformation, element.GetTransformMatrix(), transform);

                element.Render(transformation, g);
            }
        }

        public override bool OnLoadElement(XmlTextReader xmlReader)
        {
            SVGElement element = SVGGroup.CreateElement(xmlReader.Name.ToLower());
            if (element == null)
                return false;

            element.parent = this;

            if (element.Load(xmlReader))
            {
                this.elements.Add(element);
                return true;
            }

            return false;
        }

        private static SVGElement CreateElement(string elementName)
        {
            if (elementName == "circle")
                return new SVGCircle();

            if (elementName == "ellipse")
                return new SVGEllipse();

            if (elementName == "line")
                return new SVGLine();

            if (elementName == "polyline")
                return new SVGPolyline();

            if (elementName == "polygon")
                return new SVGPolygon();

            if (elementName == "path")
                return new SVGPath();

            if (elementName == "rect")
                return new SVGRect();

            if (elementName == "text")
                return new SVGText();

            if (elementName == "g")
                return new SVGGroup();

            if (elementName == "use")
                return new SVGUse();

            if (elementName == "svg")
                return new SVGContainer();

            return null;
        }

        public int TotalElements(bool includeGroups = false)
        {
            int result = 0;
            foreach (SVGElement element in this.elements)
            {
                if (element is SVGGroup)
                {
                    if (includeGroups)
                        result++;

                    result += ((SVGGroup)element).TotalElements(includeGroups);
                }
                else
                    result++;
            }

            return result;
        }
    }
}
