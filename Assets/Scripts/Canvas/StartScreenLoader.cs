using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

// Details: StartScreenLoader
// Loading screen at the boot of the game for 5 seconds

public class StartScreenLoader : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(StartScreenLoaderFn());
        AkSoundEngine.PostEvent("Play_BGM_Menu", gameObject);
    }

    IEnumerator StartScreenLoaderFn()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("StartSettings");
    }
}
