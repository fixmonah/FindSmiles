using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Card _card;
    [SerializeField] private Sprite _front;

    // Start is called before the first frame update
    void Start()
    {
        _card.SetFrontImage(_front);
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyUp(KeyCode.H)){ _card.Hide(); }
        //if (Input.GetKeyUp(KeyCode.S)) { _card.Show(); }
        //if (Input.GetKeyUp(KeyCode.F)) { _card.TurnOverFront(); }
        //if (Input.GetKeyUp(KeyCode.B)) { _card.TurnOverBack(); }
    }
}
