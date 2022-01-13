using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MOVE_DIRECTION
{
    LEFT, RIGHT
}
public class platformRow : MonoBehaviour
{
    public float speed = 1f;

    public List<Transform> platforms;

    public Transform platformPrefab;

    public Transform coinPrefab;

    public Transform sawPrefab;

    public Transform wallPrefab;

    public MOVE_DIRECTION direction;

    private float rowWidth;

    private float gap = 2f;

    private int level;

    // Start is called before the first frame update
    void Start()
    {
        rowWidth = (ActualResolutionWidth(Camera.main.orthographicSize) / 100f);
        createPlatform();
    }

    // Update is called once per frame
    void Update()
    {
        movingPlatform(speed);
    }

    public int ActualResolutionWidth(float orthoSize)
    {
        return (int)(orthoSize * 2.0 * (Screen.width * 1.0) / Screen.height * 100);
    }

    public int ActualResolutionHeight(float orthoSize)
    {
        return (int)(orthoSize * 2.0 * 100);
    }

    public void createPlatform()
    {
        int platformCount = (int)(rowWidth / gap) + 3;
        rowWidth = platformCount * gap;
        float positionX = - rowWidth / 2f;
        for(int i = 0; i < platformCount; i ++)
        {
            platforms.Add(Instantiate(platformPrefab, new Vector3(positionX, transform.position.y, 0), Quaternion.identity));
            platforms[i].transform.SetParent(transform);
            positionX += gap;
        }
        customPlatform();
;   }

    private void movingPlatform(float speed)
    {
        switch (direction)
        {
            case MOVE_DIRECTION.RIGHT:
                for (int i = 0; i < platforms.Count; i++)
                {
                    platforms[i].transform.position += speed * Time.deltaTime * Vector3.right;
                    if (platforms[i].transform.position.x > rowWidth/2)
                        platforms[i].transform.position = new Vector3(-rowWidth / 2, transform.position.y);
                }
                break;
            case MOVE_DIRECTION.LEFT:
                for (int i = 0; i < platforms.Count; i++)
                {
                    platforms[i].transform.position += speed * Time.deltaTime * Vector3.left;
                    if (platforms[i].transform.position.x < -rowWidth/2)
                        platforms[i].transform.position = new Vector3(rowWidth/2, transform.position.y);
                }
                break;
        }
    }

    private float[,] getData()
    {
        if (this.level < 30)
        {
            return Gamedata.instance.levelData[this.level];
        }
        else
        {
            return Gamedata.instance.levelData[20 + (this.level % 10)];
        }
    }

    private void customPlatform()
    {
        var data = getData();
        int r = Random.Range(0, 3);
        this.speed = Random.Range(data[r,0], data[r, 0] + 0.5f);
        this.gap = Random.Range(data[r, 1]/100 + 0.5f, data[r, 1] + 1f);

        int Coin = Random.Range(-2, 3);
        int Wall = Random.Range(-2, 3);
        int Saw = Random.Range(-2, 3);
        if(data[r,2] == 1)
        {
            placeCoin(Coin);
        }
        if (data[r, 3] == 1)
        {
            placeWall(Wall);
        }
        if (data[r, 4] == 1)
        {
            placeSaw(Saw);
        }
    }

    private void placeCoin(int Coin)
    {
        Instantiate(coinPrefab, new Vector3(Coin * 1.5f, transform.position.y + 0.6f, 0), Quaternion.identity);
    }

    private void placeSaw(int Saw)
    {
        Instantiate(sawPrefab, new Vector3(Saw * 2.5f, transform.position.y + 0.6f, 0), Quaternion.identity);
    }

    private void placeWall(int Wall)
    {
        Instantiate(wallPrefab, new Vector3(Wall * 1.5f, transform.position.y + 0.6f, 0), Quaternion.identity);
    }
    public void setLevel(int level)
    {
        this.level = level;
        this.direction = ((level) % 2 == 0) ? MOVE_DIRECTION.RIGHT : MOVE_DIRECTION.LEFT;
    }
}
