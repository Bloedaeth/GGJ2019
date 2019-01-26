using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonControls : MonoBehaviour
{
	public KeyCode biteControl = KeyCode.Mouse0;
	public KeyCode flyControl = KeyCode.W;
	public KeyCode breathFireControl = KeyCode.Mouse1;
    public KeyCode regurgitateTest = KeyCode.P; //biteControl will have the regurgitate behaviour if in lair, but this control can be used for testing otherwise
}
