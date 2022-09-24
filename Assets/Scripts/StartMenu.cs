#if UNITY_EDITOR
using UnityEditor;
#endif
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private TextMeshProUGUI bestScoreText;

    private void Start()
    {
        bestScoreText.text = SessionStorage.Instance.GetBestScoreText();
    }

    private void Update()
    {
        startButton.interactable = SessionStorage.Instance.HasUsername;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}