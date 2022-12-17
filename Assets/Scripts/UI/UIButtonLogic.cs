using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButtonLogic : MonoBehaviour
{
    public void LoadMenuScene() => SceneManager.LoadScene("Main Menu");
    
    public void LoadLevelScene() => SceneManager.LoadScene("Load Screen");
    
    public void LoadRankingScene() => SceneManager.LoadScene("Ranking");
    
    public void QuitGame() => Application.Quit();

    public void SubmitPlayerScore()
    {
        RankingManager rankingManager = (RankingManager) GameObject.Find("Raking Scene Script").GetComponent<RankingManager>();
        rankingManager.SubmitPlayerScore();
    }
}
