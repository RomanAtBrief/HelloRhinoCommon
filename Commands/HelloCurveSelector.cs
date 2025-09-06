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
    public class HelloCurveSelector : Command
    {
        // 2. Create command constractor  
        public HelloCurveSelector()
        {
            Instance = this;
        }

        // 3. Create the only instance of this command
        public static HelloCurveSelector Instance { get; private set; }

        // 4. The command name as it appears on the Rhino command line
        public override string EnglishName => "HelloCurveSelector";

        // 5. Actual command code
        protected override Result RunCommand(RhinoDoc doc, RunMode mode)
        {
            // Initialize getObject to be able to select different objects in Rhino            
            var getCurve = new GetObject();

            // Add Rhino command line prompt
            getCurve.SetCommandPrompt("Select some curves");

            // Filter and allow to select only curves
            getCurve.GeometryFilter = Rhino.DocObjects.ObjectType.Curve;

            // Ask user to select objects, 0 means user has to press enter to finish
            getCurve.GetMultiple(1, 0);

            // Check if the selection result was successfull
            if (getCurve.CommandResult() != Result.Success)
            {
                return getCurve.CommandResult();
            }

            // Create list to hold all selected points
            var curves = new List<Curve>(getCurve.ObjectCount);

            // Loop over all selected points and add them to our list
            for (int i = 0; i < getCurve.ObjectCount; i++)
            {
                var curve = getCurve.Object(i).Curve();
                if (null != curve)
                {
                    curves.Add(curve);
                }
            }

            // Report to Rhino console
            RhinoApp.WriteLine("The user selected {0} curves successfully", curves.Count.ToString());

            // Return success
            return Result.Success;
        }
    }
}