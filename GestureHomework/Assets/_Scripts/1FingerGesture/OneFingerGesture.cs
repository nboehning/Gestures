using UnityEngine;
using System.Collections;

public class OneFingerGesture : MonoBehaviour
{
    private GameObject objectToMove;
    private Vector2 prevPosition;
    private Vector2 curPosition;
    private float touchDelta;
    public int iComfort;
	
	// Update is called once per frame
	void Update ()
    {
	    if (Input.touchCount == 1)
	    {
	        if (Input.GetTouch(0).phase == TouchPhase.Began)
	        {
	            prevPosition = Input.GetTouch(0).position;
	        }
	        if (Input.GetTouch(0).phase == TouchPhase.Ended)
	        {
	            curPosition = Input.GetTouch(0).position;

	            touchDelta = curPosition.magnitude - prevPosition.magnitude;

	            if (Mathf.Abs(touchDelta) > iComfort)
	            {
	                Debug.Log("Swipe");

	                if (touchDelta > 0)
	                {
                        if (Mathf.Abs(curPosition.x - prevPosition.x) > Mathf.Abs(curPosition.y - prevPosition.y))
                        {
                            Debug.Log("Right");
                        }
                        else
                        {
                            Debug.Log("Top");
                        }
                    }
	                else
	                {
	                    if (Mathf.Abs(curPosition.x - prevPosition.x) > Mathf.Abs(curPosition.y - prevPosition.y))
	                    {
	                        Debug.Log("Right");
	                    }
	                    else
	                    {
	                        Debug.Log("Top");
	                    }
	                }
	            }
	            else
	            {
	                if (Input.GetTouch(0).tapCount == 1)
	                {
	                    Debug.Log("Detected 1 finger single tap");

	                    RaycastHit hit;
	                    Ray ray;

	                    ray = Camera.current.ScreenPointToRay(Input.GetTouch(0).position);
	                    if (Physics.Raycast(ray, out hit))
	                    {
	                        // Select an object
	                        Debug.Log(hit.transform.name);

	                        if (objectToMove != null && hit.transform.name != objectToMove.transform.name)
	                        {
	                            // Stop the particle system
	                            objectToMove.GetComponent<ParticleSystem>().Play();
	                        }

	                        objectToMove = GameObject.Find(hit.transform.name);
	                        objectToMove.GetComponent<ParticleSystem>().Play();
	                    }
	                }
	                else if (Input.GetTouch(0).tapCount == 2)
	                {
	                    if (objectToMove != null)
	                    {
	                        Vector3 pos;
	                        pos.x = Input.GetTouch(0).position.x;
	                        pos.y = Input.GetTouch(0).position.y;

	                        pos.z = Mathf.Abs(Camera.current.transform.position.z - objectToMove.transform.position.z);

	                        objectToMove.transform.position = Camera.current.ScreenToWorldPoint(pos);
	                    }
	                }
	            }
	        }
	    }
	}
}
