using Assets.Utility;
using BrightLib.RPGDatabase.Runtime;

public class Section
{
    protected string _title;

    protected void DrawTitle()
    {
        BrightGUILayout.LabelBold(_title);
    }

    public virtual void Draw()
    {

    }

    public virtual void Draw(RPGDatabaseManager database)
    {

    }
}