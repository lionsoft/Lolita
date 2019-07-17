﻿using System;
using Microsoft.EntityFrameworkCore.Storage;
using Pomelo.EntityFrameworkCore.Lolita;
using Pomelo.EntityFrameworkCore.Lolita.Update;

// ReSharper disable once CheckNamespace
namespace Microsoft.EntityFrameworkCore
{
    public static class ValuingExtensions
    {
        public static LolitaSetting<TEntity> WithSQL<TEntity, TProperty>(this LolitaValuing<TEntity, TProperty> self, Func<string, ISqlGenerationHelper, string> sql, params object[] parameters)
            where TEntity : class, new()
        {
            if (sql == null)
            {
                throw new ArgumentNullException(nameof(sql), "Sql expression cannot be null.");
            }

            var field = self.CurrentField;
            var sqlGenerationHelper = self.Inner.GetService<ISqlGenerationHelper>();
            var _sql = sql(field, sqlGenerationHelper);

            var paramCnt = self.Inner.Parameters.Count;
            if (parameters != null && parameters.Length > 0)
            {
                for (var i = 0; i < parameters.Length; i++)
                {
                    _sql = _sql.Replace("{" + i + "}", "{" + (i + paramCnt) + "}");
                    self.Inner.Parameters.Add(parameters[i]);
                }
            }
            var field_info = new SetFieldInfo { Field = self.CurrentField, Index = -1, Type = "WithSQL", Value = _sql };
            var factory = self.GetService<ISetFieldSqlGenerator>();
            self.Inner.Operations.Add(factory.TranslateToSql(field_info));
            return self.Inner;
        }

        public static LolitaSetting<TEntity> WithSQL<TEntity, TProperty>(this LolitaValuing<TEntity, TProperty> self, Func<string, string> sql, params object[] parameters)
            where TEntity : class, new()
        {
            return self.WithSQL((x, y) => sql(x));
        }
        private static LolitaSetting<TEntity> valuing<TEntity, TProperty>(this LolitaValuing<TEntity, TProperty> self, string type, object value)
            where TEntity : class, new()
        {
            var factory = self.GetService<ISetFieldSqlGenerator>();
            self.Inner.Parameters.Add(value);
            var sql = factory.TranslateToSql(new SetFieldInfo { Field = self.CurrentField, Index = self.Inner.Parameters.Count - 1, Type = type, Value = value });
            self.Inner.Operations.Add(sql);
            return self.Inner;
        }

        public static LolitaSetting<TEntity> WithValue<TEntity, TProperty>(this LolitaValuing<TEntity, TProperty> self, object value)
            where TEntity : class, new()
            => self.valuing("WithValue", value);

        public static LolitaSetting<TEntity> Plus<TEntity>(this LolitaValuing<TEntity, long> self, object value)
            where TEntity : class, new()
            => self.valuing("Plus", value);

        public static LolitaSetting<TEntity> Subtract<TEntity>(this LolitaValuing<TEntity, long> self, object value)
            where TEntity : class, new()
            => self.valuing("Subtract", value);

        public static LolitaSetting<TEntity> Multiply<TEntity>(this LolitaValuing<TEntity, long> self, object value)
            where TEntity : class, new()
            => self.valuing("Multiply", value);

        public static LolitaSetting<TEntity> Divide<TEntity>(this LolitaValuing<TEntity, long> self, object value)
            where TEntity : class, new()
            => self.valuing("Divide", value);

        public static LolitaSetting<TEntity> Mod<TEntity>(this LolitaValuing<TEntity, long> self, object value)
            where TEntity : class, new()
            => self.valuing("Mod", value);

        public static LolitaSetting<TEntity> Plus<TEntity>(this LolitaValuing<TEntity, int> self, object value)
            where TEntity : class, new()
            => self.valuing("Plus", value);

        public static LolitaSetting<TEntity> Subtract<TEntity>(this LolitaValuing<TEntity, int> self, object value)
            where TEntity : class, new()
            => self.valuing("Subtract", value);

