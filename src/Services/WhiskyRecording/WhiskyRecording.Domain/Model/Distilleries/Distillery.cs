using System;
using System.Collections.Generic;
using System.Text;
using WhiskyArchive.Services.WhiskyRecording.Domain.Exceptions;
using WhiskyArchive.Services.WhiskyRecording.Domain.SeedWork;

namespace WhiskyArchive.Services.WhiskyRecording.Domain.Model.Distilleries
{
    public class Distillery : Entity, IAggregateRoot
    {
        public string DistilleryId { get; private set; }
        public DistilleryName DistilleryName { get; private set; }
        public string Established { get; private set; }
        public string Introdution { get; private set; }
        public string SmwsCode { get; private set; }


        protected Distillery()
        {
        }

        public Distillery(DistilleryName distilleryName, string established, string introdution, string smwsCode):this()
        {
            DistilleryId = Guid.NewGuid().ToString();
            DistilleryName = distilleryName ?? throw new WhiskyRecordingDomainException("Parameter not provided:" + nameof(distilleryName));
            Established = established ?? throw new WhiskyRecordingDomainException("Parameter not provided:" + nameof(established));
            Introdution = introdution ?? throw new WhiskyRecordingDomainException("Parameter not provided:" + nameof(introdution));
            SmwsCode = smwsCode ?? throw new WhiskyRecordingDomainException("Parameter not provided:" + nameof(smwsCode));
        }

        public void UpdateName(string chineseTradition=null, string chineseSimplified = null, string english = null)
        {
            if (chineseTradition == null & chineseSimplified == null & english == null)
            {
                throw new WhiskyRecordingDomainException("Chinese or english must be provided.");
            }

            this.DistilleryName = this.DistilleryName.Update(chineseTradition, chineseSimplified, english);
        }

        public void UpdateIntrodution(string intro)
        {
            this.Introdution = intro ?? throw new WhiskyRecordingDomainException("The intro must be provided.", new ArgumentNullException());
        }

        public void UpdateSmwsCode(string smwsCode)
        {
            this.SmwsCode=smwsCode?? throw new WhiskyRecordingDomainException("The smwsCode must be provided.", new ArgumentNullException());
        }
    }
}
