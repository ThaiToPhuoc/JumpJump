using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameplay : MonoBehaviour
{
    public List<stage> stages;

    public stage stageprefab_1;

    public Transform camera;

    private float positionY = 0f;

    private int currentPosition = 1;
    private int currentStageLevel = 0;

    // Start is called before the first frame update
    void Start()
    {
        createStages();
    }

    // Update is called once per frame
    void Update()
    {
        if (camera.transform.position.y > currentPosition * Gamedata.instance.stageHeigth)
        {
            updateStage();
            currentPosition++;
            currentStageLevel++;
        }
    }

    private void createStages()
    {
        for (int i = 0; i < 3; i++)
        {
            stages.Add(Instantiate(stageprefab_1, new Vector3(0,positionY,0), Quaternion.identity));
            stages[i].setStageLevel(currentStageLevel);
            stages[i].transform.SetParent(transform);
            positionY += Gamedata.instance.stageHeigth;
            currentStageLevel++;
        }
    }

    private void updateStage()
    {
        var stage1 = stages[0];
        stages.Remove(stages[0]);
        stages.Add(Instantiate(stageprefab_1, new Vector3(0, positionY, 0), Quaternion.identity));
        stages[2].setStageLevel(currentStageLevel);
        stages[2].transform.SetParent(transform);
        positionY += Gamedata.instance.stageHeigth;
        Destroy(stage1.gameObject);
    }    
}