        public static LolitaSetting<TEntity> Multiply<TEntity>(this LolitaValuing<TEntity, int> self, object value)
            where TEntity : class, new()
            => self.valuing("Multiply", value);

        public static LolitaSetting<TEntity> Divide<TEntity>(this LolitaValuing<TEntity, int> self, object value)
            where TEntity : class, new()
            => self.valuing("Divide", value);

        public static LolitaSetting<TEntity> Mod<TEntity>(this LolitaValuing<TEntity, int> self, object value)
            where TEntity : class, new()
            => self.valuing("Mod", value);

        public static LolitaSetting<TEntity> Plus<TEntity>(this LolitaValuing<TEntity, short> self, object value)
            where TEntity : class, new()
            => self.valuing("Plus", value);

        public static LolitaSetting<TEntity> Subtract<TEntity>(this LolitaValuing<TEntity, short> self, object value)
            where TEntity : class, new()
            => self.valuing("Subtract", value);

        public static LolitaSetting<TEntity> Multiply<TEntity>(this LolitaValuing<TEntity, short> self, object value)
            where TEntity : class, new()
            => self.valuing("Multiply", value);

        public static LolitaSetting<TEntity> Divide<TEntity>(this LolitaValuing<TEntity, short> self, object value)
            where TEntity : class, new()
            => self.valuing("Divide", value);

        public static LolitaSetting<TEntity> Mod<TEntity>(this LolitaValuing<TEntity, short> self, object value)
            where TEntity : class, new()
            => self.valuing("Mod", value);

        public static LolitaSetting<TEntity> Plus<TEntity>(this LolitaValuing<TEntity, ulong> self, object value)
            where TEntity : class, new()
            => self.valuing("Plus", value);

        public static LolitaSetting<TEntity> Subtract<TEntity>(this LolitaValuing<TEntity, ulong> self, object value)
            where TEntity : class, new()
            => self.valuing("Subtract", value);

        public static LolitaSetting<TEntity> Multiply<TEntity>(this LolitaValuing<TEntity, ulong> self, object value)
            where TEntity : class, new()
            => self.valuing("Multiply", value);

        public static LolitaSetting<TEntity> Divide<TEntity>(this LolitaValuing<TEntity, ulong> self, object value)
            where TEntity : class, new()
            => self.valuing("Divide", value);

        public static LolitaSetting<TEntity> Mod<TEntity>(this LolitaValuing<TEntity, ulong> self, object value)
            where TEntity : class, new()
            => self.valuing("Mod", value);

        public static LolitaSetting<TEntity> Plus<TEntity>(this LolitaValuing<TEntity, uint> self, object value)
            where TEntity : class, new()
            => self.valuing("Plus", value);

        public static LolitaSetting<TEntity> Subtract<TEntity>(this LolitaValuing<TEntity, uint> self, object value)
            where TEntity : class, new()
            => self.valuing("Subtract", value);

        public static LolitaSetting<TEntity> Multiply<TEntity>(this LolitaValuing<TEntity, uint> self, object value)
            where TEntity : class, new()
            => self.valuing("Multiply", value);

        public static LolitaSetting<TEntity> Divide<TEntity>(this LolitaValuing<TEntity, uint> self, object value)
            where TEntity : class, new()
            => self.valuing("Divide", value);

        public static LolitaSetting<TEntity> Mod<TEntity>(this LolitaValuing<TEntity, uint> self, object value)
            where TEntity : class, new()
            => self.valuing("Mod", value);

        public static LolitaSetting<TEntity> Plus<TEntity>(this LolitaValuing<TEntity, ushort> self, object value)
            where TEntity : class, new()
            => self.valuing("Plus", value);

        public static LolitaSetting<TEntity> Subtract<TEntity>(this LolitaValuing<TEntity, ushort> self, object value)
            where TEntity : class, new()
            => self.valuing("Subtract", value);

        public static LolitaSetting<TEntity> Multiply<TEntity>(this LolitaValuing<TEntity, ushort> self, object value)
            where TEntity : class, new()
            => self.valuing("Multiply", value);

