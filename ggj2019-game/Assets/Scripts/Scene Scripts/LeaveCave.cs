using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoxCollider))]
public class LeaveCave : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
            FindObjectOfType<LevelManager>().LoadLevelAsync("Main", LoadSceneMode.Single);
    }

    private void OnTriggerStay(Collider other)
    {

        if (other.transform.tag == "Player" && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("try to move through door");
            FindObjectOfType<LevelManager>().ContinueToScene();
        }
    }
}
