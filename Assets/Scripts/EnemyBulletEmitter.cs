using UnityEngine;
using System.Collections;

public class EnemyBulletEmitter : BulletEmitter {

    bool IsBelongsToArmy() {
        return gameObject.transform.parent != null;
    }

    void Update () {
        if (IsBelongsToArmy()) {
            return;
        }

        if (NeedReload()) {
            Reload();
        }

        if (CanFire()) {
            StartCoroutine (Fire ());
        }
    }

}
