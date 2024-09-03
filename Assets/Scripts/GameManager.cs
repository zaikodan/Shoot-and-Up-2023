using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class GameManager : MonoBehaviourPun
{
    int score, record, playerCoins, deathsAmount;
    [SerializeField]UIManager managerUI;

    const string playerPrefab = "Prefabs/Player";
    int playersAmount;

    #region Singleton
    public static GameManager instance;

    public int Score { get => score; }
    public int PlayerCoins { get => playerCoins; }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }

        SceneManager.sceneLoaded += SceneLoaded;
    }

    #endregion

    private void Start()
    {
        photonView.RPC("AddPlayer", RpcTarget.AllBuffered);
    }

    void CreatePlayer()
    {
        NetworkManager.instance.Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
    }

    [PunRPC]
    void AddPlayer()
    {
        playersAmount++;
        if (playersAmount == PhotonNetwork.PlayerList.Length) 
        {
            CreatePlayer(); 
        }
    }

    private void AddCoins(int value)
    {
        playerCoins += value;
    }

    public void RemoveCoins(int value)
    {
        playerCoins -= value;
    }

    private void SceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Time.timeScale = 1;
        switch (scene.name)
        {
            case "Menu":
                break;
            case "Jogo":
                managerUI = FindObjectOfType<UIManager>();
                score = 0;
                managerUI.UpdateScore(score);
                break;
            default:
                break;
        }
    }

    public void LoadScene(string nameScene)
    {
        SceneManager.LoadScene(nameScene);
    }

    public void AddScore(int value)
    {
        score += value;
        managerUI.UpdateScore(score);
    }
    public void GameOver()
    {
        managerUI.RewardWindow.SetActive(true);
        Time.timeScale = 0;
    }

    public void FinishMatch()
    {
        #region Contador de mortes
        deathsAmount++;
        Debug.Log("Numero de mortes: " + deathsAmount);
        if (deathsAmount == 3)
        {
            AdsManager.instance.ShowInterstitial(true);
            deathsAmount = 0;
        }
        else
        {
            AdsManager.instance.ShowInterstitial(false);
        }
        #endregion

        AddCoins(score / 100);
        bool newRecord = CheckRecord();//Checagem do Record
        managerUI.GameOver(score, record, newRecord);
    }

    public void ResquestAdReward()
    {
        AdsManager.instance.delegateCompleteReward = RevivePlayer;
        AdsManager.instance.ShowRewarded();
    }

    private void RevivePlayer()
    {
        PlayerController player = FindAnyObjectByType<PlayerController>();
        player.ActiveBuff(3, 0, PlayerStats.health.Value);
        Time.timeScale = 1;
    }

    public void RequestAdCoin()
    {
        AdsManager.instance.ShowRewarded();
        AdsManager.instance.delegateCompleteReward = AdCoinsComplete;
        AdsManager.instance.delegateHalfReward = AdCoinsHalf;
    }

    private void AdCoinsComplete()
    {
        AddCoins(100);
    }

    private void AdCoinsHalf()
    {
        AddCoins(50);
    }

    private bool CheckRecord()
    {
        record = PlayerPrefs.GetInt("Record");

        if(score > record)
        {
            record = score;
            PlayerPrefs.SetInt("Record", record);
            Highscores.instance.AddNewHighscore(PlayerStats.playerName, score);
            return true;
        }
        else
        {
            return false;
        }
    }
}
