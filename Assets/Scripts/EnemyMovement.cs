using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform[] patrolPoints;
    public float speed;
    public int patrolDestination;

    public Transform playerTransform;
    public bool isChasing;
    public float chaseDistance;

    public UniformDistributionMethod uniformDistributionScript;
    private List<float> riValues = new List<float>();

    void Start()
    {
        uniformDistributionScript = GetComponent<UniformDistributionMethod>();
        if (uniformDistributionScript != null)
        {
            // Llama a la función FillRiValues() del script UniformDistributionMethod.
            riValues = uniformDistributionScript.GetRiValues();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isChasing)
        {
            if (transform.position.x > playerTransform.position.x)
            {
                transform.localScale = new Vector3(0.6f, 0.6f, 1);
                transform.position += Vector3.left * speed * Time.deltaTime;
            }
            if (transform.position.x < playerTransform.position.x)
            {
                transform.localScale = new Vector3(-0.6f, 0.6f, 1);
                transform.position += Vector3.right * speed * Time.deltaTime;
            }
        }
        else // not chasing random walk
        {
            if(Vector2.Distance(transform.position, playerTransform.position) < chaseDistance)
            {
                isChasing = true;
            }
            
            if(patrolDestination == 0)
            {
                transform.position = Vector2.MoveTowards(transform.position, patrolPoints[0].position, speed * Time.deltaTime);
                if(Vector2.Distance(transform.position, patrolPoints[0].position) < .2f)
                {
                    transform.localScale = new Vector3(0.6f, 0.6f, 1);
                    patrolDestination = 1;
                }
            }
            if(patrolDestination == 1)
            {
                transform.position = Vector2.MoveTowards(transform.position, patrolPoints[1].position, speed * Time.deltaTime);
                if(Vector2.Distance(transform.position, patrolPoints[1].position) < .2f)
                {
                    transform.localScale = new Vector3(-0.6f, 0.6f, 1);
                    patrolDestination = 0;
                }
            }
        }

    }
}
