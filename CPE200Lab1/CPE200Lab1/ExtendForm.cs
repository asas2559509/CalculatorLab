using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CPE200Lab1
{
    public partial class ExtendForm : Form
    {
        private bool isNumberPart = false;
        private bool isContainDot = false;
        private bool checkOperand = false;
        private int checkturn = 0;

        private CalculatorEngine engine;
        public ExtendForm()
        {
            InitializeComponent();
            engine = new CalculatorEngine();
        }

        private string getLastInString(string str)
        {
            if (str.Length is 1)
                return str;
            return str.Substring(str.Length - 1);
        }

        private void btnNumber_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            if (lblDisplay.Text is "0")
            {
                lblDisplay.Text = "";
            }
            if (!isNumberPart)
            {
                isNumberPart = true;
                isContainDot = false;
            }
            lblDisplay.Text += ((Button)sender).Text;
            checkOperand = false;
        }

        private void btnBinaryOperator_Click(object sender, EventArgs e)
        {
            if(lblDisplay.Text is "0")
            {
                return;
            }
            if(checkOperand == true)
            {
                string[] parts = lblDisplay.Text.Split(' ');
                string[] parts2 = new string[parts.Length];
                for (int i = 0; i <= parts.Length-3; i++)
                {
                    parts2[i] = parts[i];
                }
                parts2[parts.Length - 2] = ((Button)sender).Text;
                lblDisplay.Text =  string.Join(" ", parts2);
                return;
            }

            if (lblDisplay.Text is "Error")
            {
                return;
            }
            isNumberPart = false;
            isContainDot = false;
            lblDisplay.Text += " " + ((Button)sender).Text + " ";
            checkturn++;
            checkOperand = true;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            // check if the last one is operator
            string current = lblDisplay.Text;
            if (getLastInString(current) is " ")
            {
                lblDisplay.Text = current.Substring(0, current.Length - 3);
            } else
            {
                lblDisplay.Text = current.Substring(0, current.Length - 1);
            }
            if (lblDisplay.Text is "")
            {
                lblDisplay.Text = "0";
            }
            //new
            
            string checkNumAndOperand = lblDisplay.Text;
            int numlast = checkNumAndOperand.Length-1;
            if (checkNumAndOperand[numlast] == ' '|| checkNumAndOperand.Length == '+' || checkNumAndOperand.Length == '-' || checkNumAndOperand.Length == 'X' || checkNumAndOperand.Length == '÷')
            {
                isNumberPart = false;
            }
            else
            {
            isNumberPart = true;
            }
            
            checkOperand = false;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            lblDisplay.Text = "0";
            checkOperand = false;
            isContainDot = false;
            isNumberPart = false;
            checkturn = 0;
        }

        private void btnEqual_Click(object sender, EventArgs e)
        {
           if(checkturn == 0)
            {
                return;
            } 
            string result = engine.Process(lblDisplay.Text);
            if (result is "E")
            {
                lblDisplay.Text = "Error";
            } else
            {
                lblDisplay.Text = result;
            }
            checkturn = 0;
        }

        private void btnSign_Click(object sender, EventArgs e)
        {
            
            if (lblDisplay.Text is "Error"|| isNumberPart is false)
            {
                return;
            }
            string[] parts = lblDisplay.Text.Split(' ');
            string lastpoint = parts[parts.Length-1];
            lblDisplay.Text = lastpoint;
            if(lastpoint[0] == '-')
            {
                lastpoint = lastpoint.Substring(1,lastpoint.Length-1);
                parts[parts.Length - 1] = lastpoint;
            }
            else
            {
                parts[parts.Length - 1] = "-" + parts[parts.Length - 1];
            }
            lblDisplay.Text = string.Join(" ", parts);

            /*
            string current = lblDisplay.Text;
            if (current is "0")
            {
                lblDisplay.Text = "-";
            } else if (getLastInString(current) is "-")
            {
                lblDisplay.Text = current.Substring(0, current.Length - 1);
                if (lblDisplay.Text is "")
                {
                    lblDisplay.Text = "0";
                }
            } else
            {
                lblDisplay.Text = current + "-";
            }
            */
        }

        private void btnDot_Click(object sender, EventArgs e)
        {
            if(!isContainDot)
            {
                isContainDot = true;
                lblDisplay.Text += ".";
            }
        }

        private void btnPercent_Click(object sender, EventArgs e)
        {
            string result = engine.calculate_persent(lblDisplay.Text);
            if (result is "E")
            {
                lblDisplay.Text = "Error";
            }
            else
            {
                lblDisplay.Text = result;
            }
        }
    }
}
