using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class levelManager : MonoBehaviour
{
    public static levelManager Instance;
    [SerializeField] private GameObject _loaderCanvas; 
    [SerializeField] private Image _progressBar; 
    private float _target;
    // Start is called before the first frame update
    void Awake()
    {
        if(Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else{
            Destroy(gameObject);
        }
    }

    public async void LoadScene(string sceneName){
        _progressBar.fillAmount = 0;
        _target = 0;

        var scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = false;
        _loaderCanvas.SetActive(true);
        do
        {
            await Task.Delay(100);
            _target = scene.progress;
        } while (scene.progress < 0.9f);

        await Task.Delay(1000);

        scene.allowSceneActivation = true;
        _loaderCanvas.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        _progressBar.fillAmount = Mathf.MoveTowards(_progressBar.fillAmount, _target,3 * Time.deltaTime);
    }
}
