using System;
using System.Collections;
using System.Collections.Generic;
public class WebData
    {

    }
    public class User : WebData
    {
        public string? Supplier { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? LastDate { get; set; }
        public string? UID { get; set; }
}
    public class UserItem : WebData 
    {
        public string? UID { get; set; }
        public string? ItemList { get; set; }
        public DateTime? LastUpdateDate { get; set; }
    }
    public class UserAsset  : WebData
    {
        public string? UID { get; set; }
        public int? HeartCount { get; set; }
        public DateTime? LastHeartChargeDate { get; set; }
        public int? CoinCount { get; set; }

    }
