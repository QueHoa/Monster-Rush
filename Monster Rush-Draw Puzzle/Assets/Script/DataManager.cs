using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    private void Awake()
    {
        instance = this;
    }

    public int currentLevel
    {
        get
        {
            return PlayerPrefs.GetInt(DataString.currentLevel, 1);
        }
        set
        {
            PlayerPrefs.SetInt(DataString.currentLevel, value);
        }
    }
}

public struct DataString
{
    public const string currentLevel = "Current_Level";
    
}