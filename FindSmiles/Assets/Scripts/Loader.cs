using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Loader : MonoBehaviour
{
    [SerializeField] Image[] _sprites;


    // Start is called before the first frame update
    void Start()
    {
        //DataBase.Instance.OnDownloadComplete = () => OnImageDownload();
    }

    //private void OnImageDownload() 
    //{
    //    List<string> inventoryList = new List<string>(DataBase.Instance.GetDataBase().Keys);
    //
    //    for (int i = 0; i < inventoryList.Count; i++)
    //    {
    //        _sprites[i].sprite = DataBase.Instance.GetDataBase()[inventoryList[i]];
    //    }
    //}
}
