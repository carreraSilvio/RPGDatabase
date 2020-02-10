using BrightLib.Utility;

public class Section
{
    protected string _title;

    protected void DrawTitle()
    {
        GUILayoutUtility.LabelBold(_title);
    }

    public virtual void Draw()
    {

    }

    public virtual void Draw(DatabaseManager database)
    {

    }
}