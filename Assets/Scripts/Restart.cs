using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Restart : MonoBehaviour {

    GameObject sensor;

    void Start() {
        transform.GetComponent<AudioSource>().Play();
        sensor = GameObject.FindGameObjectWithTag("Sensor");
        sensor.GetComponent<Sensor>().m_run = false;
        sensor.GetComponent<Sensor>().m_stream.Close();
    }
    
    void Update() {
        if (Input.GetKeyDown("space")) {
            SceneManager.LoadScene("Welcome");
        }
        if (Input.GetKeyDown("escape")) {
            Application.Quit();
        }
    }

}
