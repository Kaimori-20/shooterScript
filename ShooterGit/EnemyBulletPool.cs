using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletPool : MonoBehaviour
{
    // 敵の本体のオブジェクト
    [SerializeField] private GameObject _masterObject;

    // 敵のゲームオブジェクト
    [SerializeField] private EnemyCommonController _enemy;

    // EnemyBulletControllerのプレハブの取得
    [SerializeField] private EnemyBulletController _bullet;

    // 敵の弾のオブジェクトプール
    [SerializeField] private EnemyBulletPool _bulletPool;

    // 敵の弾の発射地点
    [SerializeField] private EnemyBulletPos _bulletPos;

    // プレイヤーのオブジェクト
    [SerializeField] private GameObject _player;

    // 最初に生成する数
    [SerializeField] private int _maxCount;

    // 生成した弾を格納するQueue
    private Queue<EnemyBulletController> _bulletQueue;

    // 初回生成時のポジション
    private Vector3 _setPos = new Vector3(0, 0, 0);

    // 起動時の処理
    private void Awake() {

        // Queueの初期化
        _bulletQueue = new Queue<EnemyBulletController>();

        // 弾を生成するループ
        for (int i = 0; i < _maxCount; i++) {

            // 生成
            EnemyBulletController tmpBullet = Instantiate(_bullet, _setPos, Quaternion.identity, transform);

            // Queueに追加
            _bulletQueue.Enqueue(tmpBullet);
        }
    }

    private void Update() {

        
    }

    //弾を貸し出す処理
    public EnemyBulletController Launch(Vector3 pos) {
        //Queueが空ならnull
        if (_bulletQueue.Count <= 0) {
            return null;
        }

        //Queueから弾を一つ取り出す
        EnemyBulletController tmpBullet = _bulletQueue.Dequeue();
        //弾を表示する
        tmpBullet.gameObject.SetActive(true);
        //渡された座標に弾を移動する
        tmpBullet.ShowInStage(pos);
        //呼び出し元に渡す
        return tmpBullet;
    }

    // 弾の回収処理
    public void Collect(EnemyBulletController bullet) {

        // 弾のゲームオブジェクトを非表示
        bullet.gameObject.SetActive(false);

        // Queueに格納
        _bulletQueue.Enqueue(bullet);
    }

    // 弾の発射地点を弾のプレハブに渡す
    public EnemyBulletPos BulletPos() {

        return _bulletPos;
    }
}

