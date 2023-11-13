using Palitri.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palitri.SVG.Elements
{
    public class SVGUse : SVGElement
    {
        public override string NodeName { get { return "use"; } }

        public string href;
        public float x, y, width, height;

        public SVGElement reference;

        public override void UpdateProperties()
        {
            this.x = this.GetAttribute<float>("x");
            this.y = this.GetAttribute<float>("y");
            this.width = this.GetAttribute<float>("width");
            this.height = this.GetAttribute<float>("height");

            this.href = this.GetAttribute<string>("href");
            if (string.IsNullOrWhiteSpace(this.href))
                this.href = this.GetAttribute<string>("xlink:href");
            this.reference = this.FindReference(this.href);
        }

        public override void Render(Matrix transform, IGraphicsDevice g)
        {
            if (this.reference != null)
            {
                Matrix transformation = new Matrix();
                Matrix.CreateTranslation(transformation, this.x, this.y);
                Matrix.Multiply(transformation, transformation, transform);

                this.reference.Render(transformation, g);
            }
        }

        private SVGElement FindReference(string href)
        {
            if (string.IsNullOrWhiteSpace(href))
                return null;

            href = href.Trim('#');

            SVGGroup parent = (SVGGroup)this.parent;
            while (parent != null)
            {
                SVGElement reference = FindElement(parent, href);
                if (reference != null)
                    return reference;

                parent = (SVGGroup)parent.parent;
            }

            return null;
        }

        private static SVGElement FindElement(SVGGroup parent, string name)
        {
            foreach (SVGElement element in parent.elements)
            {
                if (element.Id == name)
                    return element;

                if (element is SVGGroup)
                    return FindElement((SVGGroup)element, name);
            }

            return null;
        }
    }
}
