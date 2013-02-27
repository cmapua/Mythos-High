using UnityEngine;
using System;
using System.Collections.Generic;

class Particles : MonoBehaviour
{
    float duration;
    //public Transform parentPos;
    void Awake()
    {
        duration = transform.Find("heal-skill-animation").particleSystem.duration;
        Invoke("destroy", duration);
    }

    void Update()
    {
        //transform.Translate(parentPos.position);
    }
    void destroy() { Destroy(gameObject); }
}
