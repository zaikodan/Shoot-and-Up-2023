using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] Text coinsUI;
    [SerializeField] Button[] upgradeButtons, buffButtons;
    const int levelMax = 5;


    private Stat BuyUpgrade(Stat stat)
    {
        GameManager.instance.RemoveCoins(stat.Price);
        coinsUI.text = GameManager.instance.PlayerCoins.ToString();
        stat.Upgrade();
        return stat;
    }

    void CheckButton(Stat stat, Button button)
    {
        Text textUI = button.GetComponentInChildren<Text>();

        if(stat.Level == levelMax)
        {
            button.interactable = false;
            textUI.text = stat.Name + " Level Maximo";
            return;
        }
        else if(GameManager.instance.PlayerCoins < stat.Price)
        {
            button.interactable = false;
        }

        textUI.text = stat.Name + " Level: " + stat.Level + "\n" + "Preço: " + stat.Price;
    }

    void CheckAllButtons()
    {
        CheckButton(PlayerStats.health, upgradeButtons[0]);
        CheckButton(PlayerStats.damage, upgradeButtons[1]);
        CheckButton(PlayerStats.speed, upgradeButtons[2]);
        CheckButton(PlayerStats.fireRate, upgradeButtons[3]);

        CheckButton(BuffStats.speed, buffButtons[0]);
        CheckButton(BuffStats.damage, buffButtons[1]);
        CheckButton(BuffStats.fireRate, buffButtons[2]);
        CheckButton(BuffStats.heal, buffButtons[3]);
        CheckButton(BuffStats.duration, buffButtons[4]);
    }

    void ClickButton(int index)
    {
        switch (index)
        {
            case 0:
                PlayerStats.health = BuyUpgrade(PlayerStats.health);
                break;

                case 1:
                PlayerStats.damage = BuyUpgrade(PlayerStats.damage);

                break;

                case 2:
                PlayerStats.speed = BuyUpgrade(PlayerStats.speed);

                break;

                case 3:
                PlayerStats.fireRate = BuyUpgrade(PlayerStats.fireRate);

                break;
        }

        CheckAllButtons();
        coinsUI.text = "Moedas: " + GameManager.instance.PlayerCoins;
    }

    void ClickBuffButton(int index)
    {
        switch (index)
        {
            case 0:
                BuffStats.speed = BuyUpgrade(BuffStats.speed);
                break;

            case 1:
                BuffStats.damage = BuyUpgrade(BuffStats.damage);

                break;

            case 2:
                BuffStats.fireRate = BuyUpgrade(BuffStats.fireRate);

                break;

            case 3:
                BuffStats.heal = BuyUpgrade(BuffStats.heal);

                break;

            case 4:
                BuffStats.duration = BuyUpgrade(BuffStats.duration);

                break;

        }

        CheckAllButtons();
        coinsUI.text = "Moedas: " + GameManager.instance.PlayerCoins;
    }

    void SetButtons()
    {
        for (int i = 0;  i < upgradeButtons.Length; i++)
        {
            int x = i;
            upgradeButtons[i].onClick.AddListener(delegate { ClickButton(x); });
        }

        for (int i = 0; i < buffButtons.Length; i++)
        {
            int x = i;
            buffButtons[i].onClick.AddListener(delegate { ClickBuffButton(x); });
        }

        coinsUI.text = "Moedas: " + GameManager.instance.PlayerCoins;
    }

    private void Start()
    {
        SetButtons();
        CheckAllButtons();
    }

}
