using SplashKitSDK;

public class SoundManager
{
    private Music _bgm;
    private SoundEffect _singleSFX;
    private SoundEffect _doubleSFX;
    private SoundEffect _tripleSFX;
    private SoundEffect _tetrisSFX;
    private SoundEffect _rotateSFX;
    private SoundEffect _dropSFX;
    private SoundEffect _gameOverSFX;

    public SoundManager()
    {
        _bgm = new Music("bgm", "Resources/bgm.mp3");
        _singleSFX = SplashKit.LoadSoundEffect("single", "sounds/Resources/single.wav");
        _doubleSFX = SplashKit.LoadSoundEffect("double", "sounds/Resources/double.wav");
        _tripleSFX = SplashKit.LoadSoundEffect("triple", "sounds/Resources/triple.wav");
        _tetrisSFX = SplashKit.LoadSoundEffect("tetris", "sounds/Resources/tetris.wav");
        _rotateSFX = SplashKit.LoadSoundEffect("rotate", "sounds/Resources/rotate.wav");
        _dropSFX = SplashKit.LoadSoundEffect("drop", "sounds/Resources/drop.wav");
        _gameOverSFX = SplashKit.LoadSoundEffect("game_over", "sounds/Resources/game_over.wav");
    }

    public void PlayBGM()
    {
        SplashKit.PlayMusic(_bgm, -1);
    }

    public void StopBGM()
    {
        SplashKit.StopMusic();
    }

    public void PlaySingleClear()
    {
        SplashKit.PlaySoundEffect(_singleSFX);
    }

    public void PlayDoubleClear()
    {
        SplashKit.PlaySoundEffect(_doubleSFX);
    }

    public void PlayTripleClear()
    {
        SplashKit.PlaySoundEffect(_tripleSFX);
    }

    public void PlayTetrisClear()
    {
        SplashKit.PlaySoundEffect(_tetrisSFX);
    }

    public void PlayRotate()
    {
        SplashKit.PlaySoundEffect(_rotateSFX);
    }

    public void PlayDrop()
    {
        SplashKit.PlaySoundEffect(_dropSFX);
    }

    public void PlayGameOver()
    {
        SplashKit.PlaySoundEffect(_gameOverSFX);
    }
}
