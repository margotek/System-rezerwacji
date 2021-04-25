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
using Microsoft.Win32;
using MySql.Data.MySqlClient;

namespace CMS_dvldesigns
{
    /// <summary>
    /// Logika interakcji dla klasy Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        private bool clicked = false;
        private Point lmAbs = new Point();

        string conn = "server=localhost;database=gpro;uid=root;password=";

        OpenFileDialog op = new OpenFileDialog();

        public Window1 win1;

        public FileInfo[] MyCollection { get; set; }

        public Window2()
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

        private void ListViewItem_Selected(object sender, RoutedEventArgs e)
        {
            
        }

        private void ListViewItem_Selected_1(object sender, RoutedEventArgs e)
        {
            
        }

        private void Btnmainmenu_Click(object sender, RoutedEventArgs e)
        {
            win1.tb.RejectChanges();
            this.Close();
        }

        private void Btnadd_Click(object sender, RoutedEventArgs e)
        {
            MySqlConnection connect = new MySqlConnection(conn);

            try
            {
                if (!string.IsNullOrEmpty(name.Text) && !string.IsNullOrEmpty(image.Text) && !string.IsNullOrEmpty(price.Text) && !string.IsNullOrEmpty(type.Text) && !string.IsNullOrEmpty(category.Text) && !string.IsNullOrEmpty(recommended.Text) && !string.IsNullOrEmpty(quantity.Text))
                {
                    connect.Open();
                    string query = "INSERT INTO tbl_product (name,image,price,type,category,recommended,quan) VALUES('" + name.Text + "', '" + image.Text + "', '" + price.Text + "', '" + type.Text + "', '" + category.Text + "', '" + recommended.Text + "', '" + quantity.Text + "')";
                    MySqlCommand cmd = new MySqlCommand(query, connect);
                    cmd.ExecuteNonQuery();
                    connect.Close();
                    win1.tb.AcceptChanges();
                    MessageBox.Show("Produkt został dodany do bazy!");
                }
                else
                {
                    MessageBox.Show("Wypełnij wszystkie pola");
                }

                name.Text = "";
                image.Text = "";
                price.Text = "";
                type.Text = "";
                category.Text = "";
                recommended.Text = "";
                quantity.Text = "";
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
            //Binding binId = new Binding();
            //binId.Source = win1.dv;
            //binId.Path = new PropertyPath("[" + cri.ToString() + "][id]");
            //id.SetBinding(TextBox.TextProperty, binId);

            Binding bindName = new Binding();
            bindName.Source = win1.dv;
            bindName.Path = new PropertyPath("[" + cri.ToString() + "][name]");
            name.SetBinding(TextBox.TextProperty, bindName);

            Binding bindImage = new Binding();
            bindImage.Source = win1.dv;
            bindImage.Path = new PropertyPath("[" + cri.ToString() + "][image]");
            image.SetBinding(TextBox.TextProperty, bindImage);

            Binding bindPrice = new Binding();
            bindPrice.Source = win1.dv;
            bindPrice.Path = new PropertyPath("[" + cri.ToString() + "][price]");
            price.SetBinding(TextBox.TextProperty, bindPrice);

            Binding bindType = new Binding();
            bindType.Source = win1.dv;
            bindType.Path = new PropertyPath("[" + cri.ToString() + "][type]");
            type.SetBinding(TextBox.TextProperty, bindType);

            Binding bindCategory = new Binding();
            bindCategory.Source = win1.dv;
            bindCategory.Path = new PropertyPath("[" + cri.ToString() + "][category]");
            category.SetBinding(TextBox.TextProperty, bindCategory);

            Binding bindRecommended = new Binding();
            bindRecommended.Source = win1.dv;
            bindRecommended.Path = new PropertyPath("[" + cri.ToString() + "][recommended]");
            recommended.SetBinding(TextBox.TextProperty, bindRecommended);

            Binding bindQuantity = new Binding();
            bindQuantity.Source = win1.dv;
            bindQuantity.Path = new PropertyPath("[" + cri.ToString() + "][quan]");
            quantity.SetBinding(TextBox.TextProperty, bindQuantity);
        }

        private void Filechooser_Click(object sender, RoutedEventArgs e)
        {
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                        "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                        "Portable Network Graphic (*.png)|*.png";

            op.InitialDirectory = @"C:\xampp\htdocs\ognioNajnowsze\images";

            bool? myResult;
            myResult = op.ShowDialog();
            if (myResult != null && myResult == true)
            {
                image.Text = op.SafeFileName;
                image.Focus();
            }
        }
    }
}
