using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem shootingSystem;
    [SerializeField]
    private Transform bulletSpawnPoint;
    [SerializeField]
    private ParticleSystem impactParticleSystem;
    [SerializeField]
    private TrailRenderer bulletTrail;
    [SerializeField] 
    private float shootDelay = 0.1f;
    [SerializeField]
    private float speed = 100;
    [SerializeField]
    private LayerMask Mask;
    [SerializeField]
    private bool bouncingBullets;
    [SerializeField]
    private float bounceDistance = 10f;

    private float lastShootTime;

    public void shoot()
    {
        if(lastShootTime + shootDelay < Time.time)
        {
            shootingSystem.Play();
            Vector3 direction = transform.forward;
            TrailRenderer trail = Instantiate(bulletTrail, bulletSpawnPoint.position, Quaternion.identity);

            if (Physics.Raycast(bulletSpawnPoint.position, direction, out RaycastHit hit, float.MaxValue, Mask))
            {
                StartCoroutine(SpawnTrail(trail, hit.point,hit.normal, bounceDistance, true));
            }
            else
            {
                //remember to change vector2 if bug
                StartCoroutine(SpawnTrail(trail, direction * 100, Vector3.zero, bounceDistance, false));
            }

            lastShootTime = Time.time;
        }
    }

    private IEnumerator SpawnTrail(TrailRenderer trail, Vector3 HitPoint, Vector3 HitNormal, float bounceDistance, bool MadeImpact)
    {
        Vector3 startPosition = trail.transform.position;
        Vector3 direction = (HitPoint - trail.transform.position).normalized;

        float distance = Vector3.Distance(trail.transform.position, HitPoint);
        float startingDistance = distance;

        while(distance > 0)
        {
            trail.transform.position = Vector3.Lerp(startPosition, HitPoint, 1 - (distance / startingDistance));
            distance -= Time.deltaTime * speed;

            yield return null;
        }

        trail.transform.position = HitPoint;

        if (MadeImpact)
        {
            Instantiate(impactParticleSystem, HitPoint, Quaternion.LookRotation(HitNormal));

            if(bouncingBullets && bounceDistance > 0)
            {
                Vector3 bounceDirection = Vector3.Reflect(direction, HitNormal);
                
                if(Physics.Raycast(HitPoint, bounceDirection, out RaycastHit hit, bounceDistance, Mask))
                {
                    yield return StartCoroutine(SpawnTrail(trail,
                        hit.point,
                        hit.normal,
                        bounceDistance - Vector3.Distance(hit.point,HitPoint),
                        true
                        ));
                }
                else
                {
                    yield return StartCoroutine(SpawnTrail(
                        trail,
                        bounceDirection * bounceDistance,
                        Vector3.zero,
                        0,
                        false
                        ));
                }
            }
        }
        Destroy(trail.gameObject, trail.time);
    }

}
