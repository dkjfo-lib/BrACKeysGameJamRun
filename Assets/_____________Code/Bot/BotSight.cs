using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotSight : MonoBehaviour
{
    public float sightRadius = 10;
    public Faction FactionToHunt = Faction.AlwaysHit;
    [Space]
    List<Transform> enemies = new List<Transform>();
    List<Transform> enemiesCanSee = new List<Transform>();

    Vector3 GetDirection(Transform target) => (target.position - transform.position).normalized;

    Transform currentTarget;
    public bool canSeeTarget => currentTarget != null;
    public Vector3 targetVector => currentTarget.position - transform.position;
    public Vector3 targetDirection => (currentTarget.position - transform.position).normalized;

    void Start()
    {
        GetComponent<CircleCollider2D>().radius = sightRadius;
        StartCoroutine(UpdateSight());
    }

    IEnumerator UpdateSight()
    {
        while (true)
        {
            enemiesCanSee.Clear();
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i] == null)
                {
                    enemies.Remove(enemies[i]);
                    i--;
                }
                else
                {
                    var _targetDirection = GetDirection(enemies[i]);
                    var hit = Physics2D.Raycast(transform.position + _targetDirection * .5f, _targetDirection, sightRadius, Layers.Hittable);
                    if (hit.transform == enemies[i])
                    {
                        enemiesCanSee.Add(enemies[i]);
                    }
                    //yield return new WaitForEndOfFrame();
                }
            }
            currentTarget = transform.GetClosest(enemiesCanSee);
            yield return new WaitForSeconds(1);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, sightRadius);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var hittable = collision.GetComponent<IHittable>();
        if (hittable == null) return;
        if (hittable.Faction == FactionToHunt)
        {
            enemies.Add(collision.transform);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        enemies.Remove(collision.transform);
    }
}
