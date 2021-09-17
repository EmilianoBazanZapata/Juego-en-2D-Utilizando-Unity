using System.Diagnostics;
using System.Numerics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //fuerza de salto
    public float JumpForce = 5f;
    public float SuperJumpForce = 8f;
    //rigidbody del personaje que usare para saltar
    private Rigidbody2D rigidBody;
    //variable para detectar la capa del suelo
    public LayerMask GroundLayer;
    //variable para manejar las animaciones
    public Animator AnimatorPlayer;
    //fuerza que aplico para caminar
    public float WalkSpeed = 4.0f;
    //referencia a una instancia unica de la clase
    public static PlayerController SharedInstance;
    //variable para almacenar la posicion del personaje
    private UnityEngine.Vector3 StartPosition;

    private int HealtPoints, ManaPoints;

    //variables constantes
    private const int INITIAL_HEALT = 100, INITIAL_MANA = 25, MAX_HEALTH = 100, MAX_MANA = 100, MIN_HEALTH = 15;
    private const float MIN_SPEED = 2.5f, HEALT_TIME_DECRASE = 3.5f;

    private void Awake()
    {
        SharedInstance = this;
        rigidBody = GetComponent<Rigidbody2D>();
        //posicion inicial del personaje
        StartPosition = this.transform.position;
    }

    public void StartGame()
    {
        AnimatorPlayer.SetBool("IsGrounded", true);
        //asigno la posicion inicial del personaje cada vez que reinciamos el juego
        this.transform.position = StartPosition;
        HealtPoints = INITIAL_HEALT;
        ManaPoints = INITIAL_MANA;
        StartCoroutine("TirePlayer");
    }
    //corrutina para restar vida al jugador
    IEnumerator TirePlayer()
    {
        while (this.HealtPoints > MIN_HEALTH)
        {
            this.HealtPoints--;
            yield return new WaitForSeconds(HEALT_TIME_DECRASE);
        }
        yield return null;
    }

    // Update is called once per frame
    private void Update()
    {
        //Solo se podra saltar si el Modo de Juego es InGame
        if (GameManager.SharedInstance.CurrentGameState == GameState.InGame)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //aqi el usuario acaba de pulsar la tecla espacio
                Jump();
            }
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                //aqi el usuario acaba de pulsar la tecla espacio
                SuperJump();
            }
            AnimatorPlayer.SetBool("IsGrounded", IsTouchingTheGround());
        }
    }

    private void FixedUpdate()
    {
        //la velocidad en el eje x ira cambiando respetando la velocidad maxima
        //la velocidad en el eje y sera la que acumule al caminar
        //Solo se podra mover si el Modo de Juego es InGame
        if (GameManager.SharedInstance.CurrentGameState == GameState.InGame)
        {
            float CurrentSpeed = (WalkSpeed - MIN_SPEED) * this.HealtPoints / 100.0f;
            //UnityEngine.Debug.Log(CurrentSpeed);
            if (Input.GetButton("HorizontalPositive"))
            {
                if (rigidBody.velocity.x < CurrentSpeed)
                {

                    rigidBody.velocity = new UnityEngine.Vector2(CurrentSpeed, rigidBody.velocity.y);
                    transform.localScale = new UnityEngine.Vector2(1, 1);
                }
            }
            if (Input.GetButton("HorizontalNegative"))
            {
                if (rigidBody.velocity.x > -CurrentSpeed)
                {
                    rigidBody.velocity = new UnityEngine.Vector2(-CurrentSpeed, rigidBody.velocity.y);
                    transform.localScale = new UnityEngine.Vector2(-1, 1);
                }
            }
            SuperRun();
        }
    }
    private void SuperRun()
    {
        if (Input.GetButton("SuperRun"))
        {
            this.WalkSpeed = 6.0f;
        }
        else
        {
            this.WalkSpeed = 4.0f;
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
    private void SuperJump()
    {
        //f = m * a

        if (IsTouchingTheGround() == true)
        {

            rigidBody.AddForce(UnityEngine.Vector2.up * SuperJumpForce, ForceMode2D.Impulse);
        }
    }

    //metodo para validar si el personaje toca el suelo o no
    bool IsTouchingTheGround()
    {
        //trazare un raycast hacia abajo para veirificar si toco el suelo hasta un maximo de 80cm
        if (Physics2D.Raycast(this.transform.position, UnityEngine.Vector2.down, 0.8f, GroundLayer))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    //metodo que se usara para matar al jugador
    public void Kill()
    {
        GameManager.SharedInstance.GameOver();
        //almacenare la distancia maxima recorrida por primera vez que en este caso de 0 
        float CurrentMaxDistance = PlayerPrefs.GetFloat("MaxDistance", 0);
        if (CurrentMaxDistance < this.GetDistance())
        {
            //si la distancia al morir es mayor que 0 tenemos un nuevo record
            PlayerPrefs.SetFloat("MaxDistance", this.GetDistance());
        }
        StopCoroutine("TirePlayer");
    }
    //metodo para calcular la distancia recorrida
    public float GetDistance()
    {                                                         //inicio
        float TraveledDistance = UnityEngine.Vector2.Distance(new UnityEngine.Vector2(StartPosition.x, 0),
                                                              //final
                                                              new UnityEngine.Vector2(this.transform.position.x, 0));
        return TraveledDistance;//this.transfomr.x - startposition.x
    }
    //metodo para aumentar la vida
    public void CollectHealt(int points)
    {
        this.HealtPoints += points;
        if (HealtPoints >= MAX_HEALTH)
        {
            this.HealtPoints = MAX_HEALTH;
        }

    }
    //metodo para aumentar el mana
    public void CollectMana(int points)
    {
        this.ManaPoints += points;
        if (this.ManaPoints >= MAX_MANA)
        {
            this.ManaPoints = MAX_MANA;
        }
    }

    //metodo para retornar el valor de vida
    public int GetHealth()
    {
        return this.HealtPoints;
    }
    //metodo para retornar el valor de mana inicial
    public int GetMana()
    {
        return this.ManaPoints;
    }
}