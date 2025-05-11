using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace calculator.Pages
{
    [IgnoreAntiforgeryToken]
    public class IndexModel : PageModel
    {
        public string finalNumber { get; set; } = "";
        public string operationMember { get; set; } = "";
        public double firstNumber { get; set; } = 0;
        public double secondNumber { get; set; } = 0;
        public bool firstNumberActive { get; set; } = true;

        public void OnPost(double numberOnDisplayFirst, double numberOnDisplaySecond, string operation, bool firstActive)
        {
            firstNumber = numberOnDisplayFirst;
            secondNumber = numberOnDisplaySecond;
            firstNumberActive = firstActive;
            if (TempData.ContainsKey("Operation"))
            {
                operationMember = (string)TempData.Peek("Operation");
            }


            switch (operation)
            {
                case "%":
                case "/":
                case "*":
                case "+":
                case "-":
                    operationMember = operation;
                    TempData["Operation"] = operation;
                    firstNumberActive = false;
                    break;

                case "CE":
                    if (firstNumberActive)
                    {
                        firstNumber = 0;
                    }
                    else
                    {
                        secondNumber = 0;
                    }
                    break;

                case "C":
                    firstNumber = 0;
                    secondNumber = 0;
                    firstNumberActive = true;
                    break;

                case "Del":
                    if (firstNumberActive)
                    {
                        firstNumber = (double)Math.Floor(numberOnDisplayFirst / 10);
                    }
                    else
                    {
                        secondNumber = (double)Math.Floor(numberOnDisplaySecond / 10);
                    }
                    break;

                case "+/-":
                    if (firstNumberActive)
                    {
                        firstNumber = numberOnDisplayFirst * -1;
                    }
                    else
                    {
                        secondNumber = numberOnDisplaySecond * -1;
                    }
                    break;

                case "1/x":
                    if (numberOnDisplayFirst == 0)
                    {
                        finalNumber = "Ошибка! Делить на 0 нельзя!";
                        break;
                    }
                    secondNumber = 0;
                    finalNumber = $"{1.0 / numberOnDisplayFirst}";
                    break;

                case "x^2":
                    finalNumber = $"{Math.Pow(numberOnDisplayFirst, 2)}";
                    secondNumber = 0;
                    break;

                case "sqrt(x)":
                    if (numberOnDisplayFirst < 0)
                    {
                        finalNumber = "Ошибка! Число должно быть положительное!";
                        break;
                    }
                    secondNumber = 0;
                    finalNumber = $"{Math.Sqrt(numberOnDisplayFirst)}";
                    break;

                case "=":
                    operationMember = (string)TempData["Operation"];
                    firstNumberActive = true;
                    if (string.IsNullOrEmpty(operationMember))
                    {
                        finalNumber = "Ошибка! Операция не выбрана!";
                        break;
                    }
                    switch (operationMember)
                    {
                        case "%":
                            if (numberOnDisplaySecond == 0)
                            {
                                finalNumber = "Ошибка! Делить на 0 нельзя!";
                                break;
                            }
                            finalNumber = $"{numberOnDisplayFirst % numberOnDisplaySecond}"; break;
                        case "/":
                            if (numberOnDisplaySecond == 0)
                            {
                                finalNumber = "Ошибка! Делить на 0 нельзя!";
                                break;
                            }
                            finalNumber = $"{numberOnDisplayFirst / numberOnDisplaySecond}"; break;
                        case "*": finalNumber = $"{numberOnDisplayFirst * numberOnDisplaySecond}"; break;
                        case "+": finalNumber = $"{numberOnDisplayFirst + numberOnDisplaySecond}"; break;
                        case "-": finalNumber = $"{numberOnDisplayFirst - numberOnDisplaySecond}"; break;
                    }
                    break;

                default:
                    if (double.TryParse(operation, out _))
                    {
                        if (firstNumberActive)
                        {
                            firstNumber = double.Parse(numberOnDisplayFirst.ToString() + operation);
                        }
                        else
                        {
                            secondNumber = double.Parse(numberOnDisplaySecond.ToString() + operation);
                        }
                    }
                    break;
            }
        }
    }
}