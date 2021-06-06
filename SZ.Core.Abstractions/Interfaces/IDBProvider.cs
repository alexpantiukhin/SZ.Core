using Microsoft.EntityFrameworkCore;

using System;

namespace SZ.Core.Abstractions.Interfaces
{
    public interface IDBProvider<TDB> : IDisposable, IAsyncDisposable
        where TDB : DbContext
    {
        TDB DB { get; }
    }
}
