using UnityEngine;
using System.Collections;

public class TwoFingerGesture : MonoBehaviour
{

	public GameObject objectToRotate;
	private Vector2 curPosition;
	private Vector2 prevPosition;
	private float touchDelta;
	public float comfortZone = 2;
	public Camera myCamera;
    public int zoom = 5;
    private float angle;
    private int rotateSpeed = 8;

	// Update is called once per frame
	void Update ()
	{
		if (Input.touchCount == 2 && Input.GetTouch(0).phase == TouchPhase.Moved &&
			Input.GetTouch(1).phase == TouchPhase.Moved)
		{
			curPosition = Input.GetTouch(0).position - Input.GetTouch(1).position;
			prevPosition = (Input.GetTouch(0).position - Input.GetTouch(0).deltaPosition) -
						   (Input.GetTouch(1).position - Input.GetTouch(1).deltaPosition);

			touchDelta = curPosition.magnitude - prevPosition.magnitude;

		    angle = Vector2.Angle(prevPosition, curPosition);

		    if (angle > 0.1f)
		    {
		        // Rotation
		        Debug.Log(Vector3.Cross(curPosition, prevPosition));

		        if (Vector3.Cross(curPosition, prevPosition).z < 0)
		        {
		            Debug.Log("Counter-Clockwise");
                    objectToRotate.transform.Rotate(Vector3.up, angle * -1 * rotateSpeed);
                }
		        else
		        {
		            Debug.Log("Clockwise");
		            objectToRotate.transform.Rotate(Vector3.up, angle * rotateSpeed);
		        }
		    }

			if (Mathf.Abs(touchDelta) > comfortZone)
			{
				
				if (touchDelta > 0)
				{
					// Zoom in
					Debug.Log("Zoom In");

				    myCamera.fieldOfView = Mathf.Clamp(Mathf.Lerp(myCamera.fieldOfView, myCamera.fieldOfView - Mathf.Abs(touchDelta) * zoom,
				        Time.deltaTime * zoom), 10, 17);
				}
				else
				{
					// Zoom out
					Debug.Log("Zoom Out");

				    myCamera.fieldOfView = Mathf.Clamp(Mathf.Lerp(myCamera.fieldOfView, myCamera.fieldOfView + Mathf.Abs(touchDelta)*zoom,
				        Time.deltaTime*zoom), 10, 17);
				}
			}

		}
	}
}
