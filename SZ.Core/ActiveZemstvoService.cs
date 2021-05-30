using System;

using SZ.Core.Abstractions.Interfaces;

namespace SZ.Core
{
    public class ActiveZemstvoService : IActiveZemstvoService
    {
        public Guid? ActiveZemstvo { get; set; }
    }
}
