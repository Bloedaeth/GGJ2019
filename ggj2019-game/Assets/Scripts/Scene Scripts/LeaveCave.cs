using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoxCollider))]
public class LeaveCave : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
            FindObjectOfType<LevelManager>().LoadLevelAsync("Main", LoadSceneMode.Additive);
    }
}
