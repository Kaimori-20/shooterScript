using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    // ObjectPool���擾
    [SerializeField] private ObjectPool _objectPool;

    // �e�̃Q�[���I�u�W�F�N�g
    [SerializeField] private GameObject _bullet;

    // �e�̈ړ����x
    [SerializeField] private float _bulletSpeed;    

    // ��ʊO�̔��莞�A�o�ĂȂ��̂ɏ�����̂�h��
    [SerializeField] private float _screenOver;

    // �΂߂ɒe���o�锻�肩
    [SerializeField] private bool _isSide;

    // ���˂����ʒu�͍�����
    [SerializeField] private bool _isLeft;

    // �e�̈ړ�����R���g���[�����߂̕ϐ�
    [SerializeField] private Vector3 _bulletPosController;

    // �G(Enemy)�̃^�O���Ƃ�ϐ�
    private string _enemyTag = "Enemy";

    // �e�̌��ݒn
    private Vector3 _bulletNowPos;

    // ��ʊO�ɏo��������Ƃ�̂Ɏg�p
    private float _screenTop; // ��ʂ̏�
    private float _screenUnder; // ��ʂ̉�
    private float _screenRight; // ��ʂ̉E
    private float _screenLeft; // ��ʂ̍�


    private void Start() {

        // ��ʂ̈�ԏ��y���W���擾
        _screenTop = Camera.main.ViewportToWorldPoint(new Vector2(0, 1)).y + _screenOver;

        // ��ʂ̈�ԉ���y���W���擾
        _screenUnder = Camera.main.ViewportToWorldPoint(new Vector2(0, 0)).y - _screenOver;

        // ��ʂ̉E�����擾
        _screenRight = Camera.main.ViewportToWorldPoint(new Vector2(1, 0)).x + _screenOver;

        // ��ʂ̍������擾
        _screenLeft = Camera.main.ViewportToWorldPoint(new Vector2(0, 0)).x - _screenOver;

        // �I�u�W�F�N�g�v�[�����擾
        _objectPool = transform.parent.GetComponent<ObjectPool>();

        //_bulletNowPos = new Vector3(
        //    _bullet.transform.position.x + _bulletPosController.x,
        //    _bullet.transform.position.y + _bulletPosController.y,
        //    _bullet.transform.position.z + _bulletPosController.z);

        // �����������͔�A�N�e�B�u�ɂ��Ă���
        gameObject.SetActive(false);

        
    }

    /// <summary>
    /// �e�̋����𐧌䂷��
    /// </summary>
    private void Update() {

        _bulletNowPos = _bullet.transform.position;

        // ���ʂ���o��e�̏ꍇ
        if (!_isSide) {

            // �܂������ɒe�𔭎�
            transform.position = Vector3.MoveTowards(
                    transform.position, new Vector3(
                    _bullet.transform.position.x,
                    _bulletNowPos.y + _bulletPosController.y,
                    _bulletNowPos.z),
                    _bulletSpeed * Time.deltaTime);


        }
        // ������o��e�̏ꍇ
        else if (_isSide) {

            // �΂߂ɒe�𔭎�
            transform.position = Vector3.MoveTowards(
                transform.position,
                new Vector3(
                _bulletNowPos.x + _bulletPosController.x,
                _bulletNowPos.y + _bulletPosController.y,
                _bulletNowPos.z),
                _bulletSpeed * Time.deltaTime);
        }

        // �e�̉���B��ʊO�ɏo����������
        if (transform.position.y > _screenTop || transform.position.y < _screenUnder ||
            transform.position.x > _screenRight || transform.position.x < _screenLeft) {

            HideFromStage();
        }
    }

    // �G�ɓ���������e���������
    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.tag == _enemyTag) {

            HideFromStage();
        }
    }

    // 
    public void ShowInStage(Vector3 pos) {

        // position��n���ꂽ���W�ɐݒ�
        transform.position = pos;
    }

    //
    public void HideFromStage() {

        // �I�u�W�F�N�g�v�[����Collect�֐����Ăяo�����M�����
        _objectPool.Collect(this);
    }
}
