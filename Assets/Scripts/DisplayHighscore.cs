using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayHighscore : MonoBehaviour
{
    public Text highscoreFields;

    void Start()
    {

            highscoreFields.text = "Fetching...";
        
    }

    public void OnHighscoresDownloaded(Highscore[] highscoreList)
    {
        highscoreFields.text = "";
        for (int i = 0; i < highscoreList.Length; i++)
        {
            highscoreFields.text += i + 1 + ". " + highscoreList[i].username + " - " + highscoreList[i].score + "\n";
            
        }
    }

    public void DownloadHighscores()
    {
        highscoreFields.text = "Fetching...";

        Highscores.instance.DownloadHighscores();
    }
}
