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
using Rhino.DocObjects;

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
            // Create a place to store collection of footprint curves
            CurveList fotprints = new CurveList();

            // GetObject class is the tool commands use to interactively select objects
            GetObject getFootprint = new GetObject();

            // Prompt user
            getFootprint.SetCommandPrompt("Select closed curve footprints.");

            // Filter only curves
            getFootprint.GeometryFilter = ObjectType.Curve;

            // Allow to select multiple footprints / curves. 
            // Specify min and max number of objects to select
            getFootprint.GetMultiple(1, 0);

            // Check if selection was successful
            if (getFootprint.CommandResult() != Result.Success)
            {
                return getFootprint.CommandResult();
            }

            // Loop over all selected footprints and extrude
            for (int i = 0; i < getFootprint.ObjectCount; i++)
            {
                // Extract curve from GetObject, getFootprint
                Curve footprint = getFootprint.Object(i).Curve();

                // Check if there is curve
                if (footprint != null)
                {
                    Random random = new Random();
                    Extrusion massing = Extrusion.Create(footprint, random.Next(5, 10), true);
                    doc.Objects.AddExtrusion(massing);
                }
            }

            // Redraw view
            doc.Views.Redraw();

            // Return success
            return Result.Success;
        }
    }
}
