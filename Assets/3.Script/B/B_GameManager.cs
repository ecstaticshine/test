using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class B_GameManager : MonoBehaviour
{
    public static B_GameManager instance = null;

    [SerializeField] private GameObject Unity;
    [SerializeField] private GameObject Urara;
    [SerializeField] private GameObject Box;
    [SerializeField] private GameObject pause;
    public TMP_Text Score_Text;
    public B_Player player;
    public B_Move move;
    public float gameTime = 0f;
    public bool isLive = true;
    public bool isClear = false;
    public bool isStop = false;
    public int character = 0;
    public int score = 0;
    private int scoreMultipler = 1;
    //public float maxGameTime = 15f;
    //public bool  isBoss = false;

    private Dictionary<string, Queue<GameObject>> poolDictionary = new Dictionary<string, Queue<GameObject>>();
    private float scoreBuffer = 0f;
    private float scorePerSecond = 1000f;

    [Header("아이템 설정")]
    public int coinScore = 100;      // 코인 점수
    public int healToScore = 500;    // 회복템 -> 점수 변환 시 점수

    [SerializeField] private GameObject GameOver;
    public bool canGameOverInput = false;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        Time.timeScale = 1;
        GameOver.SetActive(false);
    }

    private void Start()
    {
        character = PlayerPrefs.GetInt("SelectedCharacter");

        scoreMultipler = 1;

        switch (character)
        {
            case 0:
                SetUpCharacter(Unity);
                break;
            case 1:
                SetUpCharacter(Urara);
                break;
            case 2:
                SetUpCharacter(Box);
                Time.timeScale = 1.5f;
                scoreMultipler = 2;
                break;
        }
        //초기화
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
        if (Input.GetKeyDown(KeyCode.Escape) && !isStop)
        {
            Stop();
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && isStop)
        {
            Resume();
        }

        if (!isLive || isClear)
        {
            StartCoroutine(GameOverDelay());

            // 사용자가 아무 키나 누르면
            if (canGameOverInput && Input.anyKeyDown)
            {
                SaveData data = ScoreManager.instance.showData();

                if (data.list.Count < 10)
                {
                    SceneLoader.Instance.LoadInputScoreScene(score);
                }
                else
                {

                    if (score > data.list[9].playerScore)
                    {
                        SceneLoader.Instance.LoadInputScoreScene(score);
                    }
                    else
                    {
                        SceneLoader.Instance.LoadResultScene();
                    }
                }
            }
        }
        else
        {
            gameTime += Time.deltaTime;

            scoreBuffer += Mathf.RoundToInt(scorePerSecond * scoreMultipler *Time.deltaTime);

            if (scoreBuffer >= scorePerSecond)
            {
                score += Mathf.RoundToInt(scorePerSecond);

                scoreBuffer = 0;
            }

            Score_Text.text = string.Format("SCORE : {0:D6}", score);
        }
    }

    public IEnumerator GameOverDelay()
    {
        yield return new WaitForSeconds(3f);
        GameOver.SetActive(true);

        yield return new WaitForSeconds(1f);
        canGameOverInput = true;
    }

    private void SetUpCharacter(GameObject characterObject)
    {
        characterObject.SetActive(true);

        move = characterObject.GetComponent<B_Move>();
        player = characterObject.GetComponent<B_Player>();
    }

    public void GameStart(int characterNum)
    {
        character = characterNum;

        SceneManager.LoadScene("Main");
    }

    public void Stop()
    {
        pause.SetActive(true);

        move.enabled = false;

        isStop = true;

        Time.timeScale = 0;
    }

    public void Resume()
    {
        pause.SetActive(false);

        move.enabled = true;

        isStop = false;

        Time.timeScale = 1;
    }

    public void AddScore(int score)
    {
        this.score += score;
    }

    // 코인 획득 시 호출
    public void GetCoin()
    {
        score += (coinScore * scoreMultipler);
        Debug.Log("코인 획득! 현재 점수: " + score);

        // 효과음 재생 (아까 만든 오디오 매니저 활용)
        AudioManager.Instance.PlaySFX("Coin");
    }

    // 회복 아이템 획득 시 호출
    public void GetHealItem()
    {
        // 핵심 기획: 피가 꽉 찼으면 점수로!
        if (player.getCurrentHealth() >= player.getMaxHealth())
        {
            score += (healToScore * scoreMultipler);
            Debug.Log("체력 Full! 점수로 변환: " + score);
            AudioManager.Instance.PlaySFX("Coin"); // 돈 버는 소리
        }
        else
        {
            player.healHealth();
            Debug.Log("체력 회복! 현재 체력: " + player.getCurrentHealth());
            AudioManager.Instance.PlaySFX("Heal"); // 회복 소리
        }
    }

    public GameObject Get(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(prefab.name))
        {
            poolDictionary.Add(prefab.name, new Queue<GameObject>());
        }

        if (poolDictionary[prefab.name].Count > 0)
        {
            GameObject obj = poolDictionary[prefab.name].Dequeue();
            obj.SetActive(true);
            obj.transform.position = position;
            obj.transform.rotation = rotation;
            return obj;
        }
        else
        {
            GameObject newObj = Instantiate(prefab, position, rotation);
            newObj.name = prefab.name;
            return newObj;
        }
    }

    public void Return(GameObject obj)
    {
        if (poolDictionary.ContainsKey(obj.name))
        {
            obj.SetActive(false);
            poolDictionary[obj.name].Enqueue(obj);
        }
        else
        {
            Destroy(obj);
        }
    }
}
