using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletPos : MonoBehaviour
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

    // ターゲットのスクリプト
    [SerializeField] private TergetController _terget;

    // プレイヤーのオブジェクト
    [SerializeField] private GameObject _player;

    // 弾の発射先
    [SerializeField] private GameObject _tergetObj;


    [Header("弾の速度")]
    [SerializeField] private float _bulletSpeed;

    [Header("弾の発射感覚")]
    [SerializeField] private float _bulletTime;

    // 弾の発射方向の制御するための項目
    [Header("弾の発射方向に関する項目")]
    [Header("プレイヤーを追尾するかどうか")][SerializeField] private bool _isTracking;
    [Header("回転する弾かどうか")]
    [SerializeField] private bool _isRoatBullet;
    [Header("右回りかどうか")]
    [SerializeField] private bool _isRightRoat;

    [Header("弾が振り子のように振れる挙動")]
    [Header("振れるかどうか")]
    [SerializeField] private bool _isShakeBullet;

    [Header("回転する速さや開店する時間などの制御項目")]
    [Header("回転するスピード")] [SerializeField] private float _roatSpeed;
    [Header("回転する時間")] [SerializeField] private float _roatTime;

    
    // 発射する目標の位置を確認
    private Vector3 _toDirection;

    // 弾の発射感覚のための計測時間
    private float _bulletNowTime;

    // 回転している時の経過時間
    private float _roatTimer;

    // 計測時間のリセット時間
    private float _timerReset = 0;

    // やられたかどうか
    public bool _isDead;

    private void Awake() {

        _isDead = false;

        if (_isRoatBullet) {

            _isShakeBullet = false;
        }

        _toDirection = _tergetObj.transform.position - transform.position;

        //transform.rotation = Quaternion.FromToRotation(Vector3.up, _toDirection);

    }

    private void Update() {

        if (!_isDead) {

            #region // 回転させる場合のスクリプト
            if (_isRoatBullet || _isShakeBullet) {

                // 右回りの場合
                if (_isRightRoat) {

                    gameObject.transform.Rotate(_roatSpeed * Time.deltaTime * new Vector3(0, 0, -10));
                }
                // 左回りの場合
                else if (!_isRightRoat) {

                    gameObject.transform.Rotate(_roatSpeed * Time.deltaTime * new Vector3(0, 0, 10));
                }
            }
            #endregion

            #region // 弾を振れさせる場合
            if (_isShakeBullet) {

                // 回転時間の計測
                _roatTimer += Time.deltaTime;

                // 一定時間以上たったら
                if (_roatTimer >= _roatTime) {

                    // 右回りの場合
                    if (_isRightRoat) {

                        // 右に回るフラグをオフにする
                        _isRightRoat = false;
                        
                        // 計測時間のリセット
                        _roatTimer = _timerReset;
                    }
                    // 左回りの場合
                    else if (!_isRightRoat) {

                        // 右に回るフラグをオフにする
                        _isRightRoat = true;

                        // 計測時間のリセット
                        _roatTimer = _timerReset;
                    }
                }
            
                
            }
            #endregion

            // ターゲットの位置を確認する
            _toDirection = _tergetObj.transform.position - transform.position;

            // 発射感覚の計測
            _bulletNowTime += Time.deltaTime;

            // 一定時間たったら弾がプレイヤーに向けて発射される
            if (_bulletNowTime >= _bulletTime && !_isDead) {

                // オブジェクトプールのLaunch関数呼び出し
                _bulletPool.Launch(transform.position);

                // インターバルタイムのリセット
                _bulletNowTime = 0;
            }

            // やられたかどうかを確認する
            _isDead = _enemy.IsDead();
        }

        // やられた判定をとったら
        if (_isDead) {

            // 発射地点を非表示にする
            gameObject.SetActive(false);
        }
    }

    // 弾の速度をBulletControllerのbulletSpeedに格納
    public float BulletSpeed() {

        return _bulletSpeed;
    }

    public Vector3 Direction() {

        return _toDirection;
    }
    public bool IsTracking() {

        return _isTracking;
    }
    public bool IsShake() {

        return _isShakeBullet;
    }
    public float ShakeTime() {

        return _roatTime;
    }

    public void IsDeadFalse() {

        _isDead = false;
    }
}
