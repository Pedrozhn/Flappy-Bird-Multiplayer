using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviourPun
{
    const float jumpForce = 8;
    Rigidbody2D rigidbody2D;

    bool controllerOn = true;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    [PunRPC]
    private void Initialize()
    {
        if (!photonView.IsMine)
        {
            rigidbody2D.isKinematic = true;
            controllerOn = false;
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0) && controllerOn)
        {
            rigidbody2D.velocity = Vector3.zero;
            rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (controllerOn)
        {
            if (collision.gameObject.tag == "Obstacle")
            {
                //GameOver();
                GameManager.instance.photonView.RPC("SetScore", RpcTarget.All, -10);
            }
            else if (collision.gameObject.tag == "Score")
            {
                GameManager.instance.photonView.RPC("SetScore", RpcTarget.All, 1);
            }
        }
    }

    void GameOver()
    {
        
        if(PlayerPrefs.GetInt("Record") < GameManager.instance.Score)
        {
            PlayerPrefs.SetInt("Record", GameManager.instance.Score);
        }
        UIManager.instance.GameOver();
        
    }
}
