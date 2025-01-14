﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Linq.Dynamic.Core;

namespace Core.Persistance.Dynamic.ExtensionsIoC
{
    public static class IQureyableDynamicFilterExtensions
    {
        private static readonly string [] _orders = { "asc", "desc" };
        private static readonly string [] _logics = { "and", "or" };
        

        private static readonly IDictionary<string, string>
       Operators = new Dictionary<string, string>
       {
            { "eq", "=" },
            { "neq", "!=" },
            { "lt", "<" },
            { "lte", "<=" },
            { "gt", ">" },
            { "gte", ">=" },
            { "isnull", "== null" },
            { "isnotnull", "!= null" },
            { "startswith", "StartsWith" },
            { "endswith", "EndsWith" },
            { "contains", "Contains" },
            { "doesnotcontain", "Contains" }
       };

        public static IQueryable<T> ToDynamic<T>(
            this IQueryable<T> query, DynamicQuery dynamic)
        {
            if (dynamic.Filter is not null) query = Filter(query, dynamic.Filter);
            if (dynamic.Sorts is not null && dynamic.Sorts.Any()) query = Sort(query, dynamic.Sorts);
            return query;
        }

        private static IQueryable<T> Filter<T>(
            IQueryable<T> queryable, Filter filter)
        {
            IList<Filter> filters = GetAllFilters(filter);
            string?[] values = filters.Select(f => f.Value).ToArray();
            string where = Transform(filter, filters);
            queryable = queryable.Where(where, values);

            return queryable;
        }

      

        private static IQueryable<T> Sort<T>(
            IQueryable<T> queryable, IEnumerable<Sort> sort)
        {
            if (sort.Any())
            {
                string ordering = string.Join(",", sort.Select(s => $"{s.Field} {s.Asc_Desc}"));
                return queryable.OrderBy(ordering);    
            }

            return queryable;
        }

        public static IList<Filter> GetAllFilters(Filter filter)
        {
            List<Filter> filters = new();
            GetFilters(filter, filters);
            return filters;
        }

        private static void GetFilters(Filter filter, IList<Filter> filters)
        {
            filters.Add(filter);
            if (filter.Filters is not null && filter.Filters.Any())
                foreach (Filter item in filter.Filters)
                    GetFilters(item, filters);
        }

        public static string Transform(Filter filter, IList<Filter> filters)
        {
            int index = filters.IndexOf(filter);
            string comparison = Operators[filter.Operator];
            StringBuilder where = new();

            if (!string.IsNullOrEmpty(filter.Value))
            {
                if (filter.Operator == "doesnotcontain")
                    where.Append($"(!np({filter.Field}).{comparison}(@{index}))");
                else if (comparison == "StartsWith" ||
                         comparison == "EndsWith" ||
                         comparison == "Contains")
                    where.Append($"(np({filter.Field}).{comparison}(@{index}))");
                else
                    where.Append($"np({filter.Field}) {comparison} @{index}");
            }
            else if (filter.Operator == "isnull" || filter.Operator == "isnotnull")
            {
                where.Append($"np({filter.Field}) {comparison}");
            }

            if (filter.Logic is not null && filter.Filters is not null && filter.Filters.Any())
                return
                    $"{where} {filter.Logic} ({string.Join($" {filter.Logic} ", filter.Filters.Select(f => Transform(f, filters)).ToArray())})";

            return where.ToString();
        }
    }
}
