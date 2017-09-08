using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPE200Lab1
{   //ของหลักมาจาก CalculatorEngine
    class RPNCalculatorEngine : CalculatorEngine
    {
        public string Process(string str)
        {
            string result;
            //Split input string to multiple parts by space
            List<string> parts = str.Split(' ').ToList<string>();
            Stack<string> stack = new Stack<string>();
            for(int i = 0; i < parts.Count; i++)
            {
                if(parts[i]=="+"|| parts[i] == "-" || parts[i] == "X" || parts[i] == "÷" )
                {
                    string secondOperand = stack.Pop().ToString();
                    string firstOperand = stack.Pop().ToString();
                    result = calculate(parts[i], firstOperand, secondOperand, 4);
                    stack.Push(result);
                }
                else
                {
                    stack.Push(parts[i]);
                }
            }
            //stack.Push(10);
            return stack.Pop().ToString();
  
        }
    }
}
