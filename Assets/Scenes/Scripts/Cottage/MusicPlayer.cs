using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource music_player;
    [SerializeField] private AudioSource turn_on_off_sound_player;
    private bool is_music_playing;

    private void Start()
    {
        is_music_playing = true;
        music_player.Play();
        gameObject.transform.DORotate(new Vector3(0, 360, 0), 5f, RotateMode.LocalAxisAdd).SetEase(Ease.Linear).SetLoops(-1, LoopType.Incremental);
    }

    public void ToggleMusic()
    {
        if (!is_music_playing)
        {
            music_player.Play();
            gameObject.transform.DORotate(new Vector3(0, 360, 0), 5f, RotateMode.LocalAxisAdd).SetEase(Ease.Linear).SetLoops(-1, LoopType.Incremental);
        }
        else
        {
            music_player.Pause();
            gameObject.transform.DOKill();
        }

        turn_on_off_sound_player.Play();
        is_music_playing = !is_music_playing;
    }
}
