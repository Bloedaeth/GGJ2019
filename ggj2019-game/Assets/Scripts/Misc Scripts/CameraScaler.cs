using System.Collections;
using UnityEngine;

public class CameraScaler : MonoBehaviour
{
    public GameObject player;
    Vector3 previousPlayerPos;
    [SerializeField] GameObject LeftBound = null;

    [Tooltip("Time for camera to scale")]
    public float scaleTime = 2.0f;

    void Start()
    {
        if (player == null)
        {
            Debug.LogError("Player has not been assigned");
        }
    }

    void Update()
    {
        // debug control
        if (Input.GetKeyDown(KeyCode.L)) // when player levels up
        {
            StartScaleCam();
        }        
    }

    private void LateUpdate()
    {
        //Follow the player
        if (previousPlayerPos != null)
        {
            transform.position += player.transform.position - previousPlayerPos;
            previousPlayerPos = player.transform.position;
            transform.position = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
        }

        //Don't go past the left bound
        if (LeftBound!= null && transform.position.x < LeftBound.transform.position.x)
        {
            transform.position = new Vector3(LeftBound.transform.position.x, transform.position.y, transform.position.z);
        }
    }

    public void StartScaleCam()
    {
        StartCoroutine(ScaleCam(scaleTime));
    }

    private IEnumerator ScaleCam(float timeToMove)
    {
        var currentPos = transform.position;
        var t = Time.deltaTime / timeToMove;
        while (t < 1)
        {
            t += Time.deltaTime / scaleTime;
            transform.position = Vector3.Slerp(currentPos, new Vector3(transform.position.x, currentPos.y + 3, currentPos.z - 8), t);
            yield return null;
        }

        Debug.Log("cam pos is now " + transform.position);
    }
}
