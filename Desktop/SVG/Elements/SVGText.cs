using Palitri.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Palitri.SVG.Elements
{
    public class SVGText : SVGElement
    {
        public override string NodeName { get { return "text"; } }

        public override void Render(Matrix3 transform, IGraphicsDevice g)
        {
        }
    }
}
