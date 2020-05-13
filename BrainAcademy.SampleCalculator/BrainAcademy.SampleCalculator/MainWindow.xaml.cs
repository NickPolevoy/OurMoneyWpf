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
using NCalc;

namespace BrainAcademy.SampleCalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Grid_Click(object sender, RoutedEventArgs e)
        {
            // обработчик работает только для кнопок
            Button btn = e.Source as Button;
            if (btn == null)
                return;
            // обработчик работает только для кнопок у которых есть значение (контент)
            string value = btn.Content.ToString();
            if (String.IsNullOrEmpty(value))
                return;
            switch(value)
            {
                // стираем текстовое поле
                case "C":
                    TextBoxMain.Text = "0";
                    break;

                // считаем выражение с помощью библиотеки NCalc
                // https://ncalc.codeplex.com/
                case "=":
                    try
                    {
                        NCalc.Expression exp = new NCalc.Expression(TextBoxMain.Text);
                        TextBoxMain.Text = exp.Evaluate().ToString();
                    }
                    catch
                    {
                        TextBoxMain.Text = "Error";
                    }
                    break;
                
                // добавялем новое значение в текстовое поле и удаляем стартовый 0
                default:
                    TextBoxMain.Text = TextBoxMain.Text.TrimStart('0') + value;                    
                    break;
            }
            
        }
    }
}
