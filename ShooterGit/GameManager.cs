using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // 画面外の判定時、出てないのに消えるのを防ぐ
    [SerializeField] private float _screenOver;
    
    // 経過した時間
    [SerializeField] private List<float> _timeLapse;

    // 時間経過によって出現する敵のパターンの順番
    [SerializeField] private List<GameObject> _timeLapseObjects;

    

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

    }

    // カメラの外をほかのスクリプトに送る
    public float ScreenTop() {

        return _screenTop;
    }

    public float ScreenUnder() {

        return _screenUnder;
    }

    public float ScreenRight() {

        return _screenRight;
    }

    public float ScreenLeft() {

        return _screenLeft;
    }
}
