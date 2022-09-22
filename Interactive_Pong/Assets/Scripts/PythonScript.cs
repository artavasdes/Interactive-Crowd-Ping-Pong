using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Python.Runtime;
using System;

public class PythonScript : MonoBehaviour
{

    dynamic np;

    // Start is called before the first frame update
    void OnClick()
    {
        Runtime.PythonDLL = Application.dataPath + "/StreamingAssets/embedded-python/python37.dll";
        PythonEngine.Initialize(mode: ShutdownMode.Reload);
        try
        {
            np = PyModule.Import("numpy");
            print("pi: " + np.pi);
        }
        catch (Exception e)
        {
            print(e);
            print(e.StackTrace);
        }

    }

     public void OnApplicationQuit()
    {
        if (PythonEngine.IsInitialized)
    {
            print("ending python");
            PythonEngine.Shutdown(ShutdownMode.Reload);
        }
    }
}