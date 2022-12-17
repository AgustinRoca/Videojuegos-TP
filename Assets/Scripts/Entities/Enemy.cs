using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(SoundEffectController))]

public class Enemy : Actor
{
    enum EnemyBehaviourStates
    {
        chasing,
        leavingPlayerAlone,
    }
    
    public EnemyStats EnemyStats;
    private NavMeshAgent navMeshAgent;               //  Nav mesh agent component
    private float speedRun;                       //  Running speed
    private Vector3 playerLastPosition;                       //  Last position of the player

    private EnemyBehaviourStates state;
    private float timeLeavingThePlayerAlone = 3;  // tres segundos
    public Animator animator;
	private SoundEffectController _soundFxController;
    [SerializeField] private List<AudioClip> _zombieClips = new List<AudioClip>();
    
    void Start()
    {
		_soundFxController = GetComponent<SoundEffectController>();
        animator = GetComponent<Animator>();
        animator.SetBool("nearPlayer", false);
        animator.SetBool("Dead", false);
        state = EnemyBehaviourStates.chasing;
        speedRun = EnemyStats.MovementSpeed;
        playerLastPosition = GameObject.Find("Character").transform.position;
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.isStopped = false;
        navMeshAgent.speed = speedRun;             //  Set the navemesh speed with the normal speed of the enemy
        navMeshAgent.SetDestination(playerLastPosition);    //  Set the destination to the first waypoint
    }
 
    private void Update()
    {
        if(!navMeshAgent.isStopped){
            if (state == EnemyBehaviourStates.chasing)
            {
                var playerObject = GameObject.Find("Character");
                if(playerObject != null){
                    playerLastPosition = playerObject.transform.position;
                    
                    if (Vector3.Distance(transform.position, playerLastPosition) <= EnemyStats.AttackRange)
                    {
                        animator.SetBool("nearPlayer", true);
                    }
                    else
                    {
                        animator.SetBool("nearPlayer", false);
                    }
                    
                    if (Vector3.Distance(transform.position, playerLastPosition) <= EnemyStats.TouchRange)
                    {
                        // Enemy touched player
                        var characterLifeController =
                            playerObject.GetComponent(typeof(CharacterLifeController)) as CharacterLifeController;
                        characterLifeController.TakeDamage(1);
                        EventManager.instance.EnemyTouchedPlayer();
                    }
                    else
                    {
                        Chasing();
                    }
                }
            }
            else if(state == EnemyBehaviourStates.leavingPlayerAlone)
            {
                // Solo estaremos en este estado por un tiempo corto para darle al player oportunidad de recuperarse
                timeLeavingThePlayerAlone -= Time.deltaTime;
                if (timeLeavingThePlayerAlone <= 0)
                {
                    timeLeavingThePlayerAlone = 1;
                    state = EnemyBehaviourStates.chasing;
                }
            }
        }
    }
 
    private void Chasing()
    {
        Move(speedRun);
        navMeshAgent.SetDestination(playerLastPosition);          //  set the destination of the enemy to the player location
    }
    
    void Move(float speed)
    {
        navMeshAgent.speed = speed;
		if(!_soundFxController.IsPlaying()){
			_soundFxController.Play(_zombieClips[Random.Range(0, _zombieClips.Count)], false, 0.5F);
		}
    }

    public void Push(Vector3 direction, float force)
    {
        gameObject.GetComponent<Rigidbody>().AddForce(direction * force, ForceMode.Impulse);
    }
    
    public void Stop()
    {
        navMeshAgent.isStopped = true;
        navMeshAgent.speed = 0;
		if(_soundFxController.IsPlaying()){
			_soundFxController.Stop();
		}
    }

    public void LeavePlayerAloneForAMoment()
    {
        state = EnemyBehaviourStates.leavingPlayerAlone;
    }

    public void BuffSpeed(){
        speedRun = EnemyStats.MovementSpeed + 5;
        if(navMeshAgent != null){
            navMeshAgent.speed = speedRun;  
        }
    }

    public void DebuffSpeed(){
        speedRun = EnemyStats.MovementSpeed;
        if(navMeshAgent != null){
            navMeshAgent.speed = speedRun;  
        }  
    }
}