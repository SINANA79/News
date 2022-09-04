using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsProject.Core.Domain.Entities
{
    public class Tag
    {
        public Guid TagId { get; set; }
        public string TagName { get; set; }
        public News News { get; set; }
    }
}
