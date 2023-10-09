using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Upendo.Modules.UserManager.Utility
{
  // using System.ComponentModel;

  /// <summary>Defines common constants for the user manager.</summary>
  public class Constants
  {
    /// <summary>The module path.</summary>
    public const string ModulePath = "DesktopModules/MVC/Upendo.Modules.UserManager/";

    /// <summary>The relative path to the resources files for this module.</summary>
    public const string ResourcesPath = "~/DesktopModules/MVC/Upendo.Modules.UserManager/App_LocalResources/";

    /// <summary>The local shared resources filename for this module.</summary>
    public const string SharedResourcesFilename= "Shared.resx";

    /// <summary>The default Icon Set the IconHelper.Get() uses for this module; see https://www.w3schools.com/icons/default.asp</summary>
    public const string DefaultIconSet = "Google";

    /// <summary>The default Icon Color the IconHelper.Get() uses for this module; see https://www.w3schools.com/icons/default.asp</summary>
    public const string DefaultIconColor = "none"; // "none" is handled in the IconHelper.Get() method

    /// <summary>The default rows per page for User and Role editing grids</summary>
    public const string DefaultSelectRowsPerPage = "10";

    /// <summary>The default rows per page for User and Role editing grids</summary>
    public const string DefaultSelectUserStatus = "Authorized";

    /// <summary>The default sort (orderby) User editing grids</summary>
    public const string DefaultSelectSortField = "Username";

    /// <summary>Things that are nothing in Html or Encoded Urls</summary>
    public static string[] HtmlNonBreakingSpaces = { "&nbsp;", "&#160;", "%20" };
  }
}