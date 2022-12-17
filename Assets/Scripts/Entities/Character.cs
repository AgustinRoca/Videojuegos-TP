using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovementController))]
[RequireComponent(typeof(CharacterLifeController))]
[RequireComponent(typeof(SoundEffectController))]
[RequireComponent(typeof(AudioSource))]

public class Character : Actor
{
    private MovementController _movementController;
	public CharacterLifeController _lifeController;
    public WeaponController _weaponController;
    [SerializeField] private SoundEffectController _walkSoundFxController;
    [SerializeField] private AudioClip _walkingClip;
	

    // BINDING COMBAT KEYS
    [SerializeField] private KeyCode _shoot = KeyCode.Mouse0;
    [SerializeField] private KeyCode _weaponSlot1 = KeyCode.Alpha1;
    [SerializeField] private KeyCode _weaponSlot2 = KeyCode.Alpha2;
    [SerializeField] private KeyCode _weaponSlot3 = KeyCode.Alpha3;

    [SerializeField] private KeyCode _activateInvincibility = KeyCode.I;

    // BINDING MOVEMENT KEYS
    [SerializeField] private KeyCode _moveForward = KeyCode.W;
    [SerializeField] private KeyCode _moveBack = KeyCode.S;
    [SerializeField] private KeyCode _moveLeft = KeyCode.A;
    [SerializeField] private KeyCode _moveRight = KeyCode.D;
    public float rotationSpeed;

    private Animator animator;

    /* Commands */
    private CmdMovement _cmdMoveForward;
    private CmdMovement _cmdMoveBackwards;
    private CmdMovement _cmdMoveLeft;
    private CmdMovement _cmdMoveRight;
    private CmdInvincibility _cmdInvincibility;



    void Start()
    {
	    animator = GetComponent<Animator>();
	    animator.SetBool("playerIsMoving", false);
	    
        _movementController = GetComponent<MovementController>();
		_lifeController = GetComponent<CharacterLifeController>();
        _weaponController = GetComponent<WeaponController>();

        _weaponController.addWeapon(GameObject.Find("Gun"), WeaponFactory.eWeapon.Pistol);

        _cmdMoveForward = new CmdMovement(_movementController, new Vector3(-1,0,0));
        _cmdMoveBackwards = new CmdMovement(_movementController, new Vector3(1,0,0));
        _cmdMoveLeft = new CmdMovement(_movementController, new Vector3(0,0,-1));
        _cmdMoveRight = new CmdMovement(_movementController, new Vector3(0,0,1));
        _cmdInvincibility = new CmdInvincibility(_lifeController);
    }

    // Update is called once per frame
    void Update()
    {
	    LookAtMouse();

	    var playerIsMoving = false;
	    if (Input.GetKey(_moveForward))
	    {
		    playerIsMoving = true;
		    EventQueueManager.instance.AddMovementCommand(_cmdMoveForward);
	    }
	    if (Input.GetKey(_moveBack))
	    {
		    playerIsMoving = true;
		    EventQueueManager.instance.AddMovementCommand(_cmdMoveBackwards);
	    }
	    if (Input.GetKey(_moveRight))
	    {
		    playerIsMoving = true;
		    EventQueueManager.instance.AddMovementCommand(_cmdMoveRight);
	    }
	    if (Input.GetKey(_moveLeft))
	    {
		    playerIsMoving = true;
		    EventQueueManager.instance.AddMovementCommand(_cmdMoveLeft);
	    }
	    
        if (playerIsMoving)
	    {
		    animator.SetBool("playerIsMoving", true);
			if(!_walkSoundFxController.IsPlaying()){
				_walkSoundFxController.Play(_walkingClip, true, 0.5F);
			}
	    }
	    else
	    {
		    animator.SetBool("playerIsMoving", false);
			if(_walkSoundFxController.IsPlaying()){
				_walkSoundFxController.Stop();
			}
	    }

        if (Input.GetKey(_shoot)) {
			_weaponController.attack();
		}

        if (Input.GetKeyDown(_activateInvincibility))
        {
	        EventQueueManager.instance.AddCommand(_cmdInvincibility);
        }

        if (Input.GetKeyDown(_weaponSlot1)) _weaponController.changeWeapon(0);
        if (Input.GetKeyDown(_weaponSlot2)) _weaponController.changeWeapon(1);
        if (Input.GetKeyDown(_weaponSlot3)) _weaponController.changeWeapon(2);
    }

    private void LookAtMouse()
    {
	    Plane playerPlane = new Plane(Vector3.up, transform.position);
	    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
	    float hitDist;
	    if (playerPlane.Raycast(ray, out hitDist))
	    {
		    Vector3 targetPoint = ray.GetPoint(hitDist);
		    Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
		    transform.rotation = targetRotation;//Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
	    }
    }

    public void addWeapon(WeaponFactory.eWeapon gun){
        _weaponController.addWeapon(GameObject.Find("Gun"), gun);
    }
}
