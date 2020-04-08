using System;
using System.Collections.Generic;
using System.Text;
using WhiskyArchive.Services.WhiskyRecording.Domain.SeedWork;

namespace WhiskyArchive.Services.WhiskyRecording.Domain.Model.Distilleries
{
    public class DistilleryName : ValueObject
    {
        public DistilleryName(string chineseTraditional, string chineseSimplified, string english)
        {
            ChineseTraditional = chineseTraditional ?? throw new ArgumentNullException(nameof(chineseTraditional));
            ChineseSimplified = chineseSimplified ?? throw new ArgumentNullException(nameof(chineseSimplified));
            English = english ?? throw new ArgumentNullException(nameof(english));
        }

        public string ChineseTraditional { get; private set; }
        public string ChineseSimplified { get; private set; }
        public string English { get; private set; }

        public DistilleryName Update(string cht = null, string chs = null, string english = null)
        {
            return new DistilleryName(
                cht ?? this.ChineseTraditional,
                chs ?? this.ChineseSimplified, 
                english ?? this.English);
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return this.ChineseTraditional;
            yield return this.ChineseSimplified;
            yield return this.English;
        }
    }
}
