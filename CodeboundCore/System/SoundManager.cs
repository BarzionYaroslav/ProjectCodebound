namespace Codebound.System;
using SDL3;
static public class SoundManager
{
    static public nint SoundMixer;
    static public nint MusicMixer;
    static public SoundHolder? CurrentSong;
    static private Dictionary<string, SoundHolder> loadedSounds = new Dictionary<string, SoundHolder>();
    static SoundManager()
    {
        // SDL init
        if (!SDL.Init(SDL.InitFlags.Audio))
        {
            return;
        }

        // Mixer init
        if (!Mixer.Init())
        {
            SDL.Quit();
            return;
        }
        var mixer = Mixer.CreateMixerDevice(SDL.AudioDeviceDefaultPlayback, IntPtr.Zero);
        if (mixer == IntPtr.Zero)
        {
            Mixer.Quit();
            SDL.Quit();
            return;
        }
        SoundMixer = mixer;
        mixer = Mixer.CreateMixerDevice(SDL.AudioDeviceDefaultPlayback, IntPtr.Zero);
        if (mixer == IntPtr.Zero)
        {
            Mixer.Quit();
            SDL.Quit();
            return;
        }
        MusicMixer = mixer;
    }

    static public void ClearSounds()
    {
        foreach (string i in loadedSounds.Keys)
        {
            loadedSounds[i].Dispose();
            loadedSounds.Remove(i);
        }
    }

    static public void PlaySound(string asset)
    {
        var snd = AssetManager.GetSoundPath(asset);
        if (!loadedSounds.ContainsKey(snd))
        {
            SoundHolder holdah = new SoundHolder(snd, SoundMixer);
            loadedSounds.Add(snd, holdah);
            holdah.Play();
        }
        else
        {
            loadedSounds[snd].Play();
        }
    }

    static public void PlayBGM(string asset)
    {
        var snd = AssetManager.GetMusicPath(asset);
        if (CurrentSong != null)
            CurrentSong.Dispose();
        CurrentSong = new SoundHolder(snd, MusicMixer);
        CurrentSong.PlayLooped();
    }

    static public void Kill()
    {
        ClearSounds();
        if (CurrentSong != null)
            CurrentSong.Dispose();
        Mixer.Quit();
        SDL.Quit();
    }
}