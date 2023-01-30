using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    [SerializeField] private string _nextScene;

    private void Update() {

        if (Input.GetKey(KeyCode.Z)) {

            SceneManager.LoadScene(_nextScene);
        }
    }
}
