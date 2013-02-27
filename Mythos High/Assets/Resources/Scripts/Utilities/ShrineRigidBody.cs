using UnityEngine;
using System.Collections;

public class ShrineRigidBody : MonoBehaviour
{
    public Rigidbody[] bodies;
    public float intensity = 100f, duration = 3f;

    //public Color[] start, end;

    void Start()
    {
        //start = new Color[bodies.Length];
       // end = new Color[bodies.Length];
        for (int i = 0; i < bodies.Length; i++)
        {
            //start[i] = bodies[i].gameObject.renderer.material.color;
            //end[i] = new Color(start[i].r, start[i].g, start[i].b, 0f);
            bodies[i].AddForce(-Vector3.right * intensity, ForceMode.Impulse);
        }
        
        //Invoke("destroy", 3f);
        //StartCoroutine(delay());
    }

    //IEnumerator delay()
    //{
    //    yield return new WaitForSeconds(1.5f);
    //    StartCoroutine(fadeOut());
    //}

    //IEnumerator fadeOut()
    //{
    //    for (float i = 0; i < duration; i += Time.deltaTime)
    //    {
    //        for(int j = 0; j < bodies.Length; j++) {
    //            bodies[j].gameObject.renderer.material.color = Color.Lerp(start[j], end[j], i / duration);
    //        }
    //        yield return null;
    //    }
    //}

    void Update()
    {
        //if (Input.GetKeyUp(KeyCode.J)) StartCoroutine(fadeOut());
    }

    void destroy() { DestroyObject(gameObject); }
}
