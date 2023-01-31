using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // �I�u�W�F�N�g�v�[���X�N���v�g���擾
    [SerializeField] private ObjectPool _objectPool;

    // �Q�[���}�l�[�W���[�̃X�N���v�g���Ƃ�
    [SerializeField] private GameManager _gameManager;

    

    // �ʏ펞�̃v���C���[�̈ړ����x
    [SerializeField] private float _nomalSpeed;

    // ������蓮���Ƃ��̃v���C���[�̈ړ����x
    [SerializeField] private float _slowSpeed;

    // �����o
    [SerializeField] private float _bulletInterval;

    // �_���[�W���󂯂����̃C���^�[�o���^�C��
    [SerializeField] private float _damageIntervalTime;

    // �_���[�W���󂯂����̖��G����
    [SerializeField] private float _mutekiTime;

    // �e�X�e�[�^�X�̏����l
    // �c�@�̏����l
    [SerializeField] private int _initialZanki;
    // �{���̏����l
    [SerializeField] private int _initialBomb;
    // �p���[�̏����l
    [SerializeField] private int _initialPower;

    private Animator _anim = null;

    // �e��^�O
    private string _enemyTag = "Enemy";
    private string _enemyBulletTag = "EnemyBullet";
    private string _zankiItemTag = "ZankiItem";
    private string _bombItemTag = "BombItem";
    private string _barrierItemTag = "BarrierItem";
    private string _powerItemTag = "PowerItem";
    private string _damagePlayertag = "DamagePlayer";
    private string _playerTag = "Player";

    // �c�@
    private int _zanki;

    // �{��
    private int _bomb;

    // �v���C���[�̃p���[���
    private int _power;

    // �v���C���[�̃o���A���
    private bool _isBarrier;

    // ���G���Ԃ̌v��
    private float _muteki;

    // �_���[�W��H��������̃C���^�[�o���^�C��
    private float _damageIntervalTimer;

    // �v���C���[�̈ړ����x
    private float _playerSpeed;

    

    // �G�̒e�������������ǂ���
    private bool _isDamage;

    // ���G���ǂ���
    private bool _isMuteki;


    // ��ʊO�ɏo�Ȃ��悤�ɂ��邽�߂̕ϐ�
    private float _screenTop; // ��ʂ̏�
    private float _screenUnder; // ��ʂ̉�
    private float _screenRight; // ��ʂ̉E
    private float _screenLeft; // ��ʂ̍�

    

    private void Start() {

        _anim = GetComponent<Animator>();

        _anim.SetBool("isDamage", false);

        // ��ʒ[�̍��W��GameManager����Ƃ�
        _screenTop = _gameManager.ScreenTop();
        _screenUnder = _gameManager.ScreenUnder();
        _screenRight = _gameManager.ScreenRight();
        _screenLeft = _gameManager.ScreenLeft();

        _zanki = _initialZanki; // �c�@�̏����ݒ�
        _bomb = _initialBomb; // �{���̏����ݒ�
        _power = _initialPower; // �p���[�̏����l

        
    }

    private void Update() {

        

        // �㉺���E�ɓ����L�[�̐ݒ�
        // ���E
        float horizontalKey = Input.GetAxisRaw("Horizontal");
        // �㉺
        float verticalKey = Input.GetAxisRaw("Vertical");

        // �_���[�W���󂯂Ă��Ȃ��Ƃ�
        if (!_isDamage) {

            // �ړ�������������߂�
            Vector2 direction = new Vector2(horizontalKey, verticalKey).normalized;

            // �ړ���������ƃX�s�[�h��������
            GetComponent<Rigidbody2D>().velocity = direction * _playerSpeed;

            if (Input.GetKey(KeyCode.LeftShift)) {

                // �v���C���[�̃X�s�[�h���������ɂ���
                _playerSpeed = _slowSpeed;                
            } else {

                // �v���C���[�̃X�s�[�h���͂₭����
                _playerSpeed = _nomalSpeed;
            }
        }

        

        // �_���[�W���󂯂����̏���
        if (_isDamage) {

            _damageIntervalTimer += Time.deltaTime;

            // ���̏�Ɏ~�܂�
            GetComponent<Rigidbody2D>().velocity = transform.position * _playerSpeed;

            _playerSpeed = 0;

            // �o���A�𒣂��Ă����ꍇ
            if (_isBarrier) {

                // �o���A����������
                _isBarrier = false;
            }
            // �c�@��1���������ꍇ
            else if (_zanki > 1 && !_isBarrier) {

                _anim.SetBool("isDamage", true);

                // �c�@������炷
                _zanki -= 1;
            }
            // �c�@��1�ȉ��̏ꍇ
            else if (_zanki <= 1 && !_isBarrier) {

                _anim.SetBool("isDamage", true);

                // �Q�[���I�[�o�[�ɂȂ�
                _gameManager.GameOver();
            }

            if (_damageIntervalTimer >= _damageIntervalTime) {

                _isDamage = false;
            }
        }


    }

    // �����ɐG�ꂽ���̏���
    private void OnTriggerEnter2D(Collider2D collision) {

        // �����Ă����̂��G�������͓G�̒e��������
        if (collision.tag == _enemyBulletTag�@|| collision.tag == _enemyTag) {

            // �_���[�W���󂯂��t���O���I���ɂ���
            _isDamage = true;
        }
    }

    public bool IsDamage() {

        return _isDamage;
    }
}
