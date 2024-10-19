using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using TMPro;
public class GunShoot : MonoBehaviour
{
    private StarterAssetsInputs _input;
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private GameObject bulletPoint;
    [SerializeField]
    private float bulletSpeed = 600f;
    [SerializeField]
    private float maxBullet = 10;
    private float currentBullet; 
    [SerializeField]
    private TextMeshProUGUI textBulletCount;

    private Animation animatorReload;
    private float reloadTime = 1f;
    // Start is called before the first frame update
    void Start()
    {
        _input = transform.root.GetComponent<StarterAssetsInputs>();
        animatorReload = this.GetComponent<Animation>();
        currentBullet = maxBullet;
    }

    // Update is called once per frame
    void Update()
    {
        if(animatorReload.isPlaying)
        {
            return ;
        }
        if(_input.shoot)
        {
            if(currentBullet <= 0)
            {
               Reload();
            }
            else
            {
                Shoot();
                _input.shoot = false;
                currentBullet--;
                ChangeDisplayBullet();
            }
        }
        if(_input.reload)
        {
            Reload();
            _input.reload = false;
        }
    }
    void Shoot()
    {
       GameObject bullet = Instantiate(bulletPrefab,bulletPoint.transform.position,transform.rotation);
       bullet.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed);
       Destroy(bullet,2);
    }
    void Reload()
    {
        // Play the reload animation if it's not already playing
        if (!animatorReload.isPlaying)
        {
            // Start coroutine to wait for the animation to complete before reloading bullets
            StartCoroutine(PlayReloadAnimation());
        }
    }
    IEnumerator PlayReloadAnimation()
    {
        animatorReload.Play();

        // Wait for the duration of the animation (or custom reload time)
        yield return new WaitForSeconds(reloadTime);
       
        currentBullet = maxBullet;
        ChangeDisplayBullet();
        _input.shoot = false;
    }
    void ChangeDisplayBullet()
    {
        textBulletCount.text = currentBullet+"/"+maxBullet;
    }
}
