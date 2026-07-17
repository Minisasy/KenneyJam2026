using UnityEngine;
using UnityEngine.Events;

public class Interact : MonoBehaviour
{
    [SerializeField] GameObject outline;

    public UnityEvent interact;

    CanvasObjectController canvas;

    private void Start()
    {
        canvas = GetComponentInChildren<CanvasObjectController>();
    }

    public void OnHover()
    {
        outline.SetActive(true);
    }
    public void ExitHover()
    {
        outline.SetActive(false);
        canvas.TurnOff();
    }

    public void OnInteract()
    {
        interact.Invoke();
    }
}
