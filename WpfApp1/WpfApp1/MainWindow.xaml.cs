using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
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

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public DataTable dataTable = new DataTable();

        public static void DTtoTrace(DataTable dataTable)
        {
            Trace.WriteLine("");
            Trace.WriteLine("Общая информация");
            Trace.WriteLine(String.Format("x = " + dataTable.Columns.Count));
            Trace.WriteLine(String.Format("y = " + dataTable.Rows.Count));

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                Trace.Write("|");
                for (int j = 0; j < dataTable.Columns.Count; j++)
                {
                    Trace.Write(String.Format("{0,3}", dataTable.Rows[i].ItemArray[j].ToString()));
                    Trace.Write("|");
                }
                Trace.WriteLine("");
            }

            for (int i = 0; i < dataTable.Columns.Count; i++)
            {
                Trace.WriteLine(String.Format(dataTable.Columns[i].ColumnName + " " + dataTable.Columns[i].DataType));
            }
        }

        public void FillDT()
        {
            dataTable.TableName = "Client";
            dataTable.Columns.Add("Фамилия", typeof(string));
            dataTable.Columns.Add("Имя", typeof(string));
            dataTable.Columns.Add("Отчество", typeof(string));
            dataTable.Columns.Add("Дата рождения", typeof(DateTime));
            dataTable.Columns.Add("Дата регистрации", typeof(DateTime));
            dataTable.Columns.Add("E-mail", typeof(string));
            dataTable.Columns.Add("Телефон", typeof(string));
            dataTable.Columns.Add("Пол", typeof(string));
            dataTable.Columns.Add("Логотип", typeof(string));
            dataTable.Columns.Add("Колличество покупок", typeof(int));
            dataTable.Columns.Add("Последнее посещение", typeof(DateTime));

            //dataTable.Columns.Add("Col1", typeof(int));
            //dataTable.Columns.Add("Col2", typeof(string));

            //dataTable.Rows.Add(1, "One");
            //dataTable.Rows.Add(2, "Two");

            dataTable.Rows.Add("Иванов", "Иван", "Иванович", new DateTime(2020,02,01,12,00,20), new DateTime(2020, 02, 01, 12, 00, 20), "Ivan@mail.com", "+7-800-555-35-35", "мужской", "D:/Logo", 2, new DateTime(2020, 02, 01, 12, 00, 20));
            dataTable.Rows.Add("Сергеев", "Сергей", "Сергеевич", new DateTime(2020, 02, 01, 12, 00, 20), new DateTime(2020, 02, 01, 12, 00, 20), "Ivan@mail.com", "+7-800-555-35-35", "мужской", "D:/Logo", 3, new DateTime(2020, 02, 01, 12, 00, 20));
            dataTable.Rows.Add("Сашива", "Александра", "Александровна", new DateTime(2020, 02, 01, 12, 00, 20), new DateTime(2020, 02, 01, 12, 00, 20), "Ivan@mail.com", "+7-800-555-35-35", "женский", "D:/Logo", 2, new DateTime(2020, 02, 01, 12, 00, 20));

            DTtoTrace(dataTable);
        }

        //public void Fun2()
        //{
        //    DataView dataView = new DataView();
        //    dataView.Table = dataTable;

        //    ComboBox filterComboBox = FilterCB;
        //    string filter = filterComboBox.Text;
        //    Trace.WriteLine(filter);
        //    if (filter != "All")
        //    {
        //        dataView.RowFilter = "Col2 = '" + filter + "'";
        //    }

        //    ComboBox sortComboBox = SortCB;
        //    string sort = sortComboBox.Text;
        //    Trace.WriteLine(sort);
        //    if (sort != "None")
        //    {
        //        dataView.Sort = "Col1 " + sort;
        //    }

        //    DataGrid DG = DataG;
        //    DG.ItemsSource = dataView;
        //}

        public void Fun3()
        {
            DataView dataView = new DataView();
            dataView.Table = dataTable;
            
            ComboBox surNameComboBox = SortSurName;
            string sortSurName = surNameComboBox.Text;
            Trace.WriteLine(sortSurName);
            if (sortSurName != "None")
            {
                dataView.Sort = "Фамилия " + sortSurName;
            }

            ComboBox BDayComboBox = SortBDay;
            string sortBDay = BDayComboBox.Text;
            Trace.WriteLine(sortBDay);
            if (sortBDay != "None")
            {
                dataView.Sort = "Дата рождения " + sortBDay;
            }

            ComboBox filterComboBox = FilterGender;
            string filter = filterComboBox.Text;
            Trace.WriteLine(filter);
            if (filter != "All")
            {
                dataView.RowFilter = "Пол = '" + filter + "'";
            }

            ComboBox orderComboBox = SortOrder;
            string sortOrder = orderComboBox.Text;
            Trace.WriteLine(sortOrder);
            if (sortOrder != "None")
            {
                dataView.Sort = "Колличество покупок " + sortOrder;
            }

            
            string search = Search.Text;
            if (search != String.Empty)
            {
                string filter2 = "Имя = '" + search + "' OR Фамилия = '" + search + "' OR Отчество = '" + search + "'";
                dataView.RowFilter = filter2;
            }

            DataGrid DG = DataG;
            DG.ItemsSource = dataView;
        }

        public MainWindow()
        {
            FillDT();
            InitializeComponent();
            Fun3();
        }

        private void SortSurName_DropDownClosed(object sender, EventArgs e)
        {
            Fun3();
        }

        private void SortBDay_DropDownClosed(object sender, EventArgs e)
        {
            Fun3();
        }

        private void FilterGender_DropDownClosed(object sender, EventArgs e)
        {
            Fun3();
        }

        private void SortOrder_DropDownClosed(object sender, EventArgs e)
        {
            Fun3();
        }

        private void SearchB_Click(object sender, RoutedEventArgs e)
        {
            Fun3();
        }

        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            Fun3();
        }

        //private void FilterCB_DropDownClosed(object sender, EventArgs e)
        //{
        //    Fun3();
        //}

        //private void SortCB_DropDownClosed(object sender, EventArgs e)
        //{
        //    Fun3();
        //}
    }
}
