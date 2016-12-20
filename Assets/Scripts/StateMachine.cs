using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public enum SWORDSTATE{

	ON_IDLE,
	ON_MOVE,
	ON_ATTACK

}

public class StateMachine : MonoBehaviour {

	public GameObject player;
	public GameObject enemy;

	private FSMSystem fsm;

	public void SetTransition(Transition t) { fsm.PerformTransition(t); }

	public void StartPlayerAI(){

		MakeFSM();
		StartCoroutine(UpdateState());
	}

	public void EndPlayerAI(){

		Debug.Log("Stopped Player AI");
//		StopCoroutine(UpdateState());
		StopAllCoroutines();
	}

	IEnumerator UpdateState(){

		for(;;){
//			bool onTransition = false;

			fsm.CurrentState.Reason(player, enemy);
			fsm.CurrentState.Act(player, enemy);
	
			yield return new WaitForEndOfFrame();
//			yield return new WaitForSeconds(1);
		}
	}
	
	public void MakeFSM(){

		IdleState idle = new IdleState();
		idle.AddTransition(Transition.SawPlayer, StateID.AttackingState);
		idle.AddTransition(Transition.LostPlayer, StateID.MovingState);

		MoveState move = new MoveState();
		move.AddTransition(Transition.SawPlayer, StateID.AttackingState);

		AttackState attack = new AttackState();
		attack.AddTransition(Transition.LostPlayer, StateID.MovingState);
		attack.AddTransition(Transition.IdleTransition, StateID.IdleStateID);

		DefendState defend = new DefendState();

		EvadeState evade = new EvadeState();

		WinState win = new WinState();

		DeathState death = new DeathState();





		fsm = new FSMSystem();
		fsm.AddState(move);
		fsm.AddState(attack);
		fsm.AddState(idle);
		fsm.AddState(defend);
		fsm.AddState(evade);
		fsm.AddState(win);
		fsm.AddState(death);
	}

}

public class IdleState : FSMState {

	public IdleState(){

		stateID = StateID.IdleStateID;
	}

	public override void Reason(GameObject player, GameObject enemy){

	}

	public override void Act(GameObject player, GameObject enemy){

		Debug.Log("On Idle");

	}

}

public class MoveState : FSMState {

	public float speed = 0.75f;

	public MoveState(){

		stateID = StateID.MovingState;
	}

	public override void Reason(GameObject player, GameObject enemy){

		//Attack range proximity
		if(enemy != null){

			Vector2 playerPos = new Vector2(player.transform.position.x + 0.5f, player.transform.position.y);
			Vector2 enemyPos = new Vector2(enemy.transform.position.x, enemy.transform.position.y);

			if(enemyPos.x < playerPos.x){


				//SwordAnimator.instance.Idle();
				//SwordClass.Instance.Attack();
				Debug.Log ("Transition to AttackState");

				player.GetComponent<StateMachine>().SetTransition(Transition.SawPlayer);
			}
		}else{
//			Debug.Log ("No enemies");
		}

	}

	public override void Act(GameObject player, GameObject enemy){

		//Logic
//		Debug.Log ("Moving");
		player.transform.Translate(new Vector3(speed,0,0)*Time.deltaTime);

		//Animation
		//SwordAnimator.instance.Move();

	}

}

public class AttackState : FSMState {

	public AttackState(){

		stateID = StateID.AttackingState;
	}

	public override void Reason(GameObject player, GameObject enemy){

		if(enemy != null){
//			if(Vector3.Distance(enemy.transform.position, player.transform.position) >= 1f){
//
//			
//
//			SwordClass.Instance.RemoveEnemy();
//			SwordAnimator.instance.Idle();
//
//		
//			}
			Debug.Log ("AttackState Reason: Stil has enemy");
		}else{
			Debug.Log ("Transition to MoveState");
			//SwordClass.Instance.DisableAttack();
			player.GetComponent<StateMachine>().SetTransition(Transition.LostPlayer);

		}
	}

	public override void Act(GameObject player, GameObject enemy){
		Debug.Log ("Acting on AttackState");

		//Logic

		//Animation
		//SwordAnimator.instance.StrongAttack();
	}
}

public class DefendState : FSMState {

	public DefendState(){

		stateID = StateID.DefendStateID;
	}

	public override void Reason (GameObject player, GameObject enemy){

	}

	public override void Act (GameObject player, GameObject enemy){

	}

}

public class EvadeState : FSMState {
	
	public EvadeState(){
		
		stateID = StateID.EvadeStateID;
	}
	
	public override void Reason (GameObject player, GameObject enemy){
		
	}
	
	public override void Act (GameObject player, GameObject enemy){
		
	}
	
}

public class DeathState : FSMState {
	
	public DeathState(){
		
		stateID = StateID.DeathStateID;
	}
	
	public override void Reason (GameObject player, GameObject enemy){
		
	}
	
	public override void Act (GameObject player, GameObject enemy){
		
	}
	
}

public class WinState : FSMState {
	
	public WinState(){
		
		stateID = StateID.WinStateID;
	}
	
	public override void Reason (GameObject player, GameObject enemy){
		
	}
	
	public override void Act (GameObject player, GameObject enemy){
		
	}
	
}
