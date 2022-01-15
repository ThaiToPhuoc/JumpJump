using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platform : MonoBehaviour
{
    public Collider2D collider;

    private int level;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.y < 0 && collision.gameObject.tag == "Player")
        {
            collision.transform.SetParent(transform);
            if(this.level + 1 > Gamedata.instance.score)
                score();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.SetParent(null);
    }

    public void setLevel(int level)
    {
        this.level = level;
    }
    private void score()
    {
        Gamedata.instance.score = this.level + 1;
        Gamedata.instance.finalScore++;

        if(this.level + 1 == Gamedata.instance.bonusLevel[Gamedata.instance.bonusIndex])
        {
            Gamedata.instance.finalScore += Gamedata.instance.bonusScore[Gamedata.instance.bonusIndex];
            Gamedata.instance.bonusIndex++;
        }
    }
}
