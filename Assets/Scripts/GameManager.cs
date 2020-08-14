using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool gameOver = false;
    public float restartDelay = 3f;
    public GameObject completeUI;

    public void GameOver()
    {
      
        if (!gameOver)
        {
            gameOver = true;
            Restart();
            //gameOver = false;
            Invoke("Restart", restartDelay);
        }
        
    }
    
    public void CompleteLevel()
    {
        
        completeUI.SetActive(true);
        Invoke("LevelCompleted", restartDelay);

    }

    public void LevelCompleted()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void Restart()
    {
        //Reload the sceene that is currently loaded
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        //For debugging with infinite lives
        //FindObjectOfType<Ball>().transform.position = new Vector2(0, 0);

    }

    private void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Block").Length <= 1)
        {
            CompleteLevel();
            
        }
    }
}
