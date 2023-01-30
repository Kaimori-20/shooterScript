using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingObject : MonoBehaviour
{
    // ���˒n�_�̃X�N���v�g�擾
    [SerializeField] private EnemyBulletPos _bulletPos;

    // �v���C���[�̃Q�[���I�u�W�F�N�g
    [SerializeField] private GameObject _playerObj;

    [SerializeField] private GameObject _terget;

    // �ǔ����邩�ǂ���
    private bool _isTracking;

    // �v���C���[�𒆐S�ɐU�邽�߂�bool
    private bool _isPlayerTracking;

    // �^�[�Q�b�g�͂ǂ����𒲂ׂ�
    private Vector3 _target;

    private void Start() {

        _isTracking = _bulletPos.IsTracking(); 

        
    }

    private void Update() {

        if (_isTracking) {

            // �^�[�Q�b�g�̈ʒu���m�F����
            _target = _playerObj.transform.position - transform.position;

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
