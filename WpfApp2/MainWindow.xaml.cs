using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
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
using static System.Net.Mime.MediaTypeNames;

namespace WpfApp2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string base64 = null;
       
        public MainWindow()
        {
            InitializeComponent();
        }
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ImageEncodeToBase64();
            txtBase64.Text = base64.ToString();

        }
        public void ImageEncodeToBase64()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.FilterIndex = 1;
            if (ofd.ShowDialog() == true)
            {
                using (Bitmap bm = new Bitmap(ofd.FileName))
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        bm.Save(ms, ImageFormat.Jpeg);
                        base64 = Convert.ToBase64String(ms.ToArray());
                    }
                }
            }


            /// <summary>
            /// Вывод выбранного изображения на window
            /// </summary>
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.UriSource = new Uri(ofd.FileName);
            bitmapImage.EndInit();
            imageWindow.Source = bitmapImage;

        }

        public void Base64DecodeToImage(string base64s)
        {
            byte[] binaryData = Convert.FromBase64String(base64s);

            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.StreamSource = new MemoryStream(binaryData);
            bi.EndInit();

            System.Windows.Controls.Image img = new System.Windows.Controls.Image();
            imageDecode.Source = bi;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Base64DecodeToImage(base64);
        }
    }
}