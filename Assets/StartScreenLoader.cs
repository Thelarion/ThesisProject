using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreenLoader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartScreenLoaderFn());
    }

    IEnumerator StartScreenLoaderFn()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("StartSettings");
    }
}
