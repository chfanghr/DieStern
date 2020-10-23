namespace Code.Scripts.Interactable
{
    public interface IInspector
    {
        void Inspect();

        bool Collected { get; }
    }
}