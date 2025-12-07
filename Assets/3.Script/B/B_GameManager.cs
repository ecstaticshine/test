using System.Collections.Generic;
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

    private Dictionary<string, Queue<GameObject>> poolDictionary = new Dictionary<string, Queue<GameObject>>();

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
