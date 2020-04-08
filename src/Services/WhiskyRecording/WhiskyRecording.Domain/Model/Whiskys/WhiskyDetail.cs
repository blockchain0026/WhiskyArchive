using System;
using System.Collections.Generic;
using System.Text;
using WhiskyArchive.Services.WhiskyRecording.Domain.SeedWork;

namespace WhiskyArchive.Services.WhiskyRecording.Domain.Model.Whiskys
{
    public class WhiskyDetail : ValueObject
    {
        public WhiskyDetail(string vintage, string bottled, int? statedAge, string caskType, string caskNumber, int? numOfBottles, float strength, int size, string market)
        {
            Vintage = vintage ;
            Bottled = bottled ;
            StatedAge = statedAge;
            CaskType = caskType ;
            CaskNumber = caskNumber ;
            NumOfBottles = numOfBottles;
            Strength = strength;
            Size = size;
            Market = market;
        }

        public string Vintage { get; private set; }
        public string Bottled { get; private set; }
        public int? StatedAge { get; private set; }
        public string CaskType { get; private set; }
        public string CaskNumber { get; private set; }
        public int? NumOfBottles { get; private set; }
        public float Strength { get; private set; }
        public int Size { get; private set; }
        public string Market { get; private set; }


        public WhiskyDetail DetailUpdated(string vintage=null,string bottled=null,int? statedAge=null,string caskType=null,string caskNumber=null,int? numOfBottles=null,float? strength=null,int? size =null,string market=null)
        {
            var whiskyDetail = new WhiskyDetail(
                vintage??this.Vintage,
                bottled??this.Bottled,
                statedAge??this.StatedAge,
                caskType??this.CaskType,
                caskNumber??this.CaskNumber,
                numOfBottles??this.NumOfBottles,
                strength??this.Strength,
                size??this.Size,
                market??this.Market
                );

            return whiskyDetail;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return this.Vintage;
            yield return this.Bottled;
            yield return this.StatedAge;
            yield return this.CaskType;
            yield return this.CaskNumber;
            yield return this.NumOfBottles;
            yield return this.Strength;
            yield return this.Size;
            yield return this.Market;
        }
    }
}
