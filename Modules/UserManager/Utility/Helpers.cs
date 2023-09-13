/*
Copyright © Upendo Ventures, LLC

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and 
associated documentation files (the "Software"), to deal in the Software without restriction, 
including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, 
and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, 
subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial 
portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT 
NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND 
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES 
OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN 
CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Upendo.Modules.UserManager.Utility
{
  using DotNetNuke.Framework;
  using DotNetNuke.Services.Localization;
  using System.Drawing;
  using System.Web.WebPages;
  using Upendo.Modules.UserManager.Components;

  /// <summary>Helpers for this module.</summary>
  public class Helpers
  {
    /// <summary>Misc helper methods for this module.</summary>
    public class IconHelper
    /*
old way:
GoogleIcon("lock_outline"):                       @Icon.Google("lock_outline") and GoogleIcon("near_me"): @Icon.Google("near_me")

new ways?
Icon.Google("lock_outline"):                                            @Icon.Google("lock_outline")
Icon.Get("lock_outline", "google"):                                     @Icon.Get("lock_outline", "google")
Icon.Get(iconSet: "google", iconName: "near_me", styleColor: "black") : @Icon.Get(iconSet: "google", iconName: "near_me", styleColor: "black") 

Icon.Get():                           @Icon.Get()
Icon.Get("lock", styleColor: "cyan"): @Icon.Get("lock", styleColor: "cyan") 
Icon.Get("booya"):                    @Icon.Get("booya") (does not exist)
Icon.Get("star"):                     @Icon.Get("star") 

Icon.Glyph("music"):                              @Icon.Glyph("music", "green") 
Icon.Get("music", "glyph"):                       @Icon.Get("music", "glyph", "blue")
Icon.Get(iconSet: "glyph", iconName: "MUSIC"):    @Icon.Get(iconSet: "glyph", iconName: "MUSIC")
Icon.Get(iconName: "Music", iconSet: "glyph"):    @Icon.Get(iconName: "Music", iconSet: "Glyphs")
     */
    {
      /// <summary>Gets HtmlString Icon string from the user manager resource file.</summary>
      /// <param name="iconName">The key/name of the icon key to get.</param>
      /// <param name="iconSize">xs, sm, md (default), lg, xl.</param>
      /// <param name="iconSet">The public icon setlocalization key to get (with or w/o file ext).</param>
      /// <param name="styleColor">The foreground color. Any valid CSS style color syntax including names; https://developer.mozilla.org/en-US/docs/Web/CSS/color</param>
      /// <returns>A HtmlString containing the Icon to display.</returns>
      public static IHtmlString Get(string iconName = "search", string iconSize = "md", string iconSet = Constants.DefaultIconSet, string styleColor = Constants.DefaultIconColor)
      {
        // TODO implement wrapper tag; string htmlWrapperTag = "span"
        // TODO implement inverse/contrast color; bool invertColor = false
        // TODO implement Sizes (see https://www.w3schools.com/icons/google_icons_intro.asp and others); string size = "sm" (follow Tw/Bs conventions? xs, sm, md (default), lg, xl) and see https://developers.google.com/fonts/docs/material_icons#styling_icons_in_material_design
        // TODO is it possible to only load the icon sets resources that are actually used/needed? See Views/Shared/_Layout.cshtml
        // TOOD support other icon sets (e.g. Google Symbols or Bootstrap 5)? How would we resolve names between them? (e.g. "lock_outline" vs "lock")
        iconName = iconName.ToLower();
        // if styleColor is "none" then don't add it, otherwise pass it in as a statement in the style attribute
        styleColor = styleColor == "none" ? "" : "color:" + styleColor + ";";
        switch (iconSet.ToLower())
        {
          case "google":
            return new HtmlString("<i class=\"material-icons " + iconSize + "\" style=\"" + styleColor + "margin-right:0.75rem;\" aria-hidden=\"true\">" + iconName + "</i>");
          case "glyph":
          case "glyphs":
          case "glyphicon":
          case "glyphicons":
            return new HtmlString("<span class=\"glyphicon glyphicon-" + iconName + "\" style=\"" + styleColor + "margin-right:0.75rem;\" aria-hidden=\"true\"></span>");
          default:
            return Get(iconName: iconName);
        }
      } 
      /// <summary>Warpper for previous syntax.</summary>
      public static IHtmlString Google(string iconName = "search", string iconSize = "md", string styleColor = "white")
      {
        return Get(iconName: iconName, iconSet: "google", iconSize: iconSize, styleColor: styleColor);
      }
      /// <summary>Warpper for previous syntax.</summary>
      public static IHtmlString Glyph(string iconName = "search", string styleColor = "white")
      {
        return Get(iconName: iconName, iconSet: "glyph", styleColor: styleColor);
      }
    }
    /// <summary>Localization helper methods for this module.</summary>
    public class LocalizationHelper
    {
      /// <summary>Gets a localized resource string from the user manager resource file.</summary>
      /// <param name="resourceFilename">The localization key to get (with or w/o file ext).</param>
      /// <param name="key">The localization key to get.</param>
      /// <returns>A string containing the localized text.</returns>
      public static string GetString(string key, string resourceFilename)
      {
        return Localization.GetString(key, Constants.ResourcesPath + EnsureExt(resourceFilename));
      }
      /// <summary>Gets a localized resource string from the user manager shared resource file.</summary>
      /// <param name="key">The localization key to get.</param>
      /// <returns>A string containing the localized text.</returns>
      public static string GetString(string key)
      {
        return Localization.GetString(key, Constants.ResourcesPath + Constants.SharedResourcesFilename);
      }

      private static string EnsureExt(string filename)
      {
        if (filename.EndsWith(".resx"))
        {
          return filename;
        }
        return filename += ".resx";
      }
    }
    /// <summary>Localization helper methods for this module.</summary>
    public class TextHelper
    {
      /// <summary>More readable way to handle detecting if a Text (string) item has real content</summary>
      /// <param name="value">string</param>
      /// <param name="handleHtmlWhitespaces">strip HTML whitespaces also? default is true</param>
      /// <returns>True if it has real content, False if there is </returns>
      public static bool Has(string value, bool handleHtmlWhitespaces = true)
      {
        // quick-check for performance bail-out
        if (string.IsNullOrWhiteSpace(value))
          return false;

        // if it got here and we don't want to re-check for html-whitespace, then we do have text
        if (!handleHtmlWhitespaces)
          return true;

        // convert html or url whitespace to normal spaces for final check
        foreach (var whitespace in Constants.HtmlNonBreakingSpaces)
          value = value.Replace(whitespace, " ");

        return !string.IsNullOrWhiteSpace(value);
      }
      /// <summary>Override for above so we handle anything</summary>
      /// <param name="value">object</param>
      /// <param name="handleHtmlWhitespaces">strip HTML whitespaces also? default is true</param>
      public static bool Has(object value, bool handleHtmlWhitespaces = true) => Has(value as string, handleHtmlWhitespaces);
    }
  }
}
