using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Slider healthBar;
    [SerializeField]
    private TextMeshProUGUI bulletCount;
    [SerializeField]
    private RawImage pistol;
    [SerializeField]
    private RawImage assaut;
    [SerializeField]
    private RawImage sniper;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update()
    {
        if(!GameObject.FindWithTag("Enemy"))
        {
            VictoryGame();
        }
    }
    public void VisualHealth(float Health)
    {
        healthBar.value = Health / 100;
    }
    public void VisualBullet(int Bullet)
    {
        bulletCount.text = Bullet.ToString();
    }
    public void VictoryGame()
    {
        Cursor.lockState = CursorLockMode.Confined;
        PlayerPrefs.SetInt("Wins", PlayerPrefs.GetInt("Wins") + 1);
        SceneManager.LoadScene("VictoryScene");
    }
    public void LoseGame()
    {
        Cursor.lockState = CursorLockMode.Confined;
        PlayerPrefs.SetInt("Loses", PlayerPrefs.GetInt("Loses") + 1);
        SceneManager.LoadScene("LoseScene");
    }
    public void VisualWeapon(int Weapon)
    {
        if (Weapon != 0)
            assaut.color = new Color(assaut.color.r, assaut.color.g, assaut.color.b, 0.5f);
        else assaut.color = new Color(assaut.color.r, assaut.color.g, assaut.color.b, 1);

        if (Weapon != 1)
            sniper.color = new Color(sniper.color.r, sniper.color.g, sniper.color.b, 0.5f);
        else sniper.color = new Color(sniper.color.r, sniper.color.g, sniper.color.b, 1);

        if (Weapon != 2)
            pistol.color = new Color(pistol.color.r, pistol.color.g, pistol.color.b, 0.5f);
        else pistol.color = new Color(pistol.color.r, pistol.color.g, pistol.color.b, 1);
    }
}
