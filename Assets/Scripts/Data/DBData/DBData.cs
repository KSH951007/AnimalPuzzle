using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
public class DBData
{

}
public class UserAccountDBData : DBData
{
    public string? Supplier { get; set; }
    public DateTime? CreateDate { get; set; }
    public DateTime? LastDate { get; set; }
    public string? UID { get; set; }
}
public class UserItemDBData : DBData
{
    public string? UID { get; set; }
    public string? ItemList { get; set; }
    public DateTime? LastUpdateDate { get; set; }
}
public class UserAssetDBData : DBData
{
    public string? UID { get; set; }
    public int? HeartCount { get; set; }
    public DateTime? LastHeartChargeDate { get; set; }
    public int? CoinCount { get; set; }

}
public class StageDBData : DBData
{

    public int StageLevel { get; set; }
    public int StepCount { get; set; }
    public int BoardRowCount { get; set; }
    public int BoardHeightCount { get; set; }

    public string IsPresenceCells { get; set; }
    public string BlockList { get; set; }

    //목표 데이터 추가

}
