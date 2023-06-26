namespace Erdmier.ProjectTemplate.WebUI.Extensions;

/// <summary> A custom event handler class allowing for the use of custom events in Blazor. </summary>
/// <remarks> How this works is magic; don't ask me 😁. </remarks>
[ EventHandler("onmouseleave", typeof(EventArgs), true, true) ]
public static class EventHandlers { }
