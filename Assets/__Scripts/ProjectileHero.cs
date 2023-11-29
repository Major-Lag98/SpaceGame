using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHero : MonoBehaviour
{
    private BoundsCheck bndCheck;
    // Start is called before the first frame update
    void Awake()
    {
        bndCheck = GetComponent<BoundsCheck>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!bndCheck.isOnScreen)
        {
            Destroy(gameObject);
        }
    }
}
