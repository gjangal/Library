using LibraryData;
using System;
using System.Collections.Generic;
using System.Text;
using LibraryData.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace LibraryServices
{
    public class CheckoutService : ICheckout
    {
        private readonly LibraryContext _context;

        public CheckoutService(LibraryContext context)
        {
            _context = context; 
        }

        public void Add(Checkout newCheckout)
        {
            _context.Add(newCheckout);
            _context.SaveChanges();
        }

        public void CheckInItem(int assetId, int libraryCardId)
        {
            throw new NotImplementedException();
        }

        public void CheckOutItem(int assetId, int libraryCardId)
        {
            
        }

        public IEnumerable<Checkout> GetAll()
        {
            return _context.Checkouts;
        }

        public Checkout GetById(int id)
        {
            return _context.Checkouts.FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<CheckoutHistory> GetCheckoutHistory(int assetId)
        {
            return _context.CheckoutHistories
                .Include(c=>c.LibraryAsset)
                .Include(c=>c.LibraryCard)
                .Where(c => c.LibraryAsset.Id == assetId);
        }

        public string GetCurrentHoldPatronName(int id)
        {
            throw new NotImplementedException();
        }

        public DateTime GetCurrentHoldPlaced(int id)
        {
            return GetCurrentHolds(id).FirstOrDefault().HoldPlaced;
        }

        public IEnumerable<Hold> GetCurrentHolds(int id)
        {
            return _context.Holds
                .Include(h=>h.LibraryAsset)
                .Include(h=>h.LibraryCard)
                .Where(h => h.LibraryAsset.Id == id);
        }

        public Checkout GetLatestCheckout(int id)
        {
            return _context.Checkouts
                .Where(c=>c.LibraryAsset.Id == id)
                .OrderByDescending(c=>c.Since).FirstOrDefault();
        }

        public void MarkFound(int assetId)
        {
            var item = _context.LibraryAssets
                .FirstOrDefault(a => a.Id == assetId);

            _context.Update(item);
            item.Status = _context.Statuses
                .FirstOrDefault(s => s.Name == "Lost");

            _context.SaveChanges();
        }

        public void MarkLost(int assetId)
        {
            var item = _context.LibraryAssets
                .FirstOrDefault(a => a.Id == assetId);

            _context.Update(item);

            item.Status = _context.Statuses
                .FirstOrDefault(s => s.Name == "Available");

            var checkout = _context.Checkouts.FirstOrDefault(c => c.LibraryAsset.Id == assetId);

            if(checkout != null)
            {
                _context.Remove(checkout);
            }

            _context.SaveChanges();

        }

        public void PlaceHold(int assetId, int libraryCardId)
        {
            throw new NotImplementedException();
        }
    }
}
