using BrightLib.Editor;
using BrightLib.RPGDatabase.Runtime;

namespace BrightLib.RPGDatabase.Editor
{
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
}