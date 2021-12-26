using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

/// <summary>
/// プレイヤー操作
/// </summary>
public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// 物理演算のコンポーネント
    /// </summary>
    private Rigidbody _rb;

    /// <summary>
    /// 移動速度
    /// </summary>
    public float Speed;

    /// <summary>
    /// カメラ回転x軸
    /// </summary>
    public Transform Rig;

    /// <summary>
    /// 点数カウンター
    /// </summary>
    private int _counter = 0;

    /// <summary>
    /// 点数カウンターUI
    /// </summary>
    [SerializeField] private Text _countText;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        SetCount();
    }

    private void FixedUpdate()
    {
        Move();
        RotateCamera();
    }

    /// <summary>
    /// プレイヤーを移動する
    /// </summary>
    private void Move()
    {
        // float moveHorizontal = Input.GetAxis("Horizontal"); // 左右（←→AD）の入力[-1~1]を取得
        // float moveVertical = Input.GetAxis("Vertical"); // 上下（↑↓WS）の入力[-1~1]を取得

        // Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical); // 左右入力=x軸，上下入力=z軸 のベクトルを生成

        // _rb.AddForce(movement * Speed); // 物体に力を加える

        if (Input.GetKey (KeyCode.W)) {
			transform.position += transform.forward * Speed * Time.deltaTime;
		}
		if (Input.GetKey (KeyCode.S)) {
			transform.position -= transform.forward * Speed * Time.deltaTime;
		}

		if (Input.GetKey(KeyCode.D)) {
			transform.position += transform.right * Speed * Time.deltaTime;
		}
		if (Input.GetKey (KeyCode.A)) {
			transform.position -= transform.right * Speed * Time.deltaTime;
		}
    }

    /// <summary>
    /// カメラの向きを操作する
    /// </summary>
    private void RotateCamera()
    {
        // マウス固定
        Cursor.lockState = CursorLockMode.Locked;

        // マウス位置を取得
        float moveHorizontal = Input.GetAxis("Mouse X");
        float moveVertical = Input.GetAxis("Mouse Y");

        // x,y軸を分けて回転させる．同じにするとジャイロ回転みたいになる
        transform.Rotate(0f, moveHorizontal, 0f);
        Rig.transform.Rotate(-moveVertical, 0f, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        var obj = other.gameObject; // 接触した当たり判定otherから，ゲームオブジェクトを取得する

        if(obj.CompareTag("Pick Up"))
        {
            obj.SetActive(false); // オブジェクトの状態を非表示にする
            _counter++;
            SetCount();
        }
    }

    /// <summary>
    /// カウンターをUIにセットする
    /// </summary>
    private void SetCount()
    {
        var N = 11;
        var txt = $"{_counter}/{N}";
        if(_counter == N)
        {
            txt += "<CLEAR!>";
        }
        _countText.text = txt;
    }
}
