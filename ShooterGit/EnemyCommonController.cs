using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ここでは敵の基本的なステータスをunityの画面で入力できるようにする
/// 敵の種類によってステータスが違うのでまとめられるものはここでまとめてしまう
/// </summary>

public class EnemyCommonController : MonoBehaviour
{
    // 敵のゲームオブジェクト取得
    [SerializeField] private GameObject _enemy;

    // 敵の弾のオブジェクトプール取得
    [SerializeField] private EnemyBulletPool _bulletPool;

    // 弾の発射地点
    [SerializeField] private EnemyBulletPos _bulletPos;

    // 敵の体力
    [SerializeField] private int _enemyHp;

    // プレイヤーのゲームオブジェクト
    [SerializeField] private GameObject _playerObj;

    // プレイヤーの弾のタグをとる
    private string _playerBullet = "PlayerBullet";

    // やられたかどうか
    private bool _isDead;

    // 二週目かどうか
    private bool _isSecond;

    // ハードモードかどうか
    private bool _isHard;

    private void Update() {

        // 体力が0以下になったら
        if (_enemyHp <= 0) {

            // やられた判定をとる
            _isDead = true;

            // 非表示にする
            this.gameObject.SetActive(false);
        }
    }

    // プレイヤーの弾に触れたら体力を減らす
    private void OnTriggerEnter2D(Collider2D collision) {

        // 入ってきたのがプレイヤーの弾だったら
        if (collision.tag == _playerBullet) {

            // 体力を減らす
            _enemyHp--;
        }
    }

    // ほかのスクリプトにやられた判定を返す
    public bool IsDead() {

        return _isDead;
    }

}
