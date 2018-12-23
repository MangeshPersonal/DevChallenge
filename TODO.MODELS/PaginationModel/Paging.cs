using System;
using System.Collections.Generic;
using System.Text;

namespace TODO.MODELS.PaginationModel
{
    public class Paging
    {
        const int maxPageSize = 2000;
        private int _pageSize { get; set; } = 10;
        public int pageNumber { get; set; } = 1;
        public int pageSize
        {
            get { return _pageSize; }
            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }
    }


}

