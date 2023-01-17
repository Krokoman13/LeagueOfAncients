using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

using UnityEngine.Events;

[ExecuteInEditMode]
public class SaveReminder : MonoBehaviour
{
    bool reminding;
    [SerializeField] double delayTimeSec = 30f;
    [SerializeField] bool autoSave = false;
    double passedTimeSecs = 0;

    double deltaTime;
    double previousTime;

    // Update is called once per frame
    void Update()
    {
        deltaTime = EditorApplication.timeSinceStartup - previousTime;
        previousTime = EditorApplication.timeSinceStartup;

        if (!EditorApplication.isSceneDirty)
        {
            if (reminding)
            {
                reminding = false;
                foreach (Transform child in transform)
                {
                    child.gameObject.SetActive(false);
                }
                passedTimeSecs = 0;
            }

            return;
        }

        if (!reminding)
        {
            Debug.Log("Scene is dirty");
            reminding = true;
            deltaTime = 0;
            passedTimeSecs = 0;
        }

        passedTimeSecs += deltaTime;

        if (passedTimeSecs > delayTimeSec)
        {
            Debug.LogWarning("Save!! NOW!!");

            if (autoSave) 
            {
                EditorApplication.SaveScene();
                return;
            }
            
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);
            }
        }    
    }
}
