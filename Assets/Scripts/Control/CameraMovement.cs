using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private float CameraSpeed;
    [SerializeField] private float smoothInputSpeed = 0.2f;
    [SerializeField] private float zoomMin = 2.0f;
    [SerializeField] private float zoomMax = 20.0f;
    [SerializeField] private float zoomDefault = 5.0f;
    [SerializeField] private float zoomSpeed = 1.0f;
    [SerializeField] private float zoomLerpFactor = 0.5f;
    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction zoomAction;
    private Vector2 currentInputVector;
    private Vector2 smoothInputVelocity;
    private float zoomTarget;

    private void Awake()
    {
        zoomTarget = zoomDefault;
    }

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];
        zoomAction = playerInput.actions["Zoom"];
    }

    // Update is called once per frame
    private void Update()
    {
        Vector2 moveInput = moveAction.ReadValue<Vector2>();
        Vector2 zoomInput = zoomAction.ReadValue<Vector2>();

        currentInputVector = Vector2.SmoothDamp(currentInputVector, moveInput, ref smoothInputVelocity, smoothInputSpeed);

        transform.position += new Vector3(currentInputVector.x, currentInputVector.y) * CameraSpeed * (cam.orthographicSize / 5.0f) * Time.deltaTime; // 인풋 * 속도 * 줌 배율 * 델타타임

        zoomTarget += zoomInput.y * zoomSpeed * Time.deltaTime;
        
        if (zoomTarget > zoomMax)
        {
            zoomTarget = zoomMax;
        }
        else if (zoomTarget < zoomMin)
        {
            zoomTarget = zoomMin;
        }

        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, zoomTarget, zoomLerpFactor * Time.deltaTime);
        // Debug.Log();
    }
}
