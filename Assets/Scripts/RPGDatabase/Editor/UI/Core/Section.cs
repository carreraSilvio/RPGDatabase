using BrightLib.Utility;
using RPGDatabase.Runtime.Core;

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

    public virtual void Draw(RPGDatabaseManager database)
    {

    }
}