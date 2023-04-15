using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    public Camera cam;
    public Transform followTarget;

    Vector2 startingPosition;

    float StartingZ;

    Vector2 cameraSinceStart => (Vector2)cam.transform.position - startingPosition;

    float ZdistanceFromTraget => transform.position.z - followTarget.position.z;

    float clippingPlane => (cam.transform.position.z + (ZdistanceFromTraget > 0 ? cam.farClipPlane : cam.nearClipPlane));

    float parallaxFactor => Mathf.Abs(ZdistanceFromTraget) / clippingPlane;
    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
        StartingZ = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 newposition = startingPosition + cameraSinceStart * parallaxFactor;

        transform.position = new Vector3(newposition.x, newposition.y, StartingZ);
    }
}
