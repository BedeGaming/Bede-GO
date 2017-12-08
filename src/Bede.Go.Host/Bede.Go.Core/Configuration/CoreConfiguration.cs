using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bede.Go.Core.Configuration
{
    public static class CoreConfiguration
    {
        public static double PlayAreaDiameter
        {
            get
            {
                return 0.025;
            }
        }
        public static double RadiusForDistance
        {
            get
            {
                return 6371;
            }
        }
    }
}
