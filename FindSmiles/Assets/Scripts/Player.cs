using System;
using UnityEditor;
using UnityEngine;

public class Player
{
    public int Score { get; public set; }

    public void Win()
    {
        Score++;
    }
}