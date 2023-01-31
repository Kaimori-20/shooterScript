using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotPosScript : MonoBehaviour
{
    [SerializeField] private PlayerController _player;

    // オブジェクトプールスクリプトを取得
    [SerializeField] private ObjectPool _objectPool;

    // 撃つ感覚
    [SerializeField] private float _bulletInterval;

    // Zキーを押したときに出す弾の数
    [SerializeField] private int _bulletCount;

    // 左右から出す弾かどうか
    [SerializeField] private bool _isPosSide;

    private bool _isDamage;

    // 弾を出しているかどうか
    private bool _isShot;

    private void Start() {

        _isDamage = false;

        // まだ弾を出していないためfalse
        _isShot = false;
    }

    private void Update() {

        _isDamage = _player.IsDamage();

        // Zキーが押されたら弾が発射され、話したら弾が止まる処理
        // Zキーを押したら弾が発射される
        if (Input.GetKeyDown(KeyCode.Z) && !_isShot && !_isDamage) {

            // 弾を出すコルーチンの開始
            StartCoroutine(Shot());
        }
    }

    // 発射のコルーチン
    IEnumerator Shot() {

        // 弾を一定間隔で一定量出す
        for (int i = 1; i <= _bulletCount; i++) {

            // 弾を出しているためtrueにし、入力を断り、弾が重ならないようにする
            _isShot = true;

            //// BulletControllerを探し出す
            //_bullet = this.gameObject.GetComponent<BulletController>();

            // オブジェクトプールのLaunch関数呼び出し
            _objectPool.Launch(transform.position);

            // インターバルを設ける
            yield return new WaitForSeconds(_bulletInterval);
        }

        // 弾を出し切ったためまた入力を受け付ける
        _isShot = false;

        // コルーチンの終了
        yield break;

    }

    
}
