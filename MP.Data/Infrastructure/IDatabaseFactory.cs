using System;

namespace MP.Data.Infrastructure
{
    public interface IDatabaseFactory : IDisposable
    {
        MPEntities Get();
    }
}
