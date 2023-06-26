﻿using Microsoft.AspNetCore.Mvc.Rendering;

namespace Erdmier.ProjectTemplate.WebUI.Common.Utilities;

public static class AccountManagePagesNavUtil
{
    public static string Profile => "Profile";

    public static string Email => "Email";

    public static string ChangePassword => "ChangePassword";

    public static string DownloadPersonalData => "DownloadPersonalData";

    public static string DeletePersonalData => "DeletePersonalData";

    public static string ExternalLogins => "ExternalLogins";

    public static string PersonalData => "PersonalData";

    public static string TwoFactorAuthentication => "TwoFactorAuthentication";

    public static string? ProfileNavClass(ViewContext viewContext) => PageNavClass(viewContext, Profile);

    public static string? EmailNavClass(ViewContext viewContext) => PageNavClass(viewContext, Email);

    public static string? ChangePasswordNavClass(ViewContext viewContext) => PageNavClass(viewContext, ChangePassword);

    public static string? DownloadPersonalDataNavClass(ViewContext viewContext) => PageNavClass(viewContext, DownloadPersonalData);

    public static string? DeletePersonalDataNavClass(ViewContext viewContext) => PageNavClass(viewContext, DeletePersonalData);

    public static string? ExternalLoginsNavClass(ViewContext viewContext) => PageNavClass(viewContext, ExternalLogins);

    public static string? PersonalDataNavClass(ViewContext viewContext) => PageNavClass(viewContext, PersonalData);

    public static string? TwoFactorAuthenticationNavClass(ViewContext viewContext) => PageNavClass(viewContext, TwoFactorAuthentication);

    public static string? PageNavClass(ViewContext viewContext, string pageName)
    {
        string? activePage = viewContext.ViewData["ActivePage"] as string
                             ?? Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);

        return string.Equals(activePage, pageName, StringComparison.OrdinalIgnoreCase) ? "!text-primary underline" : null;
    }
}