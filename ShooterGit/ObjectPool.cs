using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    // BulletControllerのプレハブの取得
    [SerializeField] private BulletController _bullet;

    // 最初に生成する数
    [SerializeField] private int _maxCount;

    // 生成した弾を格納するQueue
    private Queue<BulletController> _bulletQueue;

    // 初回生成時のポジション
    private Vector3 _setPos = new Vector3(0, 0, 0);

    // 起動時の処理
    private void Awake() {

        // Queueの初期化
        _bulletQueue = new Queue<BulletController>();

        // 弾を生成するループ
        for (int i = 0; i < _maxCount; i++) {

            // 生成
            BulletController tmpBullet = Instantiate(_bullet, _setPos, Quaternion.identity, transform);

            // Queueに追加
            _bulletQueue.Enqueue(tmpBullet);
        }
    }

    //弾を貸し出す処理
    public BulletController Launch(Vector3 pos) {
        //Queueが空ならnull
        if (_bulletQueue.Count <= 0) {
            return null;
        }

        //Queueから弾を一つ取り出す
        BulletController tmpBullet = _bulletQueue.Dequeue();
        //弾を表示する
        tmpBullet.gameObject.SetActive(true);
        //渡された座標に弾を移動する
        tmpBullet.ShowInStage(pos);
        //呼び出し元に渡す
        return tmpBullet;
    }

    // 弾の回収処理
    public void Collect(BulletController bullet) {

        // 弾のゲームオブジェクトを非表示
        bullet.gameObject.SetActive(false);

        // Queueに格納
        _bulletQueue.Enqueue(bullet);
    }
}
