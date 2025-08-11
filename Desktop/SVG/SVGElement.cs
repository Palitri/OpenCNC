using Palitri.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace Palitri.SVG
{
    public abstract class SVGElement
    {
        public abstract string NodeName { get; }

        public string Id { get; protected set; }

        public SVGElement parent = null;
        public Dictionary<string, string> attributes = new Dictionary<string,string>();

        public string Contents { get; set; }

        public virtual bool OnLoadElement(XmlTextReader xmlReader) { return false; }

        public virtual void UpdateProperties() { }

        public abstract void Render(Matrix3 transform, IGraphicsDevice g);

        public virtual bool Load(XmlTextReader xmlReader)
        {
            if (!((xmlReader.NodeType == XmlNodeType.Element) && xmlReader.Name.Equals(this.NodeName, StringComparison.OrdinalIgnoreCase)))
                return false;

            bool isEmptyElement = xmlReader.IsEmptyElement;

            while (xmlReader.MoveToNextAttribute())
                this.attributes.Add(xmlReader.Name.ToLower(), xmlReader.Value);

            this.Id = this.GetAttribute<string>("id");
            this.UpdateProperties();

            if (isEmptyElement)
                return true;

            bool skipped = false;
            while (!xmlReader.EOF && (skipped || xmlReader.Read()))
            {
                skipped = false;

                switch (xmlReader.NodeType)
                {
                    case XmlNodeType.Element:

                        isEmptyElement = xmlReader.IsEmptyElement;

                        if (!this.OnLoadElement(xmlReader))
                        {
                            xmlReader.Skip();
                            skipped = true;
                        }

                        break;

                    case XmlNodeType.Text:

                        this.Contents = xmlReader.Value;
                        break;

                    case XmlNodeType.EndElement:

                        if (xmlReader.Name.Equals(this.NodeName))
                            return true;

                        break;
                }
            }

            return false;
        }

        public Matrix3 GetTransformMatrix()
        {
            Matrix3 transform = new Matrix3();
            Matrix3 t = new Matrix3();

            string transformString;
            if (this.attributes.TryGetValue("transform", out transformString))
            {
                string[] elements = transformString.Split(new char[] { ' ', ',', '(', ')' }, StringSplitOptions.RemoveEmptyEntries);
                List<float> parameters = new List<float>();
                string transformType = null;
                for (int i = 0; i <= elements.Length; i++)
                {
                    float parameterValue;
                    if ((i < elements.Length) && float.TryParse(elements[i], out parameterValue))
                    {
                        parameters.Add(parameterValue);
                    }
                    else
                    {
                        if (transformType != null)
                        {
                            if (transformType == "translate")
                            {
                                Matrix3.CreateTranslation(t, parameters.Count > 0 ? parameters[0] : 0.0f, parameters.Count > 1 ? parameters[1] : 0.0f);
                                Matrix3.Multiply(transform, t, transform);
                            }

                            else if (transformType == "scale")
                            {
                                Matrix3.CreateScale(t, parameters.Count > 0 ? parameters[0] : 1.0f, parameters.Count > 1 ? parameters[1] : parameters.Count > 0 ? parameters[0] : 1.0f);
                                Matrix3.Multiply(transform, t, transform);
                            }

                            else if (transformType == "rotate")
                            {
                                float x = parameters.Count > 1 ? parameters[1] : 0.0f;
                                float y = parameters.Count > 2 ? parameters[2] : x;

                                Matrix3.CreateTranslation(t, x, y);
                                Matrix3.Multiply(transform, t, transform);

                                Matrix3.CreateRotation(t, (float)Math.PI * (parameters.Count > 0 ? parameters[0] : 0.0f) / 180.0f);
                                Matrix3.Multiply(transform, t, transform);

                                Matrix3.CreateTranslation(t, -x, -y);
                                Matrix3.Multiply(transform, t, transform);
                            }

                            else if (transformType == "skewx")
                            {
                                Matrix3.CreateSkewX(t, (float)Math.PI * (parameters.Count > 0 ? parameters[0] : 0.0f) / 180.0f);
                                Matrix3.Multiply(transform, t, transform);
                            }

                            else if (transformType == "skewy")
                            {
                                Matrix3.CreateSkewY(t, (float)Math.PI * (parameters.Count > 0 ? parameters[0] : 0.0f) / 180.0f);
                                Matrix3.Multiply(transform, t, transform);
                            }

                            else if (transformType == "matrix")
                            {
                                t.m11 = parameters.Count > 0 ? parameters[0] : 1.0f;
                                t.m12 = parameters.Count > 1 ? parameters[1] : 0.0f;
                                t.m21 = parameters.Count > 2 ? parameters[2] : 0.0f;
                                t.m22 = parameters.Count > 3 ? parameters[3] : 1.0f;
                                t.m31 = parameters.Count > 4 ? parameters[4] : 0.0f;
                                t.m32 = parameters.Count > 5 ? parameters[5] : 0.0f;
                                Matrix3.Multiply(transform, t, transform);
                            }

                        }

                        if (i < elements.Length)
                        {
                            parameters.Clear();
                            transformType = elements[i].ToLower();
                        }
                    }
                }
            }

            return transform;
        }

        public bool HasAttribute(string attributeName)
        {
            return this.attributes.ContainsKey(attributeName);
        }

        public T GetAttribute<T>(string attributeName, T defaultValue = default(T))
        {
            try
            {
                if (this.HasAttribute(attributeName))
                {
                    string value = this.attributes[attributeName];
                    value = value.Replace("mm", string.Empty).Replace("px", string.Empty);
                    return (T)Convert.ChangeType(value, typeof(T));
                }
            }
            catch
            {
            }

            return defaultValue;
        }
    }
}
