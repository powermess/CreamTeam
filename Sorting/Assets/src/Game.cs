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


        int mJerbsMax = 10000;
        int mCurrentJerbsTaken = 0;

        void Awake()
        {
            mTargets = FindObjectsOfType<TargetLand>().ToList();
            mTargets.ForEach(t => t.OnCollisionAction += OnEnterLand);

            mActors = FindObjectsOfType<Sortee>().ToList();
        }

        void Start()
        {
            UpdateJerbsCounter();
        }

        private void UpdateJerbsCounter()
        {
            JerbsCounter.text = GetJerbsText();
        }

        void OnDestroy()
        {
            mTargets.ForEach(t => t.OnCollisionAction -= OnEnterLand);
        }

        void CheckWinCondition()
        {
           if (mActors.All(a => !a.IsMoving))
                Win();                    
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
            sortee.Stop();

            if (isMurica)
                EnterMurica(sortee);
            else
                EnterMinorities(sortee);
        }

        void EnterMurica(Sortee sortee)
        {
            if (sortee.IsPatriot)
            {
                CheckWinCondition();
            }
            else
            {
                mCurrentJerbsTaken += 579600;
                UpdateJerbsCounter();
                LoseAmerica();
            }
        }

        void EnterMinorities(Sortee sortee)
        {
            if (sortee.IsPatriot)
            {
                LoseMinority();
            }
            else
            {
                CheckWinCondition();
            }
        }

        string GetJerbsText()
        {
            return string.Format("{0} / {1}", mCurrentJerbsTaken, mJerbsMax);
        }

        public void Restart()
        {
            SceneManager.LoadScene(0);
        }
    }

    
}
