using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class score : MonoBehaviour
{
    public Text myScore;

    public Text coins;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        myScore.text ="Platforms: " + Gamedata.instance.score.ToString();
        coins.text = Gamedata.instance.coins.ToString();
    }
}
