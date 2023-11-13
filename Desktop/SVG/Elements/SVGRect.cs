using Palitri.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Palitri.SVG.Elements
{
    public class SVGRect : SVGElement
    {
        public override string NodeName { get { return "rect"; } }

        public float x, y, width, height;

        public override void UpdateProperties()
        {
            this.x = this.GetAttribute<float>("x");
            this.y = this.GetAttribute<float>("y");
            this.width = this.GetAttribute<float>("width");
            this.height = this.GetAttribute<float>("height");
        }

        public override void Render(Matrix transform, IGraphicsDevice g)
        {
            g.Polyline(new Vector[] { transform.Transform(this.x, this.y), transform.Transform(this.x + this.width, this.y), transform.Transform(this.x + this.width, this.y + this.height), transform.Transform(this.x, this.y + this.height), transform.Transform(this.x, this.y) });
        }
    }
}
