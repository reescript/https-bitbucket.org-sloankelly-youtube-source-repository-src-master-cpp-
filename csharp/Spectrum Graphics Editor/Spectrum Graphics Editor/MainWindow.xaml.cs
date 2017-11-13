using System.Windows;
using SloanKelly.Tools.SGE.ViewModel.MainWindow;

namespace SloanKelly.Tools.SGE
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new MainWindowViewModel(this);
        }
    }
}
