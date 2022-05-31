using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RewardsProgramMVC.Models
{
    public class RewardsModel
    {
        public int RewardsEarned { get; set; }
        public int TotalDollarsSpent { get; set; }
        public List<RetailersModel> RetailersList { get; set; }
    }
}