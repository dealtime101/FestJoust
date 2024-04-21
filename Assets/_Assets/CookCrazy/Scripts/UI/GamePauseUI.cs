using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{

    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button optionsButton;

    private void Awake()
    {
        resumeButton.onClick.AddListener(() =>
        {
            CookCrazyGameManager.Instance.TogglePauseGame();
        });
        mainMenuButton.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.MainMenuScene);
        });
        optionsButton.onClick.AddListener(() =>
        {
            OptionsUI.Instance.Show();
        });
    }
    private void Start()
    {
        CookCrazyGameManager.Instance.OnGamePaused += CookCrazyGameManager_OnGamePaused;
        CookCrazyGameManager.Instance.OnGameUnpaused += CookCrazyGameManager_OnGameUnpaused;
        Hide();
    }

    private void CookCrazyGameManager_OnGamePaused(object sender, System.EventArgs e)
    {
        Show();
    }

    private void CookCrazyGameManager_OnGameUnpaused(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
