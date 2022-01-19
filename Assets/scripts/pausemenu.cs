using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pausemenu : MonoBehaviour
{
    public audioManager audio;
    public GameObject pauseMenu;
    // Start is called before the first frame update
    public void pause()
    {
        pauseMenu.SetActive(true);
        audio.VolumeChange("Background", 0.4f);
        Time.timeScale = 0f;
    }

    public void resume()
    {
        pauseMenu.SetActive(false);
        audio.VolumeChange("Background", 1f);
        Time.timeScale = 1f;
    }

    public void restart(int sceneID)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneID);
    }
}
