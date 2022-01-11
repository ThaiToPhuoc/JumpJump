using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stage : MonoBehaviour
{
    public List<platformRow> platformRows;

    public platformRow platformRowPrefab;

    private float stageHeight;

    private float rowGap = 1.5f;
    // Start is called before the first frame update
    void Start()
    {
        stageHeight = ActualResolutionHeight(Camera.main.orthographicSize)/100f;
        setStage();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int ActualResolutionWidth(float orthoSize)
    {
        return (int)(orthoSize * 2.0 * (Screen.width * 1.0) / Screen.height * 100);
    }

    public int ActualResolutionHeight(float orthoSize)
    {
        return (int)(orthoSize * 2.0 * 100);
    }

    private void setStage()
    {
        int platformRowCount = (int)(stageHeight / rowGap);
        stageHeight = platformRowCount * rowGap;
        float positionY = -(stageHeight / 2f) + rowGap;
        for (int i = 0; i < platformRowCount; i++)
        {
            platformRows.Add(Instantiate(platformRowPrefab, new Vector3(0, 0, 0), Quaternion.identity));
            platformRows[i].transform.SetParent(transform);
            platformRows[i].transform.position = new Vector3(0, positionY, 0);
            MOVE_DIRECTION direction = (i % 2 == 0) ? MOVE_DIRECTION.RIGHT : MOVE_DIRECTION.LEFT;
            platformRows[i].createPlatform(direction);
            positionY += rowGap;
        }
    }
}
