using System.Linq.Expressions;

namespace SchoolNexAPI.Extensions
{
    public static class SchoolQueryableExtensions
    {
        public static IQueryable<T> FilterBySchool<T>(this IQueryable<T> query, Guid schoolId, bool isSuperAdmin)
        {
            if (!isSuperAdmin && schoolId != Guid.Empty)
            {
                var param = Expression.Parameter(typeof(T), "e");
                var property = Expression.Property(param, "SchoolId");

                // Match property type (Guid? or Guid)
                var constant = Expression.Constant(schoolId, property.Type);

                var equal = Expression.Equal(property, constant);
                var lambda = Expression.Lambda<Func<T, bool>>(equal, param);

                query = query.Where(lambda);
            }
            return query;
        }
    }
}
