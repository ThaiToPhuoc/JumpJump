using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameplay : MonoBehaviour
{
    public List<stage> stages;

    public stage stageprefab_1;

    private stage currentStage;

    public Transform robot;
    // Start is called before the first frame update
    void Start()
    {
        createStages();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void createStages()
    {
        float positionY = 0f;
        for (int i = 0; i < 3; i++)
        {
            stages.Add(Instantiate(stageprefab_1, new Vector3(0,positionY,0), Quaternion.identity));
            stages[i].transform.SetParent(transform);
            stages[i].setSpeed(1f);
            positionY = positionY + Gamedata.instance.rowHeigth;
        }
    }

}
