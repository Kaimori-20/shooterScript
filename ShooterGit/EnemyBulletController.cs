using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletController : MonoBehaviour
{
    // �I�u�W�F�N�g�v�[���̎擾
    [SerializeField] private EnemyBulletPool _bulletPool;

    // �e�̔��˒n�_
    [SerializeField] private EnemyBulletPos _bulletPos;

    // �e�̃Q�[���I�u�W�F�N�g
    [SerializeField] private GameObject _bulletObj;

    // �v���C���[�̃Q�[���I�u�W�F�N�g
    [SerializeField] private GameObject _playerObj;

    // �e�̈ړ����x
    [SerializeField] private float _bulletSpeed;

    // ��ʊO�̔��莞�A�o�ĂȂ��̂ɏ�����̂�h��
    [SerializeField] private float _screenOver;
    
    // ���Ԋu�Œe�𔭎˂���ۂɎg�p
    [SerializeField] private float _bulletInterval;

    // �e�̈ړ�����R���g���[�����邽�߂̕ϐ�
    [SerializeField] private Vector3 _bulletPosController;

    // �e�̌��ݒn
    private Vector3 _bulletNowPos;

    // �e�����˂���Ă��炩����������
    private float _bulletTime;

    // �e�����˂��ꂽ���ǂ���
    private bool _isShot;

    // �ǔ�����e���ǂ���
    private bool _isTracking;

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

        // �v���C���[�̃Q�[���I�u�W�F�N�g���擾
        _playerObj = GameObject.FindGameObjectWithTag("Player");

        // �I�u�W�F�N�g�v�[�����擾
        _bulletPool = transform.parent.GetComponent<EnemyBulletPool>();

        // �e�̔��˒n�_���擾
        _bulletPos = _bulletPool.BulletPos();

        // �����������͔�A�N�e�B�u�ɂ��Ă���
        gameObject.SetActive(false);

        // �e�̃X�s�[�h���i�[
        _bulletSpeed = _bulletPos.BulletSpeed();

    }
    /// <summary>
    /// �e�̋����̐���
    /// </summary>
    private void Update() {

        if (!_isShot) {

            // ���˂���p�x���m�F
            CurrentPlayer();

            // ���˂��ꂽ�t���O���I���ɂ���
            _isShot = true;
        }

        if (_isShot) {

            // �e�̔��ˎ��Ԃ̌v��
            _bulletTime += Time.deltaTime;

            // �e�̔���
            transform.position += transform.up * Time.deltaTime * _bulletSpeed;            
        }

        // �e�̉���B��ʊO�ɏo����������
        if (transform.position.y > _screenTop || transform.position.y < _screenUnder ||
            transform.position.x > _screenRight || transform.position.x < _screenLeft) {

            // ���˂��ꂽ�t���O���I�t�ɂ���
            _isShot = false;

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

        // // �I�u�W�F�N�g�v�[����Collect�֐����Ăяo�����M�����
        _bulletPool.Collect(this);
    }

    // ���˒n�_�̃X�N���v�g�����΂������擾
    public void CurrentPlayer() {

        // �^�[�Q�b�g�̈ʒu���m�F����
        Vector3 toDirection = _bulletPos.Direction();

        // �I�u�W�F�N�g��Y���W���^�[�Q�b�g�̂�������Ɍ�����
        transform.rotation = Quaternion.FromToRotation(Vector3.up, toDirection);
    }


}
