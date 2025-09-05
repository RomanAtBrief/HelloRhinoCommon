// Import C# namespaces
using System;
using System.Collections.Generic;

// Import RhinoCommon namespaces
using Rhino;
using Rhino.Geometry;
using Rhino.Commands;
using Rhino.Input.Custom;

// Namespace
namespace HelloRhinoCommon.Commands
{
    // 1. Create New class that inherits from Rhino Command
    public class HelloPickPointCommand : Command
    {
        // 2. Create command constractor
        public HelloPickPointCommand()
        {
            Instance = this;
        }

        // 3. Create the only instance of this command
        public static HelloPickPointCommand Instance { get; private set; }

        // 4. The command name as it appears on the Rhino command line
        public override string EnglishName => "HelloPickPointCommand";

        // 5. Actual command code
        protected override Result RunCommand(RhinoDoc doc, RunMode mode)
        {
            // Create a new GetPoint class instance
            var getPoint = new GetPoint();
            getPoint.SetCommandPrompt("Pick a point.");

            // Get the result of picking a point. 
            getPoint.Get();

            // Check the result
            Result result = getPoint.CommandResult();
            if (result != Result.Success)
            {
                return getPoint.CommandResult();
            }

            // Format coordinate string
            var format = string.Format("F{0}", doc.DistanceDisplayPrecision);

            // Retrieve 3d point
            var point = getPoint.Point();
            var x = point.X.ToString(format);
            var y = point.Y.ToString(format);
            var z = point.Z.ToString(format);

            // Write in Rhino console
            RhinoApp.Write("World coordinates: {0}, {1}, {2}", x, y, z);

            // Return success
            return Result.Success;
        }
    }
}