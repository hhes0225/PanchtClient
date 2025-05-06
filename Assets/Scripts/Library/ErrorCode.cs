using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ErrorCode : UInt16
{
    None = 0,

    //Common Error 贸府: 1000~
    UnhandledException = 1001,
    InvalidRequest = 1002,
    HttpConnectionFail = 1003,

    //Register Error 贸府: 2000~
    RegisterFailException = 2001,
    RegisterFailEmailExist = 2002,
    RegisterFailPasswordNotMatch = 2003,

    //Account Server 螟 Login Error 贸府: 2100~
    LoginFailException = 2101,
    LoginFailVerification = 2102,

    //牢刘 配奴 Error 贸府: 2200~
    RedisFailException = 2201,
    AuthTokenFailException = 2202,
    AuthTokenRegisterFail = 2203,
    AuthTokenInfoNotExist = 2204,
    AuthTokenIdNotMatch = 2205,
    AuthTokenTokenNotMatch = 2206,

    //Account DB Error 贸府: 3000~
    AccountDbFailException = 3001,
    AccountDbConnectionFail = 3002,

    //Game SQL(Game Data) Error 贸府: 3100~
    GameDataCreateFailException = 3101,
    GameDataLoadException = 3102,
    GameCreateFailNicknameExist = 3103,
    GameDataNotExist = 3104,
    GameCharacterDataNotExist = 3105,
    GameCharacterDataLoadFail = 3106,

    //Game Server Redis Error 贸府: 3200~
    GameServerRedisException = 3201,
    GameServerAuthTokenRegisterFail = 3202,
    GameServerAuthTokenInfoNotExist = 3203,
    GameServeAuthTokenIdNotMatch = 3204,
    GameServeAuthTokenNotMatch = 3205,

    //Game Server 螟 Login Error 贸府: 4000~
    LoginFailAccountConnectionException = 4001,

    //Attendance Error 贸府: 4100~
    AttendanceDataLoadFail = 4101,
    AttendanceDataCreateFail = 4102,
    AttendanceDataUpdateFail = 4103,
    AttendanceDataNotExist = 4104,
    AttendanceAlreadyDone = 4105,
    AttendanceDataCreateFailException = 4106,
    AttendanceDataUpdateFailException = 4107,

    //Game Matching Error 贸府: 5000~
    GameMatchingFailException = 5001,
    GameMatchingWaiting = 5002,

    //Matching Server 螟 包访 Error: 5100~
    MatchingFailError = 5102,
    MatchingNotYet = 5103,
}