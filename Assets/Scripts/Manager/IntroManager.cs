using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using System;
using System.Runtime.InteropServices;

public class IntroManager : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    private List<string> videoList = new List<string>();
    public static string lastPlayedVideo{ private set; get; } = "";


    private void Start()
    {
        string videopath = Path.Combine(Application.streamingAssetsPath, Constants.videoPath);
        Debug.Log("Video path: " + videopath);
        if (Directory.Exists(videopath))
        {
            string[] videoFiles = Directory.GetFiles(videopath, "*" + Constants.VIDEO_FILE_EXTENSION);

            foreach (string videoFile in videoFiles)
            {
                videoList.Add(videoFile);
            }
        }

        PlayRandomVideo();
    }

    private void PlayRandomVideo()
    {
        if (videoList.Count > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, videoList.Count);
            lastPlayedVideo = videoList[randomIndex];       //记录已经播放的视频

            videoPlayer.url = lastPlayedVideo;
            //videoPlayer.loopPointReached += OnvideoEnd;

            videoPlayer.Play();

            StartCoroutine(LoadSceneAfterSeconds(Constants.MENU_SCENE, 2f));
        }
        else
        {
            SceneManager.LoadScene(Constants.MENU_SCENE);
        }
    }

    // private void OnvideoEnd(VideoPlayer vp)
    // {
    //     SceneManager.LoadScene(Constants.MENU_SCENE);
    // }

    //public static string GetLastPlayedVideo() => lastPlayedVideoPath;

    private IEnumerator LoadSceneAfterSeconds(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);

        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);
        op.allowSceneActivation = false;

        // 等加载到 90%（Unity 的“已加载但未激活”状态）
        while (op.progress < 0.9f)
        {
            yield return null;
        }

        // 这里可以加一帧缓冲，减少突兀感
        yield return null;

        videoPlayer.Pause();

        op.allowSceneActivation = true;
    }

}
