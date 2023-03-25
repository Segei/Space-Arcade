using Assets.Script.Model.UpdateSystem;
using Script.Model.Entities;
using Script.Model.UpdateSystem;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace Assets.Script.View
{
    public class EndGame : MonoBehaviour
    {
        [Inject] private StartUpdate update;
        [Inject] private Ship ship;
        [Inject] private BillingSystem billingSystem;
        private bool endGame;
        [SerializeField] private GameObject canvasEnd;
        [SerializeField] private TMP_Text Score;
        [SerializeField] private Button buttonRestart, buttonExit;

        
        private void Start()
        {
            ship.OnDestroyed += (e) =>
            {
                endGame = true;
                update.Stop();                
            };
            buttonRestart.onClick.AddListener(Restart);
            buttonExit.onClick.AddListener(Exit);
        }
        private void Update()
        {
            if (!endGame)
            {
                return;
            }
            Score.text = billingSystem.Store.ToString();
            canvasEnd.SetActive(true);
            gameObject.SetActive(false);
        }

        private void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        private void Exit()
        {
            Application.Quit();
        }
    }
}
