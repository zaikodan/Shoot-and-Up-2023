using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject registerWindow;
    [SerializeField] Text playerNameUI;
    [SerializeField] InputField playerField;
    [SerializeField] Button confirmPlayerName;

    private void Start()
    {
        
        registerWindow.SetActive(!PlayerPrefs.HasKey("PlayerName"));
        playerNameUI.text = PlayerStats.playerName;
        Debug.Log(PlayerStats.playerName);

        //AdsManager.instance.SetBanner(true);
    }

    public void LoadScene(string sceneName)
    {
        GameManager.instance.LoadScene(sceneName);
        //AdsManager.instance.SetBanner(false);
    }

    public void SavePlayerName()
    {
        PlayerStats.playerName = playerField.text;
        PlayerPrefs.SetString("PlayerName", PlayerStats.playerName);
        playerNameUI.text = PlayerStats.playerName;
    }

    public void CheckPlayerName()
    {
        confirmPlayerName.interactable = playerField.text != "";
    }

    public void ResetSave()
    {
        PlayerPrefs.DeleteAll();
        registerWindow.SetActive(true);
    }

    public void RequestAdCoin()
    {
        GameManager.instance.RequestAdCoin();
    }
}
