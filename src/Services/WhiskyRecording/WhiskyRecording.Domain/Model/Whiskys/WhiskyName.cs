using System;
using System.Collections.Generic;
using System.Text;
using WhiskyArchive.Services.WhiskyRecording.Domain.SeedWork;

namespace WhiskyArchive.Services.WhiskyRecording.Domain.Model.Whiskys
{
    public class WhiskyName : ValueObject
    {
        public string Chinese { get; private set; }
        public string English { get; private set; }

        public WhiskyName(string chinese, string english)
        {
            this.Chinese = chinese ?? throw new ArgumentNullException(nameof(chinese));
            this.English = english ?? throw new ArgumentNullException(nameof(english));
        }

        public WhiskyName Update(string chinese = null, string english = null)
        {
            return new WhiskyName(chinese ?? this.Chinese, english ?? this.English);
        }
        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return this.Chinese;
            yield return this.English;
        }
    }
}
