using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

// The GameManager controls overall game flow:
// - handles win/lose conditions
// - manages UI panels
// - plays sound effects
public class GameManager : MonoBehaviour
{
    // Other scripts can easily call GameManager.Instance
    public static GameManager Instance { get; private set; }

    [Header("Refs")]

    // Reference to the player controller script (Dog)
    public PlayerController player;

    // UI panels
    public CanvasGroup winUI;
    public CanvasGroup loseUI;


    // Audio settings
    public AudioSource ambienceSource; // background ambience audio source
    public AudioClip winClip; // sound played when player wins
    public AudioClip loseClip; // sound played when player loses


    public TMP_Text countdownText;
    public float countdownTime = 3f; // how long to count down
    public bool gameActive = false; // controls when player can move

    // Internal state flag to prevent multiple win/lose triggers
    bool ended = false;

    void Awake()
    {
        // ensure only one instance of GameManager exists
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;

        // ensure overlays start hidden
        SetGroup(winUI, false);
        SetGroup(loseUI, false);
    }

    void Start()
    {
        if (ambienceSource != null && ambienceSource.clip != null)
        {
            ambienceSource.loop = true;
            ambienceSource.Play();
        }

        // Start countdown at scene load
        StartCoroutine(StartCountdown());
    }

    private IEnumerator StartCountdown()
    {
        gameActive = false; // disable player until countdown ends
        player.SetInputEnabled(false);

        float time = countdownTime;

        while (time > 0)
        {
            countdownText.text = Mathf.Ceil(time).ToString(); // "3", "2", "1"
            yield return new WaitForSeconds(1f);
            time--;
        }

        countdownText.text = "Go!";
        yield return new WaitForSeconds(1f);

        countdownText.text = ""; // clear text
        gameActive = true;
        player.SetInputEnabled(true);
    }

    // Helper to show/hide UI panels
    void SetGroup(CanvasGroup g, bool show)
    {
        if (!g) return;
        g.alpha = show ? 1f : 0f;   // visible/invisible
        g.interactable = show;      // allow clicks if visible
        g.blocksRaycasts = show;    // block background clicks if visible
    }

    // Called when the player collides with a wall
    public void GameOver()
    {
        Debug.Log("GameManager.GameOver() called");

        // Prevent multiple calls
        if (ended) return;
        ended = true;

        // Disable player movement
        if (player) player.SetInputEnabled(false);

        // Show Lose UI panel
        SetGroup(loseUI, true);

        // Make cursor is visible so player can click UI buttons
        Cursor.visible = true;

        // Play lose sound once
        if (loseClip != null)
            AudioSource.PlayClipAtPoint(loseClip, Camera.main.transform.position);
    }

    // Called when the player reaches the bone
    public void Win()
    {
        Debug.Log("GameManager.Win() called");

        if (ended) return;
        ended = true;

        // Disable player movement
        if (player) player.SetInputEnabled(false);

        // Show Win UI panel
        SetGroup(winUI, true);

        Cursor.visible = true;

        // Play win sound once
        if (winClip != null)
            AudioSource.PlayClipAtPoint(winClip, Camera.main.transform.position);
    }

    // Hook these to the buttons
    public void Retry()
    {
        // Reload the current active scene
        var scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.buildIndex);
    }

    public void Quit()
    {
        Application.Quit();

        // If running in the editor, stop playing
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
