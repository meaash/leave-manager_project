using ISDhhMuszakBeosztasUI.View;
using MaterialDesignThemes.Wpf;
using System;
using System.Windows;
using System.Windows.Controls;


namespace ISDhhMuszakBeosztasUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void bt_popuplogout_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Visible;
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
        }
        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
            ButtonOpenMenu.Visibility = Visibility.Visible;
        }

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            UserControl usc = null;
            GridMain.Children.Clear();

            if (ListViewMenu.SelectedItem != null)
            {

                switch (((ListViewItem)((ListView)sender).SelectedItem).Name)
                {
                    case "ItemHome":

                        usc = new MuszakBeosztasView();
                        GridMain.Children.Add(usc);
                        break;
                    case "ItemCreate":

                        usc = new EmployeeView();
                        GridMain.Children.Add(usc);
                        break;

                    case "ItemHoliday":
                        usc = new LeaveView();
                        GridMain.Children.Add(usc);
                        break;

                    case "ItemOverTime":
                        usc = new OverTimeView();
                        GridMain.Children.Add(usc);
                        break;
                    default:
                        break;
                }
            }
        }


        private void bt_popupsettings_Click(object sender, RoutedEventArgs e)
        {
            ListViewMenu.SelectedItem = null;
            UserControl usc = null;
            GridMain.Children.Clear();
            usc = new SettingsView();
            GridMain.Children.Add(usc);
        }
        private void bt_popupinformation_Click(object sender, RoutedEventArgs e)
        {
            ListViewMenu.SelectedItem = null;
            UserControl usc = null;
            GridMain.Children.Clear();
            usc = new InformationView();
            GridMain.Children.Add(usc);
        }

        //DarkMode: https://github.com/MaterialDesignInXAML/MaterialDesignInXamlToolkit/blob/master/MainDemo.Wpf/MainWindow.xaml.cs
        private void MenuDarkModeButton_Click(object sender, RoutedEventArgs e)
        {
            ModifyTheme(theme => theme.SetBaseTheme(DarkModeToggleButton.IsChecked == true ? Theme.Dark : Theme.Light));
        }
        private static void ModifyTheme(Action<ITheme> modificationAction)
        {
            PaletteHelper paletteHelper = new PaletteHelper();
            ITheme theme = paletteHelper.GetTheme();

            modificationAction?.Invoke(theme);

            paletteHelper.SetTheme(theme);
        }
    }
}
