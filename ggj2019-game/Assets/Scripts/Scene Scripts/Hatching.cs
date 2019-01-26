using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hatching : MonoBehaviour
{
    [SerializeField] float cutsceneDuration;
    GameObject mainCamera;
    [SerializeField] GameObject cameraEndPosition;
    [SerializeField] GameObject cameraStartPosition;
    DragonControls dragonControls;
    [SerializeField] GameObject egg;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        dragonControls = FindObjectOfType<DragonControls>();
        dragonControls.ToggleControl(false);
        StartCutscene();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StartCutscene()
    {
        StartCoroutine(HatchCutscene(cutsceneDuration));
    }

    private IEnumerator HatchCutscene(float duration)
    {
        var t = Time.deltaTime / duration;
        while (t < 1)
        {
            t += Time.deltaTime / duration;
            mainCamera.transform.position = Vector3.Slerp(cameraStartPosition.transform.position, cameraEndPosition.transform.position, t);
            yield return null;
        }
        dragonControls.ToggleControl(true);
        Destroy(egg);
    }
}
