namespace Aspnet_for_elympics.Services
{
    public interface IRequestManager
    {
        public bool TryGetTheNumberRequest(out string responseContent);
    }
}
