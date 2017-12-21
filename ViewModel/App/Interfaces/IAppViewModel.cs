﻿using System.Collections.Generic;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;

namespace ViewModel.App.Interfaces
{
    /// <summary>
    /// Interface for a view model for top-level application navigation.
    /// </summary>
    public interface IAppViewModel
    {
        Frame AppFrame { get; }
        Dictionary<string, ICommand> NavigationCommands { get; }
        void SetAppFrame(Frame appFrame);
        void AddCommands();
    }
}