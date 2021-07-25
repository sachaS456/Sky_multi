using System.Collections.Generic;

namespace Sky_multi_Core
{
    public sealed class AudioOutputDescription
    {
        private VlcManager myManager;

        public string Name { get; private set; }
        public string Description { get; private set; }

        internal AudioOutputDescription(string name, string description, VlcManager manager)
        {
            Name = name;
            Description = description;
            myManager = manager;
        }

        public IEnumerable<AudioOutputDevice> Devices => myManager.GetAudioOutputDeviceList(this.Name);
    }

}
