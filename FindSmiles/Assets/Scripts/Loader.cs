using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{
    [SerializeField] Image _loadingSprite;
    [SerializeField] Text _loadingText;

    [SerializeField] private string _loadingSceneName;
    private AsyncOperation asyncOperation;
    void Start()
    {
        DataBase.Instance.OnDownloadComplete = () => OnDownloadComplete();
        DataBase.Instance.OnImageDownload = () => OnImageDownload();

        asyncOperation = SceneManager.LoadSceneAsync(_loadingSceneName);
        asyncOperation.allowSceneActivation = false;
    }

    private void OnDownloadComplete()
    {
        asyncOperation.allowSceneActivation = true;
    }

    private void OnImageDownload()
    {
        Sprite loadingSprite = DataBase.Instance.GetLastImage();
        if (loadingSprite != null)
        {
            _loadingSprite.sprite = loadingSprite;
        }
        _loadingText.text = $"Loading: {DataBase.Instance.DownloadImagesCount} / {DataBase.Instance.ImagesCount}";
    }
}
