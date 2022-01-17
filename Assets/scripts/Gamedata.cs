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
    public float stageHeigth = 10f;
    public float[][,] levelData = new float[][,]
    {
        new float[,] { {1,120,0,0,0} , {1,120,0,0,0} , {1,120,0,0,0}},
        new float[,] { {1,140,1,0,0} , {1,120,0,0,0} , {1,140,1,0,0}},
        new float[,] { {1,120,0,0,0} , {1,120,1,0,0} , {1.5f,140,0,0,0}},
        new float[,] { {1.5f,150,1,0,0} , {1,180,1,0,0} , {1,120,0,0,0}},
        new float[,] { {1.5f,180,0,0,0} , {1.5f,150,0,0,0} , {1.5f,180,0,0,0}},
        new float[,] { {1.5f,150,0,0,0} , {1.2f,150,1,0,0} , {1,150,1,0,0}},
        new float[,] { {1.5f,180,1,0,0} , {1.5f,180,0,0,0} , {1.2f,180,0,0,0}},
        new float[,] { {1.2f,200,0,0,0} , {1,200,0,0,0} , {1.2f,180,0,0,0}},
        new float[,] { {1,200,1,0,0} , {1.5f,200,1,0,0} , {1.2f,180,1,0,0}},
        new float[,] { {1.5f,150,0,0,0} , {1.2f,180,0,0,0} , { 1.5f,200,0,0,0}},
        new float[,] { {1.8f,120,0,0,0} , {2,150,0,0,0} , { 1.5f,150,0,0,0}},
        new float[,] { {1.5f,150,0,1,0} , {1.2f,120,1,0,0} , { 1.2f,150,1,1,0}},
        new float[,] { {1.2f,200,1,0,0} , {1.5f,180,0,1,0} , { 1.5f,200,0,0,0}},
        new float[,] { {1.8f,150,0,0,0} , {1.2f,200,1,0,0} , { 1.8f,180,0,0,0}},
        new float[,] { {2,180,0,0,0} , {1.5f,150,0,1,0} , { 2,200,1,1,0}},
        new float[,] { {1.8f,180,0,1,0} , {2,150,1,0,1} , { 2,200,0,0,1}},
        new float[,] { {2,180,1,0,0} , {1.5f,180,0,1,0} , { 1.5f,180,0,1,0}},
        new float[,] { {2.2f,200,0,0,0} , {2,200,1,0,1} , { 2.2f,220,1,1,0}},
        new float[,] { {2.2f,210,0,1,0} , {2.4f,180,0,0,1} , {2.4f,240,0,0,1}},
        new float[,] { {2.5f,150,0,0,1} , {2.2f,150,0,1,0} , { 2,200,1,1,0}},
        new float[,] { {2.4f,180,1,1,1} , {2,200,1,0,1} , { 2.2f,180,0,0,0}},
        new float[,] { {2.4f,200,0,0,1} , {2.4f,180,0,0,1} , { 2.4f,220,0,1,1}},
        new float[,] { {2.4f,220,0,1,0} , {2,200,1,1,1} , { 2,240,1,1,2}},
        new float[,] { {2.6f,240,0,0,1} , {2.6f,180,0,0,1} , { 2.4f,200,0,0,1}},
        new float[,] { {2.6f,240,0,1,1} , {2.6f,220,0,1,1} , { 2.2f,180,0,1,1}},
        new float[,] { {2.8f,260,1,1,0} , {2.8f,240,0,1,0} , { 2,220,0,1,1}},
        new float[,] { {2.4f,200,0,0,1} , {3,150,1,1,1} , { 2.8f,200,0,2,1}},
        new float[,] { {2,180,0,1,1} , {2.8f,240,0,1,0} , { 2.8f,180,0,1,0}},
        new float[,] { {3,200,1,1,1} , {2.8f,220,1,1,1} , { 3,220,1,2,1}},
        new float[,] { {3,240,0,2,1} , {3,250,1,0,2} , { 3,120,1,1,2}}
    };

    public float score = 0, coins = 0, finalScore = 0, highScore = 0;

    public int[] bonusLevel = new int[] {10,20,30,35,40,45,50,60,70,80,90,100};
    public int[] bonusScore = new int[] {5,5,10,5,10,15,20,20,20,25,25,50};
    public int bonusIndex = 0, backGroundIndex = 0;

    public bool changeBG = false, gameover = false;
}
