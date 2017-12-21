﻿using Data.InMemory.Interfaces;
using Data.Transformed.Interfaces;
using Model.Interfaces;

namespace Controllers.Implementation
{
    /// <summary>
    /// Implementation of a generic Delete operation.
    /// </summary>
    public class DeleteControllerBase<TViewData> : CRUDControllerBase<TViewData>
        where TViewData : class, IStorable
    {
        public DeleteControllerBase(IDataWrapper<TViewData> source, ICatalog<TViewData> target)
            : base(source, target)
        {
        }

        /// <summary>
        /// Deletes an object from the target collection,
        /// which matches the key of the source data object.
        /// </summary>
        public override void Run()
        {
            Target.Delete(Source.DataObject.Key);
        }
    }
}