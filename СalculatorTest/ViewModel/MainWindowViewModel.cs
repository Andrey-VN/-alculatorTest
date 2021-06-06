using System;
using System.Windows;
using System.Windows.Input;
using СalculatorTest.Infrastructure;

namespace СalculatorTest.ViewModel
{
    internal class MainWindowViewModel : ViewModelBase
    {

        ReciverOfOperation reciver;

        private string inputBox = "0";

        public string InputBox
        {
            get => inputBox;
            set
            {
                Set(ref inputBox, value);
            }
        }
        public ICommand CloseApplicationCommand { get; }
        public ICommand Sum { get; }
        public ICommand Equally { get; }       
        public ICommand Dif { get; }
        public ICommand Div { get; }
        public ICommand Multi { get; }
        public ICommand Number { get; }
        public ICommand Comma { get; }
        public ICommand Reset { get; }
        public ICommand PlusOrMinus { get; }
        public ICommand Percent { get; }

        #region Команды
        #region Закрыть окно
        private void OnCloseApplicationCommandExecuted(object parameter)
        {
            Application.Current.Shutdown();
        }

        private bool CanCloseApplicationCommandExecute(object parameter)
        {
            return true;
        }
        #endregion

        #region Команды ввода
        #region Ввод цифр
        private void OnNumberCommandExecuted(object parameter)
        {
            InputBox = ReciverOfOperation.GetInputValue(parameter as string, InputBox);
        }

        private bool CanNumberCommandExecute(object parameter)
        {
            return true;
        }
        #endregion
        #region Ввод запятой
        private void OnCommaCommandExecuted(object parameter)
        {
            InputBox = ReciverOfOperation.GetComma("Comma", InputBox);
        }

        private bool CanCommaCommandExecute(object parameter)
        {
            return true;
        }
        #endregion
        #region Ввод плюс/минус
        private void OnPlusOrMinusCommandExecuted(object parameter)
        {
            InputBox = ReciverOfOperation.GetPlusOrMinus("PlusOrMinus", InputBox);
        }

        private bool CanPlusOrMinusCommandExecute(object parameter)
        {
            return true;
        }
        #endregion
        #region Ввод процента
        private void OnPercentOrMinusCommandExecuted(object parameter)
        {
            InputBox = ReciverOfOperation.GetPercent("Percent", InputBox);
        }

        private bool CanPercentOrMinusCommandExecute(object parameter)
        {
            return true;
        }
        #endregion
        #region Сброс
        private void OnResetCommandExecuted(object parameter)
        {
            ReciverOfOperation.GetReset();
            InputBox = "0";
        }

        private bool CanResetCommandExecute(object parameter)
        {
            return true;
        }
        #endregion
        #endregion

        #region Команды состояния
        #region Сумма
        private void OnSumCommandExecuted(object parameter)
        {
            reciver = ReciverOfOperation.Create("Sum");
            InputBox = reciver.GetStateValue(InputBox);
        }

        private bool CanSumCommandExecute(object parameter)
        {
            return true;
        }
        #endregion
        #region Разность
        private void OnDifCommandExecuted(object parameter)
        {
            reciver = ReciverOfOperation.Create("Dif");
            InputBox = reciver.GetStateValue(InputBox);
        }

        private bool CanDifCommandExecute(object parameter)
        {
            return true;
        }
        #endregion
        #region Деление
        private void OnDivCommandExecuted(object parameter)
        {
            reciver = ReciverOfOperation.Create("Div");
            InputBox = reciver.GetStateValue(InputBox);
        }

        private bool CanDivCommandExecute(object parameter)
        {
            return true;
        }
        #endregion
        #region Умножение
        private void OnMultiCommandExecuted(object parameter)
        {
            reciver = ReciverOfOperation.Create("Multi");
            InputBox = reciver.GetStateValue(InputBox);
        }

        private bool CanMultiCommandExecute(object parameter)
        {
            return true;
        }
        #endregion
        #region Равно
        private void OnEquallyCommandExecuted(object parameter)
        {
            reciver = ReciverOfOperation.Create("Equally");
            InputBox = reciver.GetStateValue(InputBox);
        }

        private bool CanEquallyCommandExecute(object parameter)
        {
            return true;
        }
        #endregion
        #endregion

        #endregion
        public MainWindowViewModel()
        {
            CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);
            Number = new LambdaCommand(OnNumberCommandExecuted, CanNumberCommandExecute);

