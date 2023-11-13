using Palitri.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Palitri.SVG.Elements
{
    public class SVGLine : SVGElement
    {
        public override string NodeName { get { return "line"; } }

        public float x1, y1, x2, y2;

        public override void UpdateProperties()
        {
            this.x1 = this.GetAttribute<float>("x1");
            this.y1 = this.GetAttribute<float>("y1");
            this.x2 = this.GetAttribute<float>("x2");
            this.y2 = this.GetAttribute<float>("y2");
        }

        public override void Render(Matrix transform, IGraphicsDevice g)
        {
            g.Polyline(new Vector[] { transform.Transform(this.x1, this.y1), transform.Transform(this.x2, this.y2) });
        }
    }
}
