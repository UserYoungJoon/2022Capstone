using UnityEngine;



namespace SongSelctBtn
{
    [System.Obsolete("誘멸뎄 踰")]
    public class SongSelect_NextBtn : UIButton
    {

        public StageMenu stagemenu;
        public override void ClickEvent()
        {
            /*        //AudioManager.intance.PlaySFX("Touch");
            Debug.Log("Next Song");*/
            //if (++currentSong > songList.Length - 1)
            //    currentSong = 0;
            //    SettingSong();

            //SoundManager.Instance.PlaySFXSound("metronome_tick");

            //GameObject.Find("Canvas")
            //    .transform.Find("Select")
            //    .transform.Find("Disk")
            //    .transform.Find("Mask")
            //    .transform.Find("SongImage").GetComponent<SongSelect>().currentSong += 1;

            //5/05
            //GetComponent<StageMenu>();
            //GameObject.Find("Text").GetComponent<StageMenu>();

            GameObject.Find("Canvas")
                .transform.Find("Select").GetComponent<StageMenu>().currentSong += 1;
            //이미지만 안바뀜..
            //stagemenu = GameObject.Find("StageMenu").GetComponent<StageMenu>();


            Debug.Log("Next Song");
                
        }
    }
}