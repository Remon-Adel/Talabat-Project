﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Specifications
{
    public class ProductSpecParams
    {
        private const int MaxPageSize = 10;
        public int PageIndex { get; set; } = 1;

        private int pageSize = 5;

        private string search;

        public string Search
        {
            get { return search; }
            set { search = value.ToLower(); }
        }


        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value; }
        }


        public string Sort { get; set; }
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }

    }
}
