using UnityEngine;

public class FPSRandomRotateAngle : MonoBehaviour
{
    public bool RotateX;
    public bool RotateY;
    public bool RotateZ = true;
	
	private void Start()
	{
	    var rotateVector = Vector3.zero;
	    if (RotateX)
	        rotateVector.x = Random.Range(0, 360);
        if (RotateY)
            rotateVector.y = Random.Range(0, 360);
        if (RotateZ)
            rotateVector.z = Random.Range(0, 360);
        transform.Rotate(rotateVector);
	}
}
