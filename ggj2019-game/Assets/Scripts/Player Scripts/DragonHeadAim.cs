using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonHeadAim : MonoBehaviour
{
    public bool active;
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
		if(active)
        {
			Vector2 direction = (Target - Head.transform.position).normalized;
			float targetAngle = Mathf.Atan2(direction.y, Mathf.Abs(direction.x)) * Mathf.Rad2Deg;

			//Use local y axis due to weirdness with the axes somewhere in the hierarchy
			float zRotation = targetAngle - Head.transform.localRotation.eulerAngles.y;
			if(Target.x < transform.position.x) // Facing left
				zRotation = -zRotation;

			Head.transform.Rotate(new Vector3(0, 0, zRotation), Space.World);
		}
        //Make body face
        if (Target.x < transform.position.x && GameObject.Find("Egg") == null) 
        {
            //Look left
            transform.rotation = Quaternion.Euler(Vector3.up * 270);
        }
        else
        {
            //Look right
            transform.rotation = Quaternion.Euler(Vector3.up * 90);
        }


        //Not sure what these two lines are, am guessing we established they dont work
        //Head.transform.rotation = Quaternion.Euler(Head.transform.rotation.x - 57.84f, Head.transform.rotation.y + 32.48f, Head.transform.rotation.z - 55.6f);
        //Head.transform.rotation = Head.transform.rotation * Quaternion.Euler(Offset);
        Debug.DrawLine(Head.transform.position, Target, Color.red);
        
    }

    Vector2 GetCursor()
    {
        Vector2 cursor =
            Camera.main.ScreenToWorldPoint
            (
                new Vector3
                (
					Input.mousePosition.x,
                    Input.mousePosition.y,
                    Vector3.Distance
                    (
                        Camera.main.transform.position,
                        transform.position
                    )
                )
            );
        return cursor;
    }
}

