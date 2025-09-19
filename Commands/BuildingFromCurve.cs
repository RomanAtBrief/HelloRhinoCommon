// Import C# namespaces
using System;
using System.Collections.Generic;

// Import RhinoCommon namespaces
using Rhino;
using Rhino.Geometry;
using Rhino.Commands;
using Rhino.Input.Custom;
using Rhino.Display;
using Rhino.Collections;
using Rhino.Input;

// Namespace
namespace HelloRhinoCommon.Commands
{
    // 1. Create New class that inherits from Rhino Command
    public class BuildingFromCurve : Command
    {
        // 2. Create command constractor
        public BuildingFromCurve()
        {
            Instance = this;
        }

        // 3. Create the only instance of this command
        public static BuildingFromCurve Instance { get; private set; }

        // 4. The command name as it appears on the Rhino command line
        public override string EnglishName => "BuildingFromCurve";

        // 5. Actual command code
        protected override Result RunCommand(RhinoDoc doc, RunMode mode)
        {
            // Greet the user

            // Select closed curves. Check if curve is closed, if not ignore

            // Extrude to make a building

            // ---------------------------- 
            // Select objects
            // ----------------------------

            // Create a list to hold selected points
            Point3dList points = new Point3dList();

            // Store command result
            Result commandResult;

            while (true)
            {
                // Instantiate a GetPoint class / object
                GetPoint getPoint = new GetPoint();

                // Command Prompt
                getPoint.SetCommandPrompt("Pick a point.");

                // Retrive point. 
                // Get() returns GetResult that can tell us what object was selected
                GetResult result = getPoint.Get();

                // Test if selection returns point
                if (result == GetResult.Point)
                {
                    // Add point to Rhino document
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

            // Write to Rhino console
            RhinoApp.WriteLine("User selected {0} points", points.Count.ToString());
            
            // Return success
            return commandResult;
        }
    }
}
