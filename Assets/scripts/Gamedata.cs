using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamedata : MonoBehaviour
{
    static Gamedata _instance;
    public static Gamedata instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("Gamedata");
                _instance = go.AddComponent<Gamedata>();
            }

            return _instance;

        }
    }
    public float rowHeigth = 10f;
}
