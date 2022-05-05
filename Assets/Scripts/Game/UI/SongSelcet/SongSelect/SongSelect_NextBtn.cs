using UnityEngine;



namespace SongSelctBtn
{
    [System.Obsolete("미구현 버튼")]
    public class SongSelect_NextBtn : UIButton
    {
       
        public override void ClickEvent()
        {

            SoundManager.Instance.PlaySFXSound("metronome_tick");

            GameObject.Find("Canvas")
                .transform.Find("Select")
                .transform.Find("Disk")
                .transform.Find("Mask")
                .transform.Find("SongImage").GetComponent<SongSelect>().currentSong += 1;

                // Hierachy 내의 Canvas의 자식의 Select의 자식의 Disk의 자식의 Mask의 자식의 SongImage의 SongSelect 컴포넌트 갖고오기

            Debug.Log("Next Song");
        }
    }
}