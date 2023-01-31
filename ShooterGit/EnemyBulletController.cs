using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletController : MonoBehaviour
{
    // オブジェクトプールの取得
    [SerializeField] private EnemyBulletPool _bulletPool;

    // 弾の発射地点
    [SerializeField] private EnemyBulletPos _bulletPos;

    // 弾のゲームオブジェクト
    [SerializeField] private GameObject _bulletObj;

    // プレイヤーのゲームオブジェクト
    [SerializeField] private GameObject _playerObj;

    // 弾の移動速度
    [SerializeField] private float _bulletSpeed;

    // 画面外の判定時、出てないのに消えるのを防ぐ
    [SerializeField] private float _screenOver;
    
    // 一定間隔で弾を発射する際に使用
    [SerializeField] private float _bulletInterval;

    // 弾の移動先をコントロールするための変数
    [SerializeField] private Vector3 _bulletPosController;

    // 弾の現在地
    private Vector3 _bulletNowPos;

    // 弾が発射されてからかかった時間
    private float _bulletTime;

    // 弾が発射されたかどうか
    private bool _isShot;

    // 追尾する弾かどうか
    private bool _isTracking;

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

        // プレイヤーのゲームオブジェクトを取得
        _playerObj = GameObject.FindGameObjectWithTag("Player");

        // オブジェクトプールを取得
        _bulletPool = transform.parent.GetComponent<EnemyBulletPool>();

        // 弾の発射地点を取得
        _bulletPos = _bulletPool.BulletPos();

        // 生成した時は非アクティブにしておく
        gameObject.SetActive(false);

        // 弾のスピードを格納
        _bulletSpeed = _bulletPos.BulletSpeed();

    }
    /// <summary>
    /// 弾の挙動の制御
    /// </summary>
    private void Update() {

        if (!_isShot) {

            // 発射する角度を確認
            CurrentPlayer();

            // 発射されたフラグをオンにする
            _isShot = true;
        }

        if (_isShot) {

            // 弾の発射時間の計測
            _bulletTime += Time.deltaTime;

            // 弾の発射
            transform.position += transform.up * Time.deltaTime * _bulletSpeed;            
        }

        // 弾の回収。画面外に出たら回収する
        if (transform.position.y > _screenTop || transform.position.y < _screenUnder ||
            transform.position.x > _screenRight || transform.position.x < _screenLeft) {

            // 発射されたフラグをオフにする
            _isShot = false;

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

        // // オブジェクトプールのCollect関数を呼び出し自信を回収
        _bulletPool.Collect(this);
    }

    // 発射地点のスクリプトから飛ばす方向取得
    public void CurrentPlayer() {

        // ターゲットの位置を確認する
        Vector3 toDirection = _bulletPos.Direction();

        // オブジェクトのY座標をターゲットのいる向きに向ける
        transform.rotation = Quaternion.FromToRotation(Vector3.up, toDirection);
    }


}
