using LibraryData;
using System;
using LibraryData.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace LibraryServices
{
    public class LibraryAssetService : ILibraryAsset
    {
        private LibraryContext _context;

        public LibraryAssetService(LibraryContext context)
        {
            _context = context;
        }

        public void Add(LibraryAsset asset)
        {
            _context.Add(asset);
            _context.SaveChanges();
        }

        public IEnumerable<LibraryAsset> GetAll()
        {
            return _context.LibraryAssets
                .Include(asset=>asset.Status)
                .Include(asset=>asset.Location);
        }


        public LibraryAsset GetById(int id)
        {
            return _context.LibraryAssets
                .Include(asset=>asset.Location)
                .Include(asset=>asset.Status)
                .FirstOrDefault(asset => asset.Id == id);
        }

        public LibraryBranch GetCurrentLocation(int id)
        {
            return GetById(id).Location;
        }

        public string GetDeweyIndex(int id)
        {
            var book = _context.LibraryAssets
                          .OfType<Book>()
                          .Where(b => b.Id == id)
                          .FirstOrDefault();

            if (book is null)
            {
                return "";
            }

            return book.DeweyIndex;
        }

        public string GetIsbn(int id)
        {
            var book = _context.LibraryAssets
                          .OfType<Book>()
                          .Where(b => b.Id == id)
                          .FirstOrDefault();

            if (book is null)
            {
                return "";
            }

            return book.ISBN;

        }

        public string GetTitle(int id)
        {
            return _context.LibraryAssets
                          .Where(b => b.Id == id)
                          .FirstOrDefault().Title;
        }

        public string GetType(int id)
        {
            var book = _context.LibraryAssets.OfType<Book>().Where(b => b.Id == id);

            return book.Any() ? "Book" : "Video";
        }

        public string GetAuthorOrDirector(int id)
        {
            var isBook = _context.LibraryAssets
              .OfType<Book>()
              .Where(b => b.Id == id).Any();

            var isVideo = _context.LibraryAssets
              .OfType<Video>()
              .Where(b => b.Id == id).Any();

            return isBook ? _context.Books.FirstOrDefault(book => book.Id == id).Author 
                : _context.Videos.FirstOrDefault(video => video.Id == id).Director ?? "Unknown";

        }

    }
}
