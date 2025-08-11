using Palitri.OpenCNC.Script.Utils;
using Palitri.OpenCNC.Script;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCNC.App.Rendering
{
    public class ScriptHintRenderer
    {
        private Bitmap hintBitmap;
        private Graphics hintBitmapGraphics;
        private Color colorHintFunction = Color.FromArgb(1, 144, 52);
        private Color colorHintFunctionParam = Color.FromArgb(0, 93, 147);
        private Color colorHintFunctionParamHighlight = Color.FromArgb(169, 235, 136);
        private Color colorHintWarning = Color.FromArgb(181, 115, 0);
        private Color colorHintError = Color.FromArgb(192, 14, 0);
        private Font fontHintFunction = new Font("Courier New", 13.0f, FontStyle.Bold);
        private Font fontHintFunctionUnderscore = new Font("Courier New", 7.0f, FontStyle.Bold);
        private Font fontHintWarning = new Font("Courier New", 9.0f, FontStyle.Bold);

        public Bitmap Bitmap { get { return this.hintBitmap; } }

        public ScriptHintRenderer(int width, int height)
        {
            this.hintBitmap = new Bitmap(width, height);
            this.hintBitmapGraphics = Graphics.FromImage(this.hintBitmap);
        }

        public void Render(CNCScriptEngine scriptEngine, string inputCommand, int caretPosition)
        {
            const int offset = 5;
            const int highlightSizeHorizontal = 0;
            const int highlightSizeVertical = 2;
            const int tokenSeparation = 4;

            this.hintBitmapGraphics.FillRectangle(new LinearGradientBrush(new Point(0, 0), new Point(0, this.hintBitmap.Height), Color.FromArgb(224, 225, 234), Color.FromArgb(172, 173, 181)), new Rectangle(0, 0, this.hintBitmap.Width, this.hintBitmap.Height));

            inputCommand = inputCommand.Trim();
            Tuple<string, int>[] paramsDetails = ScriptUtils.SplitParamsWithDetails(inputCommand);
            ICNCScriptCommand command = paramsDetails.Length == 0 ? null : scriptEngine.commands.FirstOrDefault(c => c.Name.Equals(paramsDetails[0].Item1, StringComparison.OrdinalIgnoreCase));
            if (command != null)
            {
                int x = offset;
                int y = offset;
                int inputParamIndex = 1;
                int paramsIteration = 1;

                this.hintBitmapGraphics.DrawString(command.Name, this.fontHintFunction, new SolidBrush(this.colorHintFunction), new Point(x, y));
                x += (int)this.hintBitmapGraphics.MeasureString(command.Name, this.fontHintFunction).Width;
                x += tokenSeparation * 2;

                do
                {
                    for (int i = 0; i < command.Parameters.Count; i++)
                    {
                        string commandParam = command.Parameters[i];
                        int paramNameWidth = (int)this.hintBitmapGraphics.MeasureString(commandParam, this.fontHintFunction).Width;
                        int paramUnderscoreWidth = command.InfiniteParameters ? (int)this.hintBitmapGraphics.MeasureString(paramsIteration.ToString(), this.fontHintFunctionUnderscore).Width : 0;
                        if (inputParamIndex < paramsDetails.Length)
                        {
                            Tuple<string, int> inputParamDetails = paramsDetails[inputParamIndex];
                            bool isCurrentParam = (caretPosition >= inputParamDetails.Item2) && (caretPosition <= inputParamDetails.Item2 + inputParamDetails.Item1.Length);

                            if (isCurrentParam)
                                this.hintBitmapGraphics.FillRectangle(new SolidBrush(this.colorHintFunctionParamHighlight), new Rectangle(x - highlightSizeHorizontal, y - highlightSizeVertical, paramNameWidth + paramUnderscoreWidth + highlightSizeHorizontal * 2, this.fontHintFunction.Height + highlightSizeVertical * 2));
                        }

                        this.hintBitmapGraphics.DrawString(commandParam, this.fontHintFunction, new SolidBrush(this.colorHintFunctionParam), new Point(x, y));
                        x += paramNameWidth;

                        if (command.InfiniteParameters)
                        {
                            this.hintBitmapGraphics.DrawString(paramsIteration.ToString(), this.fontHintFunctionUnderscore, new SolidBrush(this.colorHintFunctionParam), new PointF(x, y + this.fontHintFunction.Height * 0.5f));
                            x += paramUnderscoreWidth;
                        }

                        x += tokenSeparation;

                        inputParamIndex++;
                    }

                    paramsIteration++;
                }
                while (command.InfiniteParameters && (inputParamIndex < paramsDetails.Length));



                CNCScriptCommandResult scriptResult = command.Execute(null, inputCommand);
                int warningHeight = (int)this.hintBitmapGraphics.MeasureString(scriptResult.Text, this.fontHintWarning).Height;
                if (scriptResult.ResultType == CNCScriptCommandResultType.Warning)
                    this.hintBitmapGraphics.DrawString(scriptResult.Text, this.fontHintWarning, new SolidBrush(this.colorHintWarning), new PointF(offset, this.hintBitmap.Height - warningHeight - offset));
                else if (scriptResult.ResultType == CNCScriptCommandResultType.Error)
                    this.hintBitmapGraphics.DrawString(scriptResult.Text, this.fontHintWarning, new SolidBrush(this.colorHintError), new PointF(offset, this.hintBitmap.Height - warningHeight - offset));
            }
        }
    }
}
