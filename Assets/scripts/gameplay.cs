using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameplay : MonoBehaviour
{
    public List<stage> stages;

    public stage stageprefab_1;

    public Transform camera;

    public Sprite[] spriteArray0, spriteArray1, spriteArray2, spriteArray3;
    public SpriteRenderer background0, background1, background2, background3;

    public GameObject result;

    public static bool isPaused = false;
    public Text finalScore;

    public Text highScore;

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

        if(Gamedata.instance.changeBG)
        {
            changeBackground();
            Gamedata.instance.changeBG = false;
        }

        if(Gamedata.instance.gameover)
        {
            result.SetActive(true);
            finalScore.text = Gamedata.instance.finalScore.ToString();
            highScore.text = PlayerPrefs.GetInt("highScore").ToString();
            Gamedata.instance.gameover = false;
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

    private void changeBackground()
    {
        if (Gamedata.instance.backGroundIndex > 4)
            Gamedata.instance.backGroundIndex = 0;
        background0.sprite = spriteArray0[Gamedata.instance.backGroundIndex];
        background1.sprite = spriteArray1[Gamedata.instance.backGroundIndex];
        background2.sprite = spriteArray2[Gamedata.instance.backGroundIndex];
        background3.sprite = spriteArray3[Gamedata.instance.backGroundIndex];
        Gamedata.instance.backGroundIndex++;
    }


}
