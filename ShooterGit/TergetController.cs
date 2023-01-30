using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TergetController : MonoBehaviour
{
    [SerializeField] private GameObject _playerObj;

    [SerializeField] private EnemyBulletPos _bulletPos;

    


    private void Start() {

        IsPlayerTracking();
    }

    public void IsPlayerTracking() {

        transform.position = _playerObj.transform.position;
    }
}
