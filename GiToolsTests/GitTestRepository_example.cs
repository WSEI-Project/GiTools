using System;
using System.Collections.Generic;
using System.Text;

namespace GiToolsTests
{
    /**
     * rename this file to GitTestRepository.cs
     * test repositories go here
     */
    class GitTestRepository
    {
        /**
         * public repository
         */
        public static readonly long Public = 0;
        /**
         * url obtained from browser
         */
        public static readonly string PublicUrl = "https://github.com/exampleuser/examplerepo/";
        /**
         * url for cloning
         */
        public static readonly string PublicCloneUrl = "https://github.com/exampleuser/examplerepo.git";
        /**
         * private repository (should be only accessible with correct permissions set)
         */
        public static readonly long Private = 0;
        /**
         * url obtained from browser
         */
        public static readonly string PrivateUrl = "https://github.com/exampleuser/exampleprivaterepo/";
        /**
         * url for cloning
         */
        public static readonly string PrivateCloneUrl = "https://github.com/exampleuser/exampleprivaterepo.git";
    }
}
