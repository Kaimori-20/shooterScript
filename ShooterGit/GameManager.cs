using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // ��ʊO�̔��莞�A�o�ĂȂ��̂ɏ�����̂�h��
    [SerializeField] private float _screenOver;
    
    // �o�߂�������
    [SerializeField] private List<float> _timeLapse;

    // ���Ԍo�߂ɂ���ďo������G�̃p�^�[���̏���
    [SerializeField] private List<GameObject> _timeLapseObjects;

    

    // ��ʊO�ɏo��������Ƃ�̂Ɏg�p
    private float _screenTop; // ��ʂ̏�
    private float _screenUnder; // ��ʂ̉�
    private float _screenRight; // ��ʂ̉E
    private float _screenLeft; // ��ʂ̍�

    private void Start() {

        // ��ʂ̈�ԏ��y���W���擾
        _screenTop = Camera.main.ViewportToWorldPoint(new Vector2(0, 1)).y + _screenOver;

        // ��ʂ̈�ԉ���y���W���擾
        _screenUnder = Camera.main.ViewportToWorldPoint(new Vector2(0, 0)).y - _screenOver;

        // ��ʂ̉E�����擾
        _screenRight = Camera.main.ViewportToWorldPoint(new Vector2(1, 0)).x + _screenOver;

        // ��ʂ̍������擾
        _screenLeft = Camera.main.ViewportToWorldPoint(new Vector2(0, 0)).x - _screenOver;

    }

    // �J�����̊O���ق��̃X�N���v�g�ɑ���
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
