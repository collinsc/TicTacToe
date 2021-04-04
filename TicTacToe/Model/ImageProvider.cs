using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace TicTacToe.Model
{
    public class ImageProvider
    {
        private static ImageProvider instance;
        public static ImageProvider Instance => instance ??= new ImageProvider();

        private ImageSource _xImage;
        public ImageSource XImage => _xImage ??= CreateImage(new Uri("pack://application:,,,/TicTacToe;component/Resources/x-blue.png"));
        
        private ImageSource _oImage;
        public ImageSource OImage => _oImage ??= CreateImage(new Uri("pack://application:,,,/TicTacToe;component/Resources/o-orange.png"));

        private static ImageSource CreateImage(Uri uri)
        {
            var image = new BitmapImage();
            image.BeginInit();
            image.DecodePixelWidth = 175;
            image.UriSource = uri;
            image.CacheOption = BitmapCacheOption.None;
            image.EndInit();
            return image;
        }

    }
}
