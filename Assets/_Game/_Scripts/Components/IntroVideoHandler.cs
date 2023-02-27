using DallaiStudios.SuperPong.Managers;
using DallaiStudios.SuperPong.Types;
using UnityEngine;
using UnityEngine.Video;

namespace DallaiStudios.SuperPong.Components
{
    public class IntroVideoHandler : MonoBehaviour
    {
        private void Start() => GetComponent<VideoPlayer>().loopPointReached += this.MoveToNextScene;

        private void MoveToNextScene(VideoPlayer source)
        {
            GameManager.Instace.LoadScene(GameScenes.MAIN_MENU);
        }
    }
}