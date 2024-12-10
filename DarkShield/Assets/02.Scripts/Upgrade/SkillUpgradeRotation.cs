using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillUpgradeRotation : MonoBehaviour
{
    private GameObject _yellow;
    private GameObject _black;
    private GameObject _bigYellow;

    public float yellowRotSpeed;
    public float blackRotSpeed;
    public float bigYellowRotSpeed;

    private void Start()
    {
        _yellow = GameObject.Find("yellow");
        _black = GameObject.Find("black");
        _bigYellow = GameObject.Find("BigYellow");
    }

    private void Update()
    {
        _yellow.transform.Rotate(new Vector3(0, 10, 0) * yellowRotSpeed * Time.deltaTime);
        _black.transform.Rotate(new Vector3(0, 10, 0) * blackRotSpeed * Time.deltaTime);
        _bigYellow.transform.Rotate(new Vector3(0, 10, 0) * bigYellowRotSpeed * Time.deltaTime);
    }
}
