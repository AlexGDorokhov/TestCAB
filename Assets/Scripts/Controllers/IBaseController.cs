namespace Controllers
{
    public interface IBaseController
    {
        void Init();
        void Update();
        void FixedUpdate();
        void AddEventsHandlers();
        void RemoveEventsHandlers();
    }
}