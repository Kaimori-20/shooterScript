using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����ł͓G�̊�{�I�ȃX�e�[�^�X��unity�̉�ʂœ��͂ł���悤�ɂ���
/// �G�̎�ނɂ���ăX�e�[�^�X���Ⴄ�̂ł܂Ƃ߂�����̂͂����ł܂Ƃ߂Ă��܂�
/// </summary>

public class EnemyCommonController : MonoBehaviour
{
    // �G�̃Q�[���I�u�W�F�N�g�擾
    [SerializeField] private GameObject _enemy;

    // �G�̒e�̃I�u�W�F�N�g�v�[���擾
    [SerializeField] private EnemyBulletPool _bulletPool;

    // �e�̔��˒n�_
    [SerializeField] private EnemyBulletPos _bulletPos;

    // �G�̗̑�
    [SerializeField] private int _enemyHp;

    // �v���C���[�̃Q�[���I�u�W�F�N�g
    [SerializeField] private GameObject _playerObj;

    // �v���C���[�̒e�̃^�O���Ƃ�
    private string _playerBullet = "PlayerBullet";

    // ���ꂽ���ǂ���
    private bool _isDead;

    // ��T�ڂ��ǂ���
    private bool _isSecond;

    // �n�[�h���[�h���ǂ���
    private bool _isHard;

    private void Update() {

        // �̗͂�0�ȉ��ɂȂ�����
        if (_enemyHp <= 0) {

            // ���ꂽ������Ƃ�
            _isDead = true;

            // ��\���ɂ���
            this.gameObject.SetActive(false);
        }
    }

    // �v���C���[�̒e�ɐG�ꂽ��̗͂����炷
    private void OnTriggerEnter2D(Collider2D collision) {

        // �����Ă����̂��v���C���[�̒e��������
        if (collision.tag == _playerBullet) {

            // �̗͂����炷
            _enemyHp--;
        }
    }

    // �ق��̃X�N���v�g�ɂ��ꂽ�����Ԃ�
    public bool IsDead() {

        return _isDead;
    }

}
