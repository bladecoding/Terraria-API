using System.ComponentModel;

namespace TerrariaAPI.Hooks
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T">Class Type (NPC, Item)</typeparam>
    /// <typeparam name="F">Fnfo Type (String, Int)</typeparam>
    public class SetDefaultsEventArgs<T, F> : HandledEventArgs
    {
        public T Object { get; set; }
        public F Info { get; set; }
    }

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T">Class Type (NPC, Item)</typeparam>
    /// <typeparam name="F">Fnfo Type (String, Int)</typeparam>
    public delegate void SetDefaultsD<T, F>(SetDefaultsEventArgs<T, F> e);
}