using System;
using Controllers.Implementation;
using Data.InMemory.Interfaces;
using Data.Transformed.Interfaces;
using Model.Interfaces;

namespace Commands.Implementation
{
    /// <summary>
    /// Implementation of a generic Delete command.
    /// </summary>
    public class DeleteCommandBase<TViewData> : CRUDCommandBase
        where TViewData : class, ICopyable, IStorable
    {
        public DeleteCommandBase(IDataWrapper<TViewData> source, ICatalog<TViewData> target, Func<bool> condition)
            : base(new DeleteControllerBase<TViewData>(source, target), condition)
        {
        }
    }
}