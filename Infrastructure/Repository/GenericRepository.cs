using Domain.Entities.Common;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;
using Dapper;  // اضافه کردن Dapper برای استفاده از Query

namespace Infrastructure.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly SQLiteConnection _connection;

        public GenericRepository(string connectionString)
        {
            _connection = new SQLiteConnection(connectionString);
            _connection.Open();
        }

        public IQueryable<TEntity> GetEntitiesQuery()
        {
            // استفاده از Dapper برای اجرای کوئری
            // استفاده از Dapper برای اجرای کوئری
            var query = _connection.Query<TEntity>("SELECT * FROM " + typeof(TEntity).Name).ToList();
            return query.AsQueryable();  // اگر واقعا نیاز دارید که داده‌ها را به صورت IQueryable بازگردانید

        }

        public async Task<TEntity> GetEntityById(long entityId)
        {
            // استفاده از Dapper برای اجرای کوئری
            var query = "SELECT * FROM " + typeof(TEntity).Name + " WHERE Id = @Id";
            return await _connection.QueryFirstOrDefaultAsync<TEntity>(query, new { Id = entityId });
        }

        public void Dispose()
        {
            _connection?.Dispose();
        }
    }
}
