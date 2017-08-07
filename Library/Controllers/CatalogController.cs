using Library.Models.Catalog;
using LibraryData;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Controllers
{
    public class CatalogController:Controller
    {
        private ILibraryAsset _assets;
        public CatalogController(ILibraryAsset libraryAsset)
        {
            _assets = libraryAsset;
        }

        public IActionResult Index()
        {
            var assetModels = _assets.GetAll();
            var listingModel = assetModels.Select(a => new AssetIndexListingModel()
            {
                Id = a.Id,
                ImageUrl = a.ImageUrl,
                AuthorOrDirector = _assets.GetAuthorOrDirector(a.Id),
                Type = _assets.GetType(a.Id),
                DewNumber = _assets.GetDeweyIndex(a.Id),
                Title = a.Title
            });

            return View(new AssetIndexModel() { Assets = listingModel});
        }
    }
}
