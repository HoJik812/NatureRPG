using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    
    public Player player;
        
    public Image HpImage;
    
    public Image MpImage;

    public Button Setting;

    public Button Close;

    public Button Title;

    public GameObject SettingMenu;

    public Text Level;

    [SerializeField]
    private bool IsOpen;
    private void Awake()
    {
        IsOpen = false;
    }

    private void Start()
    {
        HpImage.fillAmount = 1f;
        MpImage.fillAmount = 1f;
    }

    private void Update()
    {
        CheckStatus();
        OpenSetting();
        GoToTitle();

    }

    private void OpenSetting()
    {  
        if (Input.GetKeyDown(KeyCode.F1))
        {
            IsOpen = !IsOpen;
            SettingMenu.SetActive(IsOpen);
        }
    }
   
    private void GoToTitle()
    {
        if (Input.GetKeyDown(KeyCode.F2) && IsOpen)
        {
            SceneManager.LoadScene("Title");
        }

    }


    private void CheckStatus()
    {
        HpImage.fillAmount = player.Hp / player.MAXHP;
        MpImage.fillAmount = player.Mp / player.MAXMP;

        Level.text = player.Level.ToString();
    }
}
