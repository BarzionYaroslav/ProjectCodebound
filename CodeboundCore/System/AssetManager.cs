using System.ComponentModel.Design.Serialization;

namespace Codebound.System;

public static class AssetManager
{
    static public string GetEntitySpritePath(string name)
    {
        return $"{GetEntitySpritesDir()}/{name}{SpriteType}";
    }
    static public string GetBackgroundSpritePath(string name)
    {
        return $"{GetBackgroundSpritesDir()}/{name}{SpriteType}";
    }
    static public string GetMiscSpritePath(string name)
    {
        return $"{GetMiscSpritesDir()}/{name}{SpriteType}";
    }
    static public string GetSoundPath(string name)
    {
        return $"{GetSoundDir()}/{name}{SoundType}";
    }
    static public string GetMusicPath(string name)
    {
        return $"{GetMusicDir()}/{name}{MusicType}";
    }
    static public string GetSpritesDir()
    {
        return $"{MainPath}/{SpritesFolder}";
    }
    static public string GetMusicDir()
    {
        return $"{MainPath}/{MusicFolder}";
    }
    static public string GetSoundDir()
    {
        return $"{MainPath}/{SoundFolder}";
    }
    static public string GetBackgroundSpritesDir()
    {
        return $"{GetSpritesDir()}/{BackgroundFolder}";
    }
    static public string GetEntitySpritesDir()
    {
        return $"{GetSpritesDir()}/{EntityFolder}";
    }
    static public string GetMiscSpritesDir()
    {
        return $"{GetSpritesDir()}/{MiscFolder}";
    }
    public const string MainPath = @"./assets";
    const string SoundFolder = "sounds";
    const string MusicFolder = "music";
    const string SpritesFolder = "sprites";
    const string EntityFolder = "entities";
    const string BackgroundFolder = "backgrounds";
    const string MiscFolder = "misc";
    const string SpriteType = ".gif";
    const string SoundType = ".wav";
    const string MusicType = ".mp3";
}