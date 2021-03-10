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
    private Dictionary<string, string> _dbJSON = new Dictionary<string, string>();
   
    public int ImagesCount { get; private set; } = 0;
    public int DownloadImagesCount { get; private set; } = 0;
    public Action OnDownloadComplete;
    public Action OnImageDownload;

    private string lastImageName = "";
    
    public Dictionary<string, Sprite> GetDataBase()
    {
        return _db;
    }
    public Sprite GetLastImage()
    {
        return _db[lastImageName];
    }

    public void InitializeManager()
    {
        // Download json
        WebClient client = new WebClient();
        string json = client.DownloadString(_dbURL);
        // Parsing json
         _dbJSON = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
        // Download images
        ImagesCount = _dbJSON.Count;
        DownloadImagesCount = 0;
        StartCoroutine(LoadImages());
    }

    IEnumerator LoadImages() 
    {
        foreach (var key in _dbJSON.Keys)
        {
            StartCoroutine(GetTextureRequest(_dbJSON[key], (response) =>
            {
                if (response != null)
                {
                    _db[key] = response;
                    DownloadImagesCount++;
                    lastImageName = key;
                }

                OnImageDownload?.Invoke();
                if (DownloadImagesCount == ImagesCount)
                {
                    OnDownloadComplete?.Invoke();
                }
            }));
            // because need slow loading progress
            yield return new WaitForSeconds(0.3f);
        }
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
