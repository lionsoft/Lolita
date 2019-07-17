using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace Pomelo.EntityFrameworkCore.Lolita.Update
{
    public class DefaultLolitaUpdateExecutor : ILolitaUpdateExecutor
    {
        public IQueryModelGenerator QueryModelGenerator { get; }

        public DefaultLolitaUpdateExecutor(IQueryModelGenerator queryModelGenerator, ISqlGenerationHelper SqlGenerationHelper/*, ICurrentDbContext CurrentDbContext, IDbSetFinder DbSetFinder*/)
        {
            QueryModelGenerator = queryModelGenerator;
            sqlGenerationHelper = SqlGenerationHelper;
/*
            dbSetFinder = DbSetFinder;
            context = CurrentDbContext.Context;
*/
        }

        private readonly ISqlGenerationHelper sqlGenerationHelper;
/*
        private IDbSetFinder dbSetFinder;
        private DbContext context;
*/

        public virtual string GenerateSql<TEntity>(LolitaSetting<TEntity> lolita, RelationalQueryModelVisitor visitor) where TEntity : class, new()
        {
            var sb = new StringBuilder("UPDATE ");
            sb.Append(lolita.FullTable)
                .AppendLine()
                .Append("SET ")
                .Append(string.Join($", { Environment.NewLine }    ", lolita.Operations))
                .AppendLine()
                .Append(ParseWhere(visitor, lolita.ShortTable))
                .Append(sqlGenerationHelper.StatementTerminator);

            return sb.ToString();
        }

        protected virtual string ParseWhere(RelationalQueryModelVisitor visitor, string Table)
        {
            if (visitor == null || visitor.Queries.Count == 0)
                return "";
            var sql = visitor.Queries.First().ToString();
            var pos = sql.IndexOf("WHERE", StringComparison.Ordinal);
            if (pos < 0)
                return "";
            return sql.Substring(pos)
                .Replace(sqlGenerationHelper.DelimitIdentifier(visitor.CurrentParameter.Name), Table);
        }

        public virtual int Execute(DbContext db, string sql, object[] param)
        {
            return db.Database.ExecuteSqlCommand(sql, param);
        }

        public Task<int> ExecuteAsync(DbContext db, string sql, CancellationToken cancellationToken = default, params object[] param)
        {
            return db.Database.ExecuteSqlCommandAsync(sql, param, cancellationToken);
        }
    }
}
