using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody rb;
    private BoxCollider boxCollider;
    private Animator amin;

    // Variables used to move sideways
    private int currentLane = 1;
    private Vector3 verticalTargetPosition;
    private int targetLane;

    // Variables used for the jump
    public float jumpLength;
    public float jumpHeight;

    private bool jumping = false;
    private float jumpStart;

    // Variables used to slip
    public float slideLength;

    private bool sliding = false;
    private float slideStart;
    private Vector3 boxColliderSize;
    private Vector3 boxColliderCenter;

    // Floor Variables
    public float speed;
    public float laneSpeed;
    public float minSpeed = 10f;
    public float maxSpeed = 40f;

    // Variables for the TouchScreen
    private bool isSwipping = false;
    private Vector2 startingTouch;

    // Life Variables
    public int maxLife = 2;
    public float lifeTime;
    public GameObject model;

    private int currentLife;

    //UI Variables
    private UIManager UIManager;
    private int coins = 0;

    //Variaveis de pontuação
    private float score = 0;

    //Variaveis de olho
    public double eye;

    private bool sleping = false;
    private bool toAwake = false;

    //Variavesi do livro
    public float BookLength;

    private bool book = false;
    private float bookStart;

    //Variaveis do CoinMagnet
    public bool magnet = false;
    public float magnetLength;

    private float magnetStart;

    //Variaveis do Valor da moeda
    public float valueCoinsLength;

    private bool valueCoins = false;
    private float valueCoinsStart;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Sets the animation
        amin = GetComponentInChildren<Animator>();
        amin.Play("runStart");

        // save the value of the center and size of the boxCollider
        boxCollider = GetComponent<BoxCollider>();
        boxColliderSize = boxCollider.size;
        boxColliderCenter = boxCollider.center;
        
        // Run
        speed = minSpeed;

        // Life
        currentLife = maxLife;

        //UI
        UIManager = FindObjectOfType<UIManager>();                
    }

    // Update is called once per frame
    void Update()
    {
        // Condition for movement
        // Left
        if (Input.GetKeyDown(KeyCode.LeftArrow) /*&& !jumping && !sliding*/ )
        {
            ChangeLane(-1);
        }
        // Right
        else if (Input.GetKeyDown(KeyCode.RightArrow) /*&& !jumping && !sliding*/)
        {
            ChangeLane(1);
        }
        // Jump
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            //FastSlide
            if (sliding)
            {
                Accelerator(2);
            }
            else
            {
                Jump();
            }
        }
        // Slide 
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            // FastJump 
            if (jumping)
            {
                Accelerator(1);
            }
            else
            {
                Slide();
            }
        } // END Condition for movement

        // Check touch screen count
        if (Input.touchCount == 1)
        {
            if (isSwipping)
            {
                Vector2 diff = Input.GetTouch(0).position - startingTouch;
                diff = new Vector2(diff.x / Screen.width, diff.y / Screen.width);

                if (diff.magnitude > 0.01f)
                {
                    // Direction check
                    if (Mathf.Abs(diff.y) > Mathf.Abs(diff.x))
                    {
                        if (diff.y < 0)
                        {

                            //FastJump
                            if (jumping)
                            {
                                Accelerator(1);
                            }
                            else
                            {
                                Slide();
                            }
                        }
                        else
                        {
                            //FastSlide
                            if (sliding)
                            {
                                Accelerator(2);
                            }
                            else
                            {
                                Jump();
                            }
                        } // END IF ELSE JUMP OR SLIDE
                    }
                    else
                    {
                        if (diff.x < 0)
                        {
                            ChangeLane(-1);
                        }
                        else
                        {
                            ChangeLane(1);
                        } // END IF ELSE LEFT OR RIGHT
                    } // END Direction check
                }
            } // END IF isSwipping

            // Touch Screen Verification
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                startingTouch = Input.GetTouch(0).position;
                isSwipping = true;
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                isSwipping = false;
            } // END Touch Screen Verification
        } // END Check touch screen count

        // Condition to stop the jump
        if (jumping)
        {
            float ratio = (transform.position.z - jumpStart) / jumpLength;

            if (ratio >= 1f && !sliding)
            {
                jumping = false;
                amin.SetBool("Jumping", false);
            }
            else
            {
                verticalTargetPosition.y = Mathf.Sin(ratio * Mathf.PI) * jumpHeight;
            } // END IF E ELSE
        }
        else
        {
            verticalTargetPosition.y = Mathf.MoveTowards(verticalTargetPosition.y, 0, 5 * Time.deltaTime);
        }// END IF JUMPING

        // Condition to stop the slide
        if (sliding)
        {
            float ratio = (transform.position.z - slideStart) / slideLength;

            if (ratio >= 1f)
            {
                sliding = false;
                boxCollider.size = boxColliderSize;
                boxCollider.center = boxColliderCenter;
                amin.SetBool("Sliding", false);
            }
        } // END IF SLIDING

        // movimentação
        Vector3 targetPosition = new Vector3(verticalTargetPosition.x, verticalTargetPosition.y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, laneSpeed * Time.deltaTime);

        //atualização de pontuação e do olho
        if (book)
        {
            float ratio = (transform.position.z - bookStart) / BookLength;

            if (ratio >= 2f)
            {
                book = false;
            }
            else
            {
                score += Time.deltaTime * speed * 8;
                UIManager.UpdateScore((int)score);
                UIManager.PowerUpText.SetActive(true);

                //atualização do olho
                eye -= Time.deltaTime * 1.1;
            }
        }
        else
        {
            score += Time.deltaTime * speed;
            UIManager.UpdateScore((int)score);
            UIManager.PowerUpText.SetActive(false);

            //atualização do olho
            eye -= Time.deltaTime;
        } // END IF E ELSE BOOK

        Eye((int)eye);
        UIManager.UpdateEye((int)eye);

        //atualização do valor da moeda
        if (valueCoins)
        {
            float ratio = (transform.position.z - valueCoinsStart) / valueCoinsLength;

            if (ratio >= 1f)
            {
                valueCoins = false;
                UIManager.CoinAText.gameObject.SetActive(false);
                UIManager.CoinBText.gameObject.SetActive(false);
                UIManager.CoinCText.gameObject.SetActive(false);
                UIManager.CoinDText.gameObject.SetActive(false);
                UIManager.CoinEText.gameObject.SetActive(false);
            }
        }
        

    } //END Update

    // Update is called in a fixed time
    private void FixedUpdate()
    {
        rb.velocity = Vector3.forward * speed;
        amin.SetBool("Moving", true);
    }

    // Function to change Lane
    void ChangeLane(int direction)
    {
        targetLane = currentLane + direction;

        // Verifica se a Lane alvo é as paredes
        if (targetLane < 0 || targetLane > 2)
        {
            currentLife--;
            amin.SetTrigger("Hit");

            speed = 0;
            if (currentLife <= 0)
            {
                GameOver();
            }
            else
            {
                StartCoroutine(Blinking(lifeTime));
            }
            return;
        }

        //transforma a Lane atual na Lane alvo
        currentLane = targetLane;
        verticalTargetPosition = new Vector3((currentLane - 1), 0, 0);

    } // END ChangeLane

    // Função para pular
    void Jump()
    {
        // verifica se está pulando/escorregando ou não
        if (!jumping && !sliding)
        {
            jumping = true;
            jumpStart = transform.position.z;

            //Ativa a animação
            amin.SetFloat("JumpSpeed", speed / jumpLength);
            amin.SetBool("Jumping", true);
        }

    } // END JUMP

    //Função para escorregar
    void Slide()
    {
        // verifica se está pulando/escorregando ou não
        if (!jumping && !sliding)
        {
            sliding = true;
            slideStart = transform.position.z;

            //Modifica o tamanho do boxCollider
            Vector3 newSize = boxCollider.size;
            newSize.y = newSize.y / 2;
            boxCollider.size = newSize;

            //Modifica o centro do box Collider
            Vector3 newCenter = boxCollider.center;
            newCenter.y = newCenter.y / 2;
            boxCollider.center = newCenter;

            //Ativa a animãção
            amin.SetFloat("JumpSpeed", speed / slideLength);
            amin.SetBool("Sliding", true);
        }
    } // END SLIDE

    //Função para acelerar as reações
    void Accelerator(int reation) {
        if (reation == 1) {
            jumping = false;
            amin.SetBool("Jumping", false);
            Slide();
        }
        else if (reation == 2) {
            sliding = false;
            amin.SetBool("Sliding", false);
            Jump();
        }
    
    }

    //Função para colisão
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle2"))
        {
            speed = 0;
            amin.SetTrigger("Hit");
            currentLife--;
            other.transform.gameObject.SetActive(false);

            if (currentLife <= 0)
            {
                GameOver();
            }
            else
            {
                StartCoroutine(Blinking(lifeTime));
            }
        }
        else if (other.CompareTag("Obstacle1"))
        {
            speed = 0;
            currentLife = currentLife - 2;
            GameOver();
        }
        else if (!other.CompareTag("Track"))
        {

            other.transform.parent.gameObject.SetActive(false);
            if (other.CompareTag("Book"))
            {
                book = true;
                bookStart = transform.position.z;

            }
            else if (other.CompareTag("Friend"))
            {
                magnet = true;
                magnetStart = transform.position.z;

            }

            if (other.CompareTag("Coffee"))
            {
                eye += 5;
                if (eye >= 30) {
                    eye = 30;
                }
                speed += 2;

            }

            //Colisão para coletar a moeda
            if (other.CompareTag("CoinA"))
            {
                ColliderCoin(10);

            }
            else if (other.CompareTag("CoinB"))
            {
                ColliderCoin(8);

            }
            else if (other.CompareTag("CoinC"))
            {
                ColliderCoin(6);
            }
            else if (other.CompareTag("CoinD"))
            {
                ColliderCoin(12);
            }
            else if (other.CompareTag("CoinE"))
            {
                ColliderCoin(20);
            }
        }

         //END COLISAO OBSTACULO
        
    } //END ONTRIGGERENTER

    IEnumerator Blinking(float time)
    {
        float timer = 0;
        float currentBlink = 1f;
        float lastBlink = 0;
        float blinkPeriod = 0.1f;
        bool enabled = false; 

        yield return new WaitForSeconds(1f);

        speed = minSpeed;

        while (timer < time)
        {
            model.SetActive(enabled);
            yield return null;
            timer += Time.deltaTime;
            lastBlink += Time.deltaTime;

            if (blinkPeriod < lastBlink)
            {
                lastBlink = 0;
                currentBlink = 1f - currentBlink;
                enabled = !enabled;
            }
        }
        
        model.SetActive(true);
        currentLife++;
    } //END BLINKING

    //função de fim de jogo
    public void GameOver() {
        //FIM DE JOGO
        speed = 0;
        eye = 0;
        amin.SetBool("Dead", true);
        UIManager.GameOverPanel.SetActive(true);
        Invoke("CallMenu", 2f);
    }

    //Função para abrir o menu
    void CallMenu() {
       GameManager.gm.EndRun();
   }

    //função para aumentar a velocidade
    public void Speed() {
       speed *= 1.1f;
       if (speed >= maxSpeed) {
           speed = maxSpeed;
       }
   }

    //Função para atualizar o oljo
    void Eye(int eye) {

        if (eye == 0)
        {
            GameOver();
        }
        else if (eye > 0 && eye <= 10)
        {
            sleping = true;
            speed -= Time.deltaTime * 0.1f;

            if (speed <= 0 || eye <= 0)
            {
                eye = 0;
                speed = 0;
            }

        }
        else if (eye > 10 && eye <= 20)
        {
            if(sleping){
                laneSpeed = minSpeed;
                sleping = false;
            }
            else if (toAwake) {
                laneSpeed = maxSpeed;
                toAwake = false;
            }                      
        }
        else if (eye > 20)
        {
            toAwake = true;
            speed += Time.deltaTime * 0.1f;
        }

    }

    //Função para o valor
    void  ColliderCoin(int value) {
        valueCoins = true;
        valueCoinsStart = transform.position.z;
        UIManager.UpdateValueCoins(value);
        
        if (value < 12)
        {
            coins = coins + value;
        }
        else {
            coins = coins - value;
            if (coins <= 0){
                coins = 0;
            }
        
        }
        UIManager.UpdateCoins(coins);
    }
}