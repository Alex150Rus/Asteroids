using UnityEngine;
using UnityEngine.UI;

namespace Asteroids.UI
{
    public class Hud : MonoBehaviour
    {
        [SerializeField]
        private Text _score;
        [SerializeField]
        private Text _lives;

        public void SetScore(int value)
        {
            _score.text = value.ToString();
        }

        public void SetLives(int value)
        {
            _lives.text = value.ToString();
        }
    }
}