using System;
using System.Threading.Tasks;
using System.Windows;
using System.Text.RegularExpressions;
using System.Threading;

namespace Number_Generator
{
    public partial class MainWindow : Window
    {
        int num1 = -2147483647, num2 = 2147483646;
        int quantity = 5;
        Random random = new Random();
        private Thread t = null;
        object locker = new Object();
        public MainWindow()
        {
            InitializeComponent();

        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            bool TBQuantityIsNumber = Regex.IsMatch(TBQuantity.Text, @"^\d+$");
            bool TextBox1IsNumber = Regex.IsMatch(TextBox1.Text, @"^-?\d+$");
            bool TextBox2IsNumber = Regex.IsMatch(TextBox2.Text, @"^-?\d+$");

            if (TextBox1.Text != "" && TextBox1IsNumber)
            {
                num1 = Convert.ToInt32(TextBox1.Text);
            }
            if (TextBox2.Text != "" && TextBox2IsNumber)
            {
                num2 = Convert.ToInt32(TextBox2.Text);
            }
            if (TBQuantity.Text != "" && TBQuantityIsNumber)
            {
                quantity = Convert.ToInt32(TBQuantity.Text);
            }

            t = new Thread(() => RunAsync(TextBox1IsNumber, TBQuantityIsNumber, quantity));

            // Если не указана нижняя граница, поток с стартует с 2.
            if (TextBox2.Text == "")
            {
                outputTextBox.Text += "2 ";
                quantity--;
            }


            t.Start();
        }



        

        public async void RunAsync(bool TextBox1IsNumber, bool TBQuantityIsNumber, int quantity)
        {

            try
            {

                await Dispatcher.InvokeAsync(async () =>
                {
                    // Если не указана верхняя граница, генерирование происходит до завершения приложения. (Priority condition)
                    if (!TextBox1IsNumber)
                    {
                        for (int i = 0; i < quantity;)
                        {

                            outputTextBox.Text += $"{random.Next(num1, num2 + 1)} ";
                            await Task.Delay(500);
                        }
                    }
                    else if (TBQuantityIsNumber) 
                    {
                        for (int i = 0; i < quantity; i++)
                        {
                            outputTextBox.Text += $"{random.Next(num1, num2 + 1)} ";
                        }

                        quantity = 0;
                    }

                });
            }
            catch (Exception ex) { MessageBox.Show($"{ex}"); }

        }


    }
}
