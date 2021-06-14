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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Word = Microsoft.Office.Interop.Word;

namespace WpfApp2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        class Student
        {
            public string fio, group, objects, appraisal,appraisal2;
        }
        class Comission
        {
            public string fio = "";
        }
        class txt {

            public string sss;
        }

        Student[] student = new Student[100];
        int g_iStudents = 0;
        string dPath;
        public static string sPath;
        private string members1;

        public MainWindow()
        {
            InitializeComponent();

            GroupText.Items.Add("ИСиП 20-11-3");
            GroupText.Items.Add("ИСиП 20-11-2");
            GroupText.Items.Add("ИСиП 20-11-1");
            GroupText.Items.Add("ИСиП 19-11-3");
            GroupText.Items.Add("ИСиП 19-11-2");
            GroupText.Items.Add("ИСиП 19-11-1");
            GroupText.Items.Add("ССА 20-11-2");
            GroupText.Items.Add("ССА 20-11-1");
            GroupText.Items.Add("ССА 19-11-2");
            GroupText.Items.Add("ССА 19-11-1");

            Comboobject.Items.Add("МДК.01.02 Поддержка и тестирование программных модулей");
            Comboobject.Items.Add("Основы алгоритмизации и программирования");
            Comboobject.Items.Add("МДК.01.01 Разработка программных модулей");
            Comboobject.Items.Add("МДК.01.04 Системное программирование");
            Comboobject.Items.Add("Операционные системы и среды");
            Comboobject.Items.Add("Основы проектирования баз данных");
            Comboobject.Items.Add("МДК 02.03  Организация администрирования компьютерных сетей");
            Comboobject.Items.Add("МДК 02.02 Программное обеспечение компьютерных сетей");
            Comboobject.Items.Add("МДК 02.01 Администрирование сетевых операционных систем");
            Comboobject.Items.Add("МДК.03.01 Эксплуатация объектов сетевой инфраструктуры");

        }
        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {
          
            string name = "";
            string group = "";
            string predmet = "";
            string spec = "";
            string kriter = "";
            string ocenka = "";
                if (listBox.SelectedItems != null) {
                    string sItem = listBox.SelectedItem.ToString();
                    for (int i = 0; i < g_iStudents; i++) {
                        if (student[i] != null && sItem.Contains(student[i].fio)) {
                        name = student[i].fio;
                        group = student[i].group;
                        if (group.Contains("ИСиП"))
                        {
                            spec = "09.02.07 Информационные сиситемы и программирование";
                        }
                        else if (group.Contains("ССА"))
                        {
                            spec = "09.02.06 Сетевое и системное администрирование";
                        }
                        predmet = student[i].objects;
                        ocenka = student[i].appraisal;
                        }
                    }
                }
            var helper = new WordHelper("VEDOMOST.docx");
            var items = new Dictionary<string, string>
            {

                {"{name}", name},
                {"{group}", group},
                {"{objects}", predmet},
                {"{spec}",spec},
                {"{appraisal}",ocenka}
            };
            helper.Process(items);
        
        
        }
        private void PrintButton_Click1(object sender, RoutedEventArgs e)
        {
            string name = "";
            string group = "";
            string spec = "";
            string predmet = "";
            string ocenka1 = "";
            string ocenka2 = "";
            string ocenka3 = "";
            string generalocenka = "";
            string finalocenka = "";
            string memb = "";
            memImport();
            memb = members1;

            if (listBox.SelectedItems != null)
            {
                string sItem = listBox.SelectedItem.ToString();
                for (int i = 0; i < g_iStudents; i++)
                {
                    if (student[i] != null && sItem.Contains(student[i].fio))
                    {
                        name = student[i].fio;
                        group = student[i].group;
                        if (group.Contains("ИСиП")) {
                            spec = "Информационные сиситемы и программирование";
                        }
                        else if (group.Contains("ССА"))
                        {
                            spec = "Сетевое и системное администрирование";
                        }
                        predmet = student[i].objects;
                        ocenka1 = student[i].appraisal;
                        ocenka2 = student[i].appraisal2;

                    }
                }
            }
            var helper = new WordHelper("Itogoviy_Protokol.docx");
            var items = new Dictionary<string, string>
            {
                {"{name}", name},
                {"{group}", group},
                {"{objects}", predmet},
                {"{spec}",spec},
                {"{ocenka1}",ocenka1},
                {"{ocenka2}",ocenka2},
                {"{ocenka3}",ocenka3},
                {"{ocenkag}",generalocenka},
                {"{finalocenka}",finalocenka},
                {"{members}", memb}
            };
            helper.Process(items);

        }

        private void memImport() {
           
            using (StreamReader sr = new StreamReader(sPath, Encoding.Default))
            {
                string sLine;
                int Count = 1;
                while ((sLine = sr.ReadLine()) != null)
                {
                    
                    if (sLine.Contains("Comission"))
                    {
                        Count = 0;
                    }
                    if (Count == 1) {
                        members1 = sLine;
                    }
                    Count++;
                }
                dPath = sPath;
                txt txt = new txt();
                txt.sss = sPath;
            }
        }
        private void Import()
        {
            /*if (sPath == null)
            {
                MessageBox.Show("Выберите файл для импорта", "ExamInfoV1", MessageBoxButton.OK, MessageBoxImage.Information);
                OpenFileDialog sPaths = new OpenFileDialog();
                sPaths.ShowDialog();
                sPath = sPaths.FileName;
            }*/

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
                        int iCount = 0;
                        string sLine;
                        bool bStudent = false;

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
                            }
                            else if (iCount == 1)
                            {
                                student[g_iStudents].group = sLine;

                            }
                            else if (iCount == 2)
                            {
                                student[g_iStudents].objects = sLine;
                            }
                            else if(iCount == 3)
                            {
                                student[g_iStudents].appraisal = sLine;
                            }
                            else
                            {
                                student[g_iStudents].appraisal2 = sLine;
                                g_iStudents++;
                            }
                            iCount++;
                        }
                    }

                    MessageBox.Show("Импорт прошел удачно", "ExamInfoV1", MessageBoxButton.OK, MessageBoxImage.Information);
                    dPath = sPath;
                    txt txt = new txt();
                    txt.sss = sPath;
                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.Message);
                }
            }
            if (g_iStudents > 0)
            {
                listBox.Items.Clear();
                for (int i = 0; i < g_iStudents; i++)
                {
                    if (student[i] != null)
                        listBox.Items.Add($"ФИО: " + student[i].fio + " \nГруппа: " + student[i].group + "\nПредмет: " + student[i].objects + " \nОценка профессиональных компетенций: " + student[i].appraisal + " \nОценка общей компетенции: " + student[i].appraisal2 + "\n");
                }
            }
        }

        private void Button_StudentAdd(object sender, RoutedEventArgs e)
        {
            string sFio = FIOText.Text;
            if (sFio.Length == 0)
            {
                MessageBox.Show("Не указано ФИО!", "ExamInfo", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string sGroup = GroupText.Text;
            if (sGroup.Length == 0)
            {
                MessageBox.Show("Не указана группа!", "ExamInfo", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            string sObject = Comboobject.Text;
            if (Comboobject.SelectedItem == null)
            {
                MessageBox.Show("Не указан предмет!", "ExamInfo", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            string sappraisal = appraisal.Text;
            string sappraisal2 = appraisal2.Text;
            if (appraisal.Text == null || appraisal2.Text == null)
            {
                MessageBox.Show("Не указана оценка!", "ExamInfo", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            student[g_iStudents] = new Student();
            student[g_iStudents].fio = sFio;
            student[g_iStudents].group = sGroup;
            student[g_iStudents].objects = sObject;
            student[g_iStudents].appraisal = sappraisal;
            student[g_iStudents].appraisal2 = sappraisal2;

            g_iStudents++;
            MessageBox.Show("Студент добавлен!","ExamInfo", MessageBoxButton.OK, MessageBoxImage.Information);

            if (g_iStudents > 0)
            {
                listBox.Items.Clear();
                for (int i = 0; i < g_iStudents; i++)
                {
                    if (student[i] != null)
                        listBox.Items.Add($"ФИО: " + student[i].fio + " \nГруппа: " + student[i].group + "\nПредмет: " + student[i].objects + " \nОценка профессиональных компетенций: " + student[i].appraisal + " \nОценка общей компетенции: " + student[i].appraisal2  + "\n");

                }
            }
            Button_ExportClick();
        }
        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            if (listBox.SelectedItem != null)
            {
                int iCount = 0;
                string sItem = listBox.SelectedItem.ToString();
                for (int i = 0; i < g_iStudents; i++)
                {
                    if (student[i] != null && sItem.Contains(student[i].fio))
                    {
                        if (FIOText.Text.Length > 0)
                            student[i].fio = FIOText.Text; iCount++;

                        if (GroupText.Text.Length > 0)
                            student[i].group = GroupText.Text; iCount++;

                        if (Comboobject.Text.Length > 0)
                            student[i].objects = Comboobject.Text; iCount++;

                        if (appraisal.Text.Length > 0)
                            student[i].appraisal = appraisal.Text; iCount++;

                        if (appraisal.Text.Length > 0)
                            student[i].appraisal2 = appraisal.Text; iCount++;

                        break;
                    }
                }

                if (iCount > 0)
                {
                    MessageBox.Show("Данные упешно изменены!");

                    if (g_iStudents > 0)
                    {
                        listBox.Items.Clear();
                        for (int i = 0; i < g_iStudents; i++)
                        {
                            if (student[i] != null)
                                listBox.Items.Add($"ФИО: " + student[i].fio + " \nГруппа: " + student[i].group + "\nПредмет: " + student[i].objects + " \nОценка профессиональных компетенций: " + student[i].appraisal + " \nОценка общей компетенции: " + student[i].appraisal2 + "\n");

                        }
                    }
                }
            }
        }
        private void scorePractikButton_Click(object sender, RoutedEventArgs e)
        {
            ScorePractikWindow spw = new ScorePractikWindow();
            spw.Show(); this.Hide();
        }
        private void MembersClick(object sender, RoutedEventArgs e)
        {
            MembersWindow mw = new MembersWindow();
            mw.Show();this.Hide();
        }
        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void Button_ImportClick(object sender, RoutedEventArgs e)
        {
            Import();
        }

        private void Button_StudentDelete(object sender, RoutedEventArgs e)
        {
            if (listBox.SelectedItem != null)
            {
                string sItem = listBox.SelectedItem.ToString();
                for (int i = 0; i < g_iStudents; i++)
                {
                    if (student[i] != null && sItem.Contains(student[i].fio))
                    {
                        student[i] = null;
                        break;
                    }
                }

                listBox.Items.Remove(listBox.SelectedItem);
                MessageBox.Show("Студент удален");
            }
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
                        if (g_iStudents > 0)
                        {
                            for (int i = 0; i < g_iStudents; i++)
                            {
                                if (student[i] != null)
                                {
                                    sw.WriteLineAsync("Student");
                                    sw.WriteLineAsync(student[i].fio);
                                    sw.WriteLineAsync(student[i].group);
                                    sw.WriteLineAsync(student[i].objects);
                                    sw.WriteLineAsync(student[i].appraisal);
                                    sw.WriteLineAsync(student[i].appraisal2);
                                    sw.WriteLineAsync("********");
                                }
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

        private void FioTextInput(object sender, TextCompositionEventArgs e)
        {
            char lan = e.Text[0];
            if (lan < 'А' || lan > 'я')
            {
                e.Handled = true;
            }
         
        }

        private void appraisal_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}