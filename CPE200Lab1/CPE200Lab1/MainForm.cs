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
        private bool isAfterOperater = true;
        private bool isAfterOperater2 = true;
        private bool isAfterEqual;
        private string firstOperand;
        private string operate;
        private string swicthOperate;
        private string operate2;
        private string operate3;
        private string RootandOneoverXoperate;
        private string M_Operate;
        private string secoundOperand;
        private bool checkEqual = false;
        private bool checkValue = false;
        private string Savenum;
        private double Msnumber;


        private CalCulatorEngine engine;

       private void resetAll()
        {
            operate = null;
            swicthOperate = null;
            secoundOperand = "0";
            firstOperand = null;
            lblDisplay.Text = "0";
            textBox1.Text = "";
            isAllowBack = true;
            hasDot = false;
            isAfterOperater = false;
            isAfterEqual = false;
            checkEqual = false;
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
            checkValue = true;
        }

        private void btnOperator_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text is "Error")
            {
                return;
            }

            if(checkValue == false)
            {
                operate = ((Button)sender).Text;
                lblDisplay.Text = "OK1";
                return;
            }
            else
            {
                operate = ((Button)sender).Text;
                
            }
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
                        if (swicthOperate != operate)
                        {
                            firstOperand = engine.Calculate(swicthOperate, operate2, firstOperand, lblDisplay.Text);
                            lblDisplay.Text =firstOperand;
                            secoundOperand = lblDisplay.Text;
                            swicthOperate = operate;
                            isAfterOperater = true;
                            return;

                        }
                        else
                        {
                            secoundOperand = lblDisplay.Text;
                        }
 
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
                    swicthOperate = operate;
                    break;
                case "%":
                    operate2 = operate3;
                    textBox1.Text = operate + " " + operate2;
                    if (firstOperand != null)
                    {
                        if(isAfterOperater == true)
                        {
                            return;
                        }
                        
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
                    isAfterOperater = true;
                    //isAfterOperater2 = true;

                    // your code here
                    break;
            }
            isAllowBack = false;
            checkValue = false;
            
        }

        private void btnMixfunction_Click(object sender, EventArgs e)
        {
            RootandOneoverXoperate = ((Button)sender).Text;
            switch (RootandOneoverXoperate)
            {
                case "√" :
                    double Root = (Double)Math.Sqrt(Convert.ToDouble(lblDisplay.Text));
                    lblDisplay.Text = Root.ToString();
                    if (lblDisplay.Text.Length > 8)
                    {
                        string rootLength = lblDisplay.Text;
                        lblDisplay.Text = rootLength.Substring(0,8);
                    }
                    break;
                case "1/X":
                    double OneOverX = (Convert.ToDouble(lblDisplay.Text));
                    lblDisplay.Text = (1/OneOverX).ToString();
                    if (lblDisplay.Text.Length > 8)
                    {
                        string OneoverXLength = lblDisplay.Text;
                        lblDisplay.Text = OneoverXLength.Substring(0,8);
                    }
                    break;
            }
        }
        private void btn_MOPerate_Click(object sender, EventArgs e)
        {
            M_Operate = ((Button)sender).Text;
            
            switch (M_Operate)
            {
                case "MS":
                    Msnumber = (Convert.ToDouble(lblDisplay.Text));
                    Savenum = Msnumber.ToString();
                    break;
                case "M-":
                    Savenum = (Msnumber - (Convert.ToDouble(lblDisplay.Text))).ToString();
                    break;
                case "M+":

                    Savenum = (Msnumber + (Convert.ToDouble(lblDisplay.Text))).ToString();

                    break;
                case "MR":
                    lblDisplay.Text = Savenum;
                    break;
                case "MC":
                    Savenum = null;
                    Msnumber = 0;


                    break;
            }
            isAfterOperater = true;
        }
        private void btnEqual_Click(object sender, EventArgs e)
        {
            
            if (lblDisplay.Text is "Error"|| RootandOneoverXoperate == "√")
            {
                return;
            }
            string secondOperand = lblDisplay.Text;

            //textBox1.Text = firstOperand + " " + secondOperand;
            string result = engine.Calculate(operate,operate2, firstOperand,secondOperand);
            
            if (result is "E" || result.Length > 8)
            {
                lblDisplay.Text = "Error";
            }
            else
            {
                lblDisplay.Text = result;
            }
            //lblDisplay.Text = result;
            isAfterEqual = true;
            checkEqual = true;
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
            if (lblDisplay.Text == "0")
            {
                return;
            }
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
