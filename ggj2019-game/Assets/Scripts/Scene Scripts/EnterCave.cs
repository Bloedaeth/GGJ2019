using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoxCollider))]
public class EnterCave : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		if(other.transform.tag == "Player")
			FindObjectOfType<LevelManager>().LoadLevelAsync("Cave2", LoadSceneMode.Single);
	}


    private void OnTriggerStay(Collider other)
    {

        if (other.transform.tag == "Player" && Input.GetKeyDown(KeyCode.Space))
        {
            FindObjectOfType<LevelManager>().ContinueToScene();
        }
    }
}
