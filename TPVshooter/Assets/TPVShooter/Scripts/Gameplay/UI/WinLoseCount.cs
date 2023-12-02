using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinLoseCount : MonoBehaviour
{
    private TextMeshProUGUI m_TextMeshProUGUI;

    private void Start()
    {
        m_TextMeshProUGUI = GetComponent<TextMeshProUGUI>();
        m_TextMeshProUGUI.text = "Win count: " + PlayerPrefs.GetInt("Wins") + " Lose count: " + PlayerPrefs.GetInt("Loses");
    }
}
