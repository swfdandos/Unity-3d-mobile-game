using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class atesetmesistemi : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;
    public float impactForce = 30f;
    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;

    private float nextTimeToFire = 0f;

    public float detectionRadius = 10f; // algýlama yarýçapý
    public LayerMask enemyLayer; // düþmanlarýn bulunacaðý katman
    private Collider[] colliders; // çember içindeki colliderlarý depolamak için bir dizi

    private void Update()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius, enemyLayer);

        int i = 0;
        while (i < hitColliders.Length)
        {
            Debug.Log("Düþman bulundu: " + hitColliders[i].gameObject.name);
            i++;
        }

        if (hitColliders.Length > 0) // Algýlanan düþman varsa
        {
            if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1f / fireRate;
                Shoot(hitColliders);
            }
        }
    }

    void Shoot(Collider[] hitColliders)
    {
        muzzleFlash.Play();

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            if (!hitColliders.Contains(hit.collider))
            {
                // Düþman algýlama yarýçapý içinde deðil, ateþ etmeyi tamamlamadan çýk
                return;
            }

            Debug.Log(hit.transform.name);
            ZombiSaldýrý enemy = hit.transform.GetComponent<ZombiSaldýrý>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }
            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 2f);
        }
    }

    /*  // --- Audio ---
    public AudioClip GunShotClip;
    public AudioSource source;
    public Vector2 audioPitch = new Vector2(.9f, 1.1f);

    // --- Muzzle ---
    public GameObject muzzlePrefab;
    public GameObject muzzlePosition;

    // --- Config ---
    public bool autoFire;
    public float shotDelay = .5f;
    public bool rotate = true;
    public float rotationSpeed = .25f;

    // --- Options ---
    public GameObject scope;
    public bool scopeActive = true;
    private bool lastScopeState;

    // --- Projectile ---
    [Tooltip("The projectile gameobject to instantiate each time the weapon is fired.")]
    public GameObject projectilePrefab;
    [Tooltip("Sometimes a mesh will want to be disabled on fire. For example: when a rocket is fired, we instantiate a new rocket, and disable" +
        " the visible rocket attached to the rocket launcher")]
    public GameObject projectileToDisableOnFire;

    // --- Timing ---
    [SerializeField] private float timeLastFired;

    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;
    public float impactForce = 30f;
    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;

    private float nextTimeToFire = 0f;

    public float detectionRadius = 10f; // algýlama yarýçapý
    public LayerMask enemyLayer; // düþmanlarýn bulunacaðý katman
    private Collider[] colliders; // çember içindeki colliderlarý depolamak için bir dizi

    private void Start()
    {
        if (source != null) source.clip = GunShotClip;
        timeLastFired = 0;
        lastScopeState = scopeActive;
    }

    private void Update()
    {
        // --- If rotate is set to true, rotate the weapon in scene ---
        if (rotate)
        {
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y
                                                                    + rotationSpeed, transform.localEulerAngles.z);
        }

        // --- Fires the weapon if the delay time period has passed since the last shot ---
        if (autoFire && ((timeLastFired + shotDelay) <= Time.time))
        {
            FireWeapon();
        }

        // --- Toggle scope based on public variable value ---
        if (scope && lastScopeState != scopeActive)
        {
            lastScopeState = scopeActive;
            scope.SetActive(scopeActive);
        }

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius, enemyLayer);

        int i = 0;
        while (i < hitColliders.Length)
        {
            Debug.Log("Düþman bulundu: " + hitColliders[i].gameObject.name);
            i++;
        }

        if (hitColliders.Length > 0) // Algýlanan düþman varsa
        {
            if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1f / fireRate;
                Shoot(hitColliders);
            }
        }
    }

    /// <summary>
    /// Creates an instance of the muzzle flash.
    /// Also creates an instance of the audioSource so that multiple shots are not overlapped on the same audio source.
    /// Insert projectile code in this function.
    /// </summary>
    public void FireWeapon()
    {
        // --- Keep track of when the weapon is being fired ---
        timeLastFired = Time.time;

        // --- Spawn muzzle flash ---
        var flash = Instantiate(muzzlePrefab, muzzlePosition.transform);

        // --- Shoot Projectile Object ---
        if (projectilePrefab != null)
        {
            GameObject newProjectile = Instantiate(projectilePrefab, muzzlePosition.transform.position, muzzlePosition.transform.rotation, transform);
        }

        // --- Disable any gameobjects, if needed ---
        if (projectileToDisableOnFire != null)
        {
            projectileToDisableOnFire.SetActive(false);
            Invoke("ReEnableDisabledProjectile", 3);
        }

        // --- Handle Audio ---
        if (source != null)
        {
            // --- Sometimes the source is not attached to the weapon for easy instantiation on quick firing weapons like machineguns, 
            // so that each shot gets its own audio source, but sometimes it's fine to use just 1 source. We don't want to instantiate 
            // the parent gameobject or the program will get stuck in a loop, so we check to see if the source is a child object ---
            if (source.transform.IsChildOf(transform))
            {
                source.Play();
            }
            else
            {
                // --- Instantiate prefab for audio, delete after a few seconds ---
                AudioSource newAS = Instantiate(source);
                if ((newAS = Instantiate(source)) != null && newAS.outputAudioMixerGroup != null && newAS.outputAudioMixerGroup.audioMixer != null)
                {
                    // --- Change pitch to give variation to repeated shots ---
                    newAS.outputAudioMixerGroup.audioMixer.SetFloat("Pitch", Random.Range(audioPitch.x, audioPitch.y));
                    newAS.pitch = Random.Range(audioPitch.x, audioPitch.y);

                    // --- Play the gunshot sound ---
                    newAS.PlayOneShot(GunShotClip);

                    // --- Remove after a few seconds. Test script only. When using in project I recommend using an object pool ---
                    Destroy(newAS.gameObject, 4);
                }
            }
        }

        // --- Insert custom code here to shoot projectile or hitscan from weapon ---
    }

    private void ReEnableDisabledProjectile()
    {
        projectileToDisableOnFire.SetActive(true);
    }

    void Shoot(Collider[] hitColliders)
    {
        muzzleFlash.Play();

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            if (!hitColliders.Contains(hit.collider))
            {
                // Düþman algýlama yarýçapý içinde deðil, ateþ etmeyi tamamlamadan çýk
                return;
            }

            Debug.Log(hit.transform.name);
            ZombiSaldýrý enemy = hit.transform.GetComponent<ZombiSaldýrý>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }
            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 2f);
        }
    }
} */

}
