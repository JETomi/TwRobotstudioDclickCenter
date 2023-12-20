using ABB.Robotics.Math;
using ABB.Robotics.RobotStudio;
using ABB.Robotics.RobotStudio.Environment;
using ABB.Robotics.RobotStudio.Stations;
using ABB.Robotics.RobotStudio.Stations.Forms;
using RobotStudio.API.Internal;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace TwRobotstudioDclickCenter
{
    public class Class1
    {
        // This is the entry point which will be called when the Add-in is loaded
        public static void AddinMain()
        {
            Logger.AddMessage(new LogMessage("TW Robotstudio Doubleclick to center loaded."));
            GraphicPicker.GraphicPick += GraphicPicker_GraphicPick;
        }

        static long LastClick = 0;
        static ProjectObject lastPickedObject;

        private static void GraphicPicker_GraphicPick(object sender, GraphicPickEventArgs e)
        {
            try
            {
                if (DateTime.Now.Ticks - LastClick < TimeSpan.TicksPerMillisecond * 200)
                {
                    if (e.PickedObject == lastPickedObject)
                    {
                        if (sender is ABB.Robotics.RobotStudio.Stations.Forms.GraphicPicker gp)
                        {
                            GraphicControl.ActiveGraphicControl.ViewCenter(e.PickedPosition, (float)0.1);
                        }
                    }
                }
                lastPickedObject = e.PickedObject;
                LastClick = DateTime.Now.Ticks;
            }
            catch (Exception ex)
            {
                Logger.AddMessage(new LogMessage("TW Robotstudio Doubleclick: " + ex.ToString(),LogMessageSeverity.Error));
            }
        }

    }
}