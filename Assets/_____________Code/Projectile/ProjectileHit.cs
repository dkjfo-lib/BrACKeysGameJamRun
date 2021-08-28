using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHit : MonoBehaviour, ICanHit
{
    public Pipe_SoundsPlay pipe_Sounds;
    [Space]
    public ClipsCollection ShootSound;
    public ClipsCollection HitSound;
    public ParticleSystem hitParticles;
    public TrailRenderer trailRenderer;
    [Space]
    public Faction hitFaction = Faction.AlwaysHit;
    public float damage = 1;
    [Space]
    public int spawnProjectiles = 2;
    public int projectilesDownFall = 1;
    public ProjectileHit Projectile;

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
        pipe_Sounds.AddClip(new PlayClipData(ShootSound, transform.position));
        //GetComponent<Collider2D>().enabled = true;
        //StartCoroutine(Init());
    }

    //IEnumerator Init()
    //{
    //    yield return new WaitForSeconds(.1f);
    //    GetComponent<Collider2D>().enabled = true;
    //}

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
            //if (hittable is GroundHittable) { } else
            //{
            //    spawnProjectiles = 0;
            //}
            DestroySelf();
        }
    }

    void DestroySelf()
    {
        var hit = Physics2D.Raycast(transform.position - transform.right, transform.right, projectileSize * 5, Layers.Hittable);
        float movementAngle = Vector2.SignedAngle(Vector2.right, -transform.right);
        float normalAngle = Vector2.SignedAngle(Vector2.right, hit.normal);

        float fallAngle = movementAngle - normalAngle;
        float descendAngle = normalAngle - fallAngle;

        if (spawnProjectiles > 0)
        {
            //float delta = Mathf.Max(fallAngle / spawnProjectiles, minDeltaAngle);
            //int prS = (int)Mathf.Ceil(-spawnProjectiles / 2f);
            //int prE = (int)Mathf.Floor(spawnProjectiles / 2f);
            //for (int i = prS; i < prE; i++)
            //{
            //    float descendInstanceAngle = descendAngle + delta * i;
            //    var newP = Instantiate(Projectile, transform.position - transform.right * projectileSize, Quaternion.Euler(0, 0, descendInstanceAngle));
            //    newP.spawnProjectiles -= projectilesDownFall;
            //}
            float delta = Mathf.Max(120 / spawnProjectiles, minDeltaAngle);
            int prS = (int)Mathf.Ceil(-spawnProjectiles / 2f);
            int prE = (int)Mathf.Floor(spawnProjectiles / 2f);
            for (int i = prS; i <= prE; i++)
            {
                float descendInstanceAngle = normalAngle + delta * i;
                var newP = Instantiate(Projectile, transform.position - transform.right * projectileSize, Quaternion.Euler(0, 0, descendInstanceAngle));
                newP.spawnProjectiles -= projectilesDownFall;
            }
        }
        trailRenderer.transform.parent = transform.parent;
        Destroy(trailRenderer.gameObject, trailRenderer.time);
        Destroy(gameObject);
    }
}
