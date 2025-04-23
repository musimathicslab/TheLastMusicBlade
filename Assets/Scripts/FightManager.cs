using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class FightManager : MonoBehaviour
{
    public int points = 0;
    public bool cutSceneEnded = false;
    public SoundTrackManager soundTrackManager;
    public bool cardChosing = false;
    public UIManager uimanager;
    
    public static FightManager Instance { get; private set; }

    private void Awake()
    {
        Time.timeScale = 1.0f;
        cutSceneEnded = false;
        cardChosing = false;
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void GameOver()
    {
        Debug.Log("GAME OVER!");
        uimanager.GameOverUI();

        StartCoroutine(FadeStopMusic());

       
    }

    public void GameWon()
    {
        Debug.Log("Game WON");
        uimanager.WonUI();
        StartCoroutine(FadeStopMusic());

    }

    public void restartGame()
    {
       
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
       
}

    IEnumerator FadeStopMusic()
    {
      
        
        Debug.Log("CALLLING THAT");
        soundTrackManager.setAllVolumeToZero();
        yield return new WaitForSeconds(1f);
    }

 

}
