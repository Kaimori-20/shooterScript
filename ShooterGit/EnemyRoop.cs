using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRoop : MonoBehaviour
{
    [SerializeField] private EnemyCommonController _enemy1;
    [SerializeField] private EnemyCommonController _enemy2;
    [SerializeField] private EnemyCommonController _enemy3;

    [SerializeField] private GameObject _enemy1Obj;
    [SerializeField] private GameObject _enemy2Obj;
    [SerializeField] private GameObject _enemy3Obj;

    [SerializeField] private float _time;

    private bool _isDeadEnemy1;
    private bool _isDeadEnemy2;
    private bool _isDeadEnemy3;

    private float _timer;

    private Animator _anim = null;

    private void Start() {
        _isDeadEnemy1 = false;
        _isDeadEnemy2 = false;
        _isDeadEnemy3 = false;


        _anim = GetComponent<Animator>();

        _anim.SetBool("isReturn", false);
    }

    private void Update() {

        _isDeadEnemy1 = _enemy1.IsDead();
        _isDeadEnemy2 = _enemy2.IsDead();
        _isDeadEnemy3 = _enemy3.IsDead();

        if (_isDeadEnemy1 && _isDeadEnemy2 && _isDeadEnemy3) {

            _timer += Time.deltaTime;

            _enemy1.Resurrection();
            _enemy2.Resurrection();
            _enemy3.Resurrection();

            _enemy1Obj.SetActive(true);
            _enemy2Obj.SetActive(true);
            _enemy3Obj.SetActive(true);

            

            if (_timer >= _time) {

                _anim.SetBool("isReturn", false);

                _timer = 0;
            }
        }
    }
}
