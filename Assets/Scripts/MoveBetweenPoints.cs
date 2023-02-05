using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBetweenPoints : MonoBehaviour
{
    [SerializeField]  bool multiPoint = false;
    [SerializeField] float speed;
    bool reverse;
    [SerializeField]  Transform endpoint;
    Transform startPoint;
    // Start is called before the first frame update
    void Start()
    {
        if (multiPoint)
        {
            startPoint = gameObject.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!reverse)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, endpoint.position, speed * Time.deltaTime);
            if (gameObject.transform.position == endpoint.position)
            {
                reverse = true;
            }
        }
        else
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, startPoint.position, speed * Time.deltaTime);
            if (gameObject.transform.position == startPoint.position)
            {
                reverse = true;
            }
        }
    }
}
