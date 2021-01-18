using System.Collections.Generic;

namespace Altkom.Orange.IServices
{
    public interface IEntityService<TEntity>
    {
        IEnumerable<TEntity> Get();
        TEntity Get(int id);
        void Add(TEntity entity);
        void Udpate(TEntity entity);
        void Remove(int id);
    }
}
