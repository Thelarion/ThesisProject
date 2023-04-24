using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

// Details: TargetController
// Overall parent of all targets
// Storage of child information

public class TargetController : MonoBehaviour
{
    private OperationController _operationController;
    [SerializeField] private int _targetCount;
    public string chosenPalette;
    private Transform targetCheckStateForDistance;
    private Transform currentShortestDistance;
    private ScoreManager scoreManager;
    private ClosedCaptions closedCaptions;
    private TargetIndicator targetIndicator;
    private StartForest startForest;
    public GameObject Goal;
    public GameObject Bells;
    private LevelLoader levelLoader;
    private enum notes
    {
        C2, C2s, D2b, D2, D2s, E2b, E2, F2, F2s, G2b, G2, G2s, A2b, A2, A2s, B2b, B2,
        C3, C3s, D3b, D3, D3s, E3b, E3, F3, F3s, G3b, G3, G3s, A3b, A3, A3s, B3b, B3,
        C4, C4s, D4b, D4, D4s, E4b, E4, F4, F4s, G4b, G4, G4s, A4b, A4, A4s, B4b, B4,
    }
    private ArrayList _materialsAvailable = new ArrayList();
    private ArrayList _materialsAvailableString = new ArrayList();
    private bool initializationState = false;

    // Set up the score manager, and operation controller
    // Set up the melody and name the targets accordingly
    void Awake()
    {
        scoreManager = GameObject.Find("ScoreSystem").GetComponent<ScoreManager>();
        _operationController = GameObject.Find("List").GetComponent<OperationController>();
        int x = 0;
        // Filter through the melody and name the targets
        foreach (notes note in _operationController._melodySequence)
        {
            Transform _childTarget = transform.GetChild(x);
            _childTarget.name = note.ToString();
            _childTarget.GetComponent<TargetIdentity>().setIndexInSequence(x);
            x++;
        }
        // Set up the materials
        InitializeMaterials();
    }

    private void Start()
    {
        closedCaptions = GameObject.Find("ClosedCaptions").GetComponent<ClosedCaptions>();
        targetIndicator = GameObject.Find("TargetIndicator").GetComponent<TargetIndicator>();
        levelLoader = GameObject.Find("LevelLoader").GetComponent<LevelLoader>();
        if (SceneManager.GetActiveScene().name == "PracticeMode")
        {
            startForest = GameObject.Find("Targets").GetComponent<StartForest>();
        }
    }

    private void Update()
    {
        // Useful when several targets are available at the same time on the map
        // This will always search for the closest note and set it active in the UI
        InitializeDistanceTarget();

        // Check for remaining targets
        CheckTargetCount();
        GetTargetWithShortestDistance();
    }

    private void InitializeDistanceTarget()
    {
        if (!initializationState)
        {
            currentShortestDistance = DistanceToTarget.CurrentTargetIdentity.transform;
            targetCheckStateForDistance = currentShortestDistance;
            // Enable occlusion when target active
            currentShortestDistance.GetComponent<CheckOcclusion>().enabled = true;
            initializationState = true;
        }
    }

    private void GetTargetWithShortestDistance()
    {
        if (DistanceToTarget.CurrentTargetIdentity != null)
        {
            currentShortestDistance = DistanceToTarget.CurrentTargetIdentity.transform;

            if (currentShortestDistance != targetCheckStateForDistance)
            {
                if (targetCheckStateForDistance != null)
                {
                    targetCheckStateForDistance.GetComponent<CheckOcclusion>().enabled = false;
                }
                currentShortestDistance.GetComponent<CheckOcclusion>().enabled = true;
                targetCheckStateForDistance = currentShortestDistance;
            }
        }


    }

    // Set the materials based on the chosen color palette
    private void InitializeMaterials()
    {
        // Load all the materials of the color palette
        var load = Resources.LoadAll("UI_Materials/" + GameManager.ChosenPalette, typeof(Material)).Cast<Material>();
        foreach (var material in load)
        {
            _materialsAvailable.Add(material);
            _materialsAvailableString.Add(material.name);
        }

        foreach (Transform item in transform)
        {
            item.transform.GetComponent<RunInterval>().MaterialsAvailable = _materialsAvailable;
            item.transform.GetComponent<RunInterval>().MaterialsAvailableString = _materialsAvailableString;
        }
    }

    // Based on the inclusion mode, move the targets less or more
    public void ToggleInclusionTargets()
    {
        foreach (Transform child in transform)
        {
            bool _currentInclusion = child.GetComponent<TargetMove>().InclusionIO;

            if (!_currentInclusion)
            {
                child.GetComponent<TargetMove>().InclusionIO = true;
            }
            else
            {
                child.GetComponent<TargetMove>().InclusionIO = false;
            }
        }
    }

    // Reactivate the movement after it was stopped to transform the position
    public void InitializeMovementAfterStopped()
    {
        foreach (Transform child in transform)
        {
            child.GetComponent<TargetMove>().InitializeMovementAfterMissOrInclusion();
        }
    }

    public void StopMovement()
    {
        foreach (Transform child in transform)
        {
            child.GetComponent<TargetMove>().StopMovementWhenMissOrInclusion();
        }
    }


    public int TargetCount { get => _targetCount; set => _targetCount = value; }


    // Always check for the target count, so that a new level loads once all targets are found
    private void CheckTargetCount()
    {

        if (CountChildrenTargets(transform) <= 0)
        {

            if (SceneManager.GetActiveScene().name == "PracticeMode")
            {
                startForest.OnNoteFound();
            }
            else
            {
                LogManager.EndTime = Time.time;
                LogManager.TotalPoints = scoreManager.CurrentScore;
                LogManager.PrintToTxt();

                AkSoundEngine.PostEvent("Stop_AllEvents", null);
                levelLoader.LoadScene();
            }
        }
    }


    // Child count = amount of targets
    public int CountChildrenTargets(Transform t)
    {
        int k = 0;
        foreach (Transform c in t)
        {
            // if (c.gameObject.activeSelf)
            if (c.gameObject)
                k++;
        }
        TargetCount = k;
        return k;
    }

    // If one tone has been found, activate the next tone
    public Transform ActivateNextTone()
    {
        if (transform.GetChild(1) != null)
        {
            transform.GetChild(1).transform.gameObject.SetActive(true);

            return transform.GetChild(1);
        }
        return null;
    }
}
