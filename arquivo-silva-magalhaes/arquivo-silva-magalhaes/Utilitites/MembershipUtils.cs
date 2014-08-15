using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArquivoSilvaMagalhaes.Utilitites
{
    public class MembershipUtils
    {
        /// <summary>
        /// The admin role name.
        /// The admin can:
        /// -Edit the contents of the archive and portal;
        /// -Edit configuration settings and users.
        /// </summary>
        public const string AdminRoleName = "admins";
        /// <summary>
        /// The content manager role name.
        /// The content manager can:
        /// -Edit the contents of the archive and portal.
        /// </summary>
        public const string ContentRoleName = "contentmanagers";
        /// <summary>
        /// The archive manager role name.
        /// The archive manager can:
        /// -Edit the contents of the archive.
        /// </summary>
        public const string ArchiveRoleName = "archivemanagers";
        /// <summary>
        /// The portal manager role name.
        /// The portal manager can:
        /// -Edit the contents of the portal.
        /// </summary>
        public const string PortalRoleName = "portalmanagers";


    }
}