using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    public int speed;
    public ProjectileTypeEnum type;
    public int size;
    public int baseDamage;
    public float lifetime;

    private bool targetSet = false;
    private Transform target;

    private Vector3 lastPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lifetime -= Time.deltaTime;
        if (targetSet) {
            if (!target)
            {
                lastPosition += Vector3.Normalize(lastPosition - gameObject.transform.position);
                gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, lastPosition, speed * Time.deltaTime);
            }
            else {
                lastPosition = target.position;
                gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, lastPosition, speed * Time.deltaTime);
            }
        }

        if (lifetime <= 0.0f) {
            Destroy(gameObject);
        }
    }

    void OnDestroy() {

    }

    public void SetTarget(Transform targetArg) {
        this.target = targetArg;
        targetSet = true;
    }
}
