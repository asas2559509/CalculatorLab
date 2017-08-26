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
    public partial class MainForm : Form
    {
        private bool hasDot;
        private bool isAllowBack;
        private bool isAfterOperater;
        private bool isAfterOperater2 = true;
        private bool isAfterEqual;
        private string firstOperand;
        private string operate;
        private string operate2;
        private string operate3;
        private string secoundOperand;
        private CalCulatorEngine engine;

       private void resetAll()
        {
            operate = "\0";
            secoundOperand = "0";
            firstOperand = null;
            lblDisplay.Text = "0";
            textBox1.Text = "";
            isAllowBack = true;
            hasDot = false;
            isAfterOperater = false;
            isAfterEqual = false;
        }



        public MainForm()
        {
            InitializeComponent();
            engine = new CalCulatorEngine();
            resetAll();
        }

        private void btnNumber_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            if (isAfterEqual)
            {
                resetAll();
            }
            if (isAfterOperater)
            {
                lblDisplay.Text = "0";
            }
            if(lblDisplay.Text.Length is 8)
            {
                return;
            }
            isAllowBack = true;
            string digit = ((Button)sender).Text;
            if(lblDisplay.Text is "0")
            {
                lblDisplay.Text = "";
            }
            lblDisplay.Text += digit;
            isAfterOperater = false;
        }

        private void btnOperator_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            if (isAfterOperater)
            {
                return;

            }
            operate = ((Button)sender).Text;
            if(isAfterOperater2)
            {
                operate3 = operate;
                isAfterOperater2 = false;

            }
            textBox1.Text = operate + " "+ operate3;
            switch (operate)
            {

                case "+":
                case "-":
                case "X":
                case "÷":
                    if(firstOperand != null)
                    {
                        secoundOperand = lblDisplay.Text;
                        string result = engine.Calculate(operate,operate2, firstOperand, secoundOperand);
                        if (result is "E" || result.Length > 8)
                        {
                            lblDisplay.Text = "Error";
                        }
                        else
                        {
                            lblDisplay.Text = result;
                        }

                    }
                    firstOperand = lblDisplay.Text;
                    isAfterOperater = true;
                    break;
                case "%":
                    operate2 = operate3;
                    textBox1.Text = operate + " " + operate2;
                    if (firstOperand != null)
                    {
                        secoundOperand = lblDisplay.Text;
                        string result = engine.Calculate(operate,operate2, firstOperand, secoundOperand);
                        if (result is "E" || result.Length > 8)
                        {
                            lblDisplay.Text = "Error";
                        }
                        else
                        {
                            lblDisplay.Text = result;
                        }
                    }
                    isAfterOperater = false;
                    isAfterOperater2 = true;

                    // your code here
                    break;
            }
            isAllowBack = false;
        }

        private void btnEqual_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            string secondOperand = lblDisplay.Text;
            string result = engine.Calculate(operate,operate2, firstOperand, secondOperand);
            if (result is "E" || result.Length > 8)
            {
                lblDisplay.Text = "Error";
            }
            else
            {
                lblDisplay.Text = result;
            }
            lblDisplay.Text = result;
            isAfterEqual = true;
        }

        private void btnDot_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            if (isAfterEqual)
            {
                resetAll();
            }
            if (lblDisplay.Text.Length is 8)
            {
                return;
            }
            if (!hasDot)
            {
                lblDisplay.Text += ".";
                hasDot = true;
            }
        }

        private void btnSign_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            if (isAfterEqual)
            {
                resetAll();
            }
            // already contain negative sign
            if (lblDisplay.Text.Length is 8)
            {
                return;
            }
            if(lblDisplay.Text[0] is '-')
            {
                lblDisplay.Text = lblDisplay.Text.Substring(1, lblDisplay.Text.Length - 1);
            } else
            {
                lblDisplay.Text = "-" + lblDisplay.Text;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            resetAll();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            if (isAfterEqual)
            {
                return;
            }
            if (!isAllowBack)
            {
                return;
            }
            if(lblDisplay.Text != "0")
            {
                string current = lblDisplay.Text;
                char rightMost = current[current.Length - 1];
                if(rightMost is '.')
                {
                    hasDot = false;
                }
                lblDisplay.Text = current.Substring(0, current.Length - 1);
                if(lblDisplay.Text is "" || lblDisplay.Text is "-")
                {
                    lblDisplay.Text = "0";
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
