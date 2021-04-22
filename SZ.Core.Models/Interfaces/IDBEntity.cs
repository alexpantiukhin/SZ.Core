using System;

namespace SZ.Core.Models.Interfaces
{ 
    public interface IDBEntity
    {
        Guid Id { get; set; }
        int ShowId { get; set; }
    }
}
