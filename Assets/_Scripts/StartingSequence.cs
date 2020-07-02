using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartingSequence : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartingWait());
    }

    IEnumerator StartingWait()
    {
        yield return new WaitForSeconds(11.5f);

        SceneManager.LoadScene("SampleScene");
    }
}
