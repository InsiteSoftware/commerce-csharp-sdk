﻿using System;
using CommerceApiSDK.Services.Interfaces;

namespace CommerceApiSDK.Services
{
    public static class OptimizelyAPI
    {
        private static IOptimizelyService _current;

        /// <summary>
        /// Current method used to initialize the Optimizely service in the mobile project
        /// </summary>
        public static IOptimizelyService Current
        {
            get
            {
                if (_current == null)
                {
                    return new OptimizelyService();
                }
                return _current;
            }
        }
    }
}
