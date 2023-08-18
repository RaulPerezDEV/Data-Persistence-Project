using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEditor;

public class MenuUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nameInputField;
    [SerializeField] TextMeshProUGUI bestScoreText;
    public string playerName;

    private void Awake()
    {
        GameObject[] i = GameObject.FindGameObjectsWithTag("MenuHandler");
        if (i.Length > 1)
        {
            Destroy(i[0]);
        }

        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        LoadBestScore();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.LeftControl))
        {
            ResetBestScore();
            LoadBestScore();
        }
    }

    public void SetPlayerName()
    {
        Debug.Log(nameInputField.text); 
        playerName = nameInputField.text;
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); 
#endif
    }

    class PlayerData
    {
        public string name;
        public int score;
    }

    void ResetBestScore()
    {
        PlayerData playerData = new PlayerData();
        playerData.name = "player";
        playerData.score = 0;

        string json = JsonUtility.ToJson(playerData);
        Debug.Log(json);

        File.WriteAllText(Application.dataPath + "/saveFile.json", json);
    }
    void LoadBestScore()
    {
        if (File.Exists(Application.dataPath + "/saveFile.json"))
        {
            string json = File.ReadAllText(Application.dataPath + "/saveFile.json");
            PlayerData loadedPlayerData = JsonUtility.FromJson<PlayerData>(json);
            string bestScoreName = loadedPlayerData.name;
            int bestScore = loadedPlayerData.score;
            bestScoreText.text = "Best Score: " + bestScoreName + " : " + bestScore + " points";
        }      
    }
}
