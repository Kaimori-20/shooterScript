using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingObject : MonoBehaviour
{
    // 発射地点のスクリプト取得
    [SerializeField] private EnemyBulletPos _bulletPos;

    // プレイヤーのゲームオブジェクト
    [SerializeField] private GameObject _playerObj;

    [SerializeField] private GameObject _terget;

    // 追尾するかどうか
    private bool _isTracking;

    // プレイヤーを中心に振るためのbool
    private bool _isPlayerTracking;

    // ターゲットはどこかを調べる
    private Vector3 _target;

    private void Start() {

        _isTracking = _bulletPos.IsTracking(); 

        
    }

    private void Update() {

        if (_isTracking) {

            // ターゲットの位置を確認する
            _target = _playerObj.transform.position - transform.position;

            if (!_isPlayerTracking) {

                // オブジェクトのY座標をターゲットの方向に向ける
                transform.rotation = Quaternion.FromToRotation(Vector3.up, _target);
            } 
            else if (_isPlayerTracking) {

                // オブジェクトのY座標をターゲットの方向に向ける
                transform.rotation = Quaternion.FromToRotation(Vector3.up, -_target);
            }
            
        }
    }
}
