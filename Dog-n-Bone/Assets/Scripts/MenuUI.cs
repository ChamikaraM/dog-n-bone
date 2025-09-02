using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    [Header("Scene")]
    public string gameSceneName = "Game";    // Name of the main game scene
    [Header("Panels")]
    public CanvasGroup mainGroup;            // MainPanel's CanvasGroup
    public CanvasGroup settingsGroup;        // SettingsPanel's CanvasGroup

    void Start()
    {
        // Panel setup
        SetGroup(mainGroup, true);
        SetGroup(settingsGroup, false);
    }

    public void StartGame()
    {        
        SceneManager.LoadScene(gameSceneName, LoadSceneMode.Single);
    }

    // "Settings" button → show settings panel
    public void OpenSettings()
    {
        SetGroup(mainGroup, false);
        SetGroup(settingsGroup, true);
    }

    // "Back" button in settings → return to main panel
    public void CloseSettings()
    {
        SetGroup(settingsGroup, false);
        SetGroup(mainGroup, true);
    }

    // "Quit" button → exits in build or editor
    public void QuitGame()
    {
        Application.Quit();

        // If running in the editor, stop playing
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    // Show/hide a CanvasGroup
    private void SetGroup(CanvasGroup g, bool show)
    {
        if (!g) return;
        g.alpha = show ? 1f : 0f;
        g.interactable = show;
        g.blocksRaycasts = show;
    }
}
