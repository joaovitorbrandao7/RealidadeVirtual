using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public Camera playerCamera;
    public GameObject espada; // Referência ao GameObject da espada
    public float walkSpeed = 6f;
    public float runSpeed = 12f;
    public float jumpPower = 7f;
    public float gravity = 10f;
    public float lookSpeed = 2f;
    public float lookXLimit = 45f;
    public float defaultHeight = 2f;
    public float crouchHeight = 1f;
    public float crouchSpeed = 3f;

    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;
    private CharacterController characterController;

    private bool canMove = true;
    private bool isAttacking = false;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }


    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (espada == null)
        {
            Debug.LogError("Espada não atribuída! Certifique-se de vincular a espada no Inspector.");
        }
    }

    void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        // Controle de movimento
        bool isRunning = Input.GetMouseButton(0); // Botão esquerdo do mouse para correr
        float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpPower;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.R) && canMove)
        {
            characterController.height = crouchHeight;
            walkSpeed = crouchSpeed;
            runSpeed = crouchSpeed;

        }
        else
        {
            characterController.height = defaultHeight;
            walkSpeed = 6f;
            runSpeed = 12f;
        }

        characterController.Move(moveDirection * Time.deltaTime);

        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }

        if (Input.GetMouseButtonDown(1) && !isAttacking) // Botão direito do mouse
        {
            StartCoroutine(AnimarEspada());
        }
    }

    IEnumerator AnimarEspada()
    {
        isAttacking = true;


        Quaternion ataqueRotation = Quaternion.Euler(-15.37f, -100f, -90f);

        float tempo = 0.2f; 
        float elapsed = 0f;

        Quaternion inicial = espada.transform.localRotation;
        while (elapsed < tempo)
        {
            espada.transform.localRotation = Quaternion.Lerp(inicial, ataqueRotation, elapsed / tempo);
            elapsed += Time.deltaTime;
            yield return null;
        }

        espada.transform.localRotation = ataqueRotation;

        yield return new WaitForSeconds(0.1f);
        audioManager.PlaySFX(audioManager.espada);

        elapsed = 0f;
        while (elapsed < tempo)
        {
            espada.transform.localRotation = Quaternion.Lerp(espada.transform.localRotation, inicial, elapsed / tempo);
            elapsed += Time.deltaTime;
            yield return null;
        }

        espada.transform.localRotation = inicial;
        isAttacking = false;
    }


}