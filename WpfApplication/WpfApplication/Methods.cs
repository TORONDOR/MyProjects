using System.Text;
using System.Windows.Media;
using System.Drawing;
using System.Windows.Interop;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Security.Cryptography;

namespace WpfApplication
{
    static class Methods
    {
        //public static ImageSource ToImageSource(this Icon icon)
        //{
        //    ImageSource imageSource = Imaging.CreateBitmapSourceFromHIcon(
        //        icon.Handle,
        //        Int32Rect.Empty,
        //        BitmapSizeOptions.FromEmptyOptions());

        //    return imageSource;
        //}

        public static string GetMd5Hash(MD5 md5Hash, string input)
        {

            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            foreach (byte t in data)
            {
                sBuilder.Append(t.ToString("x2"));
            }
            return sBuilder.ToString();
        }
    }
}
