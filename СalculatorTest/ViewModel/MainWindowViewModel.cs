using System;
using System.Windows;
using System.Windows.Input;
using СalculatorTest.Infrastructure;

namespace СalculatorTest.ViewModel
{
    internal class MainWindowViewModel : ViewModelBase
    {
        private bool onPropertyChangedOff = true;

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

        private void OnEquallyCommandExecuted(object parameter)
        {
            reciver = ReciverOfOperation.Create("=");
            InputBox = reciver.GetValue(InputBox);
        }

        private bool CanEquallyCommandExecute(object parameter)
        {
            return true;
        }

        private void OnSumCommandExecuted(object parameter)
        {
            reciver = ReciverOfOperation.Create("+");
            InputBox = reciver.GetValue(InputBox);
        }

        private bool CanSumCommandExecute(object parameter)
        {
            return true;
        }

        private void OnCloseApplicationCommandExecuted(object parameter)
        {
            Application.Current.Shutdown();
        }

        private bool CanCloseApplicationCommandExecute(object parameter)
        {
            return true;
        }

        private void OnValueInputCommandExecuted(object parameter)
        {
            string stringParameter = parameter as string;

            if (stringParameter == "AC")
                InputBox = "0";
            //else if (stringParameter == "+/-" & InputBox[0] != '-')
            //{
            //    InputBox = InputBox.Insert(0, "-");
            //}
            //else if (stringParameter == "+/-" & InputBox[0] == '-')
            //{
            //    InputBox = InputBox.Remove(0, 1);
            //}
            else if (InputBox == "0" & stringParameter != ",")
                InputBox = stringParameter;
            else
                InputBox += stringParameter;
        }

        private bool CanValueInputCommandExecute(object parameter)
        {
            return true;
        }
        public MainWindowViewModel()
        {
            CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);
            ValueInputCommand = new LambdaCommand(OnValueInputCommandExecuted, CanValueInputCommandExecute);
            Sum = new LambdaCommand(OnSumCommandExecuted, CanSumCommandExecute);
            Equally = new LambdaCommand(OnEquallyCommandExecuted, CanEquallyCommandExecute);
        }
    }
    public enum State
    {
        Sum = 0,
        Equally = 1
    }

    public abstract class ReciverOfValues
    {
        public static double ValueStatic { get; protected set; }
        public static ReciverOfOperation Create(string type)
        {
            switch (type)
            {
                case "+":
                    return new GetTectSum();
                case "=":
                    return new GetTectEqually();
                default:
                    throw new ArgumentException();
            }
        }

        public abstract string GetValue(string inputBox);
    }
    public abstract class ReciverOfOperation
    {
        public static State State { get; set; }
        public static double ValueStatic { get; protected set; }
        public static ReciverOfOperation Create(string type)
        {
            SetState(type);
            switch (type)
            {
                case "+":
                    return new GetTectSum();
                case "=":
                    return new GetTectEqually();
                default:
                    throw new ArgumentException();
            }
        }
        private static void SetState(string type)
        {
            switch (type)
            {
                case "+":
                    State = State.Sum;
                    break;
                case "=":
                    State = State.Equally;
                    break;
                default:
                    throw new ArgumentException();
            }
        }

        public abstract string GetValue(string inputBox);
    }

    public class GetTectSum : ReciverOfOperation
    {
        public override string GetValue(string inputBox)
        {
            if (ValueStatic == default)
            {
                ValueStatic += Convert.ToDouble(inputBox);
                return inputBox;
            }

            ValueStatic += Convert.ToDouble(inputBox);
            return ValueStatic.ToString();
        }
    }

    public class GetTectEqually : ReciverOfOperation
    {
        public override string GetValue(string inputBox)
        {
            return ValueStatic.ToString();
        }
    }
}
