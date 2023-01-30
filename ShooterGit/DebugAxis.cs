using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugAxis : MonoBehaviour
{
    [SerializeField] private GameObject _debugObj;

    [SerializeField] private bool _isRoat;

    [SerializeField] private bool _isShake;

    [SerializeField] private bool _isRight;

    [SerializeField] private float _roatSpeed;

    [SerializeField] private float _roatTime;

    private float _roatTimer;

    private void Start() {

        if (_isRoat) {

            _isShake = false;
        }
    }

    private void Update() {

        if(_isRoat){

            if (_isRight) {

                gameObject.transform.Rotate(new Vector3(0, 0, -10) * Time.deltaTime * _roatSpeed);
            }
            if (!_isRight) {

                gameObject.transform.Rotate(new Vector3(0, 0, 10) * Time.deltaTime * _roatSpeed);
            }
        }
        

        if (_isShake) {

            if (_isRight) {

                _roatTimer += Time.deltaTime;

                gameObject.transform.Rotate(new Vector3(0, 0, -10) * Time.deltaTime * _roatSpeed);

                if (_roatTimer >= _roatTime) {

                    _isRight = false;
                    _roatTimer = 0;
                }
            }
            if (!_isRight) {

                _roatTimer += Time.deltaTime;

                gameObject.transform.Rotate(new Vector3(0, 0, 10) * Time.deltaTime * _roatSpeed);

                if (_roatTimer >= _roatTime) {

                    _isRight = true;
                    _roatTimer = 0;
                }
            }
        }
    }
}
