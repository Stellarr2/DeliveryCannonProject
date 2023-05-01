using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Tooltip("Speed at which the player moves")]
    [SerializeField] float speed;
    [Tooltip("How much the player is knocked back when cannon is shot")]
    [SerializeField] float knockbackPower;
    [Tooltip("How long the player is slowed after shooting")]
    [SerializeField] float knockbackTime;

    PackageCannon packageCannon;
    Rigidbody2D playerBody;
    Transform _transform;
    Vector2 movementVector;

    bool knockedBack = false;
    float timer = 0f;

    void Awake()
    {
        _transform = transform;
        playerBody = GetComponent<Rigidbody2D>();
        packageCannon = GetComponent<PackageCannon>();
    }

    void Start()
    {
        packageCannon.OnCannonShoot += PackageCannon_OnCannonShoot;
        packageCannon.OnRocketShoot += PackageCannon_OnRocketShoot;
    }

    void PackageCannon_OnCannonShoot(object sender, PackageCannon.OnCannonShootEventArgs e)
    {
        KnockbackPlayer(e);
    }

    void PackageCannon_OnRocketShoot(object sender, PackageCannon.OnCannonShootEventArgs e)
    {
        RocketKnockbackPlayer(e);
    }

    void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        if(GameManager.Instance.state != GameManager.GameState.Gameplay) {return;}
        movementVector = InputManager.Instance.GetMovementVector();
        if(knockedBack)
        {
            timer += Time.deltaTime;
            if(timer > knockbackTime)
            {
                knockedBack = false;
                timer = 0f;
            }
            _transform.position += new Vector3(movementVector.x, movementVector.y, 0f)*speed/4*Time.deltaTime;
        }
        else
        {
            _transform.position += new Vector3(movementVector.x, movementVector.y, 0f)*speed*Time.deltaTime;

        }
    }

    void KnockbackPlayer(PackageCannon.OnCannonShootEventArgs e)
    {
        playerBody.AddForce(-e.e_cannon.right*knockbackPower, ForceMode2D.Impulse);
        knockedBack = true;
    }

    void RocketKnockbackPlayer(PackageCannon.OnCannonShootEventArgs e)
    {
        playerBody.AddForce(-e.e_cannon.right*knockbackPower, ForceMode2D.Impulse);
    }
}
