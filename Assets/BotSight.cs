using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotSight : MonoBehaviour
{
    public float sightRadius = 10;
    [Space]
    public bool canSeeTarget;

    Transform target => PlayerSinglton.thePlayer.transform;
    public Vector3 targetVector => PlayerSinglton.thePlayer.transform.position - transform.position;
    public Vector3 targetDirection => (PlayerSinglton.thePlayer.transform.position - transform.position).normalized;

    void Start()
    {
        StartCoroutine(UpdateSight());
    }

    IEnumerator UpdateSight()
    {
        while (true)
        {
            if (target == null) yield return new WaitUntil(() => target != null);

            var _targetDirection = targetDirection;
            var hit = Physics2D.Raycast(transform.position + _targetDirection * 1.5f, _targetDirection, sightRadius, Layers.Hittable);
            canSeeTarget = hit.transform == target;
            yield return new WaitForSeconds(1);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, sightRadius);
    }
}
