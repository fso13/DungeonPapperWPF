using System;
using System.Collections.Generic;
using System.Configuration;
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
            VoiceEnable.IsChecked = ConfigurationManager.AppSettings["VoiceEnable"] == "true";
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            AddUpdateAppSettings("VoiceEnable","true");
        }

        public void AddUpdateAppSettings(string key, string value)
        {
            try
            {
                System.Configuration.ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap();
                fileMap.ExeConfigFilename = "DungeonPapperWPF.exe.config";
                Configuration configFile = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
                var settings = configFile.AppSettings.Settings;
                if (settings[key] == null)
                {
                    settings.Add(key, value);
                }
                else
                {
                    settings[key].Value = value;
                }
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error writing app settings");
            }
        }

        private void VoiceEnable_Unchecked(object sender, RoutedEventArgs e)
        {
            AddUpdateAppSettings("VoiceEnable",  "false");

        }
    }
}
