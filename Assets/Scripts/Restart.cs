using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Restart : MonoBehaviour {

    // Use this for initialization
    void Start () {
        transform.GetComponent<AudioSource>().Play();
    }
    
    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown ("space")) {
            SceneManager.LoadScene (0);
        }
    }
}
