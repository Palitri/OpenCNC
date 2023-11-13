using Palitri.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Palitri.SVG.Elements
{
    public class SVGCircle : SVGElement
    {
        public override string NodeName { get { return "circle"; } }

        public float x, y, r;

        public override void UpdateProperties()
        {
            this.x = this.GetAttribute<float>("cx");
            this.y = this.GetAttribute<float>("cy");
            this.r = this.GetAttribute<float>("r");
        }

        public override void Render(Matrix transform, IGraphicsDevice g)
        {
            g.Arc(transform.Transform(this.x, this.y), transform.TransformDimensions(this.r, 0.0f), transform.TransformDimensions(0.0f, this.r), 0.0f, (float)Math.PI * 2.0f);
        }
    }
}
