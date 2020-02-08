using BrightLib.Extensions;

public class Section
{
    protected string _title;

    protected void DrawTitle()
    {
        GUILayoutExtensions.LabelBold(_title);
    }

    public virtual void Draw()
    {

    }

    public virtual void Draw(DatabaseManager database)
    {

    }
}