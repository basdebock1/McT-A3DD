using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int Health { get; set; }
    public int MaxHealth { get; set; }

    private static readonly object _lock = new object();
    private static Player _instance;
    private Player() { }

    public static Player GetInstance()
    {
        if(_instance == null)
        {
            lock (_lock)
            {
                if(_instance == null)
                {
                    _instance = new Player();
                }
            }
        }
        return _instance;
    }

    // Start is called before the first frame update
    void Start()
    {
        Health = 3;
        MaxHealth = 3;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
