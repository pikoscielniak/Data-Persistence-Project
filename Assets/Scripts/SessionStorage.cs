using UnityEngine;

public class SessionStorage : MonoBehaviour
{
    public static SessionStorage Instance { get; private set; }

    public string CurrentUsername { get; set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public bool HasUsername => !string.IsNullOrWhiteSpace(CurrentUsername);
}