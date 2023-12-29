using LLServer.Common;
using Microsoft.AspNetCore.Mvc;

namespace LLServer.Controllers.Debugging
{
    [ApiController]
    [Route("Debug/FlagTester")]
    public class FlagTester : ControllerBase
    {
        public static int flagCount = 200;
        public static string flags = new string('1', 200);
        public static int sectionIndex = 0;
        public static int sectionLength = 200;
        public static int leftHalfLength = sectionLength / 2;
        public static bool isLeft = true;
        public static bool success = false;
        public static int successIndex = 0;
        
        public static int[] overrides = Enumerable.Repeat(-1, flags.Length).ToArray();
        public static bool testMode = false;

        [HttpGet]
        public IActionResult Get()
        {
            return new ContentResult
            {
                ContentType = "text/html",
                StatusCode = 200,
                Content = @"
<html>
    <style>
        body {
            font-family: ""Comic Sans MS"", cursive, sans-serif;
        }
    </style>
    <head>
        <title>Flag Tester</title>
    </head>
    <body>
        <h1>Flag Tester</h1>
        " + (testMode ? @"<h2>Test mode is enabled.</h2>" : @"<h2>Test mode is not enabled.</h2>") + @"
        <p>Press Success if the right flag seems to be set, failure if not. This will binary search through all flags and end up with an index.</p>
        <p>This affects the flags response in UserDataResponse.</p>
        <p>Press Reset to reset the flags.</p>
        <p>Current flags: <code>" + GetHTMLColoredFlags() + @"</code></p>
        <p>Current section index: <code>" + sectionIndex + @"</code></p>
        <p>Current section length: <code>" + sectionLength + @"</code></p>
        <form method=""post"">
            <p>Binary Search</p>
            <input type=""submit"" name=""flag"" value=""Success"" />
            <input type=""submit"" name=""flag"" value=""Failure"" />
            <input type=""submit"" name=""flag"" value=""All 1s"" />
            <input type=""submit"" name=""flag"" value=""Reset"" />
            <br />
            <p>Manual Bit Overrides</p>
            <input type=""number"" name=""bitIndex"" value=""" + -1 + @""" />
            <input type=""submit"" name=""flag"" value=""Toggle bit"" />
            <input type=""submit"" name=""flag"" value=""Reset bit"" />
            <input type=""submit"" name=""flag"" value=""Reset all overrides"" />
            <br />
            <br />
            <input type=""number"" name=""flagCount"" value=""" + flagCount + @""" />
            <input type=""submit"" name=""flag"" value=""Set flag count"" />
            <br />
            <input type=""submit"" name=""flag"" value=""Toggle test mode"" />
        </form>
        <img src=""/oom.png"" />

        " + (success ? @"<p>Finished: flag " + successIndex : @"") + @"
    </body>
</html>
"
            };
        }

        [HttpPost]
        public IActionResult Post([FromForm(Name = "flag")] string flag, [FromForm(Name = "bitIndex")] int bitIndex, [FromForm(Name = "flagCount")] int _flagCount)
        {
            if (flag == "Toggle bit")
            {
                if (bitIndex != -1)
                {
                    overrides[bitIndex] = overrides[bitIndex] == 1 ? 0 : 1;

                    UpdateFlags();
                }
                
                return RedirectToAction("Get");
            }
            else if (flag == "Reset bit")
            {
                if (bitIndex != -1)
                {
                    overrides[bitIndex] = -1;

                    UpdateFlags();
                }
                
                return RedirectToAction("Get");
            }
            else if (flag == "Reset all overrides")
            {
                overrides = Enumerable.Repeat(-1, flags.Length).ToArray();
                
                UpdateFlags();
                
                return RedirectToAction("Get");
            }

            // If successful, update the flags based on binary search
            if (flag == "Success")
            {
                UpdateFlagsOnSuccess();
            }
            else if (flag == "Failure")
            {
                UpdateFlagsOnFailure();
            }
            else if (flag == "All 1s")
            {
                flags = new string('1', flags.Length);
                return RedirectToAction("Get");
            }
            else if (flag == "Reset")
            {
                sectionIndex = 0;
                sectionLength = flagCount;
                leftHalfLength = sectionLength / 2;
                isLeft = true;
                success = false;
                successIndex = 0;
                UpdateFlags();
            }
            else if (flag == "Toggle test mode")
            {
                testMode = !testMode;
            }
            else if (flag == "Set flag count")
            {
                flagCount = int.Clamp(_flagCount, 1, 256);
                flags = new string('1', flagCount);
                sectionIndex = 0;
                sectionLength = flagCount;
                leftHalfLength = sectionLength / 2;
                isLeft = true;
                success = false;
                successIndex = 0;
                UpdateFlags();
            }
            
            // Update the flags
            UpdateFlags();

            return RedirectToAction("Get");
        }

