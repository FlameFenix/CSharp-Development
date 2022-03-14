namespace Cars_Market.Services.Contracts
{
    public interface IByteConverter
    {
        public byte[] ConvertToByteArray(IFormFile file);
    }
}
