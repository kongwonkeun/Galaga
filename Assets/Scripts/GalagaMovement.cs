using UnityEngine;
using System.Collections;

public class GalagaMovement : MonoBehaviour {

    public float speed = 1f;
    GameObject dir;

    void Start() {
        dir = GameObject.FindGameObjectWithTag("Sensor");
    }

    void Update() {
        if (Input.GetKeyDown("escape")) {
            Application.Quit();
        }
        var position = gameObject.transform.position;
        int d = dir.GetComponent<Sensor>().m_direction;
        gameObject.transform.position = new Vector2(Mathf.Clamp(position.x + d * speed, -6f, 6f), position.y);
        // gameObject.transform.position = new Vector2(Mathf.Clamp(position.x + Input.GetAxis ("Horizontal") * speed, -6f, 6f), position.y);
    }

}
