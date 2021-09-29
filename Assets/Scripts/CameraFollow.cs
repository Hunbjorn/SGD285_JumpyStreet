
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	
    public Transform target;

    public float smoothSpeed = 0.125f;
	public float turnSpeed = 4.0f;
	public float height = 1f;
	public float distance = 2f;
	
    public Vector3 offsetX;
	public Vector3 offsetY;
	
    void LateUpdate () 
    {
        transform.position = target.position + offsetX + offsetY;
		transform.LookAt(target.position);
    }
}
