using System;
using System.Diagnostics;
using System.Linq;
using System.Timers;
using System.Windows;
using System.Windows.Controls;

namespace TaskManager
{
    public partial class MainWindow : Window
    {
        private Timer Timerrefresh;
        private Process[] allProcesses;
        public MainWindow()
        {
            InitializeComponent();
            ListFill();
            allProcesses = Process.GetProcesses();
            InitializeTimers();
        }

        //обновление списка каждую секунду
        //для коректрой работы кнопки Kill Process
        private void InitializeTimers()
        {
            Timerrefresh = new Timer(1000);
            Timerrefresh.Elapsed += RefreshTimer;
            Timerrefresh.AutoReset = true;
            Timerrefresh.Start();

        }

        private void RefreshTimer(object sender, ElapsedEventArgs e)
        {
            Dispatcher.Invoke(() => Refresh());
        }

        //звполнение ListBox данными
        public void ListFill()
        {
            // очистка перед заполнением
            processListBox.Items.Clear();

            Process[] processes = Process.GetProcesses().OrderBy(p => p.Id).ToArray();
            processListBox.Items.Add("Id\t\tPriority\t\tProcess Name");
            foreach (Process process in processes)
            {
                processListBox.Items.Add($"{process.Id}\t\t{process.BasePriority}\t\t{process.ProcessName}");
            }


        }

        //поисковая строка
        private void DbidFaind_TextChanged(object sender, TextChangedEventArgs e)
        {
            // попытка имитации placeholder из Html потому что в xaml я похожего свойства не нашла
            if (TbidFaind.Text != "")
            {
                placeholder.Content = "";
            }
            if (TbidFaind.Text == "")
            {
                placeholder.Content = "faind by id";
            }

            FilterAndSortProcessList(TbidFaind.Text.ToLower());

        }

        private void FilterAndSortProcessList(string filterText)
        {
            allProcesses = Process.GetProcesses();
            Process[] filteredProcesses = allProcesses;

            if (!string.IsNullOrEmpty(filterText))
            {
                filteredProcesses = allProcesses.Where(process => process.Id.ToString().Contains(filterText)).ToArray();
            }

            processListBox.Items.Clear();

            foreach (Process process in filteredProcesses)
            {
                string processInfo = $"{process.Id}\t\t{process.BasePriority}\t\t{process.ProcessName}";
                processListBox.Items.Add(processInfo);
            }
        }

        // кнопка Kill Process
        private void KillProcess(object sender, RoutedEventArgs e)
        {
            try
            {
                if (processListBox.SelectedItem != null)
                {
                    string? idp = processListBox.SelectedItem.ToString();
                    int tabIndex = idp.IndexOf('\t');
                    idp = idp.Substring(0, tabIndex);
                    int idproc = Convert.ToInt32(idp);
                    Process selProcess = Process.GetProcessById(idproc);
                    selProcess.Kill();
                }

                ListFill();
                FilterAndSortProcessList(TbidFaind.Text.ToLower());
            }
            catch (Exception ex) 
            { 
                MessageBox.Show(ex.Message);
            }
            
        }

        void Refresh()
        {
            if (processListBox.SelectedItem == null)
            {
               ListFill();
               FilterAndSortProcessList(TbidFaind.Text.ToLower());
            }
                
        }

        private void BtnRefresh(object sender, RoutedEventArgs e)
        {
            ListFill();
            FilterAndSortProcessList(TbidFaind.Text.ToLower());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
