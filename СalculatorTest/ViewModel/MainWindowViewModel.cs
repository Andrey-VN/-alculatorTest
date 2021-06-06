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
        public ICommand ValueInputCommand { get; }
        public ICommand Sum { get; }
        public ICommand Equally { get; }       
        public ICommand Dif { get; }
        public ICommand Div { get; }
        public ICommand Dis { get; }
        public ICommand Multi { get; }

        #region Комманды
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

        #region Ввод цифр
        private void OnValueInputCommandExecuted(object parameter)
        {
            InputBox = ReciverOfOperation.GetInputValue(parameter as string, InputBox);
        }

        private bool CanValueInputCommandExecute(object parameter)
        {
            //if (InputBox.IndexOf(",") >= 1)
            //    return false;
            return true;
        }
        #endregion
        #region Сброс
        private void OnDisCommandExecuted(object parameter)
        {
            ReciverOfOperation.ValueNull();
            InputBox = "0";
        }

        private bool CanDisCommandExecute(object parameter)
        {
            return true;
        }
        #endregion

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
        public MainWindowViewModel()
        {
            CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);
            ValueInputCommand = new LambdaCommand(OnValueInputCommandExecuted, CanValueInputCommandExecute);

            Sum = new LambdaCommand(OnSumCommandExecuted, CanSumCommandExecute);
            Dif = new LambdaCommand(OnDifCommandExecuted, CanDifCommandExecute);
            Div = new LambdaCommand(OnDivCommandExecuted, CanDivCommandExecute);
            Multi = new LambdaCommand(OnMultiCommandExecuted, CanMultiCommandExecute);

            Equally = new LambdaCommand(OnEquallyCommandExecuted, CanEquallyCommandExecute);
            Dis = new LambdaCommand(OnDisCommandExecuted, CanDisCommandExecute);
            
        }


    }

    public abstract class ReciverOfOperation
    {
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
        public static string GetInputValue(string value, string inputBox)
        {

            if ((inputBox == "0" | inputBox == "-0" | ValueStaticB == default) & value != "+/-" & value != "%" & value != ",")
            {
                if (inputBox == "-0")
                {
                    ValueStaticB = "-" + value;
                    return ValueStaticB;
                }
                if (inputBox.IndexOf(",") >= 1 & value == ",")
                {
                    return ValueStaticB;
                }
                ValueStaticB = value;
                return ValueStaticB;
            }
            else if (inputBox.IndexOf(",") >= 1 & value == ",")
                return ValueStaticB;
            else if (value == "%")
            {
                ValueStaticB = (Convert.ToDecimal(ValueStaticB) / 100).ToString();
                return ValueStaticB;
            }
            else if (value == "+/-" & inputBox[0] != '-')
            {
                if (ValueStaticB == default)
                {
                    ValueStaticB = "-0";
                    return ValueStaticB;
                }
                ValueStaticB = inputBox;
                return ValueStaticB.Insert(0, "-"); ;
            }
            else if (value == "+/-" & inputBox[0] == '-')
            {
                if (ValueStaticB == default)
                {
                    ValueStaticB = "0";
                    return ValueStaticB;
                }
                ValueStaticB = inputBox;
                return ValueStaticB.Remove(0, 1);
            }
            return Concatenation(value, inputBox);

        }
        private static string Concatenation(string value, string inputBox)
        {
            ValueStaticB = inputBox + value;
            return ValueStaticB;
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
            State = null;
            ValueStaticA = default;
            ValueStaticB = default;
            IsEqually = false;
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
            decimal getValue = Convert.ToDecimal(ValueStaticA) + Convert.ToDecimal(ValueStaticB);
            ValueStaticA = getValue.ToString();
            if (IsEqually != true)
                ValueStaticB = default;
            return getValue.ToString();
        }
    }

    public class GetTectDif : ReciverOfOperation
    {
        public override string GetStateValue(string inputBox)
        {
            decimal getValue = Convert.ToDecimal(ValueStaticA) - Convert.ToDecimal(ValueStaticB);
            ValueStaticA = getValue.ToString();

            if (IsEqually != true)
                ValueStaticB = default;

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
                ValueStaticB = default;
                return ValueStaticA;
            }
            if (ValueStaticB == default & IsEqually != true)
                ValueStaticB = "1";
            if (ValueStaticB == "0")
                return "Ошибка";

            decimal getValue = Convert.ToDecimal(ValueStaticA) / Convert.ToDecimal(ValueStaticB);
            ValueStaticA = getValue.ToString();

            if (IsEqually != true)
                ValueStaticB = default;
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
                ValueStaticB = default;
                return ValueStaticA;
            }
            if (ValueStaticB == default & IsEqually != true)
                ValueStaticB = "1";

            decimal getValue = Convert.ToDecimal(ValueStaticA) * Convert.ToDecimal(ValueStaticB);
            ValueStaticA = getValue.ToString();

            if (IsEqually != true)
                ValueStaticB = default;
            return getValue.ToString();
        }
    }
}
