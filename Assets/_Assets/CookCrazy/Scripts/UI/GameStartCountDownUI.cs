using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameStartCountDownUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countdownText;

    private void Start()
    {
        CookCrazyGameManager.Instance.OnStateChanged += CookCrazyGameManager_OnStateChanged;
        Hide();
    }

    private void CookCrazyGameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (CookCrazyGameManager.Instance.IsCountdownToStartActive())
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Update()
    {
        countdownText.text = Mathf.Ceil(CookCrazyGameManager.Instance.GetCountdowntoStartTimer()).ToString();
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
