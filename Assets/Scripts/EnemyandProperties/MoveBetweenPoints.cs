using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBetweenPoints : MonoBehaviour
{
    [SerializeField]  bool bothWays = false;
    [SerializeField] float speed = 5.0f;
    private bool reverse = false;
    [SerializeField] Transform endpoint;
    private Vector3 startPoint;
    private Vector3 ePoint;
    // Start is called before the first frame update
    void Start()
    {
        if (bothWays)
        {
            startPoint = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        }
        ePoint = endpoint.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!reverse)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, ePoint, speed * Time.deltaTime);
            if (Vector3.Distance(gameObject.transform.position, ePoint) <= 0.9)
            {
                reverse = true;
            }
        }
        else if (bothWays && reverse) 
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, startPoint, speed * Time.deltaTime);
            if (Vector3.Distance(gameObject.transform.position, startPoint) <= 0.9)
            {
                reverse = false;
            }
        }
    }
}
