using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaSceneLoader : MonoBehaviour
{
    private bool isLoaded;
    private bool shouldLoad;

    [SerializeField]
    private Offset enteredDolphin;
    [Header("Distance Check")]
    [SerializeField]
    private float loadRange;
    [SerializeField]
    private GameObject areaWayPoint;
    // Start is called before the first frame update
    void Start()
    {
        int amountOfScenes = SceneManager.sceneCount;
        if(amountOfScenes > 0)
        {
            for (int i = 0; i < amountOfScenes; i++)
            {
                Scene scene = SceneManager.GetSceneAt(i);
                if(scene.name == gameObject.name)
                {
                    isLoaded = true;
                }
            }
        }
    }
    private void Update()
    {
        
    }

    void DistanceCheck()
    {
        if (Vector3.Distance(enteredDolphin.transform.position, areaWayPoint.transform.position) < loadRange)
        {
            LoadScene();
        }
        else
        {
            UnloadScene();
        }
    }

    void LoadScene()
    {
        if(!isLoaded)
        {
            SceneManager.LoadSceneAsync(gameObject.name, LoadSceneMode.Additive);
            isLoaded = true;
        }
    }

    void UnloadScene()
    {
        if (isLoaded)
        {
            SceneManager.UnloadSceneAsync(gameObject.name);
            isLoaded = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Offset triggeredDolphin = other.GetComponent<Offset>();
        if (triggeredDolphin == null) return;

        enteredDolphin = triggeredDolphin;
        LoadScene();
    }

    private void OnTriggerExit(Collider other)
    {
        Offset triggeredDolphin = other.GetComponent<Offset>();
        if (triggeredDolphin == null) return;

        if (enteredDolphin != triggeredDolphin) return;
        enteredDolphin = null;
        UnloadScene();
    }
}
