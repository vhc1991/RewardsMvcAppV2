using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RewardsProgramMVC.Models
{
    public class RetailersModel
    {

            public int Id { get; set; }
            public string Name { get; set; }
            public int TotalSpent { get; set; }
            public int TotalRewardsEarned { get; set; }

        
    }
}