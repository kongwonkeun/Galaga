using UnityEngine;
using System.Collections;

public class BulletEmitter : MonoBehaviour {

    public GameObject bulletEmitter;
    public GameObject bulletPref;
    public bool cooling = false;
    public float bulletSpeedScale;
    public Vector2 bulletDirection;
    public GameObject loadedBullet;
    public float coolingSeconds;
    public string bulletTag;

    public IEnumerator Fire() {
        cooling = true;
        Rigidbody2D rigidbody = loadedBullet.GetComponent<Rigidbody2D>();
        rigidbody.AddForce(bulletDirection * bulletSpeedScale);
        loadedBullet.tag = bulletTag;
        transform.GetComponent<AudioSource>().Play();
        Destroy(loadedBullet, 10.0f);

        loadedBullet.transform.parent = null;
        loadedBullet = null;
        yield return new WaitForSeconds(coolingSeconds);
        cooling = false;
    }

    public bool NeedReload() {
        return !cooling && !loadedBullet;
    }

    public void Reload() {
        loadedBullet = (GameObject) Instantiate(bulletPref, bulletEmitter.transform.position, Quaternion.identity);
        loadedBullet.transform.parent = gameObject.transform;
    }

    public bool CanFire() {
        return !cooling;
    }

}
