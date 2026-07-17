using System.Collections;
using UnityEngine;

public class CanvasObjectController : MonoBehaviour
{
    [SerializeField] float scaleTime = 1f;
    [SerializeField] float growFactor = 1f;
    float maxSize;

    bool scaledUp = false;

    private void Start()
    {
        maxSize = transform.localScale.x;
        transform.localScale = Vector3.zero;
    }

    private void Update()
    {
        transform.LookAt(Camera.main.transform.position);
    }

    public void TurnOn()
    {
        StartCoroutine(ScaleUp());
    }

    public void TurnOff()
    {
        if (scaledUp)
        {
            StartCoroutine(ScaleDown());
        }
        scaledUp = false;
    }

    IEnumerator ScaleUp()
    {
        float timer = 0;
        while (maxSize > transform.localScale.x)
        {
            timer += Time.deltaTime;
            transform.localScale += new Vector3(maxSize, maxSize, maxSize) * Time.deltaTime * growFactor;
            yield return null;
        }
        transform.localScale = Vector3.one;
        timer = 0;
        scaledUp = true;
    }

    IEnumerator ScaleDown()
    {
        float timer = 0;
        while (0 < transform.localScale.x)
        {
            timer += Time.deltaTime;
            transform.localScale -= new Vector3(maxSize, maxSize, maxSize) * Time.deltaTime * growFactor;
            yield return null;
        }
        transform.localScale = Vector3.zero;
        timer = 0;
    }
}
