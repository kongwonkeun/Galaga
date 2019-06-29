using UnityEngine;
using System.Collections;

public class GalagaBulletEmitter : BulletEmitter {

    GameObject speed;

    void Start() {
        speed = GameObject.FindGameObjectWithTag("Sensor");
    }

    void Update() {
        coolingSeconds = 0.5f - (float) speed.GetComponent<Sensor>().m_speed / 1000;
        if (NeedReload()) {
            Reload();
        }
        if (CanFire()) {
            StartCoroutine(Fire());
        }
    }

    public new bool CanFire() {
        return !cooling;
    }

}
