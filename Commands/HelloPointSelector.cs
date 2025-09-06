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
    public class HelloPointSelector : Command
    {
        // 2. Create command constractor  
        public HelloPointSelector()
        {
            Instance = this;
        }

        // 3. Create the only instance of this command
        public static HelloPointSelector Instance { get; private set; }

        // 4. The command name as it appears on the Rhino command line
        public override string EnglishName => "HelloPointSelector";

        // 5. Actual command code
        protected override Result RunCommand(RhinoDoc doc, RunMode mode)
        {
            // Initialize getObject to be able to select different objects in Rhino            
            var getObject = new GetObject();

            // Filter and allow to select only points
            getObject.GeometryFilter = Rhino.DocObjects.ObjectType.Point;

            // Ask user to select objects, 0 means user has to press enter to finish
            getObject.GetMultiple(1, 0);

            // Check if the selection result was successfull
            if (getObject.CommandResult() != Result.Success)
            {
                return getObject.CommandResult();
            }

            // Create list to hold all selected points
            var points = new List<Point>(getObject.ObjectCount);

            // Loop over all selected points and add them to our list
            for (int i = 0; i < getObject.ObjectCount; i++)
            {
                var point = getObject.Object(i).Point();
                if (null != point)
                {
                    points.Add(point);
                }
            }

            // Report to Rhino console
            RhinoApp.WriteLine("The user selected {0} points successfully", points.Count.ToString());

            // Return success
            return Result.Success;
        }
    }
}