namespace Deniz.TiberiumSunEditor.Gui.Model;

public class GameEntityEventArgs : EventArgs
{
    public GameEntityEventArgs(GameEntityModel gameEntity)
    {
        GameEntity = gameEntity;
    }

    public GameEntityModel GameEntity { get; }
}