        public static LolitaSetting<TEntity> Divide<TEntity>(this LolitaValuing<TEntity, ushort> self, object value)
            where TEntity : class, new()
            => self.valuing("Divide", value);

        public static LolitaSetting<TEntity> Mod<TEntity>(this LolitaValuing<TEntity, ushort> self, object value)
            where TEntity : class, new()
            => self.valuing("Mod", value); public static LolitaSetting<TEntity> Plus<TEntity>(this LolitaValuing<TEntity, double> self, object value)
             where TEntity : class, new()
             => self.valuing("Plus", value);

        public static LolitaSetting<TEntity> Subtract<TEntity>(this LolitaValuing<TEntity, double> self, object value)
            where TEntity : class, new()
            => self.valuing("Subtract", value);

        public static LolitaSetting<TEntity> Multiply<TEntity>(this LolitaValuing<TEntity, double> self, object value)
            where TEntity : class, new()
            => self.valuing("Multiply", value);

        public static LolitaSetting<TEntity> Divide<TEntity>(this LolitaValuing<TEntity, double> self, object value)
            where TEntity : class, new()
            => self.valuing("Divide", value);

        public static LolitaSetting<TEntity> Mod<TEntity>(this LolitaValuing<TEntity, double> self, object value)
            where TEntity : class, new()
            => self.valuing("Mod", value); public static LolitaSetting<TEntity> Plus<TEntity>(this LolitaValuing<TEntity, float> self, object value)
             where TEntity : class, new()
             => self.valuing("Plus", value);

        public static LolitaSetting<TEntity> Subtract<TEntity>(this LolitaValuing<TEntity, float> self, object value)
            where TEntity : class, new()
            => self.valuing("Subtract", value);

        public static LolitaSetting<TEntity> Multiply<TEntity>(this LolitaValuing<TEntity, float> self, object value)
            where TEntity : class, new()
            => self.valuing("Multiply", value);

        public static LolitaSetting<TEntity> Divide<TEntity>(this LolitaValuing<TEntity, float> self, object value)
            where TEntity : class, new()
            => self.valuing("Divide", value);

        public static LolitaSetting<TEntity> Mod<TEntity>(this LolitaValuing<TEntity, float> self, object value)
            where TEntity : class, new()
            => self.valuing("Mod", value);

        public static LolitaSetting<TEntity> Append<TEntity>(this LolitaValuing<TEntity, string> self, string value)
            where TEntity : class, new()
            => self.valuing("Append", value);

        public static LolitaSetting<TEntity> Prepend<TEntity>(this LolitaValuing<TEntity, string> self, string value)
            where TEntity : class, new()
            => self.valuing("Prepend", value);

        public static LolitaSetting<TEntity> AddMilliseconds<TEntity>(this LolitaValuing<TEntity, DateTime> self, int value)
            where TEntity : class, new()
            => self.valuing("AddMilliseconds", value);

        public static LolitaSetting<TEntity> AddSeconds<TEntity>(this LolitaValuing<TEntity, DateTime> self, int value)
            where TEntity : class, new()
            => self.valuing("AddSeconds", value);

        public static LolitaSetting<TEntity> AddMinutes<TEntity>(this LolitaValuing<TEntity, DateTime> self, int value)
            where TEntity : class, new()
            => self.valuing("AddMinutes", value);

        public static LolitaSetting<TEntity> AddHours<TEntity>(this LolitaValuing<TEntity, DateTime> self, int value)
            where TEntity : class, new()
            => self.valuing("AddHours", value);

        public static LolitaSetting<TEntity> AddDays<TEntity>(this LolitaValuing<TEntity, DateTime> self, int value)
            where TEntity : class, new()
            => self.valuing("AddDays", value);

        public static LolitaSetting<TEntity> AddMonths<TEntity>(this LolitaValuing<TEntity, DateTime> self, int value)
            where TEntity : class, new()
            => self.valuing("AddMonths", value);

        public static LolitaSetting<TEntity> AddYears<TEntity>(this LolitaValuing<TEntity, DateTime> self, int value)
            where TEntity : class, new()
            => self.valuing("AddYears", value);
    }
}
