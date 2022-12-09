using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerHealthSystem : MonoBehaviour
{
    public static int health = 3;
    public Image[] hearts;
    public Sprite fullHearts;
    public Sprite emptyHeart;

    void Awake(){
        health = 3;
    }
    // Update is called once per frame
    void Update()
    {
        foreach (Image img in hearts){
            img.sprite = emptyHeart;
        }
        for (int i = 0; i < health; i++){
            hearts[i].sprite = fullHearts;
        }
    }
}
