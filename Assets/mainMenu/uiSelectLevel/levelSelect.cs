using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Threading.Tasks;

public class levelSelect : MonoBehaviour
{
    public static levelSelect Instance;
    [SerializeField] private GameObject _loaderCanvas;
    [SerializeField] private Image _progressBar;
    private float _target;
    void Awake(){
        if(Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }else{
            Destroy(gameObject);
        }
    }

    public async void LoadScene(string sceneName) { 
    _target = 0; 
    _progressBar.fillAmount = 0; 
    var scene = SceneManager.LoadSceneAsync(sceneName); 
    scene.allowSceneActivation = false; 
    _loaderCanvas.SetActive(true); 
    do 
    { 
        await Task.Delay(100);
        _target = scene.progress; 
    }while (scene.progress < 0.9f); 

        await Task.Delay(1000);

    scene.allowSceneActivation = true; 
    _loaderCanvas.SetActive(false); 
} 
    // Update is called once per frame
    void Update()
    {
        _progressBar.fillAmount = Mathf.MoveTowards(_progressBar.fillAmount, _target, 3 * Time.deltaTime);
    }
}
