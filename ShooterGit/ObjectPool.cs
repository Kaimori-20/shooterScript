using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    // BulletController�̃v���n�u�̎擾
    [SerializeField] private BulletController _bullet;

    // �ŏ��ɐ������鐔
    [SerializeField] private int _maxCount;

    // ���������e���i�[����Queue
    private Queue<BulletController> _bulletQueue;

    // ���񐶐����̃|�W�V����
    private Vector3 _setPos = new Vector3(0, 0, 0);

    // �N�����̏���
    private void Awake() {

        // Queue�̏�����
        _bulletQueue = new Queue<BulletController>();

        // �e�𐶐����郋�[�v
        for (int i = 0; i < _maxCount; i++) {

            // ����
            BulletController tmpBullet = Instantiate(_bullet, _setPos, Quaternion.identity, transform);

            // Queue�ɒǉ�
            _bulletQueue.Enqueue(tmpBullet);
        }
    }

    //�e��݂��o������
    public BulletController Launch(Vector3 pos) {
        //Queue����Ȃ�null
        if (_bulletQueue.Count <= 0) {
            return null;
        }

        //Queue����e������o��
        BulletController tmpBullet = _bulletQueue.Dequeue();
        //�e��\������
        tmpBullet.gameObject.SetActive(true);
        //�n���ꂽ���W�ɒe���ړ�����
        tmpBullet.ShowInStage(pos);
        //�Ăяo�����ɓn��
        return tmpBullet;
    }

    // �e�̉������
    public void Collect(BulletController bullet) {

        // �e�̃Q�[���I�u�W�F�N�g���\��
        bullet.gameObject.SetActive(false);

        // Queue�Ɋi�[
        _bulletQueue.Enqueue(bullet);
    }
}
