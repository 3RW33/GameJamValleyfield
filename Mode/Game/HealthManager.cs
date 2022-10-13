using System;
using System.Collections;
using System.Collections.Generic;
using Harmony;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Game
{
    public class HealthManager : MonoBehaviour
    {
        [SerializeField] private Vector2 originalPosition;
        [SerializeField] private GameObject healthPointPrefab;
        [SerializeField] private Sprite lostHealthSprite;
        [SerializeField] private Image fadeImage;
        [SerializeField] private float opacityChange = 0.1f;
        
        private Cart cart;
        private List<Image> images;
        private Main main;

        private void Awake()
        {
            cart = Finder.Cart;
            images = new List<Image>();
            main = Finder.Main;
        }

        private void OnEnable()
        {
            CartHitEventChannel.OnCartHit += OnCartHit;
        }

        private void Start()
        {
            for (int i = 0; i < cart.Health; i++)
            {
                var image = Instantiate(healthPointPrefab, originalPosition, Quaternion.Euler(0, 0, 0), transform);
                var rectTransform = image.GetComponent<RectTransform>();

                rectTransform.anchoredPosition =
                    new Vector2((rectTransform.sizeDelta.x * i)  + originalPosition.x, originalPosition.y);

                images.Add(image.GetComponent<Image>());
            }
        }

        private void OnCartHit(int healthRemaining)
        {
            if (healthRemaining > 0) images[healthRemaining].sprite = lostHealthSprite;
            else
            {
                if (healthRemaining == 0)
                    images[healthRemaining].sprite = lostHealthSprite;
                StartCoroutine(FadeToBlack());
            }
        }

        private IEnumerator FadeToBlack()
        {
            var opacity = fadeImage.color.a;
            var color = fadeImage.color;
            while (opacity < 1f)
            {
                opacity += opacityChange * Time.deltaTime;
                fadeImage.color = new Color(color.r, color.g, color.b, opacity);
                yield return null;
            }
            
            main.GoToEndGame();
        }
    }
}