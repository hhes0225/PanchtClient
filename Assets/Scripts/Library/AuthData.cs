using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuthData
{
    private static AuthData _instance;
    public static AuthData Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new AuthData();
            }
            return _instance;
        }
    }

    public string Id { get; set; } = string.Empty;
    public string AuthToken { get; set; } = string.Empty;

    public void SetAuthData(string id, string authToken)
    {
        Id = id;
        AuthToken = authToken;
    }
}
