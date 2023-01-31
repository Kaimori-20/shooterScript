using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // 画面外の判定時、出てないのに消えるのを防ぐ
    [SerializeField] private float _screenOver;

    // ゲームオーバー時のメッセージ
    [SerializeField] private GameObject _overMessegi;

    // ゲームクリア時のメッセージ
    [SerializeField] private GameObject _clearMessegi;

    [SerializeField] private string _nextScene;

    [SerializeField] private float _time;

    // 経過した時間
    [SerializeField] private List<float> _timeLapse;

    // 時間経過によって出現する敵のパターンの順番
    [SerializeField] private List<GameObject> _timeLapseObjects;

    // ゲームオーバーになったかどうか
    private bool _isGameOver;

    // ゲームをクリアしたかどうか
    private bool _isClear;

    // クリア、ゲームオーバー時の計測時間
    private float _timer;

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

        _overMessegi.SetActive(false);

        _clearMessegi.SetActive(false);

        _timer = 0;
    }

    private void Update() {

        if (_isGameOver || _isClear) {

            _timer += Time.deltaTime;

            if (_isGameOver) {

                _overMessegi.SetActive(true);
            } else if (_isClear) {

                _clearMessegi.SetActive(true);
            }

            if (_timer >= _time) {

                SceneManager.LoadScene(_nextScene);
            }
        }
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

    public void GameOver() {

        _isGameOver = true;
    }
}
