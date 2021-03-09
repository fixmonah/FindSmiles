using System;
using UnityEditor;
using UnityEngine;

public class Player
{
    public int Score { get; internal set; }

    internal void Win()
    {
        Score++;
    }
}