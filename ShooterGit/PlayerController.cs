using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // オブジェクトプールスクリプトを取得
    [SerializeField] private ObjectPool _objectPool;

    // ゲームマネージャーのスクリプトをとる
    [SerializeField] private GameManager _gameManager;

    

    // 通常時のプレイヤーの移動速度
    [SerializeField] private float _nomalSpeed;

    // ゆっくり動くときのプレイヤーの移動速度
    [SerializeField] private float _slowSpeed;

    // 撃つ感覚
    [SerializeField] private float _bulletInterval;

    // ダメージを受けた時のインターバルタイム
    [SerializeField] private float _damageIntervalTime;

    // ダメージを受けた時の無敵時間
    [SerializeField] private float _mutekiTime;

    // 各ステータスの初期値
    // 残機の初期値
    [SerializeField] private int _initialZanki;
    // ボムの初期値
    [SerializeField] private int _initialBomb;
    // パワーの初期値
    [SerializeField] private int _initialPower;

    private Animator _anim = null;

    // 各種タグ
    private string _enemyTag = "Enemy";
    private string _enemyBulletTag = "EnemyBullet";
    private string _zankiItemTag = "ZankiItem";
    private string _bombItemTag = "BombItem";
    private string _barrierItemTag = "BarrierItem";
    private string _powerItemTag = "PowerItem";
    private string _damagePlayertag = "DamagePlayer";
    private string _playerTag = "Player";

    // 残機
    private int _zanki;

    // ボム
    private int _bomb;

    // プレイヤーのパワー状態
    private int _power;

    // プレイヤーのバリア状態
    private bool _isBarrier;

    // 無敵時間の計測
    private float _muteki;

    // ダメージを食らった時のインターバルタイム
    private float _damageIntervalTimer;

    // プレイヤーの移動速度
    private float _playerSpeed;

    

    // 敵の弾が当たったかどうか
    private bool _isDamage;

    // 無敵かどうか
    private bool _isMuteki;


    // 画面外に出ないようにするための変数
    private float _screenTop; // 画面の上
    private float _screenUnder; // 画面の下
    private float _screenRight; // 画面の右
    private float _screenLeft; // 画面の左

    

    private void Start() {

        _anim = GetComponent<Animator>();

        _anim.SetBool("isDamage", false);

        // 画面端の座標をGameManagerからとる
        _screenTop = _gameManager.ScreenTop();
        _screenUnder = _gameManager.ScreenUnder();
        _screenRight = _gameManager.ScreenRight();
        _screenLeft = _gameManager.ScreenLeft();

        _zanki = _initialZanki; // 残機の初期設定
        _bomb = _initialBomb; // ボムの初期設定
        _power = _initialPower; // パワーの初期値

        
    }

    private void Update() {

        

        // 上下左右に動くキーの設定
        // 左右
        float horizontalKey = Input.GetAxisRaw("Horizontal");
        // 上下
        float verticalKey = Input.GetAxisRaw("Vertical");

        // ダメージを受けていないとき
        if (!_isDamage) {

            // 移動する向きを決める
            Vector2 direction = new Vector2(horizontalKey, verticalKey).normalized;

            // 移動する向きとスピードを代入する
            GetComponent<Rigidbody2D>().velocity = direction * _playerSpeed;

            if (Input.GetKey(KeyCode.LeftShift)) {

                // プレイヤーのスピードをゆっくりにする
                _playerSpeed = _slowSpeed;                
            } else {

                // プレイヤーのスピードをはやくする
                _playerSpeed = _nomalSpeed;
            }
        }

        

        // ダメージを受けた時の処理
        if (_isDamage) {

            _damageIntervalTimer += Time.deltaTime;

            // その場に止まる
            GetComponent<Rigidbody2D>().velocity = transform.position * _playerSpeed;

            _playerSpeed = 0;

            // バリアを張っていた場合
            if (_isBarrier) {

                // バリアを解除する
                _isBarrier = false;
            }
            // 残機が1よりも多い場合
            else if (_zanki > 1 && !_isBarrier) {

                _anim.SetBool("isDamage", true);

                // 残機を一つ減らす
                _zanki -= 1;
            }
            // 残機が1以下の場合
            else if (_zanki <= 1 && !_isBarrier) {

                _anim.SetBool("isDamage", true);

                // ゲームオーバーになる
                _gameManager.GameOver();
            }

            if (_damageIntervalTimer >= _damageIntervalTime) {

                _isDamage = false;
            }
        }


    }

    // 何かに触れた時の処理
    private void OnTriggerEnter2D(Collider2D collision) {

        // 入ってきたのが敵もしくは敵の弾だったら
        if (collision.tag == _enemyBulletTag　|| collision.tag == _enemyTag) {

            // ダメージを受けたフラグをオンにする
            _isDamage = true;
        }
    }

    public bool IsDamage() {

        return _isDamage;
    }
}
