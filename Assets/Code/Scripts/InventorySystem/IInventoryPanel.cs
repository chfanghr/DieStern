namespace Code.Scripts.InventorySystem
{
    public interface IInventoryPanel
    {
        Inventory Inventory { set; }
        void Collapse();
        void Expand();

        void Init();
    }
}