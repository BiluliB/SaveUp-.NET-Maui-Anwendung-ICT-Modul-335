namespace SaveUpBackend.Common
{
    /// <summary>
    /// Proxy attribute for MongoDB references to other collections
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ProxyAttribute : Attribute
    {
        public string? ForeignKey { get; set; }

        public string? PluralName { get; set; }

        public ProxyAttribute(string? pluralName = null, string? foreignKey = null)
        {
            PluralName = pluralName;
            ForeignKey = foreignKey;
        }
    }
}
