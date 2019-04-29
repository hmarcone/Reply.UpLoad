using Reply.UpLoad.Interfaces;

namespace Reply.UpLoad.Data
{
    public class UpLoadImageVM
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }

    }
}
