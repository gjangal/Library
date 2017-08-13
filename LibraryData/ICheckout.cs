using LibraryData.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryData
{
    public interface ICheckout
    {
        IEnumerable<Checkout> GetAll();
        Checkout GetById(int id);

        void Add(Checkout newCheckout);
        void CheckInItem(int assetId, int libraryCardId);
        void CheckOutItem(int assetId, int libraryCardId);

        IEnumerable<CheckoutHistory> GetCheckoutHistory(int assetId);

        //Hold Actions
        void PlaceHold(int assetId, int libraryCardId);
        string GetCurrentHoldPatronName(int id);
        DateTime GetCurrentHoldPlaced(int id);
        IEnumerable<Hold> GetCurrentHolds(int id);
        Checkout GetLatestCheckout(int id);

        void MarkLost(int assetId);
        void MarkFound(int assetId);
    }
}
