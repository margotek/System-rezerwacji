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
using Microsoft.Win32;
using WinForms = System.Windows.Forms;

namespace CMS_dvldesigns
{
    /// <summary>
    /// Logika interakcji dla klasy Window5.xaml
    /// </summary>
    public partial class Window5 : Window
    {
        private bool clicked = false;
        private Point lmAbs = new Point();

        OpenFileDialog op = new OpenFileDialog();
        WinForms.FolderBrowserDialog folder = new WinForms.FolderBrowserDialog();

        public Window5()
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

        private void Btnmainmenu_Click(object sender, RoutedEventArgs e)
        {
            MainWindow win = new MainWindow();
            win.Show();
            this.Close();
        }

        private void Btncopyfile_Click(object sender, RoutedEventArgs e)
        {
            string fileName = filetextbox.Text;
            string sourcePath = filedir.Text;
            string targetPath = newdir.Text;

            try
            {
                if (!string.IsNullOrEmpty(filetextbox.Text) && !string.IsNullOrEmpty(filedir.Text) && !string.IsNullOrEmpty(newdir.Text))
                {
                    string p = System.IO.Path.Combine(targetPath, fileName);
                    System.IO.File.Copy(sourcePath, p, true);

                    MessageBox.Show("Plik został skopiowany!");
                }
                else
                {
                    MessageBox.Show("Empty field or more!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            filetextbox.Text = "";
            filedir.Text = "";
            newdir.Text = "";
        }

        private void Btnfilechooser_Click(object sender, RoutedEventArgs e)
        {
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                        "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                        "Portable Network Graphic (*.png)|*.png";

            bool? myResult;
            myResult = op.ShowDialog();
            if (myResult != null && myResult == true)
            {
                filedir.Text = op.FileName;
                filetextbox.Text = op.SafeFileName;
            }
        }

        private void Chooserdir_Click(object sender, RoutedEventArgs e)
        {
            folder.ShowNewFolderButton = false;
            folder.SelectedPath = System.AppDomain.CurrentDomain.BaseDirectory;
            WinForms.DialogResult result = folder.ShowDialog();

            if (result == WinForms.DialogResult.OK)
            {
                string newpath = folder.SelectedPath;
                newdir.Text = newpath;
            }           
        }
    }
}
