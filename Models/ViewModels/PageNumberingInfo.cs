using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BowlingLeague.Models.ViewModels
{
    public class PageNumberingInfo
    {
        // Page numbering class.
        public int NumberItemsPerPage { get; set; }
        public int CurrentPage { get; set; }
        public int TotalNumItems { get; set; }

        public int NumPages => (int) (Math.Ceiling((decimal) TotalNumItems / NumberItemsPerPage));
    }
}
