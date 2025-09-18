// Import C# namespaces
using System;
using System.Collections.Generic;

// Import RhinoCommon namespaces
using Rhino;
using Rhino.Geometry;
using Rhino.Commands;
using Rhino.Input.Custom;
using Rhino.Display;

// Namespace
namespace HelloLevelPicker.Commands
{
    // 1. Create New class that inherits from Rhino Command
    public class HelloLevelPicker : Command
    {
        // 2. Create command constractor
        public HelloLevelPicker()
        {
            Instance = this;
        }

        // 3. Create the only instance of this command
        public static HelloLevelPicker Instance { get; private set; }

        // 4. The command name as it appears on the Rhino command line
        public override string EnglishName => "HelloLevelPicker";

        // 5. Actual command code
        protected override Result RunCommand(RhinoDoc doc, RunMode mode)
        {
            // Write to Rhino Console
            RhinoApp.WriteLine("Hello Level Picker Command");

            // Create point
            Point3d point = new Point3d(10, 0, 0);
            
            // Setup string
            string text = "Hello RhinoCommon";

            // Create dot
            TextDot dot = new TextDot(text, point);
            
            // Add point
            doc.Objects.AddPoint(point);

            // Add dot
            doc.Objects.AddTextDot(dot);
            
            // Refresh the viewport
            doc.Views.Redraw();

            // Return success
            return Result.Success;
        }
    }
}
