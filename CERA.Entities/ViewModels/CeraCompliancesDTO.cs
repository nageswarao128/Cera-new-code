using System;
using System.Collections.Generic;
using System.Text;

namespace CERA.Entities.ViewModels
{
    public class CeraCompliancesDTO
    {
        public Value[] value { get; set; }
    }
    public class Value
    {
        public string id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public Properties properties { get; set; }
    }

    public class Properties
    {
        public string scope { get; set; }
        public Assessmentresult[] assessmentResult { get; set; }
        public int resourceCount { get; set; }
        public DateTime assessmentTimestampUtcDate { get; set; }
    }

    public class Assessmentresult
    {
        public string type { get; set; }
        public float percentage { get; set; }
    }


}
