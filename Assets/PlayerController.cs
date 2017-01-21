using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum InputType
{
    hold,
    press
}

public class PlayerController : MonoBehaviour {

    [Serializable]
    public class PlayerInputEvent
    {
        public KeyCode Key;
        public InputType Type;
        public UnityEvent Action;
    }

    [SerializeField]
    private List<PlayerInputEvent> PlayerInputEvents;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		foreach(var inputEvent in PlayerInputEvents)
        {
            switch(inputEvent.Type)
            {
                case InputType.press:
                {
                    if (Input.GetKeyDown(inputEvent.Key))
                    {
                        inputEvent.Action.Invoke();
                    }
                    break;
                }                    
                case InputType.hold:
                {
                    if (Input.GetKey(inputEvent.Key))
                    {
                        inputEvent.Action.Invoke();
                    }
                    break;
                }
                default:
                    break;
                    
            }            
        }
    }


}
