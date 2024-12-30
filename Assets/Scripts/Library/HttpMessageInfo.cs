using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IResponse
{
    ErrorCode Result { get; set; }
}

[System.Serializable]
public class ReqRegister
{

    public string Id { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string ConfirmPassword { get; set; } = string.Empty;
}

[System.Serializable]
public class ResRegister:IResponse
{   
    public ErrorCode Result { get; set; } = ErrorCode.None;
}

[System.Serializable]
public class ReqLoginAccountServer
{
    public string Id { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

[System.Serializable]
public class ResLoginAccountServer : IResponse
{
    public ErrorCode Result { get; set; } = ErrorCode.None;
    public string Id { get; set; } = string.Empty;
    public string AuthToken { get; set; } = string.Empty;
}

[System.Serializable]
public class ReqLoginGameServer
{
    public string Id { get; set; } = string.Empty;
    public string AuthToken { get; set; } = string.Empty;
}

[System.Serializable]
public class ResLoginGameServer : IResponse
{
    public ErrorCode Result { get; set; } = ErrorCode.None;
    public UserData UserGameData { get; set; } = new UserData();
}

[System.Serializable]
public class ReqCreateUser
{
    public string Id { get; set; } = string.Empty;
    public string Nickname { get; set; } = string.Empty;
    public string AuthToken { get; set; } = string.Empty;
}

[System.Serializable]
public class ResCreateUser : IResponse
{
    public ErrorCode Result { get; set; } = ErrorCode.None;
    public UserData UserGameData { get; set; } = new UserData();
}

[System.Serializable]
public class ReqUserProfile
{
    public string Id { get; set; } = string.Empty;
}

[System.Serializable]
public class ResUserProfile : IResponse
{
    public ErrorCode Result { get; set; } = ErrorCode.None;
    public UserData UserGameData { get; set; } = new UserData();
}

[System.Serializable]
public class  ReqCharacterList
{
    public string Id { get; set; } = string.Empty;
}

[System.Serializable]
public class ResCharacterList : IResponse
{
    public ErrorCode Result { get; set; } = ErrorCode.None;
    public List<UserCharacterData> UserCharacterList { get; set; } = new List<UserCharacterData>();
}

[System.Serializable]
public class ReqMatching
{
    public string Id { get; set; } = string.Empty;
}

[System.Serializable]
public class ResMatching : IResponse
{
    public ErrorCode Result { get; set; } = ErrorCode.None;
}

[System.Serializable]
public class ReqCheckMatching
{
    public string Id { get; set; } = string.Empty;
}

[System.Serializable]
public class ResCheckMatching : IResponse
{
    public ErrorCode Result { get; set; } = ErrorCode.None;
    public string SocketServerAddress { get; set; } = string.Empty;
    public int RoomNumber { get; set; } = 0;
}