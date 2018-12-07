namespace Tibia.Data
{
    public interface IThing
    {
        /// <summary>
        ///     Gets the render priority.
        /// </summary>
        /// <value>
        ///     The render priority.
        /// </value>
        RenderPriority RenderPriority { get; }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        /// <value>
        ///     The name.
        /// </value>
        string Name { get; set; }
    }
}