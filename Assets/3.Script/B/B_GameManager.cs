using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class B_GameManager : MonoBehaviour
{
    public static B_GameManager instance = null;
    public float gameTime = 0f;
    public float maxGameTime = 15f;
    public bool  isBoss = false;
    public bool  isLive = true;
    public bool  isClear = false;
    public int   character = 0;
    public int   score = 0;

    private Dictionary<string, Queue<GameObject>> poolDictionary = new Dictionary<string, Queue<GameObject>>();

    public TMP_Text Score_Text;

    public B_Player player;

    [Header("아이템 설정")]
    public int coinScore = 100;      // 코인 점수
    public int healToScore = 500;    // 회복템 -> 점수 변환 시 점수

    private void Awake()
    {
        if   (instance == null) instance = this;
        else Destroy(gameObject);

        Time.timeScale = 1;
    }

    private void Start()
    {
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
        if (!isLive) return;

        gameTime += Time.deltaTime;

        Score_Text.text = string.Format("SCORE : {0:D6}", score);
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

    // 코인 획득 시 호출
    public void GetCoin()
    {
        score += coinScore;
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
            score += healToScore;
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
