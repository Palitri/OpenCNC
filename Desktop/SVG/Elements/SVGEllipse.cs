using Palitri.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Palitri.SVG.Elements
{
    public class SVGEllipse : SVGElement
    {
        public override string NodeName { get { return "ellipse"; } }

        public float x, y, rx, ry;

        public override void UpdateProperties()
        {
            this.x = this.GetAttribute<float>("cx");
            this.y = this.GetAttribute<float>("cy");
            this.rx = this.GetAttribute<float>("rx");
            this.ry = this.GetAttribute<float>("ry");
        }

        public override void Render(Matrix3 transform, IGraphicsDevice g)
        {
            g.Arc(transform.Transform(this.x, this.y), transform.TransformDimensions(this.rx, 0.0f), transform.TransformDimensions(0.0f, this.ry), 0.0f, (float)Math.PI * 2.0f);
        }
    }
}
