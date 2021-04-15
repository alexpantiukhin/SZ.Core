//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Diagnostics.CodeAnalysis;
//using System.Linq;
//using System.Linq.Expressions;
//using System.Threading;
//using System.Threading.Tasks;

//using SZ.Core.Abstractions.Interfaces;
//using SZ.Models.Interfaces;

//namespace SZ.Test.TestData
//{
//    public class TestDBSet<T, TEntity> : IDBSet<T>
//        where TEntity : class
//        where T: class
//    {
//        List<T> _list;
//        public TestDBSet(List<TEntity> list)
//        {
//            _list = list.Select(x => x as T).ToList();
//        }

//        public Type ElementType => typeof(T);
//        public Expression Expression => _list.AsQueryable().Expression;
//        public IQueryProvider Provider => _list.AsQueryable().Provider;

//        public async Task<T> AddAsync([NotNull] T item, CancellationToken cancellationToken = default)
//        {
//            _list.Add(item);

//            return item;
//        }

//        public async Task AddRangeAsync([NotNull] IEnumerable<T> items, CancellationToken cancellationToken = default)
//        {
//            _list.AddRange(items);
//        }

//        public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression = null, CancellationToken cancellationToken = default)
//        {
//            return _list.Any(expression.Compile());
//        }

//        public async ValueTask<T> FindAsync([NotNull] object key, CancellationToken cancellationToken = default)
//        {
//            return _list.First(x => (x as IDBEntity).Id == (Guid)key);
//        }

//        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> expression = null, CancellationToken cancellationToken = default)
//        {
//            return _list.FirstOrDefault(expression.Compile());
//        }

//        public IEnumerator<T> GetEnumerator()
//        {
//            return _list.GetEnumerator();
//        }

//        public async Task RemoveAsync([NotNull] T item, CancellationToken cancellationToken = default)
//        {
//            _list.Remove(item);
//        }

//        public async Task RemoveRangeAsync([NotNull] IEnumerable<T> items, CancellationToken cancellationToken = default)
//        {
//            foreach (var item in items)
//            {
//                _list.Remove(item);
//            }
//        }

//        IEnumerator IEnumerable.GetEnumerator()
//        {
//            return _list.GetEnumerator();
//        }
//    }
//}
