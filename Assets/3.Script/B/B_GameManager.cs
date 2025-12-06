using UnityEngine;
using UnityEngine.SceneManagement;

public class B_GameManager : MonoBehaviour
{
    public static B_GameManager instance = null;
    public float gameTime = 0f;
    public float maxGameTime = 15f;
    public bool  isBoss = false;
    public bool  isLive = true;
    public int   character = 0;
    public int   score = 0;

    private void Awake()
    {
        if   (instance == null) instance = this;
        else Destroy(gameObject);

        Time.timeScale = 1;
    }

    private void Start()
    {
        //√ ±‚»≠
        /*
         * 
         * isBoss = false;
         * isLive = true;
         * score = 0;
         * gameTime = 0;
         * playerHealth = player.maxhealth;?
         * 
         */
    }

    private void Update()
    {
        if (!isLive) return;

        gameTime += Time.deltaTime;
    }

    public void GameStart(int characterNum)
    {
        SceneManager.LoadScene("Main");
    }

    public void Stop()
    {
        Time.timeScale = 0;
    }

    public void Resume()
    {
        Time.timeScale = 1;
    }

    public void AddScore(int score)
    {
        this.score += score;
    }
}
