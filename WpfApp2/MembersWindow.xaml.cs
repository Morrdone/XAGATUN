using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;
using Microsoft.Office.Interop.Word;

namespace WpfApp2
{
    /// <summary>
    /// Логика взаимодействия для MembersWindow.xaml
    /// </summary>
    public partial class MembersWindow : System.Windows.Window
    {
        class Comission
        {
            public string fio;
        }

        int g_iComission = 0;
        string dPath;
        public static string sPath = MainWindow.sPath;
        Comission[] comission = new Comission[100];

        public MembersWindow()
        {
            InitializeComponent();
        }
        private void Import()
        {
            MessageBox.Show("Выберите файл для импорта", "ExamInfoV1", MessageBoxButton.OK, MessageBoxImage.Information);
            OpenFileDialog sPaths = new OpenFileDialog();
            sPaths.ShowDialog();
            sPath = sPaths.FileName;

            if (sPath.Length > 0)
            {
                try
                {
                    using (StreamReader sr = new StreamReader(sPath, Encoding.Default))
                    {
                        string sLine;

                        while ((sLine = sr.ReadLine()) != null)
                        {
                            if (sLine.Length == 0)
                                continue;

                            if (sLine.Equals("********"))
                                continue;

                            comission[g_iComission] = new Comission();
                            comission[g_iComission].fio = sLine;

                            g_iComission++;
                        }
                    }

                    MessageBox.Show("Импорт прошел удачно", "ExamInfoV1", MessageBoxButton.OK, MessageBoxImage.Information);
                    dPath = sPath;

                    if (g_iComission > 0)
                    {
                        listBox.Items.Clear();
                        for (int i = 0; i < g_iComission; i++)
                        {
                            if (comission[i] != null)
                                listBox.Items.Add($"ФИО: " + comission[i].fio + "\n");
                        }
                    }
                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.Message);
                }
            }
        }
        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            string sFio = FIOText.Text;
            if (sFio.Length == 0)
            {
                MessageBox.Show("Не указано ФИО!", "ExamInfo", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            comission[g_iComission] = new Comission();
            comission[g_iComission].fio = sFio;
            g_iComission++;
            MessageBox.Show("Член комиссии добавлен!", "ExamInfo", MessageBoxButton.OK, MessageBoxImage.Information);

            if (g_iComission > 0)
            {
                listBox.Items.Clear();
                for (int i = 0; i < g_iComission; i++)
                {
                    if (comission[i] != null)
                        listBox.Items.Add($"ФИО: " + comission[i].fio + " \n");

                }
            }
            Button_ExportClick();
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (listBox.SelectedItem != null)
            {
                string sItem = listBox.SelectedItem.ToString();
                for (int i = 0; i < g_iComission; i++)
                {
                    if (comission[i] != null && sItem.Contains(comission[i].fio))
                    {
                        comission[i] = null;
                        break;
                    }
                }

                listBox.Items.Remove(listBox.SelectedItem);
                MessageBox.Show("Член комиссии удален");
            }
        }
        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            if (listBox.SelectedItem != null)
            {
                int iCount = 0;
                string sItem = listBox.SelectedItem.ToString();
                for (int i = 0; i < g_iComission; i++)
                {
                    if (comission[i] != null && sItem.Contains(comission[i].fio))
                    {
                        if (FIOText.Text.Length > 0)
                            comission[i].fio = FIOText.Text; iCount++;

                        break;
                    }
                }

                if (iCount > 0)
                {
                    MessageBox.Show("Данные упешно изменены!");

                    if (g_iComission > 0)
                    {
                        listBox.Items.Clear();
                        for (int i = 0; i < g_iComission; i++)
                        {
                            if (comission[i] != null)
                                listBox.Items.Add($"ФИО: " + comission[i].fio + "\n");

                        }
                    }
                }
            }
        }

        private void MembersClick(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Hide();
        }

        private void ShowAllButton_Click(object sender, RoutedEventArgs e)
        {
            if (g_iComission > 0)
            {
                listBox.Items.Clear();
                for (int i = 0; i < g_iComission; i++)
                {
                    if (comission[i] != null)
                        listBox.Items.Add($"ФИО: " + comission[i].fio + " \n");

                }
            }
        }
        private void Button_ImportClick(object sender, RoutedEventArgs e)
        {
            Import();
        }

        private void Button_ExportClick()
        {
            MessageBox.Show("Выберите файл для экпорта", "ExamInfoV1", MessageBoxButton.OK, MessageBoxImage.Information);
            OpenFileDialog sPaths = new OpenFileDialog();
            sPaths.ShowDialog();
            sPath = sPaths.FileName;

            if (sPath.Length > 0)
            {
                try
                {
                    using (StreamWriter sw = new StreamWriter(sPath, true, Encoding.Default))
                    {
                        if (g_iComission > 0)
                        {
                            for (int i = 0; i < g_iComission; i++)
                            {
                                
                                if (comission[i] != null)
                                    sw.WriteLineAsync("Comission");
                                    sw.WriteLineAsync(comission[i].fio);
                                    sw.WriteLineAsync("*************");
                            }
                        }
                    }
                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.Message);
                }
            }
        }

        private void FIOTextInput(object sender, TextCompositionEventArgs e)
        {
            char lan = e.Text[0];
            if (lan < 'А' || lan > 'я')
            {
                e.Handled = true;
            }
        }

        private void scorePractikButton_Click(object sender, RoutedEventArgs e)
        {
            ScorePractikWindow spw = new ScorePractikWindow();
            spw.Show(); this.Hide();
        }
    }
}
