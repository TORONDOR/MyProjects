using System.Windows;
using System.ComponentModel;

namespace WpfApplication
{
    public partial class FileView : Window
    {
        public FileView()
        {
            InitializeComponent();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FileModel.ButtonPressed();
        }

        private void FileView_Closing(object sender, CancelEventArgs e)
        {
            FileModel.ButtonPressed();
        }
    }
}
