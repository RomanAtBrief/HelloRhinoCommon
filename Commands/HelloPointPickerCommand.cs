// Import C# namespaces
using System;
using System.Collections.Generic;

// Import RhinoCommon namespaces
using Rhino;
using Rhino.Geometry;
using Rhino.Commands;
using Rhino.Input.Custom;
using Rhino.Collections;
using Rhino.Input;

// Namespace
namespace HelloRhinoCommon.Commands
{
    // 1. Create New class that inherits from Rhino Command
    public class HelloPointPickerCommand : Command
    {
        // 2. Create command constractor  
        public HelloPointPickerCommand()
        {
            Instance = this;
        }

        // 3. Create the only instance of this command
        public static HelloPointPickerCommand Instance { get; private set; }

        // 4. The command name as it appears on the Rhino command line
        public override string EnglishName => "HelloPointPicker";

        // 5. Actual command code
        protected override Result RunCommand(RhinoDoc doc, RunMode mode)
        {
            // Create an array of points
            Point3dList points = new Point3dList();

            // Create variable commanResult to store result
            Result commandResult;

            while (true)
            {
                // Create a new GetPoint class instance
                var getPoint = new GetPoint();
                getPoint.SetCommandPrompt("Pick a point.");

                // Get the result of picking a point. 
                var result = getPoint.Get();

                // Check if result has a point selected
                if (result == GetResult.Point)
                {
                    doc.Objects.AddPoint(getPoint.Point());
                    doc.Views.Redraw();
                    points.Add(getPoint.Point());
                    commandResult = Result.Success;
                }
                else if (result == GetResult.Nothing)
                {
                    commandResult = Result.Failure;
                    break;
                }
                else
                {
                    commandResult = Result.Cancel;
                    break;
                }
            }

            // Report to Rhino console
            RhinoApp.WriteLine("The user drew {0} points successfully", points.Count.ToString());
          
            // Return success
            return commandResult;
        }
    }
}