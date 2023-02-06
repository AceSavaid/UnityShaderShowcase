using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBetweenPoints : MonoBehaviour
{
    [SerializeField]  bool multiPoint = false;
    [SerializeField] float speed;
    bool reverse = false;
    [SerializeField]  Transform endpoint;
    public Vector3 startPoint;
    // Start is called before the first frame update
    void Start()
    {
        if (multiPoint)
        {
            startPoint = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!reverse)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, endpoint.position, speed * Time.deltaTime);
            if (Vector3.Distance(gameObject.transform.position,endpoint.position )<= 0.9)
            {
                reverse = true;
            }
        }
        else
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, startPoint, speed * Time.deltaTime);
            if (Vector3.Distance(gameObject.transform.position, startPoint) <= 0.9)
            {
                reverse = false;
            }
        }
    }
}
