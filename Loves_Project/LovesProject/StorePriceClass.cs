using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LovesProject
{
    public class StorePriceClass
    {
        public int StoreID {get;set;}
        public int StoreNumber {get;set;}
        public string StoreGrade { get;set;}
        public double PreviousPrice {get;set;}
        public double NewPrice {get;set;}
        public double PriceDifference {get;set;}
        public string TimeStamp {get;set;}    
    

    public StorePriceClass()
    {
        StoreID = 0;
        StoreNumber = 0;
        StoreGrade = string.Empty;
        PreviousPrice = 0.0;
        NewPrice = 0.0;
        PriceDifference = 0.0;
        TimeStamp = string.Empty;
    }

    }
}
