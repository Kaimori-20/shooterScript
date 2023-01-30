using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingObject : MonoBehaviour
{
    // ���˒n�_�̃X�N���v�g�擾
    [SerializeField] private EnemyBulletPos _bulletPos;

    // �v���C���[�̃Q�[���I�u�W�F�N�g
    [SerializeField] private GameObject _player;

    // �ǔ����邩�ǂ���
    [SerializeField] private bool _isTracking;

    // �v���C���[�𒆐S�ɐU�邽�߂�bool
    private bool _isPlayerTracking;

    // �^�[�Q�b�g�͂ǂ����𒲂ׂ�
    private Vector3 _target;

    private void Start() {

        // �v���C���[�𒆐S�ɂ��邩�����߂邽�߂�Pos�X�N���v�g����擾
        _isPlayerTracking = _bulletPos.IsPlayerShake();
    }

    private void Update() {

        if (_isTracking) {

            // �^�[�Q�b�g�̈ʒu���m�F����
            _target = _player.transform.position - transform.position;

            if (!_isPlayerTracking) {

                // �I�u�W�F�N�g��Y���W���^�[�Q�b�g�̕����Ɍ�����
                transform.rotation = Quaternion.FromToRotation(Vector3.up, _target);
            } 
            else if (_isPlayerTracking) {

                // �I�u�W�F�N�g��Y���W���^�[�Q�b�g�̕����Ɍ�����
                transform.rotation = Quaternion.FromToRotation(Vector3.up, -_target);
            }
            
        }
    }
}
