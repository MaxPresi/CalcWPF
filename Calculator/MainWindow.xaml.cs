using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calculator
{


    public partial class MainWindow : Window
    {
        double lastNumber, result;
        SelOperador selOperador;
        int s = 0;

        public MainWindow()
        {
            InitializeComponent();
            selOperador = SelOperador.None;
        }

        private void NumBtn_Click(object sender, RoutedEventArgs e)
        {
            int selVal = int.Parse((sender as Button).Content.ToString());

            switch (s)
            {
                case 0:
                    resultLb.Content = $"{selVal}";
                    ContaLbl.Content = "";
                    s = 2;
                    break;
                case 1:
                    resultLb.Content = $"{selVal}";
                    s = 2;
                    break;
                case 2:
                    if (resultLb.Content.ToString() != "0") resultLb.Content = $"{resultLb.Content}{selVal}";
                    else resultLb.Content = $"{selVal}";
                    break;
            }
           
        }

        private void ContaBtn_Click(object sender, RoutedEventArgs e)
        {
            string operador = " "+(sender as Button).Content.ToString()+" ";

            if (double.TryParse(resultLb.Content.ToString(), out lastNumber))
            {
                ContaLbl.Content = lastNumber.ToString() + operador;
                resultLb.Content = "0";
                s = 1;
            }

            if (sender == SomaBtn) selOperador = SelOperador.Add;
            if (sender == MenosBtn) selOperador = SelOperador.Sub;
            if (sender == MultiBtn) selOperador = SelOperador.Mult;
            if (sender == DivBtn) selOperador = SelOperador.Div;
        }


        private void ACBtn_Click(object sender, RoutedEventArgs e)
        {
            resultLb.Content = "0";
            ContaLbl.Content = "";
            result = 0;
            lastNumber = 0;
            s = 0;
        }

        private void NegBtn_Click(object sender, RoutedEventArgs e)
        {
            if(double.TryParse(resultLb.Content.ToString(), out lastNumber) && resultLb.Content.ToString() != "0")
            {
                lastNumber *= -1;
                resultLb.Content = lastNumber.ToString();
            }
        }

        private void PCentBtn_Click(object sender, RoutedEventArgs e)
        {

            if (double.TryParse(resultLb.Content.ToString(), out double tempNum) && resultLb.Content.ToString() != "0")
            {
                tempNum /= 100;
                if (lastNumber != 0) tempNum *= lastNumber;
                resultLb.Content = tempNum.ToString();
                s = 2;
            }
        }

        private void DecBtn_Click(object sender, RoutedEventArgs e)
        {
            if(!resultLb.Content.ToString().Contains(","))
            resultLb.Content = $"{resultLb.Content},";
            s = 2;
        }

        

        private void IgualBtn_Click(object sender, RoutedEventArgs e)
        {
            double newNumber;

            if (double.TryParse(resultLb.Content.ToString(), out newNumber))
            {
                ContaLbl.Content = $"{ContaLbl.Content}" + newNumber.ToString();
                
                switch (selOperador)
                {
                    case SelOperador.None:
                        ContaLbl.Content = "";
                        break;
                    case SelOperador.Add:
                        result = Conta.Add(lastNumber, newNumber);
                        resultLb.Content = result.ToString();
                        break;
                    case SelOperador.Sub:
                        result = Conta.Sub(lastNumber, newNumber);
                        resultLb.Content = result.ToString();
                        break;
                    case SelOperador.Mult:
                        result = Conta.Mult(lastNumber, newNumber);
                        resultLb.Content = result.ToString();
                        break;
                    case SelOperador.Div:
                        result = Conta.Div(lastNumber, newNumber);
                        if(result == 0)
                        {
                            ContaLbl.Content = "";
                        }
                        resultLb.Content = result.ToString();
                        break;
                        
                }
                selOperador = SelOperador.None;
                s = 0;
            }

        }
    }

    public enum SelOperador
    {
        None,
        Add,
        Sub,
        Mult,
        Div
    }

    public class Conta
    {
        public static double Add(double n1, double n2)
        {
            return n1 + n2;
        }

        public static double Sub(double n1, double n2)
        {
            return n1 - n2;
        }

        public static double Mult(double n1, double n2)
        {
            return n1 * n2;
        }

        public static double Div(double n1, double n2)
        {
            if (n2 == 0)
            {
                MessageBox.Show("Dividir por 0 é impossível!", "PARADOXO MATEMÁTICO!!!", MessageBoxButton.OK, MessageBoxImage.Error);
                return 0;
            }

            return n1 / n2;
        }
    }

}
