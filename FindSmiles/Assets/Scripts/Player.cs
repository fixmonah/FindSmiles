using System;
using UnityEditor;
using UnityEngine;

public class Player
{
    public int Score { get; private set; }

    public void Win()
    {
        Score++;
    }
}