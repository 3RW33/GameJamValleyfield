using System;
using System.Collections;
using Harmony;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class Enemy : AICharacter, IHittable
    {
        [SerializeField] private float opacityDecrease = 1;

        private SpriteRenderer spriteRenderer;
        private Game game;

        public Rigidbody2D Rigidbody2D { private set; get; }
        protected Cart Cart { get; private set; }
        protected EnemyManager EnemyManager { get; private set; }
        protected int Direction { get; set; }
        protected bool IsStopped { get; set; }

        protected override void Awake()
        {
            base.Awake();
            Cart = Finder.Cart;
            EnemyManager = Finder.EnemyManager;
            game = Finder.Game;
            Rigidbody2D = GetComponent<Rigidbody2D>();
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }

        private void OnEnable()
        {
            IsStopped = false;
        }

        private void Start()
        {
            StartMovement();
        }

        public void StartMovement()
        {
            Direction = Cart.transform.position.x > transform.position.x ? 1 : -1;
            Mover.StartWalking(Direction);
        }

        public virtual void Die()
        {
            if (!IsStopped) StopMovement();
            StartCoroutine(Despawn());
        }

        protected void StopMovement()
        {
            IsStopped = true;
            Mover.StopWalking();
            StartCoroutine(MoveWithGround());
        }

        protected abstract void RemoveEnemy();

        public virtual void Hit(int hitPoints)
        {
            Die();
        }
        
        private IEnumerator Despawn()
        {
            var color = spriteRenderer.color;
            var opacity = color.a;
            do
            {
                opacity -= opacityDecrease * Time.deltaTime;
                spriteRenderer.color = new Color(color.r, color.g, color.b, opacity);
                yield return null;
            } while (opacity > 0);

            spriteRenderer.color = new Color(color.r, color.g, color.b, 1f);
            Rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
            RemoveEnemy();
        }

        protected IEnumerator MoveWithGround()
        {
            while (isActiveAndEnabled)
            {
                transform.Translate(Vector3.left * (game.EnvironmentMovementSpeed * Time.deltaTime), Space.World);
                yield return null;
            }
        }
    }
}