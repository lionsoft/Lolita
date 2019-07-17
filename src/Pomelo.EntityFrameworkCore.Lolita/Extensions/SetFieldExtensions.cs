﻿using System;
using System.Linq;
using System.Linq.Expressions;
using Pomelo.EntityFrameworkCore.Lolita;
using Pomelo.EntityFrameworkCore.Lolita.Update;

// ReSharper disable once CheckNamespace
namespace Microsoft.EntityFrameworkCore
{
    public static class DbSetExtensions
    {
        public static LolitaValuing<TEntity, TProperty> SetField<TEntity, TProperty>(this IQueryable<TEntity> self, Expression<Func<TEntity, TProperty>> SetValueExpression)
            where TEntity : class, new()
        {
            if (SetValueExpression == null)
                throw new ArgumentNullException(nameof(SetValueExpression));

            var factory = self.GetService<IFieldParser>();
            var sqlfield = factory.VisitField(SetValueExpression);

            var inner = new LolitaSetting<TEntity> { Query = self, FullTable = factory.ParseFullTable(sqlfield), ShortTable = factory.ParseShortTable(sqlfield)};
            return new LolitaValuing<TEntity, TProperty> { Inner = inner, CurrentField = factory.ParseField(sqlfield) };
        }

        public static LolitaValuing<TEntity, TProperty> SetField<TEntity, TProperty>(this LolitaSetting<TEntity> self, Expression<Func<TEntity, TProperty>> SetValueExpression)
            where TEntity : class, new()
        {
            if (SetValueExpression == null)
                throw new ArgumentNullException(nameof(SetValueExpression));

            var factory = self.GetService<IFieldParser>();
            var sqlfield = factory.VisitField(SetValueExpression);

            return new LolitaValuing<TEntity, TProperty> { Inner = self, CurrentField = factory.ParseField(sqlfield) };
        }
    }
}
