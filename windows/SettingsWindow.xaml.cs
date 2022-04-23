using DungeonPapperWPF.code;
using System.Windows;

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
            if (ConfUtil.props.ContainsKey("VoiceEnable"))
                VoiceEnable.IsChecked = ConfUtil.props["VoiceEnable"] == "true";
            else
                VoiceEnable.IsChecked = false;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            ConfUtil.save("VoiceEnable", "true");
        }


        private void VoiceEnable_Unchecked(object sender, RoutedEventArgs e)
        {
            ConfUtil.save("VoiceEnable", "false");
        }
    }
}