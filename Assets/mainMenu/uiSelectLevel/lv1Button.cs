using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lv1Button : MonoBehaviour
{
    public void ChangeScene(string sceneName){
        levelManager.Instance.LoadScene(sceneName);
    }
}
