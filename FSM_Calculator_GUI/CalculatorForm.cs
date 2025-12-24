using System;
using System.Windows.Forms;
using FSMCalculator;

namespace FSMCalculatorApp
{
    public class CalculatorForm : Form
    {
        private TextBox displayBox;
        private CalculatorBackend backend;

        public CalculatorForm()
        {
            
            this.Text = "Your ZEYADIVS's Fav FSM Calculator!";

            this.Width = 400;
            this.Height = 500;
            this.StartPosition = FormStartPosition.CenterScreen;

            backend = new CalculatorBackend();

            displayBox = new TextBox
            {
                Font = new System.Drawing.Font("Consolas", 24),
                ReadOnly = true,
                Text = "0",
                Dock = DockStyle.Top,
                TextAlign = HorizontalAlignment.Right,
                Height = 70
            };
            this.Controls.Add(displayBox);

            var buttons = new[,] {
                { "7","8","9","/" },
                { "4","5","6","*" },
                { "1","2","3","-" },
                { "0","C","=","+" }
            };

            var panel = new TableLayoutPanel { RowCount = 4, ColumnCount = 4, Dock = DockStyle.Fill, Padding = new Padding(5) };
            for (int r = 0; r < 4; r++)
            {
                panel.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
                for (int c = 0; c < 4; c++)
                {
                    if (r == 0) panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));

                    var btn = new Button { Text = buttons[r, c], Font = new System.Drawing.Font("Consolas", 18), Dock = DockStyle.Fill, Margin = new Padding(2) };
                    btn.Click += ButtonClick;
                    panel.Controls.Add(btn, c, r);
                }
            }

            this.Controls.Add(panel);
        }

        private void ButtonClick(object sender, EventArgs e)
        {
            var btn = sender as Button;
            string text = btn.Text;

            if (text == "C")
                backend.Clear();
            else if (text == "=")
                backend.CalculateResult();
            else
                backend.HandleInput(text);

            displayBox.Text = backend.GetDisplay();
        }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.Run(new CalculatorForm());
        }
    }
}
