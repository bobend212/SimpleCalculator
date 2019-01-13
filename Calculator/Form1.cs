using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{

    /// <summary>
    /// Basic calculator C# WinForms
    /// </summary>
    /// 

    public partial class MainFrame : Form
    {
        #region Constructor

        public MainFrame()
        {
            InitializeComponent();
        }

        #endregion

        #region Clearing Methods

        private void DelButton_Click(object sender, EventArgs e)
        {
           
            DeleteTextValue();

            FocusInputText();
        }

        private void CEButton_Click(object sender, EventArgs e)
        {
            this.UserInputBox.Text = string.Empty;

            FocusInputText();
        }

        #endregion

        #region Operator Methods

        private void DivideButton_Click(object sender, EventArgs e)
        {
            InsertTextValue("/");
        }

        private void MultiplyButton_Click(object sender, EventArgs e)
        {
            InsertTextValue("*");
        }

        private void MinusButton_Click(object sender, EventArgs e)
        {
            InsertTextValue("-");
        }

        private void PlusButton_Click(object sender, EventArgs e)
        {
            InsertTextValue("+");
        }

        private void EqualButton_Click(object sender, EventArgs e)
        {
            CalculateEquation();
        }

        #endregion

        #region Number Methods

        private void DotButton_Click(object sender, EventArgs e)
        {
            InsertTextValue(".");
            FocusInputText();
        }

        private void Btn0_Click(object sender, EventArgs e)
        {
            InsertTextValue("0");
            FocusInputText();
        }

        private void Btn1_Click(object sender, EventArgs e)
        {
            InsertTextValue("1");
            FocusInputText();
        }

        private void Btn2_Click(object sender, EventArgs e)
        {
            InsertTextValue("2");
            FocusInputText();
        }

        private void Btn3_Click(object sender, EventArgs e)
        {
            InsertTextValue("3");
            FocusInputText();
        }

        private void Btn4_Click(object sender, EventArgs e)
        {
            InsertTextValue("4");
            FocusInputText();
        }

        private void Btn5_Click(object sender, EventArgs e)
        {
            InsertTextValue("5");
            FocusInputText();
        }

        private void Btn6_Click(object sender, EventArgs e)
        {
            InsertTextValue("6");
            FocusInputText();
        }

        private void Btn7_Click(object sender, EventArgs e)
        {
            InsertTextValue("7");
            FocusInputText();
        }

        private void Btn8_Click(object sender, EventArgs e)
        {
            InsertTextValue("8");
            FocusInputText();
        }

        private void Btn9_Click(object sender, EventArgs e)
        {
            InsertTextValue("9");
            FocusInputText();
        }
        #endregion

        #region Private Helpers

        private void CalculateEquation()
        {
           
            this.Label.Text = ParseOperation();
           
            FocusInputText();
        }

        private string ParseOperation()
        {
            try
            {
                var userInput = this.UserInputBox.Text;

                userInput = userInput.Replace(" ", "");

                var operation = new Operation();
                var leftSide = true;

                for (int i = 0; i < userInput.Length; i++)
                {
                    if ("0123456789.".Any(c => userInput[i] == c))
                    {
                        if (leftSide)
                            operation.LeftSide = AddNumberPart(operation.LeftSide, userInput[i]);
                        else
                            operation.RightSide = AddNumberPart(operation.RightSide, userInput[i]);
                    }
                    else if ("+-*/".Any(c => userInput[i] == c))
                    {
                        if (!leftSide)
                        {
                            var operatorType = GetOperationType(userInput[i]);
                        }
                        else
                        {
                            var operatorType = GetOperationType(userInput[i]);

                            if (operation.LeftSide.Length == 0)
                            {

                                if (operatorType != OperationType.Minus)
                                    throw new InvalidOperationException($"Operator (+ * / or more than one -) specified without any left side number");
                            
                                operation.LeftSide += userInput[i];   
                            }
                            else
                            {
                                operation.OperationType = operatorType;
                                leftSide = false;
                            }
                        }
                    }
                }

                return CalculateOperation(operation);
            }
            catch (Exception ex)
            {
                return $"Invalid equation. {ex.Message}";
            }
        }

        private string CalculateOperation(Operation operation)
        {
            double left = 0;
            double right = 0;

            if (string.IsNullOrEmpty(operation.LeftSide) || !double.TryParse(operation.LeftSide, out left))
                throw new InvalidOperationException($"Left side of the operation was not a number. {operation.LeftSide}");

            if (string.IsNullOrEmpty(operation.RightSide) || !double.TryParse(operation.RightSide, out right))
                throw new InvalidOperationException($"Right side of the operation was not a number. {operation.RightSide}");

            try
            {
                switch(operation.OperationType)
                {
                    case OperationType.Add:
                        return (left + right).ToString();

                }
            }
            catch (Exception ex)
            {

            }

            return string.Empty;
        }

        private OperationType GetOperationType(char character)
        {
            switch(character)
            {
                case '+':
                    return OperationType.Add;
                case '-':
                    return OperationType.Minus;
                case '/':
                    return OperationType.Multiply;
                case '*':
                    return OperationType.Divide;
                default:
                    throw new InvalidOperationException($"Unknown operator type { character }");
            }
        }

        private string AddNumberPart(string currentNumber, char newCharacter)
        {
           if (newCharacter == '.' && currentNumber.Contains('.'))  
                throw new InvalidOperationException($"Number {currentNumber} already contains a . and another cannot be added");


            return currentNumber + newCharacter;
            
        }

        private void FocusInputText()
        {
            this.UserInputBox.Focus();
        }

        private void InsertTextValue(string value)
        {
            var selectionStart = this.UserInputBox.SelectionStart;

            this.UserInputBox.Text = this.UserInputBox.Text.Insert(this.UserInputBox.SelectionStart, value);

            this.UserInputBox.SelectionStart = selectionStart + value.Length;

            this.UserInputBox.SelectionLength = 0;
        }

        private void DeleteTextValue()
        {
            if (this.UserInputBox.Text.Length < this.UserInputBox.SelectionStart + 1)
                return;

            var selectionStart = this.UserInputBox.SelectionStart;

            this.UserInputBox.Text = this.UserInputBox.Text.Remove(this.UserInputBox.SelectionStart, 1);

            this.UserInputBox.SelectionStart = selectionStart;

            this.UserInputBox.SelectionLength = 0;
        }

        private void MainFrame_Load(object sender, EventArgs e)
        {
            Label.BackColor = Color.Transparent;
        }

        #endregion

    }
}
