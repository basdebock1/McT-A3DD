using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static readonly object _lock = new object();
    private static GameManager _instance;
    private GameManager() { }
    public static GameManager GetInstance()
    {
        if(_instance == null)
        {
            lock (_lock)
            {
                if(_instance == null)
                {
                    _instance = new GameManager();
                }
            }
        }
        return _instance;
    }

    private void Start()
    {
        Debug.Log("Game Manager Started");
    }
}
