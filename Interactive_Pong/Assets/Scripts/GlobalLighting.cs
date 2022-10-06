using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GlobalLighting : MonoBehaviour
{
    UnityEngine.Rendering.Universal.Light2D GlobalLight;
    void Start()
    {
        StartCoroutine(ExampleCoroutine());

    }


    IEnumerator ExampleCoroutine()
    {
        //Print the time of when the function is first called.
        Debug.Log("Started Coroutine at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(1);

        GlobalLight = GetComponent<UnityEngine.Rendering.Universal.Light2D>();
        GlobalLight.intensity = 0f;

        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    }
}
