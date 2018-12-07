namespace Tibia.Data
{
    public interface IPool
    {
        /// <summary>
        ///     Gets or sets the fluid.
        /// </summary>
        /// <value>
        ///     The fluid.
        /// </value>
        IFluid Fluid { get; set; }
    }
}