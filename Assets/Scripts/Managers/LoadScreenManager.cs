using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadScreenManager : MonoBehaviour
{
    [SerializeField] private Image _progressBar;
    [SerializeField] private Text _progressValue;
    [SerializeField] private string _targetScene = "Level";
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadAsync());
    }

    IEnumerator LoadAsync()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(_targetScene);
        operation.allowSceneActivation = false;
        float progress = 0;
    
        while (!operation.isDone)
        {
            progress = operation.progress;
            _progressBar.fillAmount = progress;
            _progressValue.text = $"Loading --- {progress * 100}%";

            if (operation.progress >= .9f)
            {
                _progressValue.text = "Press space to continue";
                _progressValue.fontSize = 150;
                _progressBar.fillAmount = 1;

                if (Input.GetKeyDown(KeyCode.Space)) operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
