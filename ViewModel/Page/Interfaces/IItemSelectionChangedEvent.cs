﻿using System;
using Data.Transformed.Interfaces;

namespace ViewModel.Page.Interfaces
{
    /// <summary>
    /// The event in this interface can be subscribed to by any object 
    /// interesting in knowing about changes in the selection of an item, 
    /// typically in a collection-oriented GUI control, e.g. a ListView.
    /// </summary>
    public interface IItemSelectionChangedEvent<out TViewData>
    {
        event Action<IDataWrapper<TViewData>> ItemSelectionChanged;
    }
}