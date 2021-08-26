using System.Numerics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //fuerza de salto
    public float JumpForce = 5f;
    //rigidbody del personaje que usare para saltar
    private Rigidbody2D rigidBody;
    //variable para detectar la capa del suelo
    public LayerMask GroundLayer;
    //variable para manejar las animaciones
    public Animator AnimatorPlayer;
    //fuerza que aplico para caminar
    public float WalkSpeed = 1.5f;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    private void Start()
    {
        AnimatorPlayer.SetBool("IsGrounded",true);
    }
    // Update is called once per frame
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            //aqi el usuario acaba de pulsar la tecla espacio
            Jump();
        }
        AnimatorPlayer.SetBool("IsGrounded", IsTouchingTheGround());
    }
    private void FixedUpdate()
    {
        //la velocidad en el eje x ira cambiando respetando la velocidad maxima
        //la velocidad en el eje y sera la que acumule al caminar
        if (Input.GetKey(KeyCode.D))
        {
            if (rigidBody.velocity.x < WalkSpeed)
            {
                rigidBody.velocity = new UnityEngine.Vector2(WalkSpeed, rigidBody.velocity.y);
                transform.localScale = new UnityEngine.Vector2(1, 1);
            }
        }
        if (Input.GetKey(KeyCode.A))
        {
            if (rigidBody.velocity.x > -WalkSpeed)
            {
                rigidBody.velocity = new UnityEngine.Vector2(-WalkSpeed, rigidBody.velocity.y);
                transform.localScale = new UnityEngine.Vector2(-1,1);
            }
        }
    }

    private void Jump()
    {
        //f = m * a

        if (IsTouchingTheGround() == true) 
        {

            rigidBody.AddForce(UnityEngine.Vector2.up * JumpForce, ForceMode2D.Impulse);
        }
    }

    //metodo para validar si el personaje toca el suelo o no
    bool IsTouchingTheGround() 
    {
        //trazare un raycast hacia abajo para veirificar si toco el suelo hasta un maximo de 20cm
        if (Physics2D.Raycast(this.transform.position, UnityEngine.Vector2.down, 0.8f, GroundLayer))
        {
            return true;
        }
        else 
        {
            return false;
        }
    }
}