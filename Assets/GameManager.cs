using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Constants.FIELD active_field;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Only one instance allowed
            return;
        }
        Instance = this;
    }
        
    void Start()
    {
        active_field = Constants.FIELD.NONE;
    }


}
