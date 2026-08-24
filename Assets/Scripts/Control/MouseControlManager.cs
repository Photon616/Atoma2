using UnityEngine;
using UnityEngine.InputSystem;

public class MouseControlManager : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputAction pointAction;
    private InputAction clickAction;
    private InputAction rightCLickAction;
    private Camera cam;
    private Vector3 mousePosition;
    private GameObject pointingObject;
    private GameObject selectedObject;
    private bool isDragging = false;
    private Vector2 offsetFromPointer;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        cam = GetComponent<Camera>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        pointAction = playerInput.actions["Point"];
        clickAction = playerInput.actions["Click"];
        rightCLickAction = playerInput.actions["RightClick"];
    }

    // Update is called once per frame
    private void Update()
    {
        Ray ray = cam.ScreenPointToRay(pointAction.ReadValue<Vector2>());
        if (Physics.Raycast(ray, out RaycastHit hit, 100f))
        {
            mousePosition = hit.point;
            pointingObject = hit.collider.gameObject;
        }
        // Debug.Log(pointingObject);

        if (clickAction.WasPressedThisFrame())
        {
            switch (ToolStateManager.Instance.currentTool)
            {
                case ToolStateManager.Tool.select:
                    if (pointingObject.TryGetComponent(out AtomManager c))
                    {
                        Debug.Log("a");
                        selectedObject = pointingObject;
                        selectedObject.GetComponent<AtomManager>().StartDrag();
                        isDragging = true;
                        offsetFromPointer = selectedObject.transform.position - mousePosition;
                    }
                    else
                    {
                        selectedObject = null;
                    }
                    break;
                
                case ToolStateManager.Tool.place:
                    PhysicsManager.Instance.AddAtom(mousePosition);
                    break;
                
                case ToolStateManager.Tool.bond:
                    if (pointingObject.TryGetComponent<AtomManager>(out AtomManager component)) // 원자를 클릭했는데
                    {
                        if (selectedObject == null) // 선택된 게 없으면
                        {
                            selectedObject = pointingObject; // 선택하기
                        }
                        else // 이미 선택된 게 있으면
                        {
                            AtomManager selectedManager = selectedObject.GetComponent<AtomManager>(); // 결합하기
                            PhysicsManager.Instance.AddBond(selectedManager.GetID(), component.GetID());
                        }
                    }
                    break;
            }
        }
        if (clickAction.WasReleasedThisFrame())
        {
            isDragging = false;
            selectedObject.GetComponent<AtomManager>().EndDrag();
        }

        if (isDragging)
        {
            selectedObject.transform.position = new Vector3
            (
                mousePosition.x + offsetFromPointer.x, 
                mousePosition.y + offsetFromPointer.y, 
                selectedObject.transform.position.z
            );
            // Debug.Log($"{offsetFromPointer} {mousePosition}");
        }
    }
}
