using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Python.Runtime;
using System;

public class ConfigButton : MonoBehaviour
{
    dynamic cv;
    dynamic video;
    // Start is called before the first frame update

    void Start()
    {
        Runtime.PythonDLL = Application.dataPath + "/StreamingAssets/embedded-python/python37.dll";
        PythonEngine.Initialize(mode: ShutdownMode.Reload);
        print("on startup");
    }
    public void OnClick()
    {
        dynamic frame;
        try
        {
            print("onclick");
            cv = PyModule.Import("cv2");
            video = cv.VideoCapture(0);
            //video = cv.VideoCapture("http://192.168.1.178:8080/video");
            while (Input.GetKey(KeyCode.Space) == false)
            {
                //(bool, dynamic) tuple = video.read();
                //(success, frame) = tuple;
                frame = video.read();
                //print(success);
                print(frame[0]);
                print(frame[1]);
                if(((bool) frame[0]) == true)
                {
                    cv.imshow("Feed", frame[1]);
                }               
                
            }
            video.release();
            cv.destroyAllWindows();
            print("end video");
        }
        catch (Exception e)
        {
            print("exception");
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
