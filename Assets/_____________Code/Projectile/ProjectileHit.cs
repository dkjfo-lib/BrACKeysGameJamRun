using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHit : MonoBehaviour, ICanHit
{
    public Pipe_SoundsPlay pipe_Sounds;
    [Space]
    public ClipsCollection ShootSound;
    public ClipsCollection HitSound;
    public ParticleSystem Addon_hitParticles;
    TrailRenderer Addon_trailRenderer;
    [Space]
    public Faction hitFaction = Faction.AlwaysHit;
    public float damage = 1;
    [Space]
    public float lifetime = 5;
    public float spreadAngle = 120;
    public int spawnProjectiles = 2;
    public int projectilesDownFall = 1;
    public ProjectileHit ChildProjectileType;

    public Object Me => gameObject;
    public bool IsSelfDamageOn => false;
    public bool IsFriendlyDamageOn => false;

    private float minDeltaAngle = 10;
    private float projectileSize = 1f;

    public bool IsEnemy(Faction faction)
    {
        return hitFaction == faction;
    }

    void Start()
    {
        Addon_trailRenderer = GetComponentInChildren<TrailRenderer>();
        pipe_Sounds.AddClip(new PlayClipData(ShootSound, transform.position));
        StartCoroutine(DeathTimer());
    }

    IEnumerator DeathTimer()
    {
        yield return new WaitForSeconds(lifetime);

        float movementAngle = Vector2.SignedAngle(Vector2.right, transform.right);
        DestroySelf(movementAngle);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        OnHit(collision);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        OnHit(collision);
    }

    void OnHit(Collider2D collision)
    {
        var hittable = collision.transform.GetComponent<IHittable>();
        if (this.ShouldHit(hittable))
        {
            pipe_Sounds.AddClip(new PlayClipData(HitSound, transform.position));

            hittable.GetHit(new Hit(damage));

            float normalAngle = GetHitNormal();
            DestroySelf(normalAngle);
        }
    }

    float GetHitNormal()
    {
        var hit = Physics2D.Raycast(transform.position - transform.right, transform.right, projectileSize * 5, Layers.Hittable);
        float normalAngle = Vector2.SignedAngle(Vector2.right, hit.normal);
        return normalAngle;
    }

    void DestroySelf(float normalAngle)
    {
        float movementAngle = Vector2.SignedAngle(Vector2.right, -transform.right);

        float fallAngle = movementAngle - normalAngle;
        float descendAngle = normalAngle - fallAngle;

        if (spawnProjectiles > 0)
        {
            // Spawn Projectiles like a mirror
            //float delta = Mathf.Max(fallAngle / spawnProjectiles, minDeltaAngle);
            //int prS = (int)Mathf.Ceil(-spawnProjectiles / 2f);
            //int prE = (int)Mathf.Floor(spawnProjectiles / 2f);
            //for (int i = prS; i < prE; i++)
            //{
            //    float descendInstanceAngle = descendAngle + delta * i;
            //    var newP = Instantiate(Projectile, transform.position - transform.right * projectileSize, Quaternion.Euler(0, 0, descendInstanceAngle));
            //    newP.spawnProjectiles -= projectilesDownFall;
            //}

            // Spawn Projectiles in a set sector
            float delta = Mathf.Max(spreadAngle / spawnProjectiles, minDeltaAngle);
            int prS = (int)Mathf.Ceil(-spawnProjectiles / 2f);
            int prE = (int)Mathf.Floor(spawnProjectiles / 2f);
            for (int i = prS; i <= prE; i++)
            {
                float descendInstanceAngle = normalAngle + delta * i;
                var newP = Instantiate(ChildProjectileType, transform.position - transform.right * projectileSize, Quaternion.Euler(0, 0, descendInstanceAngle));
                newP.spawnProjectiles -= projectilesDownFall;
            }
        }
        Addon_trailRenderer.transform.parent = transform.parent;
        Destroy(Addon_trailRenderer.gameObject, Addon_trailRenderer.time);
        Destroy(gameObject);
    }
}
