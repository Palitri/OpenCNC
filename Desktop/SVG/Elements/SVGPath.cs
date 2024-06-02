using Palitri.Graphics;
using SVG.Elements.PathElements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Palitri.SVG.Elements
{
    public class SVGPath : SVGElement
    {
        public override string NodeName { get { return "path"; } }

        public string d;

        public List<IPathElement> elements = new List<IPathElement>();

        public override void UpdateProperties()
        {
            this.d = this.attributes["d"];

            this.elements.Clear();

            char[] signatures = { 'M', 'L', 'H', 'V', 'Z', 'C', 'S', 'Q', 'T', 'A' };
            char[] dataSplitters = { ' ', ',', '\r', '\n', '\t' };

            int pos = 0, lastPos = 0;
            while (pos <= this.d.Length)
            {
                if ((pos == this.d.Length) || signatures.Contains(char.ToUpper(this.d[pos])))
                {
                    int elementLength = pos - lastPos;
                    if (elementLength != 0)
                    {
                        // Path data is a series of sequence of path elements, each a signature character followed by numbers. Numbers can be divided by varuios data splitters, as well as by nothing, if for example the next digit is negative and the minus sign can act as a divider
                        string[] data = this.d.Substring(lastPos + 1, elementLength - 1).Replace("-", ",-").Split(dataSplitters, StringSplitOptions.RemoveEmptyEntries);
                        char signature = this.d[lastPos];
                        bool isRelative = char.IsLower(signature);
                        if (isRelative)
                            signature = char.ToUpper(signature);

                        switch (signature)
                        {
                            case 'M':
                                for (int i = 0; i <= data.Length - 2; i += 2)
                                    if (i == 0)
                                        this.elements.Add(new PathM() { relative = isRelative, x = float.Parse(data[i + 0]), y = float.Parse(data[i + 1]) });
                                            else
                                        this.elements.Add(new PathL() { relative = isRelative, x = float.Parse(data[i + 0]), y = float.Parse(data[i + 1]) });

                                break;

                            case 'L':
                                for (int i = 0; i <= data.Length - 2; i += 2)
                                    this.elements.Add(new PathL() { relative = isRelative, x = float.Parse(data[i + 0]), y = float.Parse(data[i + 1]) });

                                break;

                            case 'H':
                                for (int i = 0; i < data.Length; i++)
                                    this.elements.Add(new PathH() { relative = isRelative, x = float.Parse(data[i]) });

                                break;

                            case 'V':
                                for (int i = 0; i < data.Length; i++)
                                    this.elements.Add(new PathV() { relative = isRelative, y = float.Parse(data[i]) });

                                break;

                            case 'Z':
                                this.elements.Add(new PathZ());

                                break;

                            case 'C':
                                for (int i = 0; i <= data.Length - 6; i += 6)
                                    this.elements.Add(new PathC() { relative = isRelative, x1 = float.Parse(data[i + 0]), y1 = float.Parse(data[i + 1]), x2 = float.Parse(data[i + 2]), y2 = float.Parse(data[i + 3]), x = float.Parse(data[i + 4]), y = float.Parse(data[i + 5]) });

                                break;

                            case 'S':
                                for (int i = 0; i <= data.Length - 4; i += 4)
                                    this.elements.Add(new PathS() { relative = isRelative, x2 = float.Parse(data[i + 0]), y2 = float.Parse(data[i + 1]), x = float.Parse(data[i + 2]), y = float.Parse(data[i + 3]) });

                                break;

                            case 'Q':
                                for (int i = 0; i <= data.Length - 4; i += 4)
                                    this.elements.Add(new PathQ() { relative = isRelative, x1 = float.Parse(data[i + 0]), y1 = float.Parse(data[i + 1]), x = float.Parse(data[i + 2]), y = float.Parse(data[i + 3]) });

                                break;

                            case 'T':
                                for (int i = 0; i <= data.Length - 2; i += 2)
                                    this.elements.Add(new PathT() { relative = isRelative, x = float.Parse(data[i + 0]), y = float.Parse(data[i + 1]) });

                                break;

                            case 'A':
                                for (int i = 0; i <= data.Length - 7; i += 7)
                                    this.elements.Add(new PathA() { relative = isRelative, rx = float.Parse(data[i + 0]), ry = float.Parse(data[i + 1]), angle = float.Parse(data[i + 2]), largeSweep = int.Parse(data[i + 3]) != 0, sweep = int.Parse(data[i + 4]) != 0, x = float.Parse(data[i + 5]), y = float.Parse(data[i + 6]) });

                                break;

                            default:
                            {
                                break;
                            }

                        }

                    }

                    lastPos = pos;
                }

                pos++;
            }
        }

        public override void Render(Matrix transform, IGraphicsDevice g)
        {
            SVGPathRenderingParameters renderParams = new SVGPathRenderingParameters();

            foreach (IPathElement element in this.elements)
                element.Render(transform, g, renderParams);
        }

    }
}
