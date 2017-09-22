using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPE200Lab1
{
    public class RPNCalculatorEngine : CalculatorEngine
    {
       
        public new string Process(string str)
        {
            
            
            if(str == "")
            {
                return "E";
            }else if(str == null){
                return "E";
            }
            Stack<string> rpnStack = new Stack<string>();
            List<string> parts;
            try
            {
                parts = str.Split(' ').ToList<string>();
            }
            catch
            {
                return "E";
            }
            if(parts.Count < 3)
            {
                if (parts.Count == 1 && parts[0] == "0")
                {
                    return "0";
                }
                return "E";
            }
            string result;
            string firstOperand, secondOperand;

            foreach (string token in parts)
            {
                if (isNumber(token))
                {
                    rpnStack.Push(token);
                }
                else if (isOperator(token))
                {
                    //FIXME, what if there is only one left in stack?
                    try
                    {
                        secondOperand = rpnStack.Pop();
                        firstOperand = rpnStack.Pop();
                    }
                    catch
                    {
                        return "E";
                    }
                    
                    result = calculate(token, firstOperand, secondOperand, 6);
                    if (result is "E")
                    {
                        return result;
                    }
                    rpnStack.Push(result);
                }
                else
                {
                    if (token.Length > 1)
                    {
                        
                        return "E";
                    }
                }
            }
            //FIXME, what if there is more than one, or zero, items in the stack?
            
            result = rpnStack.Pop();
            if (rpnStack.Count != 0)
            {
                return "E";
            }
            double convert = Convert.ToDouble(result);
            

            return convert.ToString();
        }
    }
}
