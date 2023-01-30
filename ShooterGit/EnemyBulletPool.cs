using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletPool : MonoBehaviour
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

    // �v���C���[�̃I�u�W�F�N�g
    [SerializeField] private GameObject _player;

    // �ŏ��ɐ������鐔
    [SerializeField] private int _maxCount;

    // ���������e���i�[����Queue
    private Queue<EnemyBulletController> _bulletQueue;

    // ���񐶐����̃|�W�V����
    private Vector3 _setPos = new Vector3(0, 0, 0);

    // �N�����̏���
    private void Awake() {

        // Queue�̏�����
        _bulletQueue = new Queue<EnemyBulletController>();

        // �e�𐶐����郋�[�v
        for (int i = 0; i < _maxCount; i++) {

            // ����
            EnemyBulletController tmpBullet = Instantiate(_bullet, _setPos, Quaternion.identity, transform);

            // Queue�ɒǉ�
            _bulletQueue.Enqueue(tmpBullet);
        }
    }

    private void Update() {

        
    }

    //�e��݂��o������
    public EnemyBulletController Launch(Vector3 pos) {
        //Queue����Ȃ�null
        if (_bulletQueue.Count <= 0) {
            return null;
        }

        //Queue����e������o��
        EnemyBulletController tmpBullet = _bulletQueue.Dequeue();
        //�e��\������
        tmpBullet.gameObject.SetActive(true);
        //�n���ꂽ���W�ɒe���ړ�����
        tmpBullet.ShowInStage(pos);
        //�Ăяo�����ɓn��
        return tmpBullet;
    }

    // �e�̉������
    public void Collect(EnemyBulletController bullet) {

        // �e�̃Q�[���I�u�W�F�N�g���\��
        bullet.gameObject.SetActive(false);

        // Queue�Ɋi�[
        _bulletQueue.Enqueue(bullet);
    }

    // �e�̔��˒n�_��e�̃v���n�u�ɓn��
    public EnemyBulletPos BulletPos() {

        return _bulletPos;
    }
}

