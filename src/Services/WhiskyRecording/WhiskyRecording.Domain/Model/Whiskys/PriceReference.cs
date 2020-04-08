using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WhiskyArchive.Services.WhiskyRecording.Domain.SeedWork;

namespace WhiskyArchive.Services.WhiskyRecording.Domain.Model.Whiskys
{
    public class PriceReference
  : Enumeration
    {
        public static PriceReference FacebookPublic = new PriceReference(1, nameof(FacebookPublic).ToLowerInvariant());
        public static PriceReference FacebookPrivate = new PriceReference(2, nameof(FacebookPrivate).ToLowerInvariant());
        public static PriceReference FacebookAuction = new PriceReference(3, nameof(FacebookAuction).ToLowerInvariant());
        public static PriceReference EuropeanAuction = new PriceReference(4, nameof(EuropeanAuction).ToLowerInvariant());
        public static PriceReference ChineseAuction = new PriceReference(5, nameof(ChineseAuction).ToLowerInvariant());
        public static PriceReference JapaneseAuction = new PriceReference(6, nameof(JapaneseAuction).ToLowerInvariant());
        public static PriceReference Other = new PriceReference(7, nameof(Other).ToLowerInvariant());


        protected PriceReference()
        {
        }

        public PriceReference(int id, string name)
            : base(id, name)
        {
        }

        public static IEnumerable<PriceReference> List() =>
            new[] { FacebookPublic, FacebookPrivate, FacebookAuction, EuropeanAuction, ChineseAuction, JapaneseAuction, Other };

        public static PriceReference FromName(string name)
        {
            var state = List()
                .SingleOrDefault(s => String.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (state == null)
            {
                throw new Exception($"Possible values for PriceReference: {String.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }

        public static PriceReference From(int id)
        {
            var state = List().SingleOrDefault(s => s.Id == id);

            if (state == null)
            {
                throw new Exception($"Possible values for PriceReference: {String.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }
    }
}
