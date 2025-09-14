// Import C# namespaces
using System;
using System.Collections.Generic;

// Import RhinoCommon namespaces
using Rhino;
using Rhino.Geometry;
using Rhino.Commands;
using Rhino.Input.Custom;

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

            // Return success
            return Result.Success;
        }
    }
}
