using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameplay : MonoBehaviour
{
    public List<stage> stages;

    public stage stageprefab_1;

    public Transform camera;

    private float positionY = 0f;

    private int currentLevel = 1;
    // Start is called before the first frame update
    void Start()
    {
        createStages();
    }

    // Update is called once per frame
    void Update()
    {
        if(camera.transform.position.y > currentLevel * Gamedata.instance.rowHeigth)
        {
            updateStage();
            currentLevel++;
        }
    }

    private void createStages()
    {
        for (int i = 0; i < 3; i++)
        {
            stages.Add(Instantiate(stageprefab_1, new Vector3(0,positionY,0), Quaternion.identity));
            stages[i].transform.SetParent(transform);
            positionY += Gamedata.instance.rowHeigth;
        }
    }

    private void updateStage()
    {
        var stage1 = stages[0];
        stages.Remove(stages[0]);
        stages.Add(Instantiate(stageprefab_1, new Vector3(0, positionY, 0), Quaternion.identity));
        stages[2].transform.SetParent(transform);
        positionY += Gamedata.instance.rowHeigth;

        Destroy(stage1.gameObject);
    }    
}
