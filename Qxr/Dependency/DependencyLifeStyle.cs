﻿namespace Qxr.Dependency
{
    public enum DependencyLifeStyle
    {
        /// <summary>
        /// Created on object for every reqeust
        /// </summary>
        Default,
        /// <summary>
        /// Singleton object. Created a single object on first resolving
        /// and same instance is used for subsequent resolves.
        /// </summary>
        Singleton,

        /// <summary>
        /// Transient object. Created one object for every resolving.
        /// </summary>
        Transient
    }
}
