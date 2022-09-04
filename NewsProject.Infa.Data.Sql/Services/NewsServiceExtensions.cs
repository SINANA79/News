using NewsProject.Core.Domain.Dtos.News;
using System.Reflection;
using System.Text;
using System.Linq.Dynamic.Core;

namespace NewsProject.Infa.Data.Sql.Services
{
    public static class NewsServiceExtensions
    {
        public static IQueryable<NewsWithCategoryDto> Search(this IQueryable<NewsWithCategoryDto> newses, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return newses;
            var lowerCaseSearchTerm = searchTerm.Trim().ToLower();
            return newses.Where(p => p.Title.ToLower().Contains(lowerCaseSearchTerm));
        }

        public static IQueryable<NewsWithCategoryDto> Sort(this IQueryable<NewsWithCategoryDto> newses, string orderByQueryString)
        {
            if (!newses.Any())
                return newses;

            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return newses.OrderBy(x => x.Title);

            var orderParams = orderByQueryString.Trim().Split(',');
            var propertyInfos = typeof(NewsWithCategoryDto).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var orderQueryBuilder = new StringBuilder();

            foreach (var param in orderParams)
            {
                if (string.IsNullOrWhiteSpace(param))
                    continue;

                var propertyFromQueryName = param.Split(" ")[0];
                var objectProperty = propertyInfos.FirstOrDefault(pi => pi.Name.Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase));

                if (objectProperty == null)
                    continue;

                var direction = param.EndsWith(" desc") ? "descending" : "ascending";
                orderQueryBuilder.Append($"{objectProperty.Name} {direction}, ");
            }

            var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');
            if (string.IsNullOrWhiteSpace(orderQuery))
                return newses.OrderBy(e => e.Title);

            return newses.OrderBy(orderQuery);
        }
    }
}

