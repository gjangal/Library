﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models.Catalog
{
    public class AssetIndexListingModel
    {
        public int Id { get; set; }

        public string ImageUrl { get; set; }
        public string AuthorOrDirector { get; set; }
        public int NumberOfCopies { get; set; }
        public string Type { get; set; }
        public string DewNumber { get; set; }
        public string Title { get; set; }
    }
}