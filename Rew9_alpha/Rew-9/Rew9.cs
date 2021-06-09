using System;
using System.Windows.Forms;

using OxyPlot;
using OxyPlot.Series;

using Mathos.Parser;

namespace rew
{
    public partial class Rew9 : Form
    {
        private MathParser call;

        private string mathExpression;
        private (double min, double max) OX;

        static int step_count = 1;
        static int success_count = 1;
        static int error_count = 1;
        static int warning_count = 1;
        public Rew9()
        {
            InitializeComponent();
            
            call = new MathParser();
        }

        private void showLogSuccess(string success)
        {
            log.Text += $"\n[step {step_count}][success {success_count}]: {success}";
            ++success_count;
        }
        private void showLogError(string error)
        {
            log.Text += $"\n[step {step_count}][error {error_count}]: {error}";
            ++error_count;
        }
        private void showLogWarning(string warning)
        {
            log.Text += $"\n[step {step_count}][warning {warning_count}]: {warning}";
            ++warning_count;
        }

        private string getMathExpression()
        {
            return functionField.Text.Length == 0 ? functionField.PlaceholderText : functionField.Text;
        }
        private OxyColor getColor()
        {
            switch (ColorsCollection.Text)
            {
                case "Red": return OxyColors.Red;
                case "Orange": return OxyColors.Orange;
                case "Yellow": return OxyColors.Yellow;
                case "Green": return OxyColors.Green;
                case "Blue": return OxyColors.Blue;
                case "Violet": return OxyColors.Violet;
                case "White": return OxyColors.White;
                default: return OxyColors.Black;
            }
        }

        private LineSeries getLineSeries((double min, double max) OX, double accuracy)
        {
            LineSeries line = new LineSeries() { Title = $"f(x) = {mathExpression}", MarkerStroke = OxyColors.Black };

            int beg = (int)(OX.min / accuracy);
            int end = (int)(OX.max / accuracy);

            for (int i = beg; i < end; ++i)
            {
                call.LocalVariables.Add("x", Math.Round(i * accuracy, 3));

                //Probably throw exception
                line.Points.Add(new DataPoint(i * accuracy, call.Parse(mathExpression)));

                call.LocalVariables.Remove("x");
            }

            line.Color = getColor();
            
            return line;
        }

        private void functionCreateButton_Click(object sender, EventArgs e)
        {
            PlotModel myModel = new PlotModel();

            mathExpression = getMathExpression().Replace(",", ".");

            try
            {
                OXmin.Text = OXmin.Text.Replace(".", ",");
                OXmax.Text = OXmax.Text.Replace(".", ",");

                OX.min = Convert.ToDouble(OXmin.Text.Length == 0 ? OXmin.PlaceholderText : OXmin.Text);
                OX.max = Convert.ToDouble(OXmax.Text.Length == 0 ? OXmax.PlaceholderText : OXmax.Text);
                
                if (OX.min > OX.max)
                {
                    (OX.min, OX.max) = (OX.max, OX.min);
                    OXmin.Text = OX.min.ToString();
                    OXmax.Text = OX.max.ToString();
                }
            }
            catch
            {
                showLogWarning("Input string was not in a correct format.");

                OXmin.Text = OXmin.PlaceholderText;
                OXmax.Text = OXmax.PlaceholderText;

                OX.min = Convert.ToDouble(OXmin.PlaceholderText);
                OX.max = Convert.ToDouble(OXmax.PlaceholderText);
            }
           
            try
            {
                myModel.Series.Add(getLineSeries(OX, 0.01));
                showLogSuccess($"f(x) = {mathExpression} within ({OX.min}, {OX.max}) has created.");
                plot.Model = myModel;
            }
            catch (MathParserException error)
            {
                showLogError(error.Message);
                call.LocalVariables.Remove("x");
            }
            catch (ArithmeticException warning)
            {
                showLogWarning(warning.Message);
                call.LocalVariables.Remove("x");
            }
            ++step_count;
        }
    }
}