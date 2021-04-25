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

namespace CMS_dvldesigns
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool clicked = false;
        private Point lmAbs = new Point();

        public Login login;

        public string activeUser;

        public MainWindow()
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

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            clicked = true;
            this.lmAbs = e.GetPosition(this);
            this.lmAbs.Y = Convert.ToInt16(this.Top) + this.lmAbs.Y;
            this.lmAbs.X = Convert.ToInt16(this.Left) + this.lmAbs.X;
        }

        private void Grid_MouseUp(object sender, MouseButtonEventArgs e)
        {
            clicked = false;
        }

        private void ListViewItem_Selected(object sender, RoutedEventArgs e)
        {
            
        }

        private void ListViewItem_Selected_1(object sender, RoutedEventArgs e)
        {
            
        }

        private void Btntable_Click(object sender, RoutedEventArgs e)
        {
            Window1 win1 = new Window1();
            win1.main = this;
            win1.Show();
            this.Close();
        }

        private void Btnaddimage_Click(object sender, RoutedEventArgs e)
        {
            Window5 win5 = new Window5();
            win5.Show();
            this.Close();
        }

        private void Btnlogout_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Close();
        }

        private void Btnsettings_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Btninvoice_Click(object sender, RoutedEventArgs e)
        {
            invoice inv = new invoice();
            inv.Show();
            this.Close();
        }
    }
}
