using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WhiskyArchive.Services.WhiskyRecording.Domain.SeedWork;

namespace WhiskyArchive.Services.WhiskyRecording.Domain.Model.Whiskys
{
    public class Currency
    : Enumeration
    {
        public static Currency GBP = new Currency(1, nameof(GBP).ToLowerInvariant());
        public static Currency EUR = new Currency(2, nameof(EUR).ToLowerInvariant());
        public static Currency USD = new Currency(3, nameof(USD).ToLowerInvariant());
        public static Currency TWD = new Currency(4, nameof(TWD).ToLowerInvariant());
        public static Currency RMB = new Currency(5, nameof(RMB).ToLowerInvariant());
        public static Currency YEN = new Currency(6, nameof(YEN).ToLowerInvariant());


        protected Currency()
        {
        }

        public Currency(int id, string name)
            : base(id, name)
        {
        }

        public static IEnumerable<Currency> List() =>
            new[] { GBP, EUR, USD, TWD, RMB, YEN };

        public static Currency FromName(string name)
        {
            var state = List()
                .SingleOrDefault(s => String.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (state == null)
            {
                throw new Exception($"Possible values for Currency: {String.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }

        public static Currency From(int id)
        {
            var state = List().SingleOrDefault(s => s.Id == id);

            if (state == null)
            {
                throw new Exception($"Possible values for Currency: {String.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }
    }
}
