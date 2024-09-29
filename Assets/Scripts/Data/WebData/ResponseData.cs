using System;
using System.Collections;
using System.Collections.Generic;


public enum ResponseDataType
{
    Resgister,
    Login,
    Asset,
}
public abstract class ResponseData
{

}

[Serializable]
public class ResgisterResponse : ResponseData
{
    public string Token;
    public string UID;
}
[Serializable]
public class LoginResponse : ResponseData
{
    public string Token;
    public string UID;
}
[Serializable]
public class UserPropertyResponse : ResponseData
{
    public int heartCount;
    public int coinCount;

}
[Serializable]
public class UserAssetResponse : ResponseData
{
    public int HeartCount;
    public DateTime LastHeartChargeDate;
    public int CoinCount;
}