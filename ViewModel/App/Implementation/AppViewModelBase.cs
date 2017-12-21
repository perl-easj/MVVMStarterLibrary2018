using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;
using ViewModel.App.Interfaces;

namespace ViewModel.App.Implementation
{
    /// <summary>
    /// Implementation of the IAppViewModel interface, providing a minimal
    /// view model for top-level application navigation.
    /// </summary>
    public abstract class AppViewModelBase : INotifyPropertyChanged, IAppViewModel
    {
        private Frame _appFrame;
        private Dictionary<string, ICommand> _navigationCommands;

        protected AppViewModelBase()
        {
            _appFrame = null;
            _navigationCommands = new Dictionary<string, ICommand>();
        }

        public Dictionary<string, ICommand> NavigationCommands
        {
            get { return _navigationCommands; }
        }

        public Frame AppFrame
        {
            get { return _appFrame; }
        }

        public void SetAppFrame(Frame appFrame)
        {
            _appFrame = appFrame;
            AddCommands();
            OnPropertyChanged(nameof(NavigationCommands));
        }

        /// <summary>
        /// Override in specific implementation to add navigation commands.
        /// </summary>
        public abstract void AddCommands();

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}