using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private Transform cam;

    private void Update()
    {
        transform.position = new Vector3(cam.position.x, cam.position.y, transform.position.z);
    }
}
