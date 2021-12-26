using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// アイテムを回転させる
/// </summary>
public class Rotator : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        var vec = new Vector3(15, 30, 45);
        transform.Rotate(vec * Time.deltaTime); // 毎フレームvec分回転させる
    }
}
