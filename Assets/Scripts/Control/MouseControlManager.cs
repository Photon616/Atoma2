using UnityEngine;
using UnityEngine.InputSystem;

public class MouseControlManager : MonoBehaviour
{
    public static MouseControlManager Instance{ get; private set; }

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
        Instance = this;
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
        
        if (clickAction.WasPressedThisFrame() && PauseMenu.isPaused == false && !IsMouseOnUI.isMouseOnUI)
        {
            Debug.Log("Left click");

            switch (ToolStateManager.Instance.GetCurrentTool())
            {
                case ToolStateManager.Tool.select:
                    Debug.Log("select tool");

                    if (pointingObject.TryGetComponent(out AtomManager c))
                    {
                        Debug.Log($"Atom click, id: {c.GetID()}");

                        Deselect();
                        selectedObject = pointingObject;
                        // selectedObject.GetComponent<AtomManager>().Selected();

                        selectedObject.GetComponent<AtomManager>().StartDrag();
                        isDragging = true;
                        offsetFromPointer = selectedObject.transform.position - mousePosition;
                    }
                    else
                    {
                        Debug.Log($"Clicked {pointingObject} (NOT an atom)");
                        Deselect();
                    }
                    break;
                
                case ToolStateManager.Tool.place:
                    Debug.Log("place tool");

                    PhysicsManager.Instance.AddAtom((Vector2)mousePosition);
                    break;
                
                case ToolStateManager.Tool.bond:
                    Debug.Log("bonding tool");

                    if (pointingObject.TryGetComponent<AtomManager>(out AtomManager component)) // 원자를 클릭했는데
                    {
                        Debug.Log($"Atom click, id: {component.GetID()}");

                        if (selectedObject == null) // 선택된 게 없으면
                        {
                            Debug.Log("First click");
                            selectedObject = pointingObject; // 선택하기
                            // selectedObject.GetComponent<AtomManager>().Selected();
                        }
                        else if (selectedObject != pointingObject) // 이미 선택된 게 있고 선택된 거랑 다른 원자면
                        {
                            Debug.Log("Second click");
                            AtomManager selectedManager = selectedObject.GetComponent<AtomManager>(); // 결합하기
                            PhysicsManager.Instance.AddBond(selectedManager.GetID(), component.GetID());
                            Deselect();
                            // Debug.Log("bonded");
                        }
                    }
                    else
                    {
                        Debug.Log($"Clicked {pointingObject} (NOT an atom)");
                        Deselect();
                    }
                    break;

                case ToolStateManager.Tool.delete:
                    Debug.Log("Delete tool");

                    if (pointingObject.TryGetComponent<AtomManager>(out AtomManager howBigIsThisScope))
                    {
                        Debug.Log($"Atom click, id: {howBigIsThisScope.GetID()}");
                        PhysicsManager.Instance.DeleteAtom(howBigIsThisScope.GetID());
                    }
                    break;
                
                case ToolStateManager.Tool.cut:
                    Debug.Log("Cut tool");

                    if (pointingObject.TryGetComponent<BondManager>(out BondManager component1))
                    {
                        Debug.Log($"Bond click, id: {component1.GetID()}");
                        PhysicsManager.Instance.DeleteBond(component1.GetID());
                    }
                    break;
            }
        }
        if (clickAction.WasReleasedThisFrame() && isDragging)
        {
            isDragging = false;
            selectedObject.GetComponent<AtomManager>().EndDrag();
        }

        if (rightCLickAction.WasPressedThisFrame()) // 우클릭하면 취소
        {
            ToolStateManager.Instance.SetCurrentTool(ToolStateManager.Tool.select);
            if (selectedObject != null && selectedObject.TryGetComponent<AtomManager>(out AtomManager component)) // 원자가 선택되어 있다면
            {
                component.Deselected();
            }
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

        if (selectedObject != null)
        {
            selectedObject.GetComponent<AtomManager>().Selected();
        }
    }

    private void Deselect()
    {
        if (selectedObject != null)
        {
            selectedObject.GetComponent<AtomManager>().Deselected();
        }
        selectedObject = null;
    }
}
