using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Media;
using Android.Views;
using Android.Widget;

[assembly: Xamarin.Forms.Dependency(typeof(EmergencyApp.Droid.VoicePlayer))]

namespace EmergencyApp.Droid
{
    class VoicePlayer : IVoicePlayer
    {
        static SoundPool    mSoundPool;
        static AudioManager mAudioManager;
        static int[] mSoundFile = new int[2];

        RingerMode currentRingMode;
        int        currentVolume;

        public VoicePlayer()
        {
        }

        // 起動時
        public void Initialize(Context context)
        {
            mSoundPool = new SoundPool(1, Stream.Music, 0);

            // 音声ファイルの登録   
            mSoundFile[0] = mSoundPool.Load(context, Resource.Raw.keikoku, 1);  // 助けて下さい＝警告っ（仮） 
            mSoundFile[1] = mSoundPool.Load(context, Resource.Raw.kyaa, 1);     // 痴漢です＝キャー（仮）

            // サイレントモード解除→ 音声出力
//            CancelSilentMode(context);
        }

        // 音声再生
        public void PlaySound(int rawId)
        {
            int result = 0;
            int soundId = mSoundFile[rawId];

            // サウンドID       ※読込み時に取得
            // 左音量（0.0～1.0）
            // 右音量（0.0～1.0）
            // 優先度（0～）
            // ループモード（0xff：ループ）
            // 再生速度（0.5 ～ 2.0 = 半分 ～ 2倍 の速度）
            if (mSoundPool != null)
            {
                result = mSoundPool.Play(soundId, 1, 1, 1, 0xff, 1.0f);
                if (result == 0)
                {
                    // 何もしない
                }
            }
        }

        // 音声停止
        public void PlayStop()
        {
            if (mSoundPool != null)
                mSoundPool.AutoPause();

            // マナーモード/音量を元の状態に戻す
//            RestoreSilentMode(vpContext);
        }

        // アプリ終了時
        public void PlayRelease(Context context)
        {
            if (mSoundPool != null)
            {
                mSoundPool.Release();
                mSoundPool = null;
            }
            // マナーモード/音量を元の状態に戻す
            RestoreSilentMode(context);
        }

        //
        // サイレントモード解除→ 音声出力
        //
        private void CancelSilentMode(Context context)
        {
            // AudioManager設定
            mAudioManager = (AudioManager)context.GetSystemService(Context.AudioService);
            if (mAudioManager == null)
                return;

            // 現在のサイレント状態取得
            currentRingMode = mAudioManager.RingerMode;

            // マナーモード解除
            mAudioManager.RingerMode = RingerMode.Normal;

            // 現在の音量を取得する
            currentVolume = mAudioManager.GetStreamVolume(Stream.Music);
            // ストリームごとの最大音量を取得する
            int ringMaxVolume = mAudioManager.GetStreamMaxVolume(Stream.Music);
            // 音量を設定する
            mAudioManager.SetStreamVolume(Stream.Music, ringMaxVolume, 0);
            //            mAudioManager.SetStreamVolume(Stream.Notification, 2, 0);   // 緊急時
        }

        //
        // マナーモード/音量を元の状態に戻す
        //
        private void RestoreSilentMode(Context context)
        {
            // AudioManager設定
            mAudioManager = (AudioManager)context.GetSystemService(Context.AudioService);
            if (mAudioManager == null)
                return;

            // マナーモード
            mAudioManager.RingerMode = currentRingMode;
            // 音量を元の状態に戻す
            mAudioManager.SetStreamVolume(Stream.Music, currentVolume, 0);
        }
    }
}
