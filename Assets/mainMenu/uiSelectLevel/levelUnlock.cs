using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class levelUnlock : MonoBehaviour
{
    int levelsUnlocked;
    public Button[] buttons;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Button b in buttons)
        {
            b.interactable = false;

            levelsUnlocked = PlayerPrefs.GetInt("levelsUnlocked",1);
        
            for (int i = 0; i < levelsUnlocked; i++)
            {
                buttons[i].interactable = true;
            }
        }
    }

    public void ChangeScene(string sceneName){
        levelSelect.Instance.LoadScene(sceneName);
    }
}
