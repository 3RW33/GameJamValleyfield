using System;
using Harmony;
using UnityEngine;

namespace Game
{
    [Findable(Tags.Cart)]
    public class Cart : MonoBehaviour, IHittable
    {
        [SerializeField] private int health = 10;

        private new Collider2D collider2D;
        private new Audio audio;

        public int Health => health;

        public Vector2 Size => collider2D.bounds.size;

        private void Awake()
        {
            collider2D = GetComponentInChildren<Collider2D>();
            audio = Finder.Audio;
        }

        public void Hit(int hitPoints)
        {
            health -= hitPoints;
            audio.MainSoundPlayer.Play(audio.GameClips.cartHit);
            CartHitEventChannel.Publish(health);
        }
    }
}