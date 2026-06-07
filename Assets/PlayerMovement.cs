using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{

    Rigidbody2D rgbd2d;
    Vector3 movementVector;

    [Header("Statistics: ")]
    [SerializeField] float speed = 3f;

    PlayerAnimations playerAnimations; 

    private void Awake()
    {
        rgbd2d = GetComponent<Rigidbody2D>();
        playerAnimations = GetComponent<PlayerAnimations>();
        movementVector = new Vector3();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        movementVector.x = Input.GetAxisRaw("Horizontal");
        movementVector.y = Input.GetAxisRaw("Vertical");

        playerAnimations.horizontal = movementVector.x;
        playerAnimations.vertical = movementVector.y;

        movementVector *= speed;


        rgbd2d.linearVelocity = movementVector; 
        
    }
    
    
}
