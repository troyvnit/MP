using System;
using MP.Model.Models;

namespace MP.Model.SearchModels
{
    public class ItemSearchModel
    {
        public DateTime fromDate { get; set; }
        public DateTime toDate { get; set; }
        public DepartureTime fromTime { get; set; }
        public DepartureTime toTime { get; set; }
        public TripName TripName { get; set; }
        public int skip { get; set; }
        public int take { get; set; }
    }
}