using System;
using System.IO;
using Microsoft.Win32;
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
using System.Windows.Shapes;

namespace WpfApp2
{
    /// <summary>
    /// Логика взаимодействия для ScorePractikWindow.xaml
    /// </summary>
    public partial class ScorePractikWindow : Window
    {
        public static string sPath = MainWindow.sPath;
        List<CheckBox[]> textlist = new List<CheckBox[]>();
        class Student
        {
            public string fio;
        }
        class txt
        {
            public string sss;
        }
        class Score
        {
            public int[] kriteri;
        }

        Student[] student = new Student[100];
        Score[] score = new Score[100];
        int g_iStudents = 0;
        public ScorePractikWindow()
        {
            InitializeComponent();
            FIOStart();
        }

        private void FIOStart()
        {
            try
            {
                if (sPath.Length > 0)
                {
                    using (StreamReader sr = new StreamReader(sPath, Encoding.Default))
                    {
                        int iCount = 0;
                        string sLine;

                        while ((sLine = sr.ReadLine()) != null)
                        {
                            if (sLine.Length == 0)
                                continue;

                            if (sLine.Equals("Student"))
                            {
                                iCount = 0;
                                continue;
                            }
                            if (iCount == 0)
                            {
                                student[g_iStudents] = new Student();
                                student[g_iStudents].fio = sLine;
                                g_iStudents++;
                            }
                            iCount++;
                        }
                    }

                    txt txt = new txt();
                    txt.sss = sPath;
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
            if (g_iStudents > 0)
            {
                comboBox.Items.Clear();
                for (int i = 0; i < g_iStudents; i++)
                {
                    if (student[i] != null)
                        comboBox.Items.Add(student[i].fio);
                }
            }

        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            Grid grid = new Grid();
            GridCreate(grid);
            listBox.Items.Add(grid);
        }

        private Grid GridCreate(Grid grid)
        {
            CheckBox[] checkBox = new CheckBox[30];
            TextBlock FIOBox = new TextBlock();
            FIOBox.VerticalAlignment = VerticalAlignment.Top;
            FIOBox.Text = $"ФИО {comboBox.Text}";
            FIOBox.Foreground = Brushes.WhiteSmoke;
            FIOBox.Height = 20;
            FIOBox.Margin = new Thickness(0, 0, 0, 0);
            grid.Children.Add(FIOBox);

            grid.Width = 350;
            for (int i = 0; i < 29; i++)
            {
                TextBlock IdBox = new TextBlock();
                IdBox = new TextBlock();
                IdBox.VerticalAlignment = VerticalAlignment.Top;
                IdBox.Text = $"Критерий: O{i + 1}";
                IdBox.Foreground = Brushes.WhiteSmoke;
                IdBox.Height = 20;
                IdBox.Margin = new Thickness(0, 30 * (1 + i), 0, 0);
                grid.Children.Add(IdBox);

                TextBlock textBlock = new TextBlock();
                textBlock.Name = $"textBlock{i}";
                textBlock.VerticalAlignment = VerticalAlignment.Top;
                textBlock.Text = "";
                textBlock.Foreground = Brushes.WhiteSmoke;
                textBlock.Height = 20;
                textBlock.Margin = new Thickness(120, 30 * (1 + i), 0, 0);
                grid.Children.Add(textBlock);

                checkBox[i] = new CheckBox();
                checkBox[i].Name = $"checkBox{i}";
                checkBox[i].VerticalAlignment = VerticalAlignment.Top;
                checkBox[i].Height = 20;
                checkBox[i].Width = 20;
                checkBox[i].Margin = new Thickness(0, 30 * (1 + i), 0, 0);
                grid.Children.Add(checkBox[i]);
            }
            textlist.Add(checkBox);

            return grid;
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_ImportClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog oFD = new OpenFileDialog();
            oFD.ShowDialog();
            sPath = oFD.FileName;

            try
            {
                if (sPath.Length > 0)
                {
                    using (StreamReader sr = new StreamReader(sPath, Encoding.Default))
                    {
                        int iCount = 0;
                        string sLine;

                        while ((sLine = sr.ReadLine()) != null)
                        {
                            if (sLine.Length == 0)
                                continue;

                            if (sLine.Equals("Student"))
                            {
                                iCount = 0;
                                continue;
                            }
                            if (iCount == 1)
                            {
                                student[g_iStudents] = new Student();
                                student[g_iStudents].fio = sLine;
                                g_iStudents++;
                            }
                            iCount++;
                        }
                    }

                    txt txt = new txt();
                    txt.sss = sPath;
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
            if (g_iStudents > 0)
            {
                comboBox.Items.Clear();
                for (int i = 0; i < g_iStudents; i++)
                {
                    if (student[i] != null)
                        comboBox.Items.Add(student[i].fio);
                }
            }
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void MembersClick(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();

            mw.Show();
            this.Hide();
        }

        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {


                OpenFileDialog oFD = new OpenFileDialog();
                oFD.ShowDialog();
                string sPath2 = oFD.FileName;

                try
                {
                    if (sPath.Length > 0)
                    {
                        using (StreamReader sr = new StreamReader(sPath2, Encoding.Default))
                        {
                            int iCount = 0;
                            string sLine;

                            while ((sLine = sr.ReadLine()) != null)
                            {
                                score[0] = new Score();
                                score[0].kriteri[iCount] = Convert.ToInt32(sLine);
                                iCount++;
                                if (iCount == 29)
                                {
                                    iCount = 0;
                                }
                            }
                        }
                        txt txt = new txt();
                        txt.sss = sPath2;
                    }


                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.Message);
                }

        }
    }
}
