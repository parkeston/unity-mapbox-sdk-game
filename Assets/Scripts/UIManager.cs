using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; set; }
    [SerializeField] private TMP_Text winTitle;
    [SerializeField] private GameObject winPanel;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    public void Win(string text)
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        
        winPanel.SetActive(true);
        winTitle.text = text;
    }
    public void Next()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        
        winPanel.SetActive(false);
        SceneManager.LoadScene(0);
    }
}
