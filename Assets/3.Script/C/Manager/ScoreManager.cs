using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance = null;

    private SaveData data;

    private string SavePath => Path.Combine(Application.persistentDataPath, "save.json");

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        data = Load();
    }

    public void Save(SaveData data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(SavePath, json);
        Debug.Log($"저장 완료: {SavePath}");
    }

    public SaveData Load()
    {
        if (!File.Exists(SavePath))
        {
            Debug.Log("세이브 파일 없음, 새로 생성합니다.");
            return new SaveData(0, new List<ScoreData>());
        }

        string json = File.ReadAllText(SavePath);
        SaveData data = JsonUtility.FromJson<SaveData>(json);
        return data;
    }

    
    public void UpdateScore(string playerName, int playerScore)
    {
        data.list.Add(new ScoreData(playerName, playerScore));
        data.list.Sort((a, b) => b.playerScore.CompareTo(a.playerScore));

        if (data.list.Count>10)
        {
            data.list.RemoveAt(10);
        }

        Save(data);
    }

    public SaveData showData()
    {
        return data;
    }
}
