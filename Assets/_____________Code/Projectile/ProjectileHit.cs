using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHit : MonoBehaviour, ICanHit
{
    public Pipe_CamShakes pipe_CamShakes;
    public Pipe_SoundsPlay pipe_Sounds;
    [Space]
    public ClipsCollection ShootSound;
    public ClipsCollection HitSound;
    //public ParticleSystem hitParticles;
    [Space]
    public float damage = 1;
    public int spawnProjectiles = 2;
    public ProjectileHit Projectile;
    [Space]
    public ShakeAtributes onHitShake = new ShakeAtributes(1, .5f, .125f);

    public Object Me => gameObject;
    public bool IsSelfDamageOn => false;
    public bool IsFriendlyDamageOn => true;

    private float projectileSize = .3f;

    public bool IsEnemy(Faction faction)
    {
        return true;
    }

    void Start()
    {
        pipe_CamShakes.AddCamShake(onHitShake);
        pipe_Sounds.AddClip(new PlayClipData(ShootSound, transform.position));
        StartCoroutine(Init());
    }

    IEnumerator Init()
    {
        yield return new WaitForSeconds(.1f);
        GetComponent<Collider2D>().enabled = true;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        var hittable = collision.transform.GetComponent<IHittable>();
        if (this.ShouldHit(hittable))
        {
            pipe_CamShakes.AddCamShake(onHitShake);
            pipe_Sounds.AddClip(new PlayClipData(HitSound, transform.position));

            hittable.GetHit(new Hit(damage));
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

        Debug.Log("F " + fallAngle);
        Debug.Log("N " + normalAngle);
        Debug.Log("D " + descendAngle);

        float delta = fallAngle / spawnProjectiles;
        int prS = (int)Mathf.Ceil(-spawnProjectiles / 2f);
        int prE = (int)Mathf.Floor(spawnProjectiles / 2f);
        for (int i = prS; i < prE; i++)
        {
            float descendInstanceAngle = descendAngle + delta * i;
            var newP = Instantiate(Projectile, transform.position - transform.right * projectileSize, Quaternion.Euler(0, 0, descendInstanceAngle));
            newP.spawnProjectiles -= 1;
            Debug.Log("D" + i + " " + descendInstanceAngle);
        }
        Destroy(gameObject);
    }
}
