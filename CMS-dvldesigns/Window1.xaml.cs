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
using System.Windows.Shapes;
using MySql.Data.MySqlClient;
using System.Data;
using System.ComponentModel;
using System.Globalization;


namespace CMS_dvldesigns
{
    /// <summary>
    /// Logika interakcji dla klasy Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private bool clicked = false;
        private Point lmAbs = new Point();

        string conn = "server=localhost;database=gpro;uid=root;password=";
        public DataTable tb;
        public DataView dv;

        public int currentRowIndex { get; set; }

        Window2 win2;
        Window3 win3;
        Window4 win4;

        public MainWindow main;

        public Window1()
        {
            InitializeComponent();
            FillTable();
        }

        private void FillTable()
        {
            MySqlConnection connection = new MySqlConnection(conn);

            MySqlCommand cmd = new MySqlCommand("SELECT * FROM tbl_product", connection);
            connection.Open();

            tb = new DataTable();

            tb.Load(cmd.ExecuteReader());
            connection.Close();

            //dataGrid.DataContext = tb;

            dv = tb.DefaultView; // as DataView;
            if (dv != null)
            {
                //dv.RowFilter = "name like '%test%'"; //where n is a column name of the DataTable
                dv.RowFilter = "";
            }

            dataGrid.DataContext = dv;

            // Now, for my binding preparation...
            //Binding bindMyColumn = new Binding();
            //bindMyColumn.Source = dv;
            //bindMyColumn.Path = new PropertyPath("[4][name]");
            //myTextBox.SetBinding(TextBox.TextProperty, bindMyColumn);

            
        }

        private void Btnpopupexit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void BtnOpenHam_Click(object sender, RoutedEventArgs e)
        {
            btnOpenHam.Visibility = Visibility.Collapsed;
            btnCloseHam.Visibility = Visibility.Visible;
        }

        private void BtnCloseHam_Click(object sender, RoutedEventArgs e)
        {
            btnOpenHam.Visibility = Visibility.Visible;
            btnCloseHam.Visibility = Visibility.Collapsed;
        }

        private void Grid_MouseMove(object sender, MouseEventArgs e)
        {
            if (clicked)
            {
                Point MousePosition = e.GetPosition(this);
                Point MousePositionAbs = new Point();
                MousePositionAbs.X = Convert.ToInt16(this.Left) + MousePosition.X;
                MousePositionAbs.Y = Convert.ToInt16(this.Top) + MousePosition.Y;
                this.Left = this.Left + (MousePositionAbs.X - this.lmAbs.X);
                this.Top = this.Top + (MousePositionAbs.Y - this.lmAbs.Y);
                this.lmAbs = MousePositionAbs;
            }
        }

        private void Grid_MouseUp(object sender, MouseButtonEventArgs e)
        {
            clicked = false;
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            clicked = true;
            this.lmAbs = e.GetPosition(this);
            this.lmAbs.Y = Convert.ToInt16(this.Top) + this.lmAbs.Y;
            this.lmAbs.X = Convert.ToInt16(this.Left) + this.lmAbs.X;
        }      

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(textBox.Text))
                {
                    dv.RowFilter = "name like '%" + textBox.Text + "%' OR image like '%" + textBox.Text + "%' OR type like '%" + textBox.Text.ToString() + "%' OR category like '%" + textBox.Text + "%' OR recommended like '%" + textBox.Text + "%'";
                    int temp;
                    if (int.TryParse(textBox.Text, out temp))
                    {
                        if (temp >= 0)
                        {
                            dv.RowFilter = "id = '" + textBox.Text + "' OR quan = '" + textBox.Text + "'";
                        }
                    }
                    else
                    {
                        double tmp;
                        if (double.TryParse(textBox.Text, out tmp))
                        {
                            dv.RowFilter = "price = '" + textBox.Text + "'";
                        }
                    }                 
                }
                else
                {
                    dv.RowFilter = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void MyTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void MyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                dataGrid.Focus();
            }
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            currentRowIndex = dataGrid.Items.IndexOf(dataGrid.CurrentItem);
        }     

        private void Btnaddproduct_Click(object sender, RoutedEventArgs e)
        {
            DataRow row;
            row = tb.NewRow();
            //row["id"] = 0;
            row["name"] = "test";
            row["image"] = "test2";
            row["price"] = 0;
            row["type"] = "test3";
            row["category"] = "test4";
            row["recommended"] = "test5";
            row["quan"] = 0;
            tb.Rows.Add(row);
            //tb.AcceptChanges();

            currentRowIndex = dataGrid.Items.Count - 1;

            win2 = new Window2();
            win2.win1 = this;
            win2.ShowDialog();
        }

        private void Btneditproduct_Click(object sender, RoutedEventArgs e)
        {
            win3 = new Window3();
            win3.win1 = this;
            win3.ShowDialog();
        }

        private void Btndeleteproduct_Click(object sender, RoutedEventArgs e)
        {
            win4 = new Window4();
            win4.win1 = this;
            win4.ShowDialog();
        }

        private void Btnmainmenu_Click(object sender, RoutedEventArgs e)
        {
            MainWindow win = new MainWindow();
            win.Show();
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void Btnrefreshdatagrid_Click(object sender, RoutedEventArgs e)
        {
            MySqlConnection connection = new MySqlConnection(conn);

            MySqlCommand cmd = new MySqlCommand("SELECT * FROM tbl_product", connection);
            connection.Open();

            tb = new DataTable();

            tb.Load(cmd.ExecuteReader());
            connection.Close();

            //dataGrid.DataContext = tb;

            dv = tb.DefaultView; // as DataView;
            if (dv != null)
            {
                //dv.RowFilter = "name like '%test%'"; //where n is a column name of the DataTable
                dv.RowFilter = "";
            }

            dataGrid.DataContext = dv;
        }
    }
}
