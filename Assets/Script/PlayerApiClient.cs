using System.Collections;
} else
{
    callback(true, req.downloadHandler.text);
}
}


public IEnumerator Login(string usernameOrEmail, string password, System.Action<bool, string> callback)
{
    var body = JsonUtility.ToJson(new { usernameOrEmail = usernameOrEmail, password = password });
    var req = new UnityWebRequest(serverBaseUrl + "/api/player/login", "POST");
    byte[] bodyRaw = Encoding.UTF8.GetBytes(body);
    req.uploadHandler = new UploadHandlerRaw(bodyRaw);
    req.downloadHandler = new DownloadHandlerBuffer();
    req.SetRequestHeader("Content-Type", "application/json");
    yield return req.SendWebRequest();
#if UNITY_2020_1_OR_NEWER
    if (req.result == UnityWebRequest.Result.ConnectionError || req.result == UnityWebRequest.Result.ProtocolError)
#else
if (req.isNetworkError || req.isHttpError)
#endif
    {
        callback(false, req.downloadHandler.text);
    }
    else
    {
        callback(true, req.downloadHandler.text);
    }
}


public IEnumerator GetPlayer(string playerId, System.Action<bool, string> callback)
{
    var url = serverBaseUrl + "/api/player" + (string.IsNullOrEmpty(playerId) ? "" : "?playerId=" + playerId);
    var req = UnityWebRequest.Get(url);
    req.downloadHandler = new DownloadHandlerBuffer();
    yield return req.SendWebRequest();
#if UNITY_2020_1_OR_NEWER
    if (req.result == UnityWebRequest.Result.ConnectionError || req.result == UnityWebRequest.Result.ProtocolError)
#else
if (req.isNetworkError || req.isHttpError)
#endif
    {
        callback(false, req.downloadHandler.text);
    }
    else
    {
        callback(true, req.downloadHandler.text);
    }
}


public IEnumerator UpdatePlayer(string playerId, string username, string email, string password, System.Action<bool, string> callback)
{
    var body = JsonUtility.ToJson(new { playerId = playerId, username = username, email = email, password = password });
    var req = new UnityWebRequest(serverBaseUrl + "/api/player", "PUT");
    byte[] bodyRaw = Encoding.UTF8.GetBytes(body);
    req.uploadHandler = new UploadHandlerRaw(bodyRaw);
    req.downloadHandler = new DownloadHandlerBuffer();
    req.SetRequestHeader("Content-Type", "application/json");
    yield return req.SendWebRequest();
#if UNITY_2020_1_OR_NEWER
    if (req.result == UnityWebRequest.Result.ConnectionError || req.result == UnityWebRequest.Result.ProtocolError)
#else
if (req.isNetworkError || req.isHttpError)
#endif
    {
        callback(false, req.downloadHandler.text);
    }
    else
    {
        callback(true, req.downloadHandler.text);
    }
}


public IEnumerator DeletePlayer(string playerId, System.Action<bool, string> callback)
{
    var url = serverBaseUrl + "/api/delete/" + UnityWebRequest.EscapeURL(playerId);
    var req = UnityWebRequest.Delete(url);
    req.downloadHandler = new DownloadHandlerBuffer();
    yield return req.SendWebRequest();
#if UNITY_2020_1_OR_NEWER
    if (req.result == UnityWebRequest.Result.ConnectionError || req.result == UnityWebRequest.Result.ProtocolError)
#else
if (req.isNetworkError || req.isHttpError)
#endif
    {
        callback(false, req.downloadHandler.text);
    }
    else
    {
        callback(true, req.downloadHandler.text);
    }
}
}