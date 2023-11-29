using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundsCheck : MonoBehaviour
{
    public enum eType { center, inset, outset};
    [Header("inscribed")]
    public eType boundsType = eType.center;
    public float radius = 1f;
    public bool keepOnScreen = true;

    [Header("Dyanmic")]
    public float camWidth;
    public float camHeight;
    public bool isOnScreen = true;

    // Start is called before the first frame update
    void Awake()
    {
        camHeight = Camera.main.orthographicSize;
        camWidth = Camera.main.aspect * camHeight;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 pos = transform.position;
        isOnScreen = true;
        float checkRadius = 0;
        if (boundsType == eType.inset) checkRadius = -radius;
        if (boundsType == eType.outset) checkRadius = radius;

        if (pos.x > camWidth + checkRadius)
        {
            pos.x = camWidth + checkRadius;
            isOnScreen = false;
        }

        if (pos.x < -camWidth - checkRadius)
        {
            pos.x = -camWidth - checkRadius;
            isOnScreen = false;

        }

        if (pos.y > camHeight + checkRadius)
        {
            pos.y = camHeight + checkRadius;
            isOnScreen = false;

        }

        if (pos.y < -camHeight - checkRadius)
        {
            pos.y = -camHeight - checkRadius;
            isOnScreen = false;

        }
        if (keepOnScreen && !isOnScreen)
        {
            transform.position = pos;
            isOnScreen = true;  
        }
    }
}
