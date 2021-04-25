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

namespace CMS_dvldesigns
{
    /// <summary>
    /// Logika interakcji dla klasy Window4.xaml
    /// </summary>
    public partial class Window4 : Window
    {
        private bool clicked = false;
        private Point lmAbs = new Point();

        public Window1 win1;

        string conn = "server=localhost;database=gpro;uid=root;password=";

        public Window4()
        {
            InitializeComponent();
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

        private void ListViewItem_Selected(object sender, RoutedEventArgs e)
        {
            
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            clicked = true;
            this.lmAbs = e.GetPosition(this);
            this.lmAbs.Y = Convert.ToInt16(this.Top) + this.lmAbs.Y;
            this.lmAbs.X = Convert.ToInt16(this.Left) + this.lmAbs.X;
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

        private void ListViewItem_Selected_1(object sender, RoutedEventArgs e)
        {
            
        }

        private void Btnmenu_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Btndeleteproduct_Click(object sender, RoutedEventArgs e)
        {
            MySqlConnection connect = new MySqlConnection(conn);

            try
            {
                if (!string.IsNullOrEmpty(id.Text))
                {
                    connect.Open();
                    string query = "DELETE FROM tbl_product where id=" + id.Text;
                    MySqlCommand cmd = new MySqlCommand(query, connect);
                    cmd.ExecuteNonQuery();
                    connect.Close();

                    System.Data.DataRowCollection row = win1.tb.Rows;
                    row[win1.currentRowIndex].Delete();
                    win1.tb.AcceptChanges();

                    // Reject changes on one deletion.
                    //itemColumns[3].RejectChanges();

                    MessageBox.Show("Produkt został usunięty!");
                }
                else
                {
                    MessageBox.Show("Wypełnij wszystkie pola");
                }

                id.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            int cri = win1.currentRowIndex;
            // Now, for my binding preparation...
            Binding binId = new Binding();
            binId.Source = win1.dv;
            binId.Path = new PropertyPath("[" + cri.ToString() + "][id]");
            id.SetBinding(TextBox.TextProperty, binId);
        }
    }
}
