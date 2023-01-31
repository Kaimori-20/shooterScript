using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletPos : MonoBehaviour
{
    // �G�̖{�̂̃I�u�W�F�N�g
    [SerializeField] private GameObject _masterObject;

    // �G�̃Q�[���I�u�W�F�N�g
    [SerializeField] private EnemyCommonController _enemy;

    // EnemyBulletController�̃v���n�u�̎擾
    [SerializeField] private EnemyBulletController _bullet;

    // �G�̒e�̃I�u�W�F�N�g�v�[��
    [SerializeField] private EnemyBulletPool _bulletPool;

    // �G�̒e�̔��˒n�_
    [SerializeField] private EnemyBulletPos _bulletPos;

    // �^�[�Q�b�g�̃X�N���v�g
    [SerializeField] private TergetController _terget;

    // �v���C���[�̃I�u�W�F�N�g
    [SerializeField] private GameObject _player;

    // �e�̔��ː�
    [SerializeField] private GameObject _tergetObj;


    [Header("�e�̑��x")]
    [SerializeField] private float _bulletSpeed;

    [Header("�e�̔��ˊ��o")]
    [SerializeField] private float _bulletTime;

    // �e�̔��˕����̐��䂷�邽�߂̍���
    [Header("�e�̔��˕����Ɋւ��鍀��")]
    [Header("�v���C���[��ǔ����邩�ǂ���")][SerializeField] private bool _isTracking;
    [Header("��]����e���ǂ���")]
    [SerializeField] private bool _isRoatBullet;
    [Header("�E��肩�ǂ���")]
    [SerializeField] private bool _isRightRoat;

    [Header("�e���U��q�̂悤�ɐU��鋓��")]
    [Header("�U��邩�ǂ���")]
    [SerializeField] private bool _isShakeBullet;

    [Header("��]���鑬����J�X���鎞�ԂȂǂ̐��䍀��")]
    [Header("��]����X�s�[�h")] [SerializeField] private float _roatSpeed;
    [Header("��]���鎞��")] [SerializeField] private float _roatTime;

    
    // ���˂���ڕW�̈ʒu���m�F
    private Vector3 _toDirection;

    // �e�̔��ˊ��o�̂��߂̌v������
    private float _bulletNowTime;

    // ��]���Ă��鎞�̌o�ߎ���
    private float _roatTimer;

    // �v�����Ԃ̃��Z�b�g����
    private float _timerReset = 0;

    // ���ꂽ���ǂ���
    public bool _isDead;

    private void Awake() {

        _isDead = false;

        if (_isRoatBullet) {

            _isShakeBullet = false;
        }

        _toDirection = _tergetObj.transform.position - transform.position;

        //transform.rotation = Quaternion.FromToRotation(Vector3.up, _toDirection);

    }

    private void Update() {

        if (!_isDead) {

            #region // ��]������ꍇ�̃X�N���v�g
            if (_isRoatBullet || _isShakeBullet) {

                // �E���̏ꍇ
                if (_isRightRoat) {

                    gameObject.transform.Rotate(_roatSpeed * Time.deltaTime * new Vector3(0, 0, -10));
                }
                // �����̏ꍇ
                else if (!_isRightRoat) {

                    gameObject.transform.Rotate(_roatSpeed * Time.deltaTime * new Vector3(0, 0, 10));
                }
            }
            #endregion

            #region // �e��U�ꂳ����ꍇ
            if (_isShakeBullet) {

                // ��]���Ԃ̌v��
                _roatTimer += Time.deltaTime;

                // ��莞�Ԉȏソ������
                if (_roatTimer >= _roatTime) {

                    // �E���̏ꍇ
                    if (_isRightRoat) {

                        // �E�ɉ��t���O���I�t�ɂ���
                        _isRightRoat = false;
                        
                        // �v�����Ԃ̃��Z�b�g
                        _roatTimer = _timerReset;
                    }
                    // �����̏ꍇ
                    else if (!_isRightRoat) {

                        // �E�ɉ��t���O���I�t�ɂ���
                        _isRightRoat = true;

                        // �v�����Ԃ̃��Z�b�g
                        _roatTimer = _timerReset;
                    }
                }
            
                
            }
            #endregion

            // �^�[�Q�b�g�̈ʒu���m�F����
            _toDirection = _tergetObj.transform.position - transform.position;

            // ���ˊ��o�̌v��
            _bulletNowTime += Time.deltaTime;

            // ��莞�Ԃ�������e���v���C���[�Ɍ����Ĕ��˂����
            if (_bulletNowTime >= _bulletTime && !_isDead) {

                // �I�u�W�F�N�g�v�[����Launch�֐��Ăяo��
                _bulletPool.Launch(transform.position);

                // �C���^�[�o���^�C���̃��Z�b�g
                _bulletNowTime = 0;
            }

            // ���ꂽ���ǂ������m�F����
            _isDead = _enemy.IsDead();
        }

        // ���ꂽ������Ƃ�����
        if (_isDead) {

            // ���˒n�_���\���ɂ���
            gameObject.SetActive(false);
        }
    }

    // �e�̑��x��BulletController��bulletSpeed�Ɋi�[
    public float BulletSpeed() {

        return _bulletSpeed;
    }

    public Vector3 Direction() {

        return _toDirection;
    }
    public bool IsTracking() {

        return _isTracking;
    }
    public bool IsShake() {

        return _isShakeBullet;
    }
    public float ShakeTime() {

        return _roatTime;
    }

    public void IsDeadFalse() {

        _isDead = false;
    }
}
