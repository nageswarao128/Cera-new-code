using System;
using System.Collections.Generic;
using System.Text;

namespace CERA.Entities.ViewModels
{
    public class DashboardCountModel
    {
        public int? resources { get; set; }
        public int? policies { get; set; }
        public int? users { get; set; }
        public decimal? cost { get; set; }
    }
}
