using UnityEngine;

// StartForest Details:
// Serves as a layer to support the practice mode
// Provides the Key Press action when the first note is found

public class StartForest : MonoBehaviour
{
    private bool practiceModeEndState = false;
    private LevelLoader levelLoader;
    private ClosedCaptions closedCaptions;
    private bool announcePlayedState = false;


    // Load CC and the Level Loader
    private void Start()
    {
        closedCaptions = GameObject.Find("ClosedCaptions").GetComponent<ClosedCaptions>();
        levelLoader = GameObject.Find("LevelLoader").GetComponent<LevelLoader>();
    }

    // If the practice note is found:
    // Notify the player with Wwise Event and provide a key press option
    public void OnNoteFound()
    {
        // Set end state to true
        practiceModeEndState = true;
        // If DHH player, display CC
        if (StartMenuManager.ColourState)
        {
            closedCaptions.DisplayCaptionsPracticeEnd("Practice end! If you feel ready, press Enter to start the game.");
        }
        // If event has not played yet, play Wwise Event
        if (!announcePlayedState)
        {
            announcePlayedState = true;
            AkSoundEngine.PostEvent("Play_PracticeEnd", gameObject);
        }
    }

    // Key Press option
    private void Update()
    {
        if (practiceModeEndState)
        {
            // Practice mode has ended, the player need to input the Return key
            if (Input.GetKey(KeyCode.Return))
            {
                // Reset the logging for the study run
                LogManager.ResetLogging();
                // Deactive CC to have a clean screen for the transition
                closedCaptions.DeactivateCC();
                // Fade out all Wwise events
                AkSoundEngine.PostEvent("Stop_AllEvents", null);
                // Load the scene
                levelLoader.LoadScene();
            }
        }
    }
}
