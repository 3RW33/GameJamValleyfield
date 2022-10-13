using System;
using Harmony;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class MageProjectile : Weapon
    {
        [SerializeField] private float speed = 5f;
        
        private new Rigidbody2D rigidbody2D;
        public Mage Mage { get; set; }
        
        public bool IsShot { get; private set; }

        private void Awake()
        {
            Mage = GetComponentInParent<Mage>();
            rigidbody2D = GetComponent<Rigidbody2D>();
            IsFriendly = false;
            IsShot = false;
        }

        private void Start()
        {
            gameObject.SetActive(false);
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            var cart = other.gameObject.GetComponentInParent<Cart>();
            if (cart != null)
            {
                IsShot = false;
                cart.Hit(Damage);
                gameObject.SetActive(false);
                Mage.Die();
            }
        }

        public void Shoot(int direction)
        {
            IsShot = true;
            transform.position = Mage.ShotPointPosition;
            gameObject.SetActive(true);
            rigidbody2D.velocity = new Vector2(speed * direction, 0);
        }
    }
}