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

    public List<platform> platforms;

    public platform platformPrefab;

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

    // Get Resolution
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
        customPlatform();
        int platformCount = (int)(rowWidth / gap) + 3;
        rowWidth = platformCount * gap;
        float positionX = - rowWidth / 2f;
        for(int i = 0; i < platformCount; i ++)
        {
            platforms.Add(Instantiate(platformPrefab, new Vector3(positionX, transform.position.y, 0), Quaternion.identity));
            platforms[i].transform.SetParent(transform);
            platforms[i].setLevel(level);
            positionX += gap;
        }
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
        if (this.level < 20)
        {
            return Gamedata.instance.levelData[this.level % 10];
        }
        else if(this.level < 40)
        {
            return Gamedata.instance.levelData[10 + (this.level % 10)];
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
        this.gap = Random.Range(data[r, 1] / 100 + 0.2f, data[r, 1] / 100 + 0.5f);
        this.speed = Random.Range(data[r,0], data[r, 0] + 0.6f);

        var index = new List<int> {-2,-1,0,1,2};
        var r2 = Random.Range(0, index.Count);
        if(data[r,2] > 0)
        {
            for(int i = 0; i < data[r, 2]; i++)
            {
                placeCoin(index[r2]);
                index.RemoveAt(r2);
                r2 = Random.Range(0, index.Count);
            }
        }
        if (data[r, 3] > 0)
        {
            for (int i = 0; i < data[r, 3]; i++)
            {
                placeWall(index[r2]);
                index.RemoveAt(r2);
                r2 = Random.Range(0, index.Count);
            }
        }

        if (data[r, 4] > 0)
        {
            for (int i = 0; i < data[r, 4]; i++)
            {
                placeSaw(index[r2]);
                index.RemoveAt(r2);
                r2 = Random.Range(0, index.Count);
            }
        }
    }

    private void placeCoin(int Coin)
    {
        Instantiate(coinPrefab, new Vector3(Coin * 0.85f, transform.position.y + 0.6f, 0), Quaternion.identity);
    }

    private void placeSaw(int Saw)
    {
        Instantiate(sawPrefab, new Vector3(Saw * 1f, transform.position.y + 0.6f, 0), Quaternion.identity);
    }

    private void placeWall(int Wall)
    {
        Instantiate(wallPrefab, new Vector3(Wall * 0.85f, transform.position.y + 0.7f, 0), Quaternion.identity);
    }
    public void setLevel(int level)
    {
        this.level = level;
        this.direction = ((level) % 2 == 0) ? MOVE_DIRECTION.RIGHT : MOVE_DIRECTION.LEFT;
    }
}
