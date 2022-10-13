using Harmony;
using UnityEngine;

namespace Game
{
    public class ScrollableBackground : MonoBehaviour
    {
        [SerializeField] private bool useGlobalEnvironmentSpeed;
        [SerializeField] private float speed = 1;
        [SerializeField] private int nbRepetitions = 3;

        private Vector2 originalPosition;
        private float currentOffset;
        private float spriteWidth;

        private void Awake()
        {
            if (useGlobalEnvironmentSpeed) speed = Finder.Game.EnvironmentMovementSpeed;

            var transform1 = transform;
            originalPosition = transform1.position;

            // Scale changed randomly when changing the tiled size,
            // possibly a problem in Unity?
            var originalScale = transform1.localScale;
                
            var spriteRenderer = GetComponent<SpriteRenderer>();
            var sprite = spriteRenderer.sprite;
            
            spriteWidth = sprite.texture.width / sprite.pixelsPerUnit * originalScale.x;
            var spriteHeight = sprite.texture.height / sprite.pixelsPerUnit * originalScale.y;
            
            spriteRenderer.drawMode = SpriteDrawMode.Tiled;
            spriteRenderer.size = new Vector2(spriteWidth * nbRepetitions, spriteHeight);

            transform1.localScale = originalScale;
        }

        private void Update()
        {
            var transform1 = transform;
            currentOffset += speed * Time.deltaTime;
            currentOffset %= spriteWidth * transform1.lossyScale.x;

            transform1.position = originalPosition + Vector2.left * currentOffset;
        }
    }
}