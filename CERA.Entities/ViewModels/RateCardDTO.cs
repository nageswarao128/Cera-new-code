using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CERA.Entities.ViewModels
{
    public class RateCardDTO
    {
        public object[] OfferTerms { get; set; }
        public Meter[] Meters { get; set; }
        public string Currency { get; set; }
        public string Locale { get; set; }
        public bool IsTaxIncluded { get; set; }
    }

    public class Meter
    {
        public DateTime EffectiveDate { get; set; }
        public float IncludedQuantity { get; set; }
        public string MeterCategory { get; set; }
        public string MeterId { get; set; }
        public string MeterName { get; set; }
        public Meterrates MeterRates { get; set; }
        public string MeterRegion { get; set; }
        public string MeterStatus { get; set; }
        public string MeterSubCategory { get; set; }
        public string[] MeterTags { get; set; }
        public string Unit { get; set; }
    }

    public class Meterrates
    {
        public float _0 { get; set; }
        public float _512000000000000 { get; set; }
        public float _5120000000000000 { get; set; }
        public float _50000 { get; set; }
        public float _150000 { get; set; }
        public float _500000 { get; set; }
        public float _1000000 { get; set; }
        public float _10240000000000 { get; set; }
        public float _10240000000000000 { get; set; }
        public float _51200000000000000 { get; set; }
        public float _25 { get; set; }
        public float _51200 { get; set; }
        public float _512000 { get; set; }
        public float _1024000 { get; set; }
        public float _5120000 { get; set; }
        public float _50 { get; set; }
        public float _1024 { get; set; }
        public float _10000 { get; set; }
        public float _200000 { get; set; }
        public float _5000000 { get; set; }
        public float _100000000000000 { get; set; }
        public float _500000000000000 { get; set; }
        public float _1500000000000000 { get; set; }
        public float _5000000000000000 { get; set; }
        public float _10000000000000000 { get; set; }
        public float _50000000000000000 { get; set; }
        public float _90 { get; set; }
        public float _2500000000000 { get; set; }
        public float _15000000000000 { get; set; }
        public float _40000000000000 { get; set; }
        public float _250000 { get; set; }
        public float _50000000 { get; set; }
        public float _100000000 { get; set; }
        public float _10000000000000 { get; set; }
        public float _1000000000000000 { get; set; }
        public float _10235 { get; set; }
        public float _51195 { get; set; }
        public float _153595 { get; set; }
        public float _511995 { get; set; }
        public float _99 { get; set; }
        public float _499 { get; set; }
        public float _999 { get; set; }
        public float _1000000000000 { get; set; }
        public float _25000 { get; set; }
        public float _100000 { get; set; }
        public float _1024000000000000 { get; set; }
        //[JsonProperty(PropertyName = "_50")]
        //public float __50 { get; set; }
        public float _100 { get; set; }
        public float _250 { get; set; }
        public float _500 { get; set; }
        public float _1000 { get; set; }
        public float _5000 { get; set; }
        public float _95 { get; set; }
        public float _495 { get; set; }
        public float _995 { get; set; }
        public float _20000 { get; set; }
        public float _2400000000000000 { get; set; }
        public float _7500000000000000 { get; set; }
        public float _102400 { get; set; }
        public float _9000000000000000 { get; set; }
        public float _99000000000000000 { get; set; }
        public float _87 { get; set; }
        public float _2487 { get; set; }
        public float _102350000000000 { get; set; }
        public float _511950000000000 { get; set; }
        public float _1535950000000000 { get; set; }
        public float _5119950000000000 { get; set; }
        public float _870000000000 { get; set; }
        public float _24870000000000 { get; set; }
        public float _45000000000000 { get; set; }
        public float _5120 { get; set; }
        public float _20480 { get; set; }
        public float _20 { get; set; }
        public float _240000 { get; set; }
        public float _750000 { get; set; }
        public float _950000 { get; set; }
        public float _9950000 { get; set; }
        public float _49950000 { get; set; }
        public float _500000000000 { get; set; }
        public float _5000000000000 { get; set; }
        public float _50000000000000 { get; set; }
        public float _1500 { get; set; }
        public float _4000 { get; set; }
        public float _99000 { get; set; }
        public float _499000 { get; set; }
        public float _950000000000 { get; set; }
        public float _200000000000000 { get; set; }
        public float _2000000000000000 { get; set; }
        public float _8 { get; set; }
        public float _98 { get; set; }
        public float _998 { get; set; }
        public float _1980000 { get; set; }
        public float _9980000 { get; set; }
        public float _5 { get; set; }
        public float _10 { get; set; }
        public float _250000000000 { get; set; }
        public float _900000000000 { get; set; }
        public float _990000000000000 { get; set; }
        public float _4990000000000000 { get; set; }
    }

}
