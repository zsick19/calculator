using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorSolution
{
    public struct OperationChunk
    {
        public List<string> EquationStrings { get; set; }
        public string SolvedString {  get; set; }
        public double SolvedValue { get; set; }
        public string ErrorMessage {  get; set; }
    }

    interface IMathOperations
    {
        public double AddNumbers(double num1,double num2);
        public double SubtractNumbers(double num1, double num2);
        public double MultiplyNumbers(double num1, double num2);
        public double DivideNumbers(double num1, double num2);
    }

    internal class MathOperations : IMathOperations
    {
        OperationChunk chunk;
        private double solvedValue;
        private List<double> Numbers=new List<double>();
        private List<string> Operations=new List<string>();



        public MathOperations(OperationChunk chunk)
        {
            this.chunk = chunk;
            ProceedThroughEquation();          
        }

        public void ProceedThroughEquation()
        {
            double num;
            if (chunk.EquationStrings.Count > 1)
            {
                foreach (var item in chunk.EquationStrings)
                {
                    if (!double.TryParse(item, out num))
                    {
                        Operations.Add(item);
                    }
                    else
                    {
                        double.TryParse(item, out num);
                        Numbers.Add(num);
                    }
                }

                for (var i = 0; i < Operations.Count; i++)
                {
                    switch (Operations[i])
                    {
                        case "+":
                            solvedValue = AddNumbers(Numbers[i], Numbers[i + 1]);
                            Numbers[i + 1] = solvedValue;
                            break;
                        case "-":
                            solvedValue = SubtractNumbers(Numbers[i], Numbers[i + 1]);
                            break;
                        case "*":
                            solvedValue = MultiplyNumbers(Numbers[i], Numbers[i + 1]);
                            break;
                        case "/":
                            solvedValue = DivideNumbers(Numbers[i], Numbers[i + 1]);
                            break;
                    }
                }
                chunk.SolvedValue = solvedValue;
                chunk.EquationStrings.Add($"={solvedValue.ToString()}");
            }
            else
            {                
                chunk.SolvedValue=double.Parse(chunk.EquationStrings[0]);
                chunk.EquationStrings.Add($"={chunk.EquationStrings[0]}");
            }
        }

        public string CompleteEquationString()
        {

            string completeString = String.Empty;
            foreach (var s in chunk.EquationStrings)
            {
                completeString = $"{completeString}{s}";       
            }
            return completeString;
        }

        public double AddNumbers(double num1,double num2)
        {
            return num1 + num2;
        }

        public double DivideNumbers(double num1, double num2)
        {
           return num1 / num2;
        }

        public double MultiplyNumbers(double num1, double num2)
        {
            return (num1 * num2);
        }

        public double SubtractNumbers(double num1, double num2)
        {
            return num1 - num2;
        }
    }

    internal class Utility
    {    
        public static Label MainDisplay;

        public static StringBuilder NumberStringBuilder = null;
        public static List<string> FullSetOfNumbersAndOperators = new List<string>();
        public static string operation = null;

        public static List<OperationChunk> History= new List<OperationChunk>();
                   


        public static void UpdateMainDisplay()
        {
            string mainPrompt = String.Empty;

            foreach (var s in FullSetOfNumbersAndOperators)
            {
                mainPrompt = $"{mainPrompt}{s}";
            }

            MainDisplay.Text = $"{mainPrompt}{NumberStringBuilder?.ToString()}";
        }
        public static void CalculateMainDisplay(OperationChunk chunk)
        {
            string mainSolved =String.Empty;
            foreach (var s in chunk.EquationStrings)
            {
                mainSolved = $"{mainSolved}{s}";
            }
            
            MainDisplay.Text = mainSolved ;
        }

        public static Dictionary<string, OperationChunk> Memory = new Dictionary<string, OperationChunk>();




    }
}
