namespace Codebound.System;
using SDL3;
public class SoundHolder
{
    private nint audio;
    private nint track;
    public SoundHolder(string file, nint mixer)
    {
        audio = Mixer.LoadAudio(mixer, file, predecode: true);
        track = Mixer.CreateTrack(mixer);
        Mixer.SetTrackAudio(track, audio);
    }
    public void Kill()
    {
        Mixer.DestroyTrack(track);
        Mixer.DestroyAudio(audio);
    }

    public void Play()
    {
        Mixer.PlayTrack(track, 0);
    }

    public void PlayLooped()
    {
        var looping = SDL.CreateProperties();
        SDL.SetNumberProperty(looping, Mixer.Props.PlayLoopsNumber, -1);
        Mixer.PlayTrack(track, looping);
    }
}