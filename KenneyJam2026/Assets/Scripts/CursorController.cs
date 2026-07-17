using UnityEngine;
using UnityEngine.InputSystem;

public class CursorController : MonoBehaviour
{
    [SerializeField] GameObject cursorNormal;
    [SerializeField] GameObject cursorUse;
    [SerializeField] GameObject cursorHold;

    [SerializeField] float scaleCursor = 100f;

    Vector2 mousePos;
    Vector3 newMousePos;

    Interact lastHoveredObject;
    GameObject currentCursor;
    bool click = false;

    private void Start()
    {
        Vector3 scale = new Vector3(scaleCursor, scaleCursor, scaleCursor);
        cursorNormal.transform.localScale = scale;
        cursorUse.transform.localScale = scale;
        cursorHold.transform.localScale = scale;
        Cursor.visible = false;
        currentCursor = cursorNormal;
        currentCursor.SetActive(true);
    }

    void Update()    
    {
        newMousePos = Camera.main.ScreenToWorldPoint(mousePos);
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        if (Physics.Raycast(ray, out RaycastHit hit, 100))
        {
            if (hit.transform.tag == "Interact")
            {
                lastHoveredObject = hit.transform.GetComponent<Interact>();
                if (click)
                {
                    lastHoveredObject.ExitHover();
                    currentCursor.SetActive(false);
                    currentCursor = cursorHold;
                    currentCursor.SetActive(true);
                }
                else
                {
                    lastHoveredObject.OnHover();
                    currentCursor.SetActive(false);
                    currentCursor = cursorUse;
                    currentCursor.SetActive(true);
                }
            }
            else if (lastHoveredObject != null)
            {
                lastHoveredObject.ExitHover();
                lastHoveredObject = null;
                currentCursor.SetActive(false);
                currentCursor = cursorNormal;
                currentCursor.SetActive(true);
            }
        }
        currentCursor.transform.position = mousePos;
    }

    public void GetMousePos(InputAction.CallbackContext context)
    {
        mousePos = context.ReadValue<Vector2>();
    }
    public void GetMouseButton(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            click = true;
            if (lastHoveredObject != null)
            {
                lastHoveredObject.OnInteract();
            }
        }
        if (context.canceled)
        {
            click = false;
        }
    }
}