        // Function to update flags on success
        private void UpdateFlagsOnSuccess()
        {
            //if our current side is length 1, we're done
            if (isLeft && leftHalfLength == 1)
            {
                successIndex = sectionIndex;
                success = true;
                return;
            }
            if (!isLeft && sectionLength - leftHalfLength == 1)
            {
                successIndex = sectionIndex + leftHalfLength;
                success = true;
                return;
            }

            sectionIndex = isLeft ? sectionIndex : sectionIndex + leftHalfLength;
            sectionLength = isLeft ? leftHalfLength : sectionLength - leftHalfLength;
            leftHalfLength = sectionLength / 2;
            isLeft = true;
        }

        // Function to update flags on failure
        private void UpdateFlagsOnFailure()
        {
            isLeft = !isLeft;
        }

        public void UpdateFlags()
        {
            //build the new flags
            string updatedFlags = new string('0', flags.Length);

            if (isLeft)
            {
                for (int i = sectionIndex; i < sectionIndex + leftHalfLength; i++)
                {
                    updatedFlags = updatedFlags.SetFlag(i, true);
                }
            }
            else
            {
                for (int i = sectionIndex + leftHalfLength; i < sectionIndex + sectionLength; i++)
                {
                    updatedFlags = updatedFlags.SetFlag(i, true);
                }
            }
            
            //apply overrides
            for (int i = 0; i < overrides.Length; i++)
            {
                if (overrides[i] == -1) continue;
                
                updatedFlags = updatedFlags.SetFlag(i, overrides[i] == 1);
            }
            
            flags = updatedFlags;
        }

        public string GetHTMLColoredFlags()
        {
            //build the html
            string html = "";

            string[] specialBits = new string[flags.Length];
            
            //add green to the current section
            if (isLeft)
            {
                for (int i = sectionIndex; i < sectionIndex + leftHalfLength; i++)
                {
                    specialBits[i] = "<span style=\"color: green;\">" + flags[i] + "</span>";
                }
                
                //mark the right side as grey
                for (int i = sectionIndex + leftHalfLength; i < sectionIndex + sectionLength; i++)
                {
                    specialBits[i] = "<span style=\"color: grey;\">" + flags[i] + "</span>";
                }
            }
            else
            {
                for (int i = sectionIndex + leftHalfLength; i < sectionIndex + sectionLength; i++)
                {
                    specialBits[i] = "<span style=\"color: green;\">" + flags[i] + "</span>";
                }
                
                //mark the left side as grey
                for (int i = sectionIndex; i < sectionIndex + leftHalfLength; i++)
                {
                    specialBits[i] = "<span style=\"color: grey;\">" + flags[i] + "</span>";
                }
            }
            
            //mark anything that's been overridden in red
            for (int i = 0; i < overrides.Length; i++)
            {
                if (overrides[i] == -1) continue;
                
                specialBits[i] = "<span style=\"color: red;\">" + flags[i] + "</span>";
            }
            
            //replace the special bits
            for (int i = 0; i < specialBits.Length; i++)
            {
                if (specialBits[i] == null) html += flags[i];
                else html += specialBits[i];
            }

            return html;
        }
    }
}
