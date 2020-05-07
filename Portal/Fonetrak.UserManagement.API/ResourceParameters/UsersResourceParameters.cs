using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fonetrak.UserManagement.API.ResourceParameters
{
    public class UsersResourceParameters
    {
        private const int MaxPageSize = 20;

        public int PageNumber { get; set; } = 1;

        private int _pageSize = 10;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
    }
}
