namespace Hydrogen.General.Model
{
    public interface IEntityContentContainer<TContent> where TContent : class, IEntityContent
	{
		string ContentString { get; set; }
		TContent Content { get; set; }
	}
}