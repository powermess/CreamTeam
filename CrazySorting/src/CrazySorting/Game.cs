using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.src
{
    class Game : MonoBehaviour
    {
        List<TargetLand> mTargets;
        public Text JerbsCounter;
        List<Sortee> mActors;
        public Image mWinimage;
        public Image mLoseImage;
        public Image mLoseImageMinority;
        public Button RestartButton;


        void Awake()
        {
            mTargets = FindObjectsOfType<TargetLand>().ToList();
            mTargets.ForEach(t => t.OnCollisionAction += OnEnterLand);
        }

        void Start()
        {
            
        }

        void OnDestroy()
        {
            mTargets.ForEach(t => t.OnCollisionAction -= OnEnterLand);
        }


        private void Win()
        {
            mWinimage.enabled = true;
            End();
        }

        private void LoseAmerica()
        {
            mLoseImage.enabled = true;
            End();
        }

        private void LoseMinority()
        {
            mLoseImageMinority.enabled = true;
            End();
        }

        void End()
        {
            RestartButton.enabled = true;
            mActors.ForEach(a => a.Stop());
        }

        void OnEnterLand(bool isMurica, Sortee sortee)
        {
           
        }

        void EnterMurica(Sortee sortee)
        {
            
        }

        void EnterMinorities(Sortee sortee)
        {

        }

        public void Restart()
        {
            SceneManager.LoadScene(0);
        }
    }
}
