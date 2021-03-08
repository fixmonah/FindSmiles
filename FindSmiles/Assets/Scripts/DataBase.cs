using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;

public class DataBase : MonoBehaviour
{

    #region Singleton
    private static DataBase _instance;
    public static DataBase Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance == this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        InitializeManager();
    }
    #endregion

    private string _dbURL = "https://drive.google.com/uc?export=download&id=1K0tx2Tgi-igS_jl3pTPhY6qhk2gFhfvg";
    private Dictionary<string, Sprite> _db = new Dictionary<string, Sprite>();
    public int ImagesCount { get; private set; } = 0;
    public int DownloadImagesCount { get; private set; } = 0;

    public Action OnDownloadComplete;
    public Action OnImageDownload;
    public Dictionary<string, Sprite> GetDataBase() { return _db; }
    internal void InitializeManager()
    {
        // Download json
        WebClient client = new WebClient();
        string json = client.DownloadString(_dbURL);

        // Parsing json
        Dictionary<string, string> dbDictionary = new Dictionary<string, string>();
        dbDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

        // Download images
        ImagesCount = dbDictionary.Count;
        DownloadImagesCount = 0;
        foreach (var key in dbDictionary.Keys)
        {
            StartCoroutine(GetTextureRequest(dbDictionary[key], (response) =>
            {
                if (response != null)
                {
                    _db[key] = response;
                    DownloadImagesCount++;
                }
  
                OnImageDownload?.Invoke();
                if (DownloadImagesCount == ImagesCount)
                {
                    OnDownloadComplete?.Invoke();
                }
            }));
        }
        // tested
        //OnImageDownload = () => { Debug.Log("image download"); };
        //OnDownloadComplete = () => { Debug.Log("all files download"); };
    }


    IEnumerator GetTextureRequest(string url, System.Action<Sprite> callback)
    {
        using (var www = UnityWebRequestTexture.GetTexture(url))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log(www.error);
            }
            else
            {
                if (www.isDone)
                {
                    var texture = DownloadHandlerTexture.GetContent(www);
                    var rect = new Rect(0, 0, 100f, 100f);
                    var sprite = Sprite.Create(texture, rect, new Vector2(0.5f, 0.5f));
                    callback(sprite);
                }
            }
        }
    }
}
