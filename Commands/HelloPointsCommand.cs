// Import C# namespaces
using System;
using System.Collections.Generic;

// Import RhinoCommon namespaces
using Rhino;
using Rhino.Geometry;
using Rhino.Commands;

// Namespace
namespace HelloRhinoCommon
{
    // 1. Create New class that inherits from Rhino Command
    public class HelloPointCommand : Command
    {
        // 2. Create command constractor
        public HelloPointCommand()
        {
            Instance = this;
        }

        // 3. Create the only instance of this command
        public static HelloPointCommand Instance { get; private set; }

        // 4. The command name as it appears on the Rhino command line
        public override string EnglishName => "HelloPointCommand";

        // 5. Actual command code
        protected override Result RunCommand(RhinoDoc doc, RunMode mode)
        {
            // Welcome message
            RhinoApp.WriteLine("Hello World ! ");

            // Create points
            Point3d pt0 = new Point3d(0, 0, 0);
            Point3d pt1 = new Point3d(0, 1, 0);
            Point3d pt2 = new Point3d(0, 5, 0);

            // Draw points in viewport
            doc.Objects.AddPoint(pt0);
            doc.Objects.AddPoint(pt1);
            doc.Objects.AddPoint(pt2);

            // Refresh the viewport
             doc.Views.Redraw();
             
            // Return success
            return Result.Success;
        }
    }
}