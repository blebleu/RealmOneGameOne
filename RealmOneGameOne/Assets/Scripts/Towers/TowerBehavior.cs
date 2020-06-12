using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBehavior : MonoBehaviour
{
    public float rateOfFire = 60;
    // range is in units of game tiles, will extend range from tower to outer edge of nth tile, where n = range.
    public int range = 1;
    public GameObject model;
    public GameObject detectionRadius;
    public GameObject projectile;

    float radius;

    /*
     * timer that controls time between shots. Rate of fire expressed in shots / min, so divide 60 by rate of fire to get seconds
     * between shots.
     */

    public GameObject currentTarget = null;

    private float shootTimer;

    void Awake() {
        shootTimer = 60f / rateOfFire;
        radius = (range + 0.5f) * 10;
        detectionRadius.transform.localScale = new Vector3(2*radius, 0.5f, 2*radius);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckTarget();
        // if shoot timer is not yet at zero (not ready to fire)
        if (shootTimer > 0.0)
        {
            shootTimer -= Time.deltaTime;
        }
        // else shoot timer is ready to fire (timer <= 0.0)
        else if (currentTarget != null)
        {
            // if there are enemies in range
            // shoot at enemy
            FireAtEnemy();

            // reset timer
            shootTimer = shootTimer = 60f / rateOfFire;
        }

    }

    void CheckTarget() {
        if (currentTarget == null)
        {
            foreach (GameObject enemy in GameController.enemies)
            {
                if (enemy != null)
                {
                    // get enemy position and tower position, ignoring y values
                    Vector2 enemyPos2D = new Vector2(enemy.transform.position.x, enemy.transform.position.z);
                    Vector2 towerPos2D = new Vector2(transform.position.x, transform.position.z);
                    // enemy is in range if distance between enemy and tower is less than its radius.
                    bool enemyIsInRange = Vector2.Distance(enemyPos2D, towerPos2D) <= radius;
                    if (enemyIsInRange)
                    {
                        // if enemy in range, that enemy is the current target. Preference is on oldest enemy.
                        currentTarget = enemy;
                        return;
                    }
                }
            }
        }
        else {
            Vector2 enemyPos2D = new Vector2(currentTarget.transform.position.x, currentTarget.transform.position.z);
            Vector2 towerPos2D = new Vector2(transform.position.x, transform.position.z);
            bool enemyIsInRange = Vector2.Distance(enemyPos2D, towerPos2D) <= radius;

            // if current target exists, but goes out of range, null the current target.
            if (!enemyIsInRange) {
                currentTarget = null;
            }
        }
    }

    void FireAtEnemy() {
        Transform positionToShoot = currentTarget.transform;

        GameObject projectileInstance = Instantiate(projectile,gameObject.transform);

        if (projectileInstance.TryGetComponent<ProjectileBehavior>(out ProjectileBehavior projectileBehavior)) {
            projectileBehavior.SetTarget(positionToShoot);
        }
    }
}
