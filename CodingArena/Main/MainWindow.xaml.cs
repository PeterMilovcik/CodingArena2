using System.Windows;
using CodingArena.Main.Battlefields;

namespace CodingArena.Main
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

        private void StartGameMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            // TODO: remove
            var viewModel = DataContext as BattlefieldViewModel;
            viewModel?.StartGameAsync();
        }
    }
}
