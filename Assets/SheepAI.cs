using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepAI : MonoBehaviour
{
    private Rigidbody rb;
    private GameObject dog;
    private float angle;
    private static int startleDistance = 10;
    private static int moveSpeed = 400;

    // Start is called before the first frame update
    void Start()
    {
        dog = GameObject.Find("Dog");
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(dog.transform.position);

        var heading = transform.position - dog.transform.position;
        var distance = heading.magnitude;
        var direction = heading / distance;
        var angle = Vector3.Angle(dog.transform.position, transform.position);
        //Debug.Log($"heading: {heading}");
        //Debug.Log($"distance: {distance}");
        //Debug.Log($"direction: {direction}");

        var theta = Mathf.Atan2(heading.x, heading.z) * Mathf.Rad2Deg;
        angle = theta + 180;

        //Debug.Log($"angle: {angle}");

        if (distance < startleDistance)
        {
            var step = 1000 * Time.deltaTime;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(90, angle, 0), step);

            var speed = (startleDistance - distance) / startleDistance;

            rb.velocity = Quaternion.Euler(0, angle + 90, 0) * new Vector3(speed * 10, 0, 0);
        }
    }
}
