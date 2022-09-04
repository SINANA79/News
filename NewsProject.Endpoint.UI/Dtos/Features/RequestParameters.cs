using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsProject.Endpoint.UI.Dtos.Features
{
    public class RequestParameters
    {
        const int maxPageSize = 50;
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 10;
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }
        public string? OrderBy { get; set; }
    }
    public class NewsParameters : RequestParameters
    {
        public DateTime? MinDate { get; set; }
        public DateTime? MaxDate { get; set; } = DateTime.Now;
        public bool ValidYearRange => MaxDate > MinDate;
        public string? Title { get; set; }
        public string? SearchTerm { get; set; }
    }

    public class LatestNewsParameters : RequestParameters
    {
        public string? SearchTerm { get; set; }
    }

    public class CommentsParameters : RequestParameters
    {
        public string? SearchTerm { get; set; }
    }

}
