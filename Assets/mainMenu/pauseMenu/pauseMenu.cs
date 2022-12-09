using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseMenu : MonoBehaviour
{
    public static bool gameIsPause = false;
    public GameObject pause;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(gameIsPause){
                Resume();
            }else{
                Pause();
            }
        }
    }

    public void Resume(){
        pause.SetActive(false);
        Time.timeScale = 1f;
        gameIsPause = false;
        FindObjectOfType<audioManager>().Play("inGame");
    }

    void Pause(){
        pause.SetActive(true);
        Time.timeScale = 0f;
        gameIsPause = true;
        FindObjectOfType<audioManager>().Pause("inGame");
    }

    public void Restart(){
        gameIsPause = false;
        FindObjectOfType<audioManager>().Play("inGame");
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BackToMainMenu(string sceneName){
        gameIsPause = false;
        Time.timeScale = 1f;
        FindObjectOfType<audioManager>().Stop("inGame");
        FindObjectOfType<audioManager>().Play("mainMenu");
        levelSelect.Instance.LoadScene(sceneName);
    }
}
