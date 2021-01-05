using UnityEngine;
using System.Collections;

public class movement : MonoBehaviour {

    [Header("Locomotion Setting")]
    public float velocity = 6.0F;
    public float impactForce = 5f;

    [Header("Jump Setting")]
    public float jumpForce = 8.0F;
    public float gravity = 20.0F;
    public Transform groundCheck;
    //public float distanciaDoChao = 0.4f;
    public LayerMask groundMask;
    

    public Animator anim;

    AudioSource[] audio = new AudioSource[2];

    public AudioClip andando;
    public AudioClip pulando;


    private void Start()
    {
        audio = GetComponents<AudioSource>();
        audio[0].clip = andando;
        audio[1].clip = pulando;
    }



    //VARIAVEIS PRIVADAS
    private Vector3 moveDirection = Vector3.zero;
    float speed;
    float xRaw;
    float zRaw;
    float x;
    float z;

    Vector3 velocidadeGravidade;
    float camY;
    bool estaNoChao;


    void Update() {
        CharacterController controller = GetComponent<CharacterController>();
        if (controller.isGrounded) {
            
            camY = Camera.main.transform.rotation.eulerAngles.y; //IMPEDIR QUE A CAMERA BUG O EIXO Y

            xRaw = Input.GetAxisRaw("Horizontal");
            zRaw = Input.GetAxisRaw("Vertical");


            x = Input.GetAxis("Horizontal");
            z = Input.GetAxis("Vertical");
            
            speed = velocity; //APLICA UMA FORÇA NA MOVIMENTAÇÃO DO PERSONAGEM 

            if (Input.GetButton("Jump"))
            {
                //Debug.Log("Pulou");
                audio[1].Play();
                moveDirection.y = jumpForce;
                anim.SetBool("Jump", true);
            } 
            else
            {
                anim.SetBool("Jump", false);
            }
        
        }


        //CAMERA

        if(zRaw == 1)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, camY, 0), Time.deltaTime * 5);
        }
        if(zRaw == -1)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, camY - 180, 0), Time.deltaTime * 5);
        }
        if(xRaw == 1)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, camY + 90, 0), Time.deltaTime * 5);
        }
        if(xRaw == -1)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, camY - 90, 0), Time.deltaTime * 5);
        }



        //MOVIMENTAÇÃO 
        Vector3 move = transform.forward;
        

        if(x != 0 && estaNoChao || z != 0 && estaNoChao)
        {
            
            controller.Move(move * speed * Time.deltaTime);
            anim.SetBool("Run", true);
            

        }
        else{
            anim.SetBool("Run", false);
            audio[0].Play();

        }

        estaNoChao = Physics.CheckSphere(groundCheck.position, groundMask);

        if(estaNoChao && velocidadeGravidade.y < 0)
        {
            velocidadeGravidade.y = -2f;
        }

        if(estaNoChao == false)
        {
            if(x != 0 || z != 0)
            {
                controller.Move(transform.forward * speed * Time.deltaTime);
            }
        }
        

        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);

        

    }

    RaycastHit hit;
    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.name.Equals("Parede")) //busca o objeto pelo nome
        {
            if(hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }
        }

        /*if(col.gameObject.name.Equals("Plataforma")) //busca o objeto pelo nome
        {
            this.transform.parent = col.transform;
        }*/
    }

    /*void OnCollisionExit(Collision col) 
    {
        if(col.gameObject.name.Equals("Plataforma"))
        {
            this.transform.parent = null;
        }
    }*/
}
