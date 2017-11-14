﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manage all the player mouvement (walk, run, jump, wall jump etc.)
/// </summary>
public class PlayerMovement : MonoBehaviour {
	// Player's component
	private Rigidbody2D rigidBody2D;
	private Animator animator;
	private SpriteRenderer spriteRenderer;

	// player's params
	public float walkingSpeed = 4f;
	public float runningSpeed = 9.0f;

	// private
	private Vector3 velocity;
	private Vector2 directionalInput;
	private bool isFlippedRight = true;
	private float speed = 0.0f;

	void Awake() {
		rigidBody2D = (Rigidbody2D) GetComponent(typeof(Rigidbody2D));
		animator = (Animator) GetComponent(typeof(Animator));
		spriteRenderer = (SpriteRenderer) GetComponent(typeof(SpriteRenderer));
	}

	void Update() {
		CalculVelocity(speed);
		SetSpriteRendererDirection();
	}

	public void Idle() {
		animator.SetBool("isIdle", true);
		animator.SetBool("isWalking", false);
		animator.SetBool("isRunning", false);
	}

	public void Walk() {
		speed = walkingSpeed;
		animator.SetBool("isIdle", false);
		animator.SetBool("isWalking", true);
	}

	public void Run() {
		speed = runningSpeed;
		animator.SetBool("isIdle", false);
		animator.SetBool("isRunning", true);
	}

	/// <summary>
	/// Move the player left/right
	/// </summary>
	/// <param name="velocitySpeed"></param>
	public void CalculVelocity(float velocitySpeed) {
		Vector2 velocity = new Vector2(directionalInput.x * velocitySpeed, rigidBody2D.velocity.y);
		rigidBody2D.velocity = velocity;
	}

	/// <summary>
	/// Set spriteRenderer flipping direction when
	/// directionInput change
	/// </summary>
	public void SetSpriteRendererDirection() {
		if (directionalInput.x < 0 && isFlippedRight) {
			spriteRenderer.flipX = true;
			isFlippedRight = false;
		} else if (!isFlippedRight && directionalInput.x > 0) {
			spriteRenderer.flipX = false;
			isFlippedRight = true;
		}
	}

	/// <summary>
	/// Sets the directional input.
	/// </summary>
	/// <returns>The directional input.</returns>
	/// <param name="Input">Input.</param>
	public void SetDirectionalInput(Vector2 Input) {
		directionalInput = Input;
	}
}