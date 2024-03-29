﻿using Palitri.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palitri.SVG.Elements
{
    public class SVGPolyline : SVGElement
    {
        public override string NodeName { get { return "polyline"; } }

        public List<Tuple<float, float>> points = new List<Tuple<float, float>>();

        public override void UpdateProperties()
        {
            string pointsString = this.attributes["points"];
            if (string.IsNullOrWhiteSpace(pointsString))
                return;

            string[] pointsCoords = pointsString.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string pointCoords in pointsCoords)
            {
                string[] coords = pointCoords.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (coords.Length == 2)
                {
                    float x, y;
                    if (float.TryParse(coords[0], out x) && float.TryParse(coords[1], out y))
                        this.points.Add(new Tuple<float, float>(x, y));
                }
            }
        }

        public override void Render(Matrix transform, IGraphicsDevice g)
        {
            g.Polyline(this.points.Select(p => transform.Transform(p.Item1, p.Item2)).ToArray());
        }
    }
}
