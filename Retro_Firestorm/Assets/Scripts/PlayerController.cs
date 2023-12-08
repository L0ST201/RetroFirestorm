using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class PlayerController : MonoBehaviourPunCallbacks, IDamageable
{
    [SerializeField] private Image healthbarImage;
	[SerializeField] private GameObject reticle;
    [SerializeField] private GameObject ui;
    [SerializeField] private GameObject cameraHolder;
    [SerializeField] private float mouseSensitivity, sprintSpeed, walkSpeed, jumpForce, smoothTime;
    [SerializeField] private Item[] items;

    private int itemIndex;
    private int previousItemIndex = -1;

    private float verticalLookRotation;
    private bool grounded;
    private Vector3 smoothMoveVelocity;
    private Vector3 moveAmount;

    private Rigidbody rb;
    private PhotonView PV;

    private const float maxHealth = 100f;
    private float currentHealth = maxHealth;

    private PlayerManager playerManager;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        PV = GetComponent<PhotonView>();
        playerManager = PhotonView.Find((int)PV.InstantiationData[0]).GetComponent<PlayerManager>();

        if (PV.IsMine)
        {
            LockCursor();
        }
    }

    void Start()
    {
        if (PV.IsMine)
        {
            EquipItem(0);
            InitializeReticle();
        }
        else
        {
            Destroy(GetComponentInChildren<Camera>().gameObject);
            Destroy(rb);
            Destroy(ui);
        }
    }

    void Update()
    {
        if (!PV.IsMine)
            return;

        HandleInput();
        Look();
        Move();

        if (transform.position.y < -10f)
        {
            Die();
        }
    }

    void FixedUpdate()
    {
        if (!PV.IsMine)
            return;

        rb.MovePosition(rb.position + transform.TransformDirection(moveAmount) * Time.fixedDeltaTime);
    }

    void HandleInput()
    {
        Jump();
        CheckForItemSwitch();
        CheckForShooting();
    }

    void CheckForShooting()
    {
        if (Input.GetMouseButtonDown(0))
        {
            items[itemIndex].Use();
        }
    }

    void CheckForItemSwitch()
    {
        int newWeaponIndex = -1;

        if (Input.GetAxisRaw("Mouse ScrollWheel") > 0f)
        {
            newWeaponIndex = itemIndex >= items.Length - 1 ? 0 : itemIndex + 1;
        }
        else if (Input.GetAxisRaw("Mouse ScrollWheel") < 0f)
        {
            newWeaponIndex = itemIndex <= 0 ? items.Length - 1 : itemIndex - 1;
        }

        for (int i = 0; i < items.Length; i++)
        {
            if (Input.GetKeyDown((i + 1).ToString()))
            {
                newWeaponIndex = i;
                break;
            }
        }

        if (newWeaponIndex != -1)
            EquipItem(newWeaponIndex);
    }

    void Look()
    {
        transform.Rotate(Vector3.up * Input.GetAxisRaw("Mouse X") * mouseSensitivity);

        verticalLookRotation += Input.GetAxisRaw("Mouse Y") * mouseSensitivity;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -90f, 90f);

        cameraHolder.transform.localEulerAngles = Vector3.left * verticalLookRotation;
    }

    void Move()
    {
        Vector3 moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;

        moveAmount = Vector3.SmoothDamp(moveAmount, moveDir * (Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed), ref smoothMoveVelocity, smoothTime);
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            rb.AddForce(transform.up * jumpForce);
        }
    }

    void EquipItem(int _index)
    {
        if (_index == previousItemIndex)
            return;

        itemIndex = _index;

        items[itemIndex].GameObject.SetActive(true);

        if (previousItemIndex != -1)
        {
            items[previousItemIndex].GameObject.SetActive(false);
        }

        previousItemIndex = itemIndex;

        if (PV.IsMine)
        {
            Hashtable hash = new Hashtable();
            hash.Add("itemIndex", itemIndex);
            PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
        }
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        if (changedProps.ContainsKey("itemIndex") && !PV.IsMine && targetPlayer == PV.Owner)
        {
            EquipItem((int)changedProps["itemIndex"]);
        }
    }

    public void SetGroundedState(bool _grounded)
    {
        grounded = _grounded;
    }

    public void TakeDamage(float damage, GameObject source)
	{
		if (!PV.IsMine)
			return;

		currentHealth -= damage;
		healthbarImage.fillAmount = currentHealth / maxHealth;

		if (currentHealth <= 0)
		{
			Die();
		}
	}


    public bool IsAlive()
    {
        return currentHealth > 0;
    }

    public void Destroy()
    {
        Die();
    }

    void Die()
    {
        playerManager.Die();
    }

    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

	    private void InitializeReticle()
    {
        if (reticle != null)
        {
            reticle.SetActive(true);
        }
        else
        {
            Debug.LogError("Reticle UI element is not assigned in the Inspector.");
        }
    }

}