using System.Windows;
using DungeonPapperWPF.code;

namespace DungeonPapperWPF.windows
{
    /// <summary>
    /// Логика взаимодействия для NickWindow.xaml
    /// </summary>
    public partial class NickWindow : Window
    {
        public NickWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (nickTextBox.Text != null && nickTextBox.Text.Length > 0)
            {
                ConfUtil.save("nick", nickTextBox.Text);
                this.Close();
            }
        }
    }
}
