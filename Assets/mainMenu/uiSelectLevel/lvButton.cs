using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class lvButton : MonoBehaviour
{
    int levelsUnlocked;
    public Button[] buttons;
    // Start is called before the first frame update
    void Start()
    {
        levelsUnlocked = PlayerPrefs.GetInt("levelsUnlocked", 2);
        for(int i = 0; i < buttons.Length; i++){
            if(i+2 > levelsUnlocked)
                buttons[i]. interactable = false;
        }
    }

    public void ChangeScene(string sceneName){
        FindObjectOfType<audioManager>().Stop("mainMenu");
        StartCoroutine(playSound());
        levelSelect.Instance.LoadScene(sceneName);
    }

    IEnumerator playSound(){
        yield return new WaitForSeconds(1);
        FindObjectOfType<audioManager>().Play("inGame");
    }
}
