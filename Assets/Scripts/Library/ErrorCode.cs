using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ErrorCode : UInt16
{
    None = 0,

    //Common Error 贸府: 1000~
    UnhandledException = 1001,
    InvalidRequest = 1003,
    HttpConnectionFail = 1004,

    //Register Error 贸府: 2000~
    RegisterFailException = 2001,
    RegisterFailEmailExist = 2002,
    RegisterFailPasswordNotMatch = 2003,

    //Account Server 螟 Login Error 贸府: 3000~
    LoginFailException = 3001,
    LoginFailVerification = 3002,

    //牢刘 配奴 Error 贸府: 4000~
    RedisFailException = 4001,
    AuthTokenFailException = 4002,
    AuthTokenRegisterFail = 4003,
    AuthTokenInfoNotExist = 4004,
    AuthTokenIdNotMatch = 4005,
    AuthTokenTokenNotMatch = 4006,

    //Account DB Error 贸府: 5000~
    AccountDbFailException = 5001,
    AccountDbConnectionFail = 5002,

    //Game Server 螟 Login Error 贸府: 6000~
    LoginFailAccountConnectionException = 6001,

    //Game Data Error 贸府: 7000~
    GameDataCreateFailException = 7001,
    GameDataLoadFleException = 7002,
    GameCreateFailNicknameExist = 7003,
    GameDataNotExist = 7004,
    GameCharacterDataNotExist = 7005,

    //Game Server Redis Error 贸府: 8000~
    GameServerRedisException = 8001,
    GameServerAuthTokenRegisterFail = 8002,
    GameServerAuthTokenInfoNotExist = 8003,
    GameServeAuthTokenIdNotMatch = 8004,
    GameServeAuthTokenNotMatch = 8004,

    //Game Matching Error 贸府: 9000~
    GameMatchingFailException = 9001,
    GameMatchingWaiting = 9002,
}