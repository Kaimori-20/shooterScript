using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    // ObjectPoolを取得
    [SerializeField] private ObjectPool _objectPool;

    // 弾のゲームオブジェクト
    [SerializeField] private GameObject _bullet;

    // 弾の移動速度
    [SerializeField] private float _bulletSpeed;    

    // 画面外の判定時、出てないのに消えるのを防ぐ
    [SerializeField] private float _screenOver;

    // 斜めに弾が出る判定か
    [SerializeField] private bool _isSide;

    // 発射される位置は左側か
    [SerializeField] private bool _isLeft;

    // 弾の移動先をコントロールための変数
    [SerializeField] private Vector3 _bulletPosController;

    // 敵(Enemy)のタグをとる変数
    private string _enemyTag = "Enemy";

    // 弾の現在地
    private Vector3 _bulletNowPos;

    // 画面外に出た判定をとるのに使用
    private float _screenTop; // 画面の上
    private float _screenUnder; // 画面の下
    private float _screenRight; // 画面の右
    private float _screenLeft; // 画面の左


    private void Start() {

        // 画面の一番上のy座標を取得
        _screenTop = Camera.main.ViewportToWorldPoint(new Vector2(0, 1)).y + _screenOver;

        // 画面の一番下のy座標を取得
        _screenUnder = Camera.main.ViewportToWorldPoint(new Vector2(0, 0)).y - _screenOver;

        // 画面の右側を取得
        _screenRight = Camera.main.ViewportToWorldPoint(new Vector2(1, 0)).x + _screenOver;

        // 画面の左側を取得
        _screenLeft = Camera.main.ViewportToWorldPoint(new Vector2(0, 0)).x - _screenOver;

        // オブジェクトプールを取得
        _objectPool = transform.parent.GetComponent<ObjectPool>();

        //_bulletNowPos = new Vector3(
        //    _bullet.transform.position.x + _bulletPosController.x,
        //    _bullet.transform.position.y + _bulletPosController.y,
        //    _bullet.transform.position.z + _bulletPosController.z);

        // 生成した時は非アクティブにしておく
        gameObject.SetActive(false);

        
    }

    /// <summary>
    /// 弾の挙動を制御する
    /// </summary>
    private void Update() {

        _bulletNowPos = _bullet.transform.position;

        // 正面から出る弾の場合
        if (!_isSide) {

            // まっすぐに弾を発射
            transform.position = Vector3.MoveTowards(
                    transform.position, new Vector3(
                    _bullet.transform.position.x,
                    _bulletNowPos.y + _bulletPosController.y,
                    _bulletNowPos.z),
                    _bulletSpeed * Time.deltaTime);


        }
        // 横から出る弾の場合
        else if (_isSide) {

            // 斜めに弾を発射
            transform.position = Vector3.MoveTowards(
                transform.position,
                new Vector3(
                _bulletNowPos.x + _bulletPosController.x,
                _bulletNowPos.y + _bulletPosController.y,
                _bulletNowPos.z),
                _bulletSpeed * Time.deltaTime);
        }

        // 弾の回収。画面外に出たら回収する
        if (transform.position.y > _screenTop || transform.position.y < _screenUnder ||
            transform.position.x > _screenRight || transform.position.x < _screenLeft) {

            HideFromStage();
        }
    }

    // 敵に当たったら弾を回収する
    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.tag == _enemyTag) {

            HideFromStage();
        }
    }

    // 
    public void ShowInStage(Vector3 pos) {

        // positionを渡された座標に設定
        transform.position = pos;
    }

    //
    public void HideFromStage() {

        // オブジェクトプールのCollect関数を呼び出し自信を回収
        _objectPool.Collect(this);
    }
}