            Sum = new LambdaCommand(OnSumCommandExecuted, CanSumCommandExecute);
            Dif = new LambdaCommand(OnDifCommandExecuted, CanDifCommandExecute);
            Div = new LambdaCommand(OnDivCommandExecuted, CanDivCommandExecute);
            Multi = new LambdaCommand(OnMultiCommandExecuted, CanMultiCommandExecute);
            Equally = new LambdaCommand(OnEquallyCommandExecuted, CanEquallyCommandExecute);



            Reset = new LambdaCommand(OnResetCommandExecuted, CanResetCommandExecute);
            Comma = new LambdaCommand(OnCommaCommandExecuted, CanCommaCommandExecute);
            PlusOrMinus = new LambdaCommand(OnPlusOrMinusCommandExecuted, CanPlusOrMinusCommandExecute);
            Percent = new LambdaCommand(OnPercentOrMinusCommandExecuted, CanPercentOrMinusCommandExecute);
        }


    }
    public abstract class NumberInput
    {
        public static string ValueStatic{ get; set; }

        public static NumberInput Create(string type)
        {
            switch (type)
            {
                case "Number":
                    {
                        return new GetNumber();
                    }
                case "Comma":
                    {
                        return new GetComma();
                    }
                case "Reset":
                    {
                        return new GetReset();
                    }
                case "PlusOrMinus":
                    {
                        return new GetPlusOrMinus();
                    }
                case "Percent":
                    {
                        return new GetPercent();
                    }
                default:
                    throw new ArgumentException();
            }
        }
        public abstract string GetValue(string value, string inputBox);

    }

    
    public class GetNumber : NumberInput
    {
        public override string GetValue(string value, string inputBox)
        {
            if (inputBox == "0" | inputBox == "-0" | ValueStatic == default)
            {
                if (inputBox == "-0")
                {
                    ValueStatic = "-" + value;
                    return ValueStatic;
                }
                else
                {
                    ValueStatic = value;
                    return ValueStatic;
                }
            }
            else
            {
                ValueStatic = inputBox + value;
                return ValueStatic;
            }
        }
    }
    public class GetComma : NumberInput
    {
        public override string GetValue(string value, string inputBox)
        {
            if (inputBox.IndexOf(",") >= 1)
                return ValueStatic;
            else
            {
                ValueStatic = inputBox + ",";
                return ValueStatic;
            }
        }
    }
    public class GetReset : NumberInput
    {
        public override string GetValue(string value, string inputBox)
        {
            ValueStatic = default;
            return ValueStatic;
        }
    }
    public class GetPlusOrMinus : NumberInput
    {
        public override string GetValue(string value, string inputBox)
        {
           if (inputBox[0] != '-')
            {
                if (ValueStatic == default)
                {
                    ValueStatic = "-0";
                    return ValueStatic;
                }
                ValueStatic = inputBox.Insert(0, "-");
                return ValueStatic;
            }
            else if (inputBox[0] == '-')
            {
                if (ValueStatic == default)
                {
                    ValueStatic = "0";
                    return ValueStatic;
                }
                ValueStatic = inputBox.Remove(0, 1);
                return ValueStatic;
            }
            throw new ArgumentException();
        }
    }
    public class GetPercent : NumberInput
    {
        public override string GetValue(string value, string inputBox)
        {
            ValueStatic = (Convert.ToDecimal(ValueStatic) / 100).ToString();
            return ValueStatic;
        }
    }

         
    public abstract class ReciverOfOperation
    {
        protected static NumberInput numberInput;
        protected static bool IsEqually { get; set; }
        private static void EqualSignTest(string value)
        {
            if(value == "Equally")
            {
                IsEqually = true;
            } 
            else if (value != "Equally" & IsEqually)
            {
                ValueStaticB = default;
                IsEqually = false;
            }
            else
            {
                IsEqually = false;
            }
        }
        protected static string State { get; set; }
        protected static string ValueStaticA { get; set; }
        protected static string ValueStaticB { get; set; }
        protected static string ValueStaticС { get; set; }
        public static string GetInputValue(string value, string inputBox)
        {
            numberInput = NumberInput.Create("Number");
            ValueStaticB = numberInput.GetValue(value, inputBox);
            return ValueStaticB;
        }

        public static string GetComma(string value, string inputBox)
        {
            numberInput = NumberInput.Create("Comma");
            ValueStaticB = numberInput.GetValue(value, inputBox);
            return ValueStaticB;
        }
        public static string GetPlusOrMinus(string value, string inputBox)
        {
            numberInput = NumberInput.Create("PlusOrMinus");
            ValueStaticA = numberInput.GetValue(value, inputBox);
            return ValueStaticA;
        }
        public static string GetPercent(string value, string inputBox)
        {
            numberInput = NumberInput.Create("Percent");
            ValueStaticB = numberInput.GetValue(value, inputBox);
            return ValueStaticB;
        }

        public static void GetReset()
        {
            numberInput = NumberInput.Create("Reset");
            ValueStaticB = numberInput.GetValue(null, null);

            State = default;
            ValueStaticA = default;
            ValueStaticB = default;
            IsEqually = default;
        }

        public static ReciverOfOperation Create(string type)
        {
            if (State == null)
            {
                State = type;
            }
            
            EqualSignTest(type);
            if (type == "Equally")
                return CreateDependingOnTheState(State);
            return CreateDependingOnTheState(type);

        }

        public static void ValueNull()
        {
            State = default;
            ValueStaticA = default;
            ValueStaticB = default;
            IsEqually = default;
        }
        private static ReciverOfOperation CreateDependingOnTheState(string state)
        {
            switch (State)
            {
                case "Sum":
                    {
                        State = state;
                        return new GetTectSum();
                    }

                case "Dif":
                    {
                        State = state;
                        return new GetTectDif();
                    }
                case "Div":
                    {
                        State = state;
                        return new GetTectDiv();
                    }
                case "Multi":
                    {
                        State = state;
                        return new GetTectMulti();
                    }

                default:
                    throw new ArgumentException();
            }
        }
        public abstract string GetStateValue(string inputBox);
    }

    public class GetTectSum : ReciverOfOperation
    {
        public override string GetStateValue(string inputBox)
        {
            if (ValueStaticA == default)
            {
                ValueStaticA = ValueStaticB;
                NumberInput.ValueStatic = default;
                ValueStaticB = default;
                return ValueStaticA;
            }
            decimal getValue = Convert.ToDecimal(ValueStaticA) + Convert.ToDecimal(ValueStaticB);
            ValueStaticA = getValue.ToString();
            if (IsEqually != true)
            {
                ValueStaticB = default;
                NumberInput.ValueStatic = default;
            }
            return getValue.ToString();
        }
    }
    public class GetTectDif : ReciverOfOperation
    {
        public override string GetStateValue(string inputBox)
        {
            if (ValueStaticA == default)
            {
                ValueStaticA = ValueStaticB;
                NumberInput.ValueStatic = default;
                ValueStaticB = default;
                return ValueStaticA;
            }
            decimal getValue = Convert.ToDecimal(ValueStaticA) - Convert.ToDecimal(ValueStaticB);
            ValueStaticA = getValue.ToString();

            if (IsEqually != true)
            {
                ValueStaticB = default;
                NumberInput.ValueStatic = default;
            }
                

            return getValue.ToString();
        }
    }
    public class GetTectDiv : ReciverOfOperation
    {
        public override string GetStateValue(string inputBox)
        {

            if(ValueStaticA == default)
            {
                ValueStaticA = ValueStaticB;
                NumberInput.ValueStatic = default;
                ValueStaticB = default;
                return ValueStaticA;
            }
            if (ValueStaticB == default & IsEqually != true)
            {
                ValueStaticB = "1";
                NumberInput.ValueStatic = "1";
            }
                
            if (ValueStaticB == "0")
                return "Ошибка";

            decimal getValue = Convert.ToDecimal(ValueStaticA) / Convert.ToDecimal(ValueStaticB);
            ValueStaticA = getValue.ToString();

            if (IsEqually != true)
            {
                ValueStaticB = default;
                NumberInput.ValueStatic = default;
            }
            return getValue.ToString();
        }
    }
    public class GetTectMulti : ReciverOfOperation
    {
        public override string GetStateValue(string inputBox)
        {
            if (ValueStaticA == default)
            {
                ValueStaticA = ValueStaticB;
                NumberInput.ValueStatic = default;
                ValueStaticB = default;
                return ValueStaticA;
            }
            if (ValueStaticB == default & IsEqually != true)
            {
                NumberInput.ValueStatic = "1";
                ValueStaticB = "1";
            }
                
            decimal getValue = Convert.ToDecimal(ValueStaticA) * Convert.ToDecimal(ValueStaticB);
            ValueStaticA = getValue.ToString();

            if (IsEqually != true)
            {
                ValueStaticB = default;
                NumberInput.ValueStatic = default;
            }
            return getValue.ToString();
        }
    }
}
