using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelComplete : MonoBehaviour
{
    public void backToLevelSelect(string sceneName){
        int selectLevels = SceneManager.GetActiveScene().buildIndex + 1;
        FindObjectOfType<audioManager>().Stop("inGame");
        FindObjectOfType<audioManager>().Play("mainMenu");
        levelSelect.Instance.LoadScene(sceneName);

        if(selectLevels > PlayerPrefs.GetInt("levelsUnlocked"))
            PlayerPrefs.SetInt("levelsUnlocked", selectLevels);
    }
}
