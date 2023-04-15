using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionZone : MonoBehaviour
{
    public List<Collider2D> detectColliders = new List<Collider2D>();
    Collider2D collider;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame

    private void OnTriggerEnter2D(Collider2D other)
    {
        detectColliders.Add(other);
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        detectColliders.Remove(other);
    }
    void Update()
    {
        
    }
}
