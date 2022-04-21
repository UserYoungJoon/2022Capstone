using UnityEngine;
namespace SongSelctBtn
{
    [System.Obsolete("�̱��� ��ư")]
    public class SongSelect_NextBtn : UIButton
    {
        public override void ClickEvent()
        {
            SoundManager.Instance.PlaySFXSound("metronome_tick");
            /*        //AudioManager.intance.PlaySFX("Touch");
        Debug.Log("Next Song");
        if (++currentSong > songList.Length - 1)
            currentSong = 0;
        SettingSong();*/
        }
    }
}