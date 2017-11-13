using System.Windows;
using System.Windows.Input;

namespace SloanKelly.Tools.SGE.ViewModel.MainWindow
{
    internal class MainWindowViewModel : BaseViewModel
    {
        private readonly Window _window;
        private int _resizeBorder;
        private int _titleHeight;
        private ICommand _closeWindowCommand;
        private ICommand _maximizeCommand;
        private ICommand _minimizeCommand;
        private ICommand _menuCommand;
        private string _text;

        public ICommand MenuCommand
        {
            get { return _menuCommand; }
            set { _menuCommand = value;  OnPropertyChanged(); }
        }

        public ICommand MinimizeCommand
        {
            get { return _minimizeCommand; }
            set { _minimizeCommand = value; OnPropertyChanged(); }
        }

        public ICommand MaximizeCommand
        {
            get { return _maximizeCommand; }
            set { _maximizeCommand = value; OnPropertyChanged(); }
        }

        public ICommand CloseWindowCommand
        {
            get { return _closeWindowCommand; }
            set { _closeWindowCommand = value; OnPropertyChanged(); }
        }

        public string Text
        {
            get { return _text; }
            set { _text = value;  OnPropertyChanged(); }
        }

        public int TitleHeight
        {
            get { return _titleHeight; }
            set { _titleHeight = value;  OnPropertyChanged(); }
        }

        public int ResizeBorder
        {
            get { return _resizeBorder; }
            set
            {
                _resizeBorder = value; OnPropertyChanged();
            }
        }

        public MainWindowViewModel(Window window)
        {
            _window = window;

            MinimizeCommand = new RelayCommand(() => _window.WindowState = WindowState.Minimized);
            MaximizeCommand = new RelayCommand(() => _window.WindowState ^= WindowState.Maximized);
            CloseWindowCommand = new RelayCommand(() => _window.Close());
            MenuCommand = new RelayCommand(() => SystemCommands.ShowSystemMenu(_window, _window.PointToScreen(Mouse.GetPosition(_window))));

            Text = "Spectrum Graphics Editor";

            ResizeBorder = 6;
            TitleHeight = 40;
        }
    }
}
