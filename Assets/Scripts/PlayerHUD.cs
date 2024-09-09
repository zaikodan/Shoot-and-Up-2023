using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : HUD
{
    float damageDuration, speedDuration, fireRateDuration;
    [SerializeField] Image speedBar, damageBar, fireRateBar;

    private void Awake()
    {
        healthBar = GameObject.Find("HealthBar").GetComponent<Image>();
        speedBar = GameObject.Find("SpeedBar").GetComponent<Image>();
        damageBar = GameObject.Find("DamageBar").GetComponent<Image>();
        fireRateBar = GameObject.Find("FireRateBar").GetComponent<Image>();
    }

    public void SetSpeedDuration(float duration)
    {
        speedDuration = duration;
    }

    public void SetFireRateDuration(float duration)
    {
        fireRateDuration = duration;
    }

    public void SetDamageDuration(float duration)
    {
        damageDuration = duration;
    }

    public void UpdateSpeedBar(float duration)
    {
        speedBar.fillAmount = duration / speedDuration;
    }

    public void UpdateDamageBar(float duration)
    {
        damageBar.fillAmount = duration / damageDuration;
    }

    public void UpdateFireRateBar(float duration)
    {
        fireRateBar.fillAmount = duration / fireRateDuration;
    }
}
