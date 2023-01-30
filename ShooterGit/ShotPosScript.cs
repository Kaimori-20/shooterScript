using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotPosScript : MonoBehaviour
{
    // �I�u�W�F�N�g�v�[���X�N���v�g���擾
    [SerializeField] private ObjectPool _objectPool;

    // �����o
    [SerializeField] private float _bulletInterval;

    // Z�L�[���������Ƃ��ɏo���e�̐�
    [SerializeField] private int _bulletCount;

    // ���E����o���e���ǂ���
    [SerializeField] private bool _isPosSide;

    

    // �e���o���Ă��邩�ǂ���
    private bool _isShot;

    private void Start() {

        // �܂��e���o���Ă��Ȃ�����false
        _isShot = false;
    }

    private void Update() {

        // Z�L�[�������ꂽ��e�����˂���A�b������e���~�܂鏈��
        // Z�L�[����������e�����˂����
        if (Input.GetKeyDown(KeyCode.Z) && !_isShot) {

            // �e���o���R���[�`���̊J�n
            StartCoroutine(Shot());
        }
    }

    // ���˂̃R���[�`��
    IEnumerator Shot() {

        // �e�����Ԋu�ň��ʏo��
        for (int i = 1; i <= _bulletCount; i++) {

            // �e���o���Ă��邽��true�ɂ��A���͂�f��A�e���d�Ȃ�Ȃ��悤�ɂ���
            _isShot = true;

            //// BulletController��T���o��
            //_bullet = this.gameObject.GetComponent<BulletController>();

            // �I�u�W�F�N�g�v�[����Launch�֐��Ăяo��
            _objectPool.Launch(transform.position);

            // �C���^�[�o����݂���
            yield return new WaitForSeconds(_bulletInterval);
        }

        // �e���o���؂������߂܂����͂��󂯕t����
        _isShot = false;

        // �R���[�`���̏I��
        yield break;

    }
}
