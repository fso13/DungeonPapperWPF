using System.Windows;
using DungeonPapperWPF.code;

namespace DungeonPapperWPF.windows
{
    /// <summary>
    /// Логика взаимодействия для SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();
            if (ConfUtil.read().ContainsKey("VoiceEnable"))
            {
                VoiceEnable.IsChecked = ConfUtil.read()["VoiceEnable"] == "true";
            }
            else
            {
                VoiceEnable.IsChecked = false;
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            ConfUtil.save("VoiceEnable","true");
        }


        private void VoiceEnable_Unchecked(object sender, RoutedEventArgs e)
        {
            ConfUtil.save("VoiceEnable",  "false");
        }
    }
}
