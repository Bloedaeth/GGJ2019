using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonHeadAim : MonoBehaviour
{
    public Vector3 Target;
    public Vector3 Offset;
    Animator anim;
    public GameObject Head;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector2 cursor = GetCursor();
        Target = new Vector3(cursor.x, cursor.y, transform.position.z);
        Debug.Log("MousePos: " + Input.mousePosition);
        Debug.Log("Cursor: " + cursor);
        Debug.Log("Head: " + Head.transform.position);
        Debug.Log("Target: " + Target);
		//Head.transform.LookAt(-Target); //doesnt work (weird, thought it would act the same as below)

		Head.transform.forward = -(Target - Head.transform.position).normalized;
		//Head.transform.rotation = Quaternion.Euler(Head.transform.rotation.x, 0, Head.transform.rotation.z); //doesnt help, breaks it further

		//Not sure what these two lines are, am guessing we established they dont work
		//Head.transform.rotation = Quaternion.Euler(Head.transform.rotation.x - 57.84f, Head.transform.rotation.y + 32.48f, Head.transform.rotation.z - 55.6f);
		//Head.transform.rotation = Head.transform.rotation * Quaternion.Euler(Offset);
		Debug.DrawLine(Head.transform.position, Target, Color.red);

		//Make body face
		if(Target.x < transform.position.x)
		{
			//Look left
			transform.rotation = Quaternion.Euler(Vector3.up * 270);
		}
		else
		{
			//Look right
			transform.rotation = Quaternion.Euler(Vector3.up * 90);
		}
	}

    Vector2 GetCursor()
    {
        Vector2 cursor =
            Camera.main.ScreenToWorldPoint
            (
                new Vector3
                (
					//Inverting it didnt help, broke more stuff
                    /*Camera.main.pixelWidth -*/ Input.mousePosition.x,
                    /*Camera.main.pixelHeight -*/ Input.mousePosition.y,
                    Vector3.Distance
                    (
                        Camera.main.transform.position,
                        Head.transform.position
                    )
                )
            );
        var direction = (new Vector2(Head.transform.position.x, Head.transform.position.y) - cursor);
        return cursor;
    }
}
