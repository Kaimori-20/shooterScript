using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TergetController : MonoBehaviour
{
    [SerializeField] private GameObject _playerObj;

    [SerializeField] private EnemyBulletPos _bulletPos;

    private bool _isTracking;

    private bool _isShake;

    private float _shakeTime;

    private float _shakeTimer;


    private void Start() {

        _isTracking = _bulletPos.IsTracking();

        _isShake = _bulletPos.IsShake();

        _shakeTime = _bulletPos.ShakeTime();
    }

    private void Update() {

        if (_isTracking) {

            if (_isShake) {

                _shakeTimer += Time.deltaTime;

                if (_shakeTime <= _shakeTimer) {

                    transform.position = _playerObj.transform.position;

                    _shakeTimer = 0;
                }
            }
            else if (!_isShake) {

                transform.position = _playerObj.transform.position;
            }
            
        }
    }


}
