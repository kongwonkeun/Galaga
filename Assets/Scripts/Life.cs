using UnityEngine;
using System.Collections;

public class Life : MonoBehaviour {

    public int left = 3;
    public bool spawning = false;
    public GameObject galagaPref;
    public GameObject gameoverPref;
    bool isGameOver = false;

    IEnumerator SpawnGalaga() {
        spawning = true;
        yield return new WaitForSeconds(1f);
        GameObject newGalaga = (GameObject) Instantiate(galagaPref);
        newGalaga.transform.position = new Vector2(0, -4f);
        newGalaga.tag = "Galaga";
        spawning = false;
        isGameOver = false;
    }

    void Update() {
        GameObject galaga = GameObject.FindGameObjectWithTag("Galaga");
        if (!galaga && !spawning && left > 0) {
            left -= 1;
            StartCoroutine(SpawnGalaga());
        }

        TextMesh mesh = GetComponent<TextMesh>();
        mesh.text = "" + left;

        if (left == 0 && !galaga && !spawning && !isGameOver) {
            isGameOver = true;
            Instantiate(gameoverPref);
        }
    }

}
