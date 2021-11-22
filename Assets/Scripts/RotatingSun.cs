using UnityEngine;

public class RotatingSun : MonoBehaviour
{
    public bool Rotate = true;
    public float RotationSpeed = 0.25f;

    private void Update()
    {
        if (Rotate)
            transform.RotateAround(transform.position, Vector3.right + Vector3.forward, RotationSpeed);
    }
}